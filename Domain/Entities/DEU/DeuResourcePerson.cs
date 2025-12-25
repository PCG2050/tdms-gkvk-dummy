namespace Domain.Entities.DEU
{
    public class DeuResourcePerson : AuditableBaseEntity
    {
        [Required]
        public int? DeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(DeuProgramContentAndResourcesId))]
        public DeuProgramContentAndResources? ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public string? Name { get; set; }

        [MaxLength(150)]
        public string? Designation { get; set; }

        public int? ResourceTypeId { get; set; }
        [ForeignKey(nameof(ResourceTypeId))]
        public ResourceType? ResourceType { get; set; }

        public int? ResponsibilityId { get; set; }
        [ForeignKey(nameof(ResponsibilityId))]
        public Responsibility? Responsibility { get; set; }

        [MaxLength(250)]
        public string? InstitutionOrDepartment { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
