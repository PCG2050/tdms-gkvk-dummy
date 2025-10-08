using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkTopicsCoveredInClass : AuditableBaseEntity
    {
        

        [Required]
        public int? ProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(ProgramContentAndResourcesId))]
        public KvkProgramContentAndResources ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; } // path or URL to the uploaded photo

      

    }
}
