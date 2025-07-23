using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrgUnitLocationDetailsDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public ICollection<LocationDto> Location { get; set; } = [];
    }
    public class LocationDto
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
