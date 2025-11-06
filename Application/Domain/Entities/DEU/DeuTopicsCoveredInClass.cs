
namespace Domain.Entities.DEU
{
    public class DeuTopicsCoveredInClass :AuditableBaseEntity
    {
        [Required]
        public int? DeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(DeuProgramContentAndResourcesId))]
        public DeuProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
