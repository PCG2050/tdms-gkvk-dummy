

namespace Domain.Entities.ATIC
{
    public class AticTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? AticProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(AticProgramContentAndResourcesId))]
        public AticProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
