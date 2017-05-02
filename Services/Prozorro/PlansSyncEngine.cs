using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Prozorro.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro
{
    public class PlansSyncEngine : SyncEngine<IPlansService>
    {
        public PlansSyncEngine(bool fullSync = false, DateTime? offset = null, int limit = 100)
            : base(SyncItems.Plan, new PlansService(true), fullSync, offset, limit)
        {
        }

        protected override DateTime? GetSyncOffset(StoreContext store)
        {
            return store.Plans.Max(p => (DateTime?)p.DateModified);
        }

        protected override async Task<Rest.ModifiedElement[]> GetSyncedPage(StoreContext store, DateTime offset, int limit, CancellationToken cancellationToken)
        {
            return await store.Plans.Where(p => p.DateModified < offset)
                .OrderByDescending(p => p.DateModified)
                .Take(limit)
                .Select(p => new Rest.ModifiedElement { Guid = p.Guid, DateModified = p.DateModified })
                .ToArrayAsync();
        }

        protected override async Task SyncItem(Guid guid, StoreContext store, CancellationToken cancellationToken)
        {
            var restPlan = await Service.GetPlan(guid, cancellationToken);
            Plan savedPlan = await store.Plans
                .Include(p => p.ProcuringEntity)
                .Include(p => p.ProcuringEntity.AllIdentifiers)
                .Include(p => p.AdditionalClassifications)
                .Include(p => p.Items)
                .Include(p => p.Items.Select(i => i.AdditionalClassifications))
                .FirstOrDefaultAsync(p => p.Guid == guid, cancellationToken);

            // Оновлюємо план, разом зі всіма залежними об'єктами
            store.UpdatePlan(savedPlan, restPlan);

            await store.SaveChangesAsync(cancellationToken);
        }
    }
}