using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence
{
    public static class SeedHelper
    {
        public static async Task Initialize(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            // Seed your master data here
            // Example:
            // if (!context.Roles.Any())
            // {
            //     context.Roles.Add(new Role { Name = "Admin" });
            //     context.Roles.Add(new Role { Name = "User" });
            //     context.SaveChanges();
            // }

            var defaultUser = configuration.GetSection("Seed:DefaultUser").Get<AppUser>();
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user is null)
            {
                await userManager.CreateAsync(defaultUser, defaultUser.PasswordHash);
            }
        }
    }
}
