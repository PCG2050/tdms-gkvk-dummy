using Domain.Entities.FIU;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuTeachingAidsDeveloped : AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public int? TypeOfAidId { get; set; }
        [JsonIgnore]
        public TypeOfAid? TypeOfAid { get; set; }

        [MaxLength(200)]
        public string Other { get; set; }

        [MaxLength(300)]
        public string Purpose { get; set; }

        public int Number { get; set; }
    }
}
