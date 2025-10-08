using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkTeachingAidsDeveloped : AuditableBaseEntity
    {
       
        [Required]
        public int ProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(ProgramContentAndResourcesId))]
        public KvkProgramContentAndResources ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public string? TypeOfAidDeveloped { get; set; }

        [MaxLength(200)]
        public string? Other { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }


    }
}
