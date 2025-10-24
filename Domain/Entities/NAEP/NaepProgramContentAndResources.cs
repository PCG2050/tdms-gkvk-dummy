
namespace Domain.Entities.NAEP
{
    public class NaepProgramContentAndResources : AuditableBaseEntity
    {
        public int? NaepProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(NaepProgramDetailsId))]
        public NaepProgramDetails? ProgramDetails { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<NaepResourcePerson>? ResourcePersons { get; set; }
        public ICollection<NaepTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<NaepTeachingAidsDeveloped>? TeachingAids { get; set; }
    }
}
