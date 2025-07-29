namespace Domain.Entities
{
    public abstract class AuditableBaseEntity
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public User? CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedById { get; set; }
        public User? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
