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
    public class EeuTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }


        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
