using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Services.Logging;
using Kapitalist.Services.Prozorro.Interfaces;
using System;
using System.Data;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Services.SyncService
{
    internal class SyncController<T> : ISyncController
        where T : class, ISyncEngine
    {
        public ITrace Trace { get; set; }
        private ISyncService _service;
        private T _engine;
        private CancellationTokenSource _cancellation;
        private CancellationToken _cancellationToken;
        private Task _syncTask;
        private Task _restoreTask;
        private bool _fullSync;
        private DateTime? _offset;
        private int _pageSize, _maxReties;
        private TimeSpan _defaultTimeout, _maxTimeout;
        private SyncErrorsCount _directErrors, _reverseErrors;

        public SyncController(ISyncService service)
        {
            _service = service;
        }

        private void SetDefaultValues()
        {
            _fullSync = false;
            _offset = null;
            _pageSize = _service.PageSize;
            _defaultTimeout = _service.DefaultTimeout;
            _maxTimeout = _service.MaxTimeout;
            _maxReties = _service.MaxReties;
            _directErrors = new SyncErrorsCount();
            _reverseErrors = new SyncErrorsCount();
        }

        private void ApplyArgs(string[] args)
        {
            using (var e = args.AsEnumerable().GetEnumerator())
            {
                while (e.MoveNext())
                {
                    double value;
                    switch (e.Current)
                    {
                        case "/full":
                            _fullSync = true;
                            break;

                        case "/offset":
                            DateTime offset;
                            if (e.MoveNext() && DateTime.TryParse(e.Current, out offset))
                                _offset = DateTime.SpecifyKind(offset, DateTimeKind.Utc);
                            break;

                        case "/pagesize":
                            if (e.MoveNext())
                                int.TryParse(e.Current, out _pageSize);
                            break;

                        case "/timeout":
                            if (e.MoveNext() && double.TryParse(e.Current.Replace(',', '.'),
                                NumberStyles.Number, NumberFormatInfo.InvariantInfo, out value))
                                _defaultTimeout = TimeSpan.FromMinutes(value);
                            break;

                        case "/maxtimeout":
                            if (e.MoveNext() && double.TryParse(e.Current.Replace(',', '.'),
                                NumberStyles.Number, NumberFormatInfo.InvariantInfo, out value))
                                _maxTimeout = TimeSpan.FromMinutes(value);
                            break;

                        case "/maxretries":
                            if (e.MoveNext())
                                int.TryParse(e.Current, out _maxReties);
                            break;
                    }
                }
            }
        }

        public void OnStart(string[] args)
        {
            try
            {
                SetDefaultValues();
                if (args?.Length > 0)
                {
                    Trace.TraceInformation("Sync started with args: " + string.Join(" ", args));
                    ApplyArgs(args);
                }
                else
                {
                    Trace.TraceInformation("Sync started without args.");
                }
                _engine = (T)Activator.CreateInstance(typeof(T), new object[] { _fullSync, _offset, _pageSize });
                _engine.Trace = Trace;
                _cancellation = new CancellationTokenSource();
                _cancellationToken = _cancellation.Token;

                _syncTask = ContinuesSync(false);
                _restoreTask = FixSyncErrors().ContinueWith((x) =>
                {
                    return ContinuesSync(true);
                });
            }
            catch (Exception ex)
            {
                Trace.TraceError("Sync cannot be started. " + ex);
                _service.Stop();
            }
        }

        private async Task ContinuesSync(bool reverse)
        {
            TimeSpan timeout = _defaultTimeout;
            string taskName = reverse ? "Restoring" : "Sync";
            while (true)
            {
                try
                {
                    if (reverse)
                    {
                        await _engine.PrevSync(_cancellationToken);
                        // успішна зворотня синхронізація не потребує таймаута та повторення - завершуємо задачу
                        return;
                    }
                    else
                        await _engine.NextSync(_cancellationToken);
                    timeout = _defaultTimeout;
                    Trace.TraceInformation("{0} timeout {1}", taskName, timeout);
                    await Task.Delay(timeout, _cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    Trace.TraceWarning("Concurrecny detected. {0} will be resummed with 10s delay.", taskName);
                    await Task.Delay(TimeSpan.FromSeconds(10), _cancellationToken);
                }
                catch (OptimisticConcurrencyException)
                {
                    Trace.TraceWarning("Optimistic concurrecny detected. {0} will be resummed with 10s delay.", taskName);
                    await Task.Delay(TimeSpan.FromSeconds(10), _cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (HttpRequestException ex)
                {
                    if ((timeout += timeout) > _maxTimeout)
                        timeout = _maxTimeout;
                    Trace.TraceWarning("Prozoro communication error. " + ex);
                    Trace.TraceInformation("{0} timeout {1}", taskName, timeout);
                    await Task.Delay(timeout, _cancellationToken);
                }
                catch (APITimeoutException ex)
                {
                    if ((timeout += timeout) > _maxTimeout)
                        timeout = _maxTimeout;
                    Trace.TraceWarning("Prozoro needs timeout. " + ex);
                    Trace.TraceInformation("{0} timeout {1}", taskName, timeout);
                    await Task.Delay(timeout, _cancellationToken);
                }
                catch (Exception ex)
                {
                    if (CanRetry(_engine, reverse))
                    {
                        Trace.TraceWarning("{0} error cached. Trying to continue {0}. {1}", taskName, ex);
                    }
                    else
                    {
                        var currentItem = reverse ? _engine.RestoringItem : _engine.SyncingItem;
                        if (await _engine.RegisterSyncError(reverse, _cancellationToken))
                        {
                            // Якщо помилка синхронізації актуального тендера вдало зареєстрована - пропустимо її
                            Trace.TraceError("Error while {0} {1} {2} at offset {3} will be skipped. And sync will be continued. {4}",
                                taskName, _engine.SyncType, currentItem.Guid, currentItem.DateModified.ToString(), ex);
                        }
                        else
                        {
                            // Якщо помилка синхронізації не звязана з конкретним елементом, або неможливо зареєструвати помилку
                            // - завершаемо синхронізацію з помилкою.
                            Trace.TraceError("Critical error occured while {0} {1} {2} at offset {3}. Sync cannot be continued. {4}",
                                taskName, _engine.SyncType, currentItem.Guid, currentItem.DateModified.ToString(), ex);
                            Environment.Exit(1);
                        }
                    }
                    timeout = TimeSpan.FromSeconds(10);
                    Trace.TraceInformation("{0} timeout {1}", taskName, timeout);
                    await Task.Delay(timeout, _cancellationToken);
                    timeout = _defaultTimeout;
                }
            }
        }

        private async Task FixSyncErrors()
        {
            try
            {
                await _engine.FixSyncErrors(_cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Previous sync errors cannot be fixed. " + ex);
            }
        }

        public void OnPause()
        {
            if (StopSyncTasks())
                Trace.TraceInformation("Sync paused at offset: " + _engine.SyncOffset);
            else
            {
                Trace.TraceError("Sync cannot be correctly paused. It will be stopped.");
                _service.Stop();
            }
        }

        public void OnContinue()
        {
            if (_syncTask == null && _restoreTask == null)
            {
                Trace.TraceInformation("Sync resumed from offset: " + _engine.SyncOffset);
                _cancellation = new CancellationTokenSource();
                _cancellationToken = _cancellation.Token;
                _syncTask = ContinuesSync(false);
                _restoreTask = ContinuesSync(true);
            }
            else
            {
                Trace.TraceError("Sync cannot be continued. It will be stopped.");
                _service.Stop();
            }
        }

        private bool StopSyncTasks()
        {
            bool stopped = false;
            try
            {
                if (_cancellation != null)
                {
                    _cancellation.Cancel();
                    if (_syncTask != null)
                    {
                        try
                        {
                            if (_syncTask.Wait(10000))
                            {
                                _syncTask.Dispose();
                            }
                        }
                        catch (AggregateException aex)
                        {
                            if (aex.InnerExceptions.Any(e => e.GetType() == typeof(TaskCanceledException)))
                                _syncTask.Dispose();
                        }
                        catch (TaskCanceledException ex)
                        {
                            _syncTask.Dispose();
                        }
                        _syncTask = null;
                    }
                    if (_restoreTask != null)
                    {
                        try
                        {
                            if (_restoreTask.Wait(10000))
                            {
                                _restoreTask.Dispose();
                            }
                        }
                        catch (AggregateException aex)
                        {
                            if (aex.InnerExceptions.Any(e => e.GetType() == typeof(TaskCanceledException)))
                                _restoreTask.Dispose();
                        }
                        catch (TaskCanceledException ex)
                        {
                            _restoreTask.Dispose();
                        }
                        _restoreTask = null;
                    }
                    stopped = true;
                    _cancellation.Dispose();
                    _cancellation = null;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error while stopping sync task. " + ex);
            }
            return stopped;
        }

        public void OnStop()
        {
            if (_syncTask == null || StopSyncTasks())
            {
                Trace.TraceInformation("Sync stopped at offset: " + _engine?.SyncOffset);
                _engine?.Dispose();
                _engine = null;
            }
            else
            {
                _engine?.Dispose();
                Trace.TraceError("Sync cannot be stopped. Process will be terminated.");
                Environment.Exit(1);
            }
        }

        private bool CanRetry(ISyncEngine engine, bool reverse)
        {
            SyncErrorsCount errors = reverse ? _reverseErrors : _directErrors;
            DateTime offset = reverse ? engine.RestoreOffset : engine.SyncOffset;
            if (errors.Offseet == offset)
                errors.Count++;
            else
            {
                errors.Offseet = offset;
                errors.Count = 1;
            }
            return errors.Count < _maxReties;
        }

        private class SyncErrorsCount
        {
            public DateTime Offseet;
            public int Count;
        }
    }
}