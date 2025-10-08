using Domain.Entities.FIU;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DEU
{
    public class DeuTeachingAidsDeveloped : AuditableBaseEntity
    {
        [Required]
        public int DeuProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(DeuProgramContentAndResourcesId))]
        public DeuProgramContentAndResources ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public int? TypeOfAidId { get; set; }

        public TypeOfAid? TypeOfAid { get; set; }

        [MaxLength(200)]
        public string? Other { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }
    }
}
