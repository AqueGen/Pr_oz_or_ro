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
    public class TendersSyncEngine : SyncEngine<ITendersService>
    {
        public TendersSyncEngine(bool fullSync = false, DateTime? offset = null, int limit = 100)
            : base(SyncItems.Tender, new TendersService(true), fullSync, offset, limit)
        {
        }

        protected override DateTime? GetSyncOffset(StoreContext store)
        {
            return store.Tenders.Max(t => (DateTime?)t.DateModified);
        }

        protected override async Task<Rest.ModifiedElement[]> GetSyncedPage(StoreContext store, DateTime offset, int limit, CancellationToken cancellationToken)
        {
            return await store.Tenders.Where(t => t.DateModified < offset)
                .OrderByDescending(t => t.DateModified)
                .Take(limit)
                .Select(t => new Rest.ModifiedElement { Guid = t.Guid, DateModified = t.DateModified })
                .ToArrayAsync();
        }

        /// <summary>
        /// Синхронізує вибрану закупівлю та всі звязані з нею об'єкти.
        /// </summary>
        /// <param name="guid">ідентифікатор закупівлі</param>
        /// <param name="store">контекст БД в якому потрыбно застосувати зміни</param>
        /// <param name="cancellationToken">токен для відміни синхронізації</param>
        protected override async Task SyncItem(Guid guid, StoreContext store, CancellationToken cancellationToken)
        {
            var restTender = await Service.GetTender(guid, cancellationToken);
            Tender savedTender = await store.Tenders
                .Include(t => t.ProcuringEntity)
                .Include(t => t.ProcuringEntity.AllIdentifiers)
                .Include(t => t.Lots)
                .Include(t => t.Items)
                .Include(t => t.Items.Select(i => i.AdditionalClassifications))
                .Include(t => t.Features)
                .Include(t => t.Features.Select(f => f.Values))
                .Include(t => t.Documents)
                .Include(t => t.Questions)
                .Include(t => t.Questions.Select(q => q.Author))
                .Include(t => t.Questions.Select(q => q.Author.AllIdentifiers))
                .Include(t => t.Complaints)
                .Include(t => t.Complaints.Select(c => c.Author))
                .Include(t => t.Complaints.Select(c => c.Author.AllIdentifiers))
                .Include(t => t.Complaints.Select(c => c.Documents))
                .Include(t => t.Bids)
                .Include(t => t.Bids.Select(b => b.Tenderers))
                .Include(t => t.Bids.Select(b => b.Tenderers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Bids.Select(b => b.Documents))
                .Include(t => t.Bids.Select(b => b.Parameters))
                .Include(t => t.Bids.Select(b => b.LotValues))
                .Include(t => t.Awards)
                .Include(t => t.Awards.Select(a => a.Suppliers))
                .Include(t => t.Awards.Select(a => a.Suppliers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Awards.Select(a => a.Documents))
                .Include(t => t.Awards.Select(a => a.Items))
                .Include(t => t.Awards.Select(a => a.Complaints))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Author)))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Author.AllIdentifiers)))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Documents)))
                .Include(t => t.Contracts)
                .Include(t => t.Contracts.Select(c => c.Suppliers))
                .Include(t => t.Contracts.Select(c => c.Suppliers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Contracts.Select(c => c.Documents))
                .Include(t => t.Contracts.Select(c => c.Items))
                .Include(t => t.Cancellations)
                .Include(t => t.Cancellations.Select(c => c.Documents))
                .Include(t => t.Revisions)
                .Include(t => t.Revisions.Select(r => r.Changes))
                .FirstOrDefaultAsync(t => t.Guid == guid, cancellationToken);

            // Оновлюємо закупівлю, разом зі всіма залежними об'єктами
            store.UpdateTender(savedTender, restTender);

            await store.SaveChangesAsync(cancellationToken);
        }
    }
}