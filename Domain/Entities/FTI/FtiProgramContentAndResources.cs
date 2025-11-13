namespace Domain.Entities.FTI
{
    public class FtiProgramContentAndResources : AuditableBaseEntity
    {
        public int? FtiProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<FtiResourcePerson>? ResourcePersons { get; set; }
        public ICollection<FtiTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<FtiTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
