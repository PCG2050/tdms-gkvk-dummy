using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ExistingUnitHeadAssignmentDto
    {
        public int UnitHeadId { get; set; }
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
    }

    public class BulkUnitHeadAssignmentByLocationDto
    {
        public int UnitHeadId { get; set; }
        public List<int> OrganizationUnitLocationIds {get;set; }     
    }
}
