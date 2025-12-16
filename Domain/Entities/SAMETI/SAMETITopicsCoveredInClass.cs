

namespace Domain.Entities.SAMETI
{
    public class SametiTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? SametiProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(SametiProgramContentAndResourcesId))]
        public SametiProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
