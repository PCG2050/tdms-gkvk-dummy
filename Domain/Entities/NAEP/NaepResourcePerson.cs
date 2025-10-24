namespace Domain.Entities.NAEP
{
    public class NaepResourcePerson : AuditableBaseEntity
    {
        [Required]
        public int? NaepProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(NaepProgramContentAndResourcesId))]
        public NaepProgramContentAndResources? ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Designation { get; set; }

        [MaxLength(150)]
        public int? ResourceType { get; set; }

        [MaxLength(250)]
        public int? Responsibility { get; set; }

        [MaxLength(250)]
        public string? InstitutionOrDepartment { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
