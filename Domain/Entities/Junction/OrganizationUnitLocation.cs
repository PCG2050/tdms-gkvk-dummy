using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Junction
{
    public class OrganizationUnitLocation:AuditableBaseEntity
    {
        public int OrganizationId { get; set; }
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
        public Organization Organization { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public District District { get; set; } = null!;
    }
}
