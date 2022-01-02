using Microsoft.AspNetCore.Http;

namespace ProductManagement.API.Security
{
    public static class CustomAuthorization
    {
        public static bool ValidateClaimsUser(HttpContext context, string claimName, string claimValue) =>
            context.User.Identity is not null &&
            context.User.Identity.IsAuthenticated &&
            context.User.HasClaim(x => x.Type == claimName && x.Value.Contains(claimValue));
    }
}