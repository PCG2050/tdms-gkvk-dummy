using Domain.Entities.FTI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
    public class DeuResourcePerson : AuditableBaseEntity
    {
        [Required]
        public int? DeuProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(DeuProgramContentAndResourcesId))]
        public DeuProgramContentAndResources ProgramContentAndResources { get; set; }

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
    }
}
