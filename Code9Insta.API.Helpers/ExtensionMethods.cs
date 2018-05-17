using System.Security.Claims;

namespace Code9Insta.API.Helpers
{
    public static class ExtensionMethods
    { 
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
