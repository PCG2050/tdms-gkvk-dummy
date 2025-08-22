using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UnitHeadDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<UnitHeadUnitDto> Units { get; set; } = new List<UnitHeadUnitDto>();
    }

    public class UnitHeadUnitDto
    {
        public int UnitId { get; set; }
        public string Name { get; set; } // Unit Name
        public List<UnitHeadLocationDto> Locations { get; set; } = new List<UnitHeadLocationDto>();
    }

    public class UnitHeadLocationDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } 
        public List<UnitHeadDistrictDto> Districts { get; set; } = new List<UnitHeadDistrictDto>();
    }

    public class UnitHeadDistrictDto
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }
}
