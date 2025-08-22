using Domain.Entities.Enum;
using Domain.Entities.FTI;
using Domain.Entities.Junction;
namespace Domain.Entities
{
    public class User:AuditableBaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public required string PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; } = null;
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public EmployementType EmployementType { get; set; }
        public string? Qualification { get; set; } = null;
        public DateOnly DateOfJoining { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public required int? OrganizationId { get; set; }
        public bool IsDeactivated { get; set; } = false;

        public Organization Organization { get; set; } = null!;
        public ICollection<TrainerAssignment> TrainerAssignments { get; set; } = [];

        public ICollection<UnitHeadAssignment> UnitHeadAssignments { get; set; } = [];
        public ICollection<UserSession> UserSessions { get; set; } = [];

        public string? PasswordResetToken { get; set; }
        public DateTimeOffset? PasswordResetTokenExpiresAt { get; set; }
    }
}
    