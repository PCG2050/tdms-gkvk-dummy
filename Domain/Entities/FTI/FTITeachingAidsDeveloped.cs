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
        public int? FTIProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FTIProgramContentAndResourcesId))]
        public FTIProgramContentAndResources? ProgramContentAndResources { get; set; }

        [MaxLength(200)]
        public int? TypeOfAidId { get; set; }
        [JsonIgnore]
        public TypeOfAid? TypeOfAid { get; set; }

        [MaxLength(200)]
        public string? OtherTypeOfAid { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
