using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class TrainerDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<TrainerUnitDto> Units { get; set; } = new List<TrainerUnitDto>();
    }

    public class TrainerUnitDto
    {
        public int UnitId { get; set; }
        public string Name { get; set; } // Unit Name
        public List<TrainerLocationDto> Locations { get; set; } = new List<TrainerLocationDto>();
    }

    public class TrainerLocationDto
    {
        public int StateId { get; set; }
        public string StateName { get; set; } 
        public List<TrainerDistrictDto> Districts { get; set; } = new List<TrainerDistrictDto>();
    }

    public class TrainerDistrictDto
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

    public class FlatTrainerDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }       
        
        public string Phone { get; set; }
        public UnitLocationDetailsDto UnitLocationDetails { get; set; } 
    }

}
