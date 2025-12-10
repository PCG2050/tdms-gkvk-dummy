using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.KVK
{
    public class KvkFieldDay :AuditableBaseEntity
    {
        public int? KvkProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramContentAndResourcesId))]
        public KvkProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateOnly? Date { get; set; }
        public string? FarmerName { get; set; }
        public string? Place { get; set; }

        public int? NoOfBeneficieries { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
