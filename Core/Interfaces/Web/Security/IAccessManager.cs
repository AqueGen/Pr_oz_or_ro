using System;
using System.Threading.Tasks;

namespace Kapitalist.Web.Security
{
    public interface IAccessManager
    {
        int UserOrganizationId { get; }

        Task<string> GetTenderTokenAsync(Guid guid);

        bool CanCreateTender { get; }

        bool CanCreatePlan { get; }

        bool CanCreateBid { get; }
    }
}