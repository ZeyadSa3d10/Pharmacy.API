using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pharmacy.Domain.Interfaces.Service;
using Pharmacy.Application.Services.Configration;
using Pharmacy.Domain.Models.Auth;

namespace Pharmacy.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _settings;

        public TokenService(IOptions<JwtSettings> options)
        {
            this._settings = options;
        }

        public string GenerateToken(User user,CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _settings.Value.Issuer,
                audience: _settings.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_settings.Value.ExpiresInDays),
                signingCredentials: creds
            );
            cancellationToken.ThrowIfCancellationRequested();
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
