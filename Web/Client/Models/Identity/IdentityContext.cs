using Microsoft.AspNet.Identity.EntityFramework;

namespace Kapitalist.Web.Client.Models.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext() : base("IdentityContext")
        {
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}