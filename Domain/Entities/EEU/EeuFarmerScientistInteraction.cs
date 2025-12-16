using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuFarmerScientistInteraction :AuditableBaseEntity
    {
        public int? EeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
