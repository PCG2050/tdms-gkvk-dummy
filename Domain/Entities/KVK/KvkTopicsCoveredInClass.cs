

namespace Domain.Entities.KVK
{
    public class KvkTopicsCoveredInClass : AuditableBaseEntity
    {


        [Required]
        public int? KvkProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramContentAndResourcesId))]
        public KvkProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateOnly? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }


    }
}
