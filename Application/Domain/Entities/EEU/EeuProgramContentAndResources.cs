namespace Domain.Entities.EEU
{
    public class EeuProgramContentAndResources : AuditableBaseEntity
    {
        public int? EeuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramDetailsId))]
        public EeuProgramDetails? ProgramDetails { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<EeuResourcePerson>? ResourcePersons { get; set; }
        public ICollection<EeuTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<EeuTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
