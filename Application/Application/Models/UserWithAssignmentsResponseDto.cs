using Domain.Entities.Enum;

namespace Application.Models
{
    public class UserWithAssignmentsResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Role Role { get; set; }
        public int? OrganizationId { get; set; }
        public bool IsDeactivated { get; set; }
        public Gender? Gender { get; set; }
        public EmployementType? EmploymentType { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? DateOfJoining { get; set; }
        public string? Qualification { get; set; }
        public List<int> OrganizationUnitLocationIds { get; set; } = new();
    }
}
