using Domain.Entities.Enum;
using Domain.Entities.FTI;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User:AuditableBaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; } = null;
        public Role Role { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public int Phone { get; set; }
        public bool IsPhoneConfirmed { get; set; }
        public required int? OrganizationId { get; set; }
        public Organization Organization { get; set; } = null!;
    }
}
