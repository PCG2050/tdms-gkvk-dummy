using Domain.Entities.DEU;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FtiTopicsCoveredInClass : AuditableBaseEntity
    {
        [Required]
<<<<<<< Updated upstream
        public int FTIProgramContentAndResourcesID { get; set; }
=======
        public int? FtiProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramContentAndResourcesId))]
        public FtiProgramContentAndResources? ProgramContentAndResources { get; set; }
>>>>>>> Stashed changes

        [ForeignKey(nameof(FTIProgramContentAndResourcesID))]
        public FTIProgramContentAndResources ProgramContentAndResources { get; set; }

        public DateTime? Date { get; set; }

        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? PhotoUpload { get; set; }
    }
}
