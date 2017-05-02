using System.Security.Claims;
using System.Threading.Tasks;
using Kapitalist.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Kapitalist.Web.Client.Models.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(nameof(IApplicationUser.UserOrganizationId), UserOrganizationId.ToString()));

            return userIdentity;
        }
        //add your custom properties which have not included in IdentityUser before
        public int UserOrganizationId { get; set; }
    }
}