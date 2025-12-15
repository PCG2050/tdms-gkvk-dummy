
namespace Domain.Entities
{
    public class Organization:AuditableBaseEntity
    {
        public required string Name { get; set; }
        public string? Logo { get; set; }
        public required int? DistrictId { get; set; }
        public string? PinCode { get; set; }
        public required string StorageContainerName { get; set; }

        /// <summary>
        /// Public container name (auto-generated as {StorageContainerName}-public)
        /// </summary>
        public string? StorageContainerNamePublic { get; set; }

        /// <summary>
        /// When containers were created in Azure
        /// </summary>
        public DateTimeOffset? ContainerCreatedDate { get; set; }

        /// <summary>
        /// User who created the containers
        /// </summary>
        public int? ContainerCreatedBy { get; set; }

        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; } = [];
        [JsonIgnore]
        public District District { get; set; } = null!;
    }
}
