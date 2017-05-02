using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Interfaces
{
    public interface ISyncEngine : IDisposable
    {
        SyncItems SyncType { get; }
        DateTime SyncOffset { get; }
        ModifiedElement SyncingItem { get; }
        DateTime RestoreOffset { get; }
        ModifiedElement RestoringItem { get; }
        ITrace Trace { get; set; }

        Task NextSync(CancellationToken cancellationToken);

        Task PrevSync(CancellationToken cancellationToken);

        Task<bool> RegisterSyncError(bool reverse, CancellationToken cancellationToken);

        Task FixSyncErrors(CancellationToken cancellationToken);
    }
}