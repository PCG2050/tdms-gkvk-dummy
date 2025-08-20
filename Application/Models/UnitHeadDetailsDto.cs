using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class UnitHeadDetailsDto
    {
        public int  UnitId { get; set; }
        public string Name { get; set; }
        public List<UnitLocationDto> Locations { get; set; } = new List<UnitLocationDto>();
    }
    public class UnitHeadLocationDto
    {
        public int UnitLocationId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<UnitHeadDistrictDto> Districts { get; set; } = new List<UnitHeadDistrictDto>();
    }

    public class UnitHeadDistrictDto
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

    public class SubordinateTrainerDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
