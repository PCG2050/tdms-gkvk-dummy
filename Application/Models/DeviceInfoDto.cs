namespace Application.Models
{
    public class DeviceInfoDto
    {
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public string? DeviceId { get; set; }
        public string? Location { get; set; }
        public string? DeviceType { get; set; }
    }
}
