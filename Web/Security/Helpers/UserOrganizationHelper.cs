using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Principal;

namespace Kapitalist.Web.Security.Helpers
{
    public static class UserOrganizationHelper
    {
        public static int GetUserOrganizationId(this IPrincipal user)
        {
            var claim = (user?.Identity as ClaimsIdentity)?.FindFirst(nameof(IApplicationUser.UserOrganizationId));
            if (claim == null)
                throw new AuthenticationException("User is not authenticated.");
            return Convert.ToInt32(claim.Value);
        }
    }
}