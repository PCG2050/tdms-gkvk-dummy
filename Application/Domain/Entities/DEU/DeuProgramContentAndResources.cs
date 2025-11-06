namespace Domain.Entities.DEU
{
   
        public class DeuProgramContentAndResources : AuditableBaseEntity
        {
            // FK to parent program
           
            public int? DeuProgramDetailsId { get; set; }
        [JsonIgnore]
            [ForeignKey(nameof(DeuProgramDetailsId))]
            public DeuProgramDetails? ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        // Navigation children
        public ICollection<DeuResourcePerson>? ResourcePersons { get; set; }
            public ICollection<DeuTopicsCoveredInClass>? TopicsCovered { get; set; }
            public ICollection<DeuTeachingAidsDeveloped>? TeachingAids { get; set; }
        }
    

}
