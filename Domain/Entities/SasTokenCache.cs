using System;

namespace Domain.Entities
{
    /// <summary>
    /// Entity for caching Azure Blob Storage SAS tokens
    /// Prevents regenerating tokens on every request
    /// </summary>
    public class SasTokenCache : BaseEntity
    {
        public string ContainerName { get; set; } = string.Empty;
        public string SasToken { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public bool IsPublicContainer { get; set; }
    }
}
