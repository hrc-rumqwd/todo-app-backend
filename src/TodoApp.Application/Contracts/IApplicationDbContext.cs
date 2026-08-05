using Microsoft.EntityFrameworkCore;

namespace TodoApp.Application.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cts);
    }
}
