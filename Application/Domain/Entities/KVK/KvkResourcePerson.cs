using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkResourcePerson : AuditableBaseEntity
    {


        [Required]
        public int? KvkProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramContentAndResourcesId))]
        public KvkProgramContentAndResources? ProgramContentAndResources { get; set; }

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
