using Kapitalist.Data.Store;
using Kapitalist.Web.Security.Helpers;
using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Kapitalist.Web.Security
{
    public class AccessManager : IAccessManager
    {
        private StoreContext Store { get; }

        private IPrincipal User { get; }

        public AccessManager(StoreContext store)
        {
            Store = store;
            User = HttpContext.Current.User;
        }

        public async Task<string> GetTenderTokenAsync(Guid guid)
        {
            var record = await Store.CreatedTenders.FirstOrDefaultAsync(x => x.Tender.Guid == guid && x.UserOrganizationId == UserOrganizationId);
            if (record == null)
                throw new AccessViolationException("Tender does not belong to this user.");
            return record.Token;
        }

        private int? _userOrganizationId;

        public int UserOrganizationId
        {
            get {
                if (!_userOrganizationId.HasValue)
                    _userOrganizationId = User.GetUserOrganizationId();
                return _userOrganizationId.Value;
            }
        }

        public bool CanCreateTender
        {
            get {
                return true;
                // TODO Oleg 0: implement this role
                return User.IsInRole(OrganizationRoles.ProcuringEntity.ToString());
            }
        }

        public bool CanCreatePlan
        {
            get {
                return User.IsInRole(OrganizationRoles.ProcuringEntity.ToString());
            }
        }

        public bool CanCreateBid
        {
            get {
                return User.IsInRole(OrganizationRoles.Procurer.ToString());
            }
        }
    }
}