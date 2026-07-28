using TodoApp.Shared.Commons.Abstractions;

namespace TodoApp.Shared.Commons
{
    public class PaginationResult<T> :  IPaginationBase
    {
        public IEnumerable<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalRows { get; set; }
    }
}
