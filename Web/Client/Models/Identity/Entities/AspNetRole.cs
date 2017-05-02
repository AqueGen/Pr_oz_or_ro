using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Web.Client.Models.Identity.Entities
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(128)]
        public string Discriminator { get; set; }

        [Required]
        [StringLength(256)]
        public string NameCyrillic { get; set; }

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
