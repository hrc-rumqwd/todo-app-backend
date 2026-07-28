namespace TodoApp.Shared.Commons.Abstractions
{
    public interface IPaginationBase
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
