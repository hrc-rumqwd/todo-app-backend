namespace TodoApp.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ToPaginationQuery<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
