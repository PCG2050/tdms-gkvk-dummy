namespace Domain.Entities.FTI
{
    public class FtiProgramContentAndResources : AuditableBaseEntity
    {
<<<<<<< Updated upstream
        // FK to parent program
        [Required]
        public int? FTIProgramDetailsID { get; set; }
=======
        public int? FtiProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
        public int? UnitLocationId { get; set; }
>>>>>>> Stashed changes

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails ProgramDetails { get; set; }

        // Optionally add fields describing the content/resources record
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation children
<<<<<<< Updated upstream
        public ICollection<FTIResourcePerson> ResourcePersons { get; set; }
        public ICollection<FTITopicsCoveredInClass> TopicsCovered { get; set; }
        public ICollection<FTITeachingAidsDeveloped> TeachingAids { get; set; }
=======
        public ICollection<FtiResourcePerson>? ResourcePersons { get; set; }
        public ICollection<FtiTopicsCoveredInClass>? TopicsCovered { get; set; }
        public ICollection<FtiTeachingAidsDeveloped>? TeachingAids { get; set; }
>>>>>>> Stashed changes
    }
}
