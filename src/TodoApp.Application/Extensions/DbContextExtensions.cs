using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Contracts;

namespace TodoApp.Application.Extensions
{
    public static class DbContextExtensions
    {
        public static IQueryable<TEntity> Query<TEntity>(this IApplicationDbContext context, bool asNoTracking = true) where TEntity : class, new()
        {
            var query = context.Set<TEntity>().AsQueryable();
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
    }
}
