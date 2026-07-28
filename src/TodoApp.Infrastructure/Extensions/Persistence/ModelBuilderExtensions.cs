using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Extensions.Persistence
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConfigurationForIdentity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>(cfg =>
            {
                cfg.ToTable("Users");
                cfg.HasKey(x => x.Id);
                cfg.Property(u => u.FullName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<AppRole>(cfg =>
            {
                cfg.ToTable("Roles");
                cfg.HasKey(x => x.Id);
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(cfg =>
            {
                cfg.ToTable("UserClaims");
                cfg.HasKey(x => x.Id);
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(cfg =>
            {
                cfg.ToTable("RoleClaims");
                cfg.HasKey(x => x.Id);
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(cfg =>
            {
                cfg.ToTable("UserRoles");
                cfg.HasKey(x => new { x.UserId, x.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(cfg =>
            {
                cfg.ToTable("UserTokens");
                cfg.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(cfg =>
            {
                cfg.ToTable("UserLogins");
                cfg.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            });

            return modelBuilder;
        }
    }
}
