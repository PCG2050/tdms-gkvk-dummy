


namespace Domain.Entities.STU
{
    public class StuProgramContentAndResources : AuditableBaseEntity
    {
        // FK to parent program
        [Required]
        public int? StuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StuProgramDetailsId))]
        public StuProgramDetails? ProgramDetails { get; set; }


        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<StuResourcePerson>? ResourcePersons { get; set; }
        public ICollection<StuTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<StuTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
