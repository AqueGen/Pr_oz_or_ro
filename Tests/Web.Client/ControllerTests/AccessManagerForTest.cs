using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Kapitalist.Data.Store;
using Kapitalist.Web.Security;

namespace Tests.Kapitalist.Web.Client.ControllerTests
{
    public class AccessManagerForTest : IAccessManager
    {
        public int UserOrganizationId { get; set; } = 7;

        public async Task<string> GetTenderTokenAsync(Guid guid)
        {
            var record = await Store.CreatedTenders.FirstOrDefaultAsync(x => x.Tender.Guid == guid && x.UserOrganizationId == UserOrganizationId);
            if (record == null)
                throw new AccessViolationException("Tender does not belong to this user.");
            return record.Token;
        }

        public bool CanCreateTender { get; set; } = true;

        public bool CanCreatePlan { get; set; } = true;

        public bool CanCreateBid { get; set; } = true;


        public AccessManagerForTest(StoreContext store)
        {
            Store = store;
        }

        public StoreContext Store { get; set; }
    }
}