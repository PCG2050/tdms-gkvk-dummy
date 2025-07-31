using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public abstract class AuditableBaseEntity
    {
        public int Id { get; set; }
        public int? CreatedById { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int? UpdatedById { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        [JsonIgnore]
        public User? CreatedBy { get; set; }
        [JsonIgnore]
        public User? UpdatedBy { get; set; }
    }
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
