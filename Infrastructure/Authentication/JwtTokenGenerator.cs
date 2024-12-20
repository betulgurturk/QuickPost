﻿using Application.Common.Interfaces;
using DBGenerator.Models;
using Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IOptionsMonitor<JwtSettings> _jwtSettings;

        public JwtTokenGenerator(IOptionsMonitor<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

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
