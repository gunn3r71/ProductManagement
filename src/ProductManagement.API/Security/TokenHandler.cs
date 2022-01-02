using Microsoft.IdentityModel.Tokens;
using ProductManagement.API.Extensions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ProductManagement.API.Security
{
    public class TokenHandler
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenHandler(JwtSettings jwtSettings,
                            UserManager<IdentityUser> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var identityClaims = SetClaims(claims, user, userRoles);

            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = identityClaims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            });

            return handler.WriteToken(token);
        }

        private ClaimsIdentity SetClaims(IList<Claim> claims, IdentityUser user, IList<string> roles)
        {
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new(JwtRegisteredClaimNames.Email, user.Email, ClaimValueTypes.Email));
            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new(JwtRegisteredClaimNames.Exp, ToUnixEpochDate(DateTime.UtcNow.AddHours(-3).AddHours(_jwtSettings.Expires)).ToString(), ClaimValueTypes.Integer));
            claims.Add(new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow.AddHours(-3)).ToString(), ClaimValueTypes.Integer));
            claims.Add(new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow.AddHours(-3)).ToString(), ClaimValueTypes.Integer));

            foreach (var role in roles) 
                claims.Add(new("role", role));

            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}