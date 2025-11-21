using Domain.Entities.Enum;

namespace Application.Models
{
    public class UnitHeadDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
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

    public class FlatUnitHeadDetailsDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }       
        public Gender? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public EmployementType? EmployementType { get; set; }
        public string? Qualification { get; set; } = null;
        public DateOnly? DateOfJoining { get; set; }
        public bool? IsPhoneConfirmed { get; set; }        
        public bool? IsDeactivated { get; set; } 
        public List<UnitLocationDetailsDto> UnitLocationDetails { get; set; } = new List<UnitLocationDetailsDto>();

    }

    public class UnitLocationDetailsDto
    {
        public int UnitLocationId { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public int OrganizationId { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
    }

    public class UnitHeadStatisticsDto
    {
        public int AssignedUnitsCount { get; set; }
        public int TrainersCount { get; set; }
        public int PendingApprovalsCount { get; set; }
        public int ApprovedThisMonthCount { get; set; }
    }
}
