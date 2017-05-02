using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Logging;
using Kapitalist.Services.Prozorro.Helpers.Comparers;
using Kapitalist.Services.Prozorro.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro
{
    public abstract class SyncEngine<T> : ISyncEngine, IDisposable
        where T : IGetOrderedModifications, IDisposable
    {
        protected T Service { get; }
        public SyncItems SyncType { get; }
        public ITrace Trace { get; set; }
        public int Limit { get; set; }
        public DateTime SyncOffset { get; private set; }
        public ModifiedElement SyncingItem { get; private set; }
        public DateTime RestoreOffset { get; private set; }
        public ModifiedElement RestoringItem { get; private set; }
        private bool FullSync { get; }
        private DateTime? RestoreTo { get; }

        protected SyncEngine(SyncItems syncType, T service, bool fullSync = false, DateTime? offset = null, int limit = 100)
        {
            SyncType = syncType;
            Service = service;
            Limit = limit;
            SyncOffset = offset ?? DateTime.UtcNow;
            // щоб випадково не пропустити офсетний елемент - добавимо його в зворотню синхронізацію
            RestoreOffset = SyncOffset.AddTicks(10);
            FullSync = fullSync;
            using (StoreContext store = new StoreContext() { Trace = Trace })
            {
                if (!fullSync)
                {
                    // визначаємо, чи успішною була попередня синхронізація, та її офсет
                    if (store.SyncStates.FirstOrDefault(s => s.Type == syncType)?.Restoring == false)
                        RestoreTo = GetSyncOffset(store);
                }
                // перед початком синхронізації - відмічаємо в базі початок відновлення синхронізації
                // якщо синхронізація зупиниться до повного її відновлення - цей стан буде сигналізувати, про неповноту одержаних даних
                store.SyncStates.AddOrUpdate(new SyncState { Type = syncType, Restoring = true });
                store.SaveChanges();
            }
        }

        /// <summary>
        /// Синхронізує всі закупівлі, змінені після останньої вдалої синхронізації.
        /// </summary>
        public async Task NextSync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var page = await Service.GetModificationsPage(SyncOffset, false, Limit, cancellationToken);
                if (page.Items.Length == 0)
                    return;

                // Спільний контекст застосовується для цілої сторінки закупівель.
                // Потім контекст скидається, щоб уникнути переповнення пам'яті.
                using (StoreContext store = new StoreContext() { Trace = Trace })
                {
                    foreach (var t in page.Items)
                    {
                        SyncingItem = t;
                        await SyncItem(t.Guid, store, cancellationToken);
                        SyncOffset = t.DateModified;
                        SyncingItem = null;
                    }
                }
                SyncOffset = page.NextPage.Offset.Value;
                Trace.TraceEvent(page.Items.Length, "Synced {0} {1}s. Up to {2}", page.Items.Length, SyncType, SyncOffset);
            }
        }

        public async Task PrevSync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var page = await Service.GetModificationsPage(RestoreOffset, true, Limit, cancellationToken);
                if (page.Items.Length == 0)
                    break;
                // Спільний контекст застосовується для цілої сторінки закупівель.
                // Потім контекст скидається, щоб уникнути переповнення пам'яті.
                int restored = 0;
                using (StoreContext store = new StoreContext() { Trace = Trace })
                {
                    var items = page.Items.AsEnumerable();
                    // Якщо не повна синхронізація - пропускаємо раніше просинхронізовані записи
                    if (!FullSync)
                    {
                        var synced = await GetSyncedPage(store, RestoreOffset, Limit, cancellationToken);
                        items = items.Except(synced, ModifiedElementComparer.Instance);
                    }
                    foreach (var t in items)
                    {
                        RestoringItem = t;
                        await SyncItem(t.Guid, store, cancellationToken);
                        RestoreOffset = t.DateModified;
                        RestoringItem = null;
                        restored++;
                    }
                }
                RestoreOffset = page.NextPage.Offset.Value;
                if (RestoreOffset < RestoreTo)
                    break;
                Trace.TraceEvent(restored, "Restored {0} {1}s. Down to {2}", restored, SyncType, RestoreOffset);
            }
            if (!cancellationToken.IsCancellationRequested)
            {
                using (StoreContext store = new StoreContext() { Trace = Trace })
                {
                    store.SyncStates.AddOrUpdate(new SyncState { Type = SyncType, Restoring = false });
                    await store.SaveChangesAsync();
                }
                Trace.TraceInformation("Restoring sync complated!");
            }
        }

        /// <summary>
        /// Синхронізує вибрану закупівлю та всі звязані з нею об'єкти.
        /// </summary>
        /// <param name="guid">ідентифікатор закупівлі</param>
        /// <param name="store">контекст БД в якому потрыбно застосувати зміни</param>
        /// <param name="cancellationToken">токен для відміни синхронізації</param>
        protected abstract Task SyncItem(Guid guid, StoreContext store, CancellationToken cancellationToken);

        /// <summary>
        /// Повертає офсет синхронізації
        /// </summary>
        /// <returns>найбільший DateModified серед просинхронізованих елементів</returns>
        protected abstract DateTime? GetSyncOffset(StoreContext store);

        protected abstract Task<ModifiedElement[]> GetSyncedPage(StoreContext store, DateTime offset, int limit, CancellationToken cancellationToken);

        /// <summary>
        /// Реєструє помилку синхронізації в базі.
        /// </summary>
        /// <returns>true - якщо помилка вдало зареєстрована
        /// false - якщо помилка не звязана з синхронізацією тендера, або немає можливості зареєструвати помилку в базі</returns>
        public async Task<bool> RegisterSyncError(bool reverse, CancellationToken cancellationToken)
        {
            try
            {
                var errorneusItem = reverse ? RestoringItem : SyncingItem;
                if (errorneusItem == null)
                    return false;

                using (StoreContext store = new StoreContext() { Trace = Trace })
                {
                    store.SyncErrors.AddOrUpdate(new SyncError
                    {
                        Type = SyncType,
                        Guid = errorneusItem.Guid,
                        Offset = errorneusItem.DateModified
                    });
                    await store.SaveChangesAsync(cancellationToken);
                    // Пропускаємо тендер з синхронізації
                    if (reverse)
                        RestoreOffset = errorneusItem.DateModified;
                    else
                        SyncOffset = errorneusItem.DateModified;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceWarning("{0} cannot be skipped from sync. {1}", SyncType, ex);
                return false;
            }
        }

        /// <summary>
        /// Виправляє помилки синхронізації, зареєстровані в базі.
        /// </summary>
        public async Task FixSyncErrors(CancellationToken cancellationToken)
        {
            try
            {
                using (StoreContext store = new StoreContext() { Trace = Trace })
                {
                    SyncError[] errors = await store.SyncErrors.Where(e => e.Type == SyncType).ToArrayAsync(cancellationToken);
                    foreach (SyncError error in errors)
                    {
                        try
                        {
                            using (StoreContext tempStore = new StoreContext() { Trace = Trace })
                            {
                                await SyncItem(error.Guid, tempStore, cancellationToken);
                            }
                            store.SyncErrors.Remove(error);
                            await store.SaveChangesAsync(cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceWarning("SyncError of {0} {1} cannot be fixed. {2}", SyncType, error.Guid, ex);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                Trace.TraceError("SyncErrors cannot be fixed. " + ex);
            }
        }

        public void Dispose()
        {
            Service?.Dispose();
        }
    }
}