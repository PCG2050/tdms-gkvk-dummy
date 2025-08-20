using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UnitHeadAssignmentDto
    {
        public int UnitHeadId { get; set; }
        public int UnitLocationId { get; set; }
        public bool IsActive { get; set; }
    }

    public class UnitHeadLocationAssignmentDto
    {
        public int UnitHeadId { get; set; }
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
    }
}
