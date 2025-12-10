using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkFarmerScientistInteraction :AuditableBaseEntity
    {
        public int? KvkProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramContentAndResourcesId))]
        public KvkProgramContentAndResources? ProgramContentAndResources { get; set; }
        public DateOnly? Date { get; set; }
        public string? ScientistOfficerName { get; set; }
        public string? TopicDiscussed { get; set; }
        public int? NoOfFarmersParticipated { get; set; }
        public string? PhotoUpload { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }
    }
}
