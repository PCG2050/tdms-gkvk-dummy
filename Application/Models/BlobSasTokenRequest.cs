using Application.Enums;

namespace Application.Models
{
    public class BlobSasTokenRequest
    {
        public string ContainerName { get; set; }
        public string BlobName { get; set; }
        public BlobOperationType OperationType { get; set; }
        public int ExpiryInMinutes { get; set; } = 60;
    }
}
