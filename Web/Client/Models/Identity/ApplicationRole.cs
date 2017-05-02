using Microsoft.AspNet.Identity.EntityFramework;

namespace Kapitalist.Web.Client.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public string NameCyrillic { get; set; }

        public ApplicationRole() : base()
        {

        }

        public ApplicationRole(string name) : base(name)
        {
        }
    }
}