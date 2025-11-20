using Domain.Entities.Enum;

namespace Application.Models
{
    public class TrainerLoginResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        //public DateTime ExpiresAt { get; set; }

        // Reuse your existing DTO with all trainer details & assigned locations
        public TrainerWithAssignmentsDto TrainerDetails { get; set; } = default!;
    }

    /// <summary>
    /// Unified login response for both Trainers and Unit Heads
    /// Includes user role and assignment details
    /// </summary>
    public class UnifiedLoginResponseDto
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public string UserRole { get; set; } = default!;
        public UserDetailsDto UserDetails { get; set; } = default!;
    }

    /// <summary>
    /// User details with assignments (works for both Trainer and UnitHead)
    /// </summary>
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public EmployementType EmployementType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public bool IsDeactivated { get; set; }
        public string? Qualification { get; set; }

        // List of currently assigned location IDs
        public List<int> AssignedLocationIds { get; set; } = new List<int>();

        // Detailed location information
        public List<UnitLocationDetailsDto> UnitLocationDetails { get; set; } = new List<UnitLocationDetailsDto>();
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
