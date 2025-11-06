namespace Application.Models
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public string DeviceInfo { get; set; }
        public string IpAddress { get; set; }
        public string Location { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastUsedAt { get; set; }
        public bool IsCurrent { get; set; }
    }

}
