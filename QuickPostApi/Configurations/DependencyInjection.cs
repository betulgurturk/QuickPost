using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persistence;
using QuickPostApi.Services;

namespace QuickPostApi.Configurations
{
    /// <summary>
    /// Dependency Injection for all services
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Add all services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, CurrentUserService>();

            services.AddInfrastructure(configuration);
            services.AddApplicationServices();
            services.AddPersistenceServices(configuration);
        }
    }
}
