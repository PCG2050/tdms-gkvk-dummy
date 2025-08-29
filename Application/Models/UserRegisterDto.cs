using Domain.Entities.Enum;

namespace Application.Models
{
    public class UserRegisterDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int? OrganizationId { get; set; }
        public Role Role { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? DateOfJoining { get; set; }
        public EmployementType? EmploymentType { get; set; } 
        public Gender? Gender { get; set; }
        public string? Qualification { get; set; }

        public List<int>? OrganizationUnitLocationIds { get; set; } = new();
    }
    public class UserUpdateDto
    {

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        //public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public int? OrganizationId { get; set; }
        public Role? Role { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? DateOfJoining { get; set; }
        public EmployementType? EmploymentType { get; set; }
        public Gender? Gender { get; set; }
        public string? Qualification { get; set; }
        public List<int>? OrganizationUnitLocationIds { get; set; } = new();
    }

  

}
