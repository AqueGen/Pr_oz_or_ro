using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Prozorro;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Kapitalist.Services.Prozoro
{
    [TestClass()]
    public class SyncServiceTests : IDisposable
    {
        protected readonly TendersSyncEngine _service;

        public SyncServiceTests()
        {
            _service = new TendersSyncEngine();
        }

        [TestMethod()]
        public async Task NextSyncTest()
        {
            using (var tendersService = new TendersService(true))
            {
                var offset = (await tendersService.GetModificationsPage(null, true, 2)).NextPage.Offset;
                using (var syncService = new TendersSyncEngine(true, offset))
                    await syncService.NextSync(CancellationToken.None);
            }
        }

        [TestMethod()]
        public async Task NextSyncTest2()
        {
            using (var tendersService = new PlansService(true))
            {
                var offset = (await tendersService.GetModificationsPage(null, true, 2)).NextPage.Offset;
                using (var syncService = new PlansSyncEngine(true, offset))
                    await syncService.NextSync(CancellationToken.None);
            }
        }

        [TestMethod()]
        public async Task PrevSyncTest()
        {
            using (var tendersService = new TendersService(true))
            {
                var offset = (await tendersService.GetModificationsPage(null, false, 1)).NextPage.Offset;
                using (var syncService = new TendersSyncEngine(true, offset))
                    await syncService.PrevSync(CancellationToken.None);
            }
        }

        [TestMethod()]
        public async Task SyncTenderTest()
        {
            Guid tenderGuid = new Guid("f788379e-e742-49d3-8719-0f3cac64c79e");
            var SyncItem = typeof(TendersSyncEngine).GetMethod("SyncItem", BindingFlags.Instance | BindingFlags.NonPublic);
            using (StoreContext store = new StoreContext())
            {
                await (Task)SyncItem.Invoke(_service, new object[] { tenderGuid, store, CancellationToken.None });
            }
        }

        [TestMethod()]
        public async Task SyncPlanTest()
        {
            Guid planGuid = new Guid("84777ad8-595f-49cb-9e55-37b9ea7d6f02");
            var SyncItem = typeof(PlansSyncEngine).GetMethod("SyncItem", BindingFlags.Instance | BindingFlags.NonPublic);
            using (StoreContext store = new StoreContext())
            {
                using (var service = new PlansSyncEngine())
                    await (Task)SyncItem.Invoke(service, new object[] { planGuid, store, CancellationToken.None });
            }
        }

        private async Task<Guid> GetFirstTenderGuid()
        {
            using (TendersService service = new TendersService(true))
            {
                return (await service.GetModificationsPage(null, false, 1)).Items.First().Guid;
            }
        }

        public void Dispose()
        {
            _service?.Dispose();
        }

        [TestMethod()]
        public async Task RegisterSyncErrorTest()
        {
            Guid tenderGuid = await GetFirstTenderGuid();
            using (TendersSyncEngine service = new TendersSyncEngine(true))
            {
                var СurrentTender = typeof(TendersSyncEngine).GetProperty("SyncingItem", BindingFlags.Instance | BindingFlags.Public);
                СurrentTender.SetValue(service, tenderGuid);
                await service.RegisterSyncError(false, CancellationToken.None);
                using (StoreContext store = new StoreContext())
                {
                    store.SyncErrors.First(e => e.Type == SyncItems.Tender && e.Guid == tenderGuid);
                }
            }
        }

        [TestMethod()]
        public async Task FixSyncErrorsTest()
        {
            using (StoreContext store = new StoreContext())
            {
                Tender tender = await store.Tenders.FirstOrDefaultAsync();
                store.SyncErrors.AddOrUpdate(new SyncError
                {
                    Type = SyncItems.Tender,
                    Guid = tender.Guid,
                    Offset = tender.DateModified.AddMilliseconds(-1)
                });
                await store.SaveChangesAsync();
                using (TendersSyncEngine service = new TendersSyncEngine())
                {
                    await service.FixSyncErrors(CancellationToken.None);
                }
                Assert.IsTrue(!await store.SyncErrors.AnyAsync(e => e.Type == SyncItems.Tender && e.Guid == tender.Guid));
            }
        }
    }
}