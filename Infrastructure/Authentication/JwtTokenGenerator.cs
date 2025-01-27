using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator(IOptionsMonitor<JwtSettings> jwtSettings) : IJwtTokenGenerator
    {
        private readonly IOptionsMonitor<JwtSettings> _jwtSettings = jwtSettings;

        public Task<string> GenerateToken(User user)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.Name, user.Firstname ?? string.Empty),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim("FullName", $"{user.Firstname} {user.Lastname}"),
                new Claim(ClaimTypes.Email, user.Emailaddress),
                new Claim("UserId", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.CurrentValue.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               issuer: _jwtSettings.CurrentValue.Issuer,
               audience: _jwtSettings.CurrentValue.Audience,
               claims: claims,
               expires: DateTime.Now.AddMinutes(_jwtSettings.CurrentValue.ExpiryInMinutes),
               signingCredentials: creds
           );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
