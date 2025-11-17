using Domain.Entities.FIU;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities.FTI
{
    public class FtiTeachingAidsDeveloped : AuditableBaseEntity
    {

        [Required]
<<<<<<< Updated upstream
        public int FTIProgramContentAndResourcesID { get; set; }

        [ForeignKey(nameof(FTIProgramContentAndResourcesID))]
        public FTIProgramContentAndResources ProgramContentAndResources { get; set; }
=======
        public int? FtiProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramContentAndResourcesId))]
        public FtiProgramContentAndResources? ProgramContentAndResources { get; set; }
>>>>>>> Stashed changes

        [MaxLength(200)]
        public string? TypeOfAidDeveloped { get; set; }

        [MaxLength(200)]
        public string? Other { get; set; }

        [MaxLength(300)]
        public string? Purpose { get; set; }

        public int? Number { get; set; }
    }
}
