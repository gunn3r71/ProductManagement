using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace ProductManagement.API.Security.Filters
{
    public class RequiredClaimFilter
    {
        private readonly Claim _claim;

        public RequiredClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity is not null &&
                !context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            if (!CustomAuthorization.ValidateClaimsUser(context.HttpContext, _claim.Type, _claim.Value))
                context.Result = new StatusCodeResult(403);
        }
    }

    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequiredClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}