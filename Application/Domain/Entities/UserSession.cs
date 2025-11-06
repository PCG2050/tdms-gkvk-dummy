

namespace Domain.Entities
{
    public class UserSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public bool IsActive { get; set; }
        public string? DeviceId { get; set; }
        public string? Location { get; set; }

        public User User { get; set; } = null!;
    }
}
