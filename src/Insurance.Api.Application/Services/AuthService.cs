using Insurance.Api.Application.DTOs;
using Insurance.Api.Application.DTOs.Auth;
using Insurance.Api.Application.Interfaces;
using Insurance.Api.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Insurance.Api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokenConfiguration _appSettings;

        public AuthService(IOptions<TokenConfiguration> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Generates a token from the user information
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public JwtDto GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            });

            DateTime expDate = DateTime.UtcNow.AddHours(1);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claims,
                Expires = expDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appSettings.Audience,
                Issuer = _appSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtDto
            {
                Token = tokenHandler.WriteToken(token),
                ExpDate = expDate
            };
        }
    }
}
