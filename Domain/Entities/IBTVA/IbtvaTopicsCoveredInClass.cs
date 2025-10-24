

namespace Domain.Entities.IBTVA
{
    public class IbtvaTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? IbtvaProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IbtvaProgramContentAndResourcesId))]
        public IbtvaProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
