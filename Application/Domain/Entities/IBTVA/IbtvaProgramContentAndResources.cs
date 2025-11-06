


namespace Domain.Entities.IBTVA
{
    public class IbtvaProgramContentAndResources : AuditableBaseEntity
    {
        public int? IbtvaProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IbtvaProgramDetailsId))]
        public IbtvaProgramDetails? ProgramDetails { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<IbtvaResourcePerson>? ResourcePersons { get; set; }
        public ICollection<IbtvaTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<IbtvaTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
