using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Contracts;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Extensions.Persistence;

namespace TodoApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<
        AppUser,
        AppRole, 
        Guid>, IApplicationDbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.ApplyConfigurationForIdentity();
        }
    }
}
