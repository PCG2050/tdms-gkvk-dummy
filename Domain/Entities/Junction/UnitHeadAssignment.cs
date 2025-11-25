using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Junction
{
    public class UnitHeadAssignment :AuditableBaseEntity
    {
        public int UnitLocationId { get; set; }
        public int UnitHeadId { get; set; }
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;        
        public User UnitHead { get; set; } = null!;

        //soft delete

        public bool IsDeactivated { get; set; } = false;
        public DateTimeOffset? DeactivatedAt { get; set; }

        public int? DeactivatedById { get;set; }

    }
}
