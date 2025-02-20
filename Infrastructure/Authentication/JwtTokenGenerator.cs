using Application.Common.Interfaces;
using Domain.Models;
using Infrastructure.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Trace;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator(IOptionsMonitor<JwtSettings> jwtSettings, TracerProvider tracerProvider) : IJwtTokenGenerator
    {
        private readonly IOptionsMonitor<JwtSettings> _jwtSettings = jwtSettings;
        private readonly Tracer _tracer = tracerProvider.GetTracer("MainTracer");

        public Task<string> GenerateToken(User user)
        {
            using var span = _tracer.StartActiveSpan("JwtTokenGenerator");

            span.AddEvent("Token oluşturma başladı");

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

            span.AddEvent("Token başarıyla oluşturuldu");

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
