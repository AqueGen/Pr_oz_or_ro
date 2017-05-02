using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
    public class ContactPointEx<TOrganization> : ContactPointOptional
        where TOrganization : IOrganization
    {
        public ContactPointEx()
        {
        }

        public ContactPointEx(ContactPoint contactPoint)
            : base(contactPoint)
        {
        }

        public int Id { get; set; }

        public int OrganizationId { get; set; }

        public int SortingOrder { get; set; }

        public virtual TOrganization Organization { get; set; }
    }
}