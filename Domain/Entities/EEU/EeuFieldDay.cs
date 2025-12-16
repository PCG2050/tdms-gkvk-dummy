using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.EEU
{
    public class EeuFieldDay :AuditableBaseEntity
    {
        public int? EeuProgramContentAndResourcesId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(EeuProgramContentAndResourcesId))]
        public EeuProgramContentAndResources? ProgramContentAndResources { get; set; }

        public DateOnly? Date { get; set; }
        public string? FarmerName { get; set; }
        public string? Place { get; set; }

        public int? NoOfBeneficieries { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
