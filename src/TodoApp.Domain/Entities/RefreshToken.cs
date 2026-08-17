using TodoApp.Shared.Commons;

namespace TodoApp.Domain.Entities
{
    public class RefreshToken : BaseEntity<long>
    {
        public Guid UserId { get; set; }
        public string TokenHash { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime RevokedAt { get; set; }
        public string? CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }
    }
}
