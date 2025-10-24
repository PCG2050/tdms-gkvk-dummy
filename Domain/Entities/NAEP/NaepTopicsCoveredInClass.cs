

namespace Domain.Entities.NAEP
{
    public class NaepTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? NaepProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(NaepProgramContentAndResourcesId))]
        public NaepProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
