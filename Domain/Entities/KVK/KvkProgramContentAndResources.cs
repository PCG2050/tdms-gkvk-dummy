

namespace Domain.Entities.KVK
{
    public class KvkProgramContentAndResources : AuditableBaseEntity
    {


        public int? KvkProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramDetailsId))]
        public KvkProgramDetails? ProgramDetails { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<KvkResourcePerson>? ResourcePersons { get; set; }
        public ICollection<KvkTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<KvkTeachingAidsDeveloped>? TeachingAids { get; set; }

        //new 
        public ICollection<KvkFieldVisit>? FieldVisits { get; set; }
        public ICollection<KvkFieldDay>? FieldDays { get; set; }
        public ICollection<KvkFarmerScientistInteraction>? FarmerScientistInteractions { get; set; } 

        }
}
