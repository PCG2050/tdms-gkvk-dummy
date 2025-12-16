


namespace Domain.Entities.SAMETI
{
    public class SametiProgramContentAndResources : AuditableBaseEntity
    {
        // FK to parent program
        [Required]
        public int? SametiProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(SametiProgramDetailsId))]
        public SametiProgramDetails? ProgramDetails { get; set; }


        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<SametiResourcePerson>? ResourcePersons { get; set; }
        public ICollection<SametiTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<SametiTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
