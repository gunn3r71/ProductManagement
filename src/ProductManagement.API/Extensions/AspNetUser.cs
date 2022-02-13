using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using ProductManagement.Business.Interfaces;

namespace ProductManagement.API.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext?.User?.Identity?.Name;

        public Guid GetUserId() =>
            _accessor.HttpContext is not null ? _accessor.HttpContext.User.GetUserId() : Guid.Empty;

        public string GetUserEmail() =>
            _accessor.HttpContext?.User.GetUserEmail();

        public bool IsAuthenticated() =>
            _accessor.HttpContext?.User.Identity is not null &&
            _accessor.HttpContext.User.Identity.IsAuthenticated;

        public bool IsInRole(string role) =>
            _accessor.HttpContext is not null &&
            _accessor.HttpContext.User.IsInRole(role);
        
        public IEnumerable<Claim> GetClaimsIdentity() => 
            _accessor.HttpContext?.User?.Claims;
    }

    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(null, nameof(principal));

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return !Guid.TryParse(claim, out Guid userId) ? Guid.Empty : userId;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal is null)
                throw new ArgumentException(null, nameof(principal));

            return principal.FindFirst(ClaimValueTypes.Email)?.Value;
        }
    }
}