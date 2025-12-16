using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuFieldVisit :AuditableBaseEntity
    {
        [Required]
        public int? EeuProgramContentAndResourcesId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateOnly? Date { get; set; }

        public string? ScientistOfficerVisitedName { get; set; }

        public string? Purpose { get; set; }

        public int? NoOfFieldsCovered { get; set; }

        public int? NoOfFarmerCovered { get; set; }

        public string? PhotoUpload { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        


    }
}
