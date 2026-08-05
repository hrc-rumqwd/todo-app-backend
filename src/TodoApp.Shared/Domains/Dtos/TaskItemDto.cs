namespace TodoApp.Shared.Domains.Dtos
{
    public class TaskItemDto
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public string? AuthorName { get; set; }
    }
}
