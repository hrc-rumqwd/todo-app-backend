using Microsoft.EntityFrameworkCore.Diagnostics;
using TodoApp.Application.Contracts;
using TodoApp.Shared.Commons.Abstractions;

namespace TodoApp.Infrastructure.Persistence.Interceptors
{
    internal class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IUserService _userService;

        public AuditSaveChangesInterceptor(IUserService userService)
        {
            _userService = userService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var auditEntities = eventData.Context.ChangeTracker.Entries<IAuditableEntity>();
            if (auditEntities.Any())
            {
                // Perform audit logic here
                foreach (var entity in auditEntities)
                {
                    if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    {
                        entity.Entity.CreatedAt = DateTime.UtcNow;
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        entity.Entity.CreatedBy = _userService.GetUserId().ToString();
                        entity.Entity.UpdatedBy = _userService.GetUserId().ToString();
                    }
                    else if (entity.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                    {
                        entity.Entity.UpdatedAt = DateTime.UtcNow;
                        entity.Entity.UpdatedBy = _userService.GetUserId().ToString();
                    }
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            var auditEntities = eventData.Context.ChangeTracker.Entries<IRemovableEntity>();
            if (auditEntities.Any())
            {
                // Perform audit logic here
                foreach (var entity in auditEntities)
                {
                    Console.WriteLine(entity.Entity.IsActive);
                }
            }
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
