using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.STU
{
    public class STUTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int STUProgramContentAndResourcesID { get; set; }

        [ForeignKey(nameof(STUProgramContentAndResourcesID))]
        public STUProgramContentAndResources ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }
    }
}
