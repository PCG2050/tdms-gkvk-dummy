
namespace Domain.Entities.ATIC
{
    public class AticProgramContentAndResources : AuditableBaseEntity
    {
        public int? AticProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(AticProgramDetailsId))]
        public AticProgramDetails? ProgramDetails { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<AticResourcePerson>? ResourcePersons { get; set; }
        public ICollection<AticTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<AticTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
