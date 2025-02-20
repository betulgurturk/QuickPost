using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuickPost.Application.Behaviors.Validation;
using System.Reflection;
using FluentValidation;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddOpenTelemetry().WithTracing(b =>
        b.SetResourceBuilder(
            ResourceBuilder.CreateDefault().AddService("QuickPost")
        )
        .AddSource("MainTracer")
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddEntityFrameworkCoreInstrumentation(options =>
         {
             options.SetDbStatementForText = true;
         })
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
        })
        );


            return services;
        }
    }
}
