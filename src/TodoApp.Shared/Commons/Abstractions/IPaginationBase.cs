namespace TodoApp.Shared.Commons.Abstractions
{
    public interface IPaginationBase
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }

    public static class PaginationDefaults
    {
        public const int DefaultPageSize = 10;
        public const int DefaultPageIndex = 1;
    }
}
