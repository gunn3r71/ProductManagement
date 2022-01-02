using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.API.Extensions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductManagement.API.Security
{
    public class TokenHandler
    {
        private readonly JwtSettings _jwtSettings;

        public TokenHandler(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken()
        {
            var handler = new JwtSecurityTokenHandler();


            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var token = handler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow.AddHours(-3),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.Expires - 3),
                NotBefore = DateTime.UtcNow.AddHours(-3)
            });

            return handler.WriteToken(token);
        }
    }
}