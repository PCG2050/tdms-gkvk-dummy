using Domain.Entities.FIU;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities.EEU
{
    public class EeuTeachingAidsDeveloped : AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }

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
