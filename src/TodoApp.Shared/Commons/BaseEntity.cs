using TodoApp.Shared.Commons.Abstractions;

namespace TodoApp.Shared.Commons
{
    public class BaseEntity<TKey> : IEntityId<TKey>, IActivableEntity, IAuditableEntity
    {
        public TKey Id { get; set; }
        public bool IsActivable { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
