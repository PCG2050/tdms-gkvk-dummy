using Domain.Entities.EEU;
using Domain.Entities.MasterData;
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
    public class IbtvaTeachingAidsDeveloped : AuditableBaseEntity
    {
        [Required]
        public int? IbtvaProgramContentAndResourcesId { get; set; }

        [ForeignKey(nameof(IbtvaProgramContentAndResourcesId))]
        public IbtvaProgramContentAndResources ProgramContentAndResources { get; set; }

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
