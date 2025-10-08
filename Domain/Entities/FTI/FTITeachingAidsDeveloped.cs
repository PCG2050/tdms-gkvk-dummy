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
    public class FTITeachingAidsDeveloped : AuditableBaseEntity
    {

        [Required]
        public int FTIProgramContentAndResourcesID { get; set; }

        [ForeignKey(nameof(FTIProgramContentAndResourcesID))]
        public FTIProgramContentAndResources ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public string? TypeOfAidDeveloped { get; set; }

        [MaxLength(200)]
        public string? Other { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }
    }
}
