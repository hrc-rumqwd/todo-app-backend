using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Extensions.Persistence
{
    public static class SeedDataExtensions
    {
        public static async Task<WebApplication> UseSeedData(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<UserManager<AppUser>>();
                var configuration = services.GetRequiredService<IConfiguration>();

                // Seed data here
                await SeedHelper.Initialize(configuration, context);
            }
            return app;
        }
    }
}
