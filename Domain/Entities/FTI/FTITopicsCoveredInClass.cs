using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FTITopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int FTIProgramContentAndResourcesID { get; set; }

        [ForeignKey(nameof(FTIProgramContentAndResourcesID))]
        public FTIProgramContentAndResources ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }
    }
}
