

namespace Domain.Entities.ATIC
{
    public class AticResourcePerson : AuditableBaseEntity
    {
        [Required]
        public int? AticProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(AticProgramContentAndResourcesId))]
        public AticProgramContentAndResources? ProgramContentAndResources { get; set; }

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
