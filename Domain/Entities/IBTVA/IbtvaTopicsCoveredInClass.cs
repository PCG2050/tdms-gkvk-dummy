using Domain.Entities.EEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.IBTVA
{
    public class IbtvaTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
        public int? IbtvaProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IbtvaProgramContentAndResourcesId))]
        public IbtvaProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }
    }
}
