using Application.Common.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Configurations;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Redis.OM;
using StackExchange.Redis;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(settings);

        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = settings.Get<JwtSettings>();
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });


        var redisConnectionString = configuration.GetConnectionString("Redis");
        if (redisConnectionString != null)
        {

            ConfigurationOptions options = new ConfigurationOptions
            {
                EndPoints = { redisConnectionString },
                AbortOnConnectFail = false,
                Password="123456",
                ConnectTimeout = 1000,
                AsyncTimeout = 1000,
                SyncTimeout = 1000,
            };
            ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
            services.AddSingleton<ICacheService, RedisCacheService>();
        }

    }
}
