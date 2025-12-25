using Domain.Entities.DEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuResourcePerson : AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }

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
