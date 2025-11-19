

namespace Application.Models
{
    public class TrainerLoginResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string UserRole { get; set; } = default!;
        //public DateTime ExpiresAt { get; set; }

        // Reuse your existing DTO with all trainer details & assigned locations
        public TrainerWithAssignmentsDto TrainerDetails { get; set; } = default!;
    }


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
