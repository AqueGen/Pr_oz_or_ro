using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class CreatedElementBase
    {
        public int Id { get; set; }

        [StringLength(128)]
        public string Token { get; set; }

        public int UserOrganizationId { get; set; }

        public virtual UserOrganization UserOrganization { get; set; }
    }
}