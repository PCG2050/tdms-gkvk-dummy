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
        

        // FK to KvkProgramContentAndResources
        [Required]
        public int ProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(ProgramContentAndResourcesId))]
        public KvkProgramContentAndResources ProgramContentAndResources { get; set; }

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
