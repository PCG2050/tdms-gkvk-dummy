namespace Application.Models.DataTables
{
    public class AticAdvisoryServiceDto:AticAdvisoryServiceCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class AticAdvisoryServiceCreateDto : BaseEntryCreateDto
    {
        public required string ServiceType { get; set; }
        public int ServiceCount { get; set; }
        public int BeneficiaryCount { get; set; }
    }
    public class AticAdvisoryServiceUpdateDto : BaseEntryUpdateDto
    {
        public string? ServiceType { get; set; }
        public int? ServiceCount { get; set; }
        public int? BeneficiaryCount { get; set; }
    }
}
