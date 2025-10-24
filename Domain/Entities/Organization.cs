
namespace Domain.Entities
{
    public class Organization:AuditableBaseEntity
    {
        public required string Name { get; set; }
        public string? Logo { get; set; }
        public required int? DistrictId { get; set; }
        public string? PinCode { get; set; }
        public required string StorageContainerName { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; } = [];
        [JsonIgnore]
        public District District { get; set; } = null!;
    }
}
