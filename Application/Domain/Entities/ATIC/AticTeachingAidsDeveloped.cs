using Domain.Entities.EEU;
using Domain.Entities.MasterData;
using System;

namespace Domain.Entities.ATIC
{
    public class AticTeachingAidsDeveloped : AuditableBaseEntity
    {
        [Required]
        public int? AticProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(AticProgramContentAndResourcesId))]
        public AticProgramContentAndResources? ProgramContentAndResources { get; set; }

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
