using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Junction
{
    public class UnitHeadAssignment : AuditableBaseEntity
    {
        public int UnitHeadId { get; set; }
        public int UnitLocationId { get; set; }
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public User UnitHead { get; set; } = null!;
        [JsonIgnore]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
    }
}
