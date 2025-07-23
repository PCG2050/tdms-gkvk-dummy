using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ExistingTrainerAssignmentDto
    {
        public int TrainerId { get; set; }
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
    }
}
