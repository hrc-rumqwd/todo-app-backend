using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using TodoApp.Application.Contracts;
using TodoApp.Application.Contracts.Generator;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Options;
using TodoApp.Infrastructure.Persistence;
using TodoApp.Infrastructure.Persistence.Interceptors;
using TodoApp.Infrastructure.Services.Identity;

namespace TodoApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            // Register infrastructure services, repositories, etc.
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
                // Configure your DbContext options here (e.g., connection string, provider, etc.)
                options
                    .UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(
                        sp.GetRequiredService<AuditSaveChangesInterceptor>()
                    )
            );

            services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<AuditSaveChangesInterceptor>();

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.ConfigureOptions<JwtConfigurationOptions>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();

            services.AddAuthenticationConfiguration(configuration);

            services.AddMappingConfiguration();

            return services;
        }

        private static IServiceCollection AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = configuration.GetSection(JwtConfigurationOptions.SectionKey).Get<JwtConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = jwtConfig.Audience;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,
                        ValidateIssuer = jwtConfig.ValidateIssuer,
                        ValidateAudience = jwtConfig.ValidateAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                        RequireExpirationTime = true,
                    };

                    //options.Events.OnAuthenticationFailed += context =>
                    //{
                    //    // Log the exception or handle it as needed
                    //    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger>();
                    //    logger.LogError(context.Exception, "Authentication failed.");
                    //    return Task.CompletedTask;
                    //};
                });
            return services;
        }

        private static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
