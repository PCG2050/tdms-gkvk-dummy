
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
        public string? Qualification { get; set; } 
        public DateOnly DateOfJoining { get; set; }
        public string Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public required int? OrganizationId { get; set; }
        public bool IsDeactivated { get; set; } = false;

        public Organization Organization { get; set; } = null!;
        public ICollection<TrainerAssignment> TrainerAssignments { get; set; } = [];

        public ICollection<UnitHeadAssignment> UnitHeadAssignments { get; set; } = [];
        public ICollection<UserSession> UserSessions { get; set; } = [];
       
        // Replace the existing token fields with OTP fields

       [JsonIgnore]
        public string? PasswordResetOTP { get; set; }
        [JsonIgnore]
        public DateTimeOffset? PasswordResetOTPExpiresAt { get; set; }
        [JsonIgnore]
        public int PasswordResetOTPAttempts { get; set; } = 0;
        [JsonIgnore]
        public DateTimeOffset? PasswordResetOTPLastAttempt { get; set; }

        [JsonIgnore]
        public string? LastUsedPasswordResetOTP { get; set; }
    }
}
    