using TodoApp.Shared.Commons;

namespace TodoApp.Domain.Entities
{
    public class TaskItem : BaseEntity<long>
    {
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
    }
}
