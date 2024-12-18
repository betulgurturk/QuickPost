using Application.Common.Interfaces;
using Infrastructure.Authentication;
using Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(options =>
            {
                options.Issuer = settings["Issuer"];
                options.Audience = settings["Audience"];
                options.SecretKey = settings["SecretKey"];
                options.ExpiryInMinutes = int.Parse(settings["ExpiryInMinutes"]);
            });
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();
        }
    }
}
