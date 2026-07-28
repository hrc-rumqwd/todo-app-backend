using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Commons;

namespace TodoApp.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            // Register application services, handlers, etc.
            services.AddMediatR(opt =>
            {
                opt.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            });

            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            services.AddSingleton<IBroker, Broker>();
            return services;
        }
    }
}
