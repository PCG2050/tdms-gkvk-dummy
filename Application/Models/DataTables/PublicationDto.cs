using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables
{
    // ==================== PUBLICATION DTOs ====================

    /// <summary>
    /// DTO for returning publication data (Read operations)
    /// </summary>
    public class PublicationDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Attachments { get; set; }

        // Status tracking (Draft, Pending, Approved, Rejected)
        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

        // Unit and Organization info
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }

        // Creator info
        public int? CreatedById { get; set; }
        public string? CreatedByName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        // Master Data
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? OtherPublication { get; set; }

        public int? ModeId { get; set; }
        public string? ModeName { get; set; }
        public string? ModePublication { get; set; }

        public int? RegionId { get; set; }
        public string? RegionName { get; set; }

        public int? SourceId { get; set; }
        public string? SourceName { get; set; }

        public int? KannadaNewsPaperId { get; set; }
        public string? KannadaNewsPaperName { get; set; }

        public int? EnglishNewsPaperId { get; set; }
        public string? EnglishNewsPaperName { get; set; }

        // Publication Details
        public string? MJASFormat { get; set; }
        public string? PublicationTitle { get; set; }
        public string? PublicationJournalTitle { get; set; }
        public int? PublicationYear { get; set; }
        public int? PublicationVolume { get; set; }
        public string? PublicationIssue { get; set; }
        public int? PublicationPagesFrom { get; set; }
        public int? PublicationPagesTo { get; set; }
        public string? PublicationISBN { get; set; }
        public int? PublicationUniNumber { get; set; }
        public int? PublicationNAAS { get; set; }
        public int? PublicationImpact { get; set; }
        public string? PublicationWebLink { get; set; }
        public string? PublicationCover { get; set; }
        public string? PublicationWhole { get; set; }

        // Funding & Permission
        public int? Funds { get; set; }
        public string? SponsorDetails { get; set; }
        public DateOnly? PermissionLetterDate { get; set; }
        public string? PermissionLetterUrl { get; set; }
    }

    /// <summary>
    /// Complete publication with all phases data (for editing from history)
    /// </summary>
    public class CompletePublicationDto : PublicationDto
    {
        public PublisherDetailsDto? PublisherDetails { get; set; }
        public List<ExtensionLiteratureDto>? ExtensionLiteratures { get; set; }
    }

    /// <summary>
    /// DTO for creating new publication (Phase 1)
    /// </summary>
    public class PublicationCreateDto
    {
       
        [MaxLength(500, ErrorMessage = "Title cannot exceed 500 characters")]
        public string Title { get; set; } = string.Empty;

        public DateOnly? PublicationDate { get; set; }    

        public string? Attachments { get; set; }

        [Required(ErrorMessage = "Unit location is required")]
        public int UnitLocationId { get; set; }

        // Master Data IDs
        public int? CategoryId { get; set; }

        [MaxLength(200)]
        public string? OtherPublication { get; set; }

        public int? ModeId { get; set; }

        [MaxLength(200)]
        public string? ModePublication { get; set; }

        public int? RegionId { get; set; }
        public int? SourceId { get; set; }
        public int? KannadaNewsPaperId { get; set; }
        public int? EnglishNewsPaperId { get; set; }

        // Publication fields
        [MaxLength(200)]
        public string? MJASFormat { get; set; }

        [MaxLength(500)]
        public string? PublicationTitle { get; set; }

        [MaxLength(500)]
        public string? PublicationJournalTitle { get; set; }

        [Range(1900, 2100, ErrorMessage = "Publication year must be between 1900 and 2100")]
        public int? PublicationYear { get; set; }

        public int? PublicationVolume { get; set; }

        [MaxLength(100)]
        public string? PublicationIssue { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Page number cannot be negative")]
        public int? PublicationPagesFrom { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Page number cannot be negative")]
        public int? PublicationPagesTo { get; set; }

        [MaxLength(50)]
        public string? PublicationISBN { get; set; }

        public int? PublicationUniNumber { get; set; }
        public int? PublicationNAAS { get; set; }
        public int? PublicationImpact { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(1000)]
        public string? PublicationWebLink { get; set; }

        [MaxLength(1000)]
        public string? PublicationCover { get; set; }

        [MaxLength(1000)]
        public string? PublicationWhole { get; set; }

        // Funding
        [Range(0, int.MaxValue, ErrorMessage = "Funds cannot be negative")]
        public int? Funds { get; set; }

        [MaxLength(1000)]
        public string? SponsorDetails { get; set; }

        public DateOnly? PermissionLetterDate { get; set; }

        [MaxLength(1000)]
        public string? PermissionLetterUrl { get; set; }
    }

    /// <summary>
    /// DTO for updating publication (when editing Phase 1)
    /// All fields optional - only provided fields are updated
    /// </summary>
    public class PublicationUpdateDto
    {
        [MaxLength(500)]
        public string? Title { get; set; }

        public DateOnly? PublicationDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Attachments { get; set; }

        // Optional updates for all fields
        public int? CategoryId { get; set; }

        [MaxLength(200)]
        public string? OtherPublication { get; set; }

        public int? ModeId { get; set; }

        [MaxLength(200)]
        public string? ModePublication { get; set; }

        public int? RegionId { get; set; }
        public int? SourceId { get; set; }
        public int? KannadaNewsPaperId { get; set; }
        public int? EnglishNewsPaperId { get; set; }

        [MaxLength(200)]
        public string? MJASFormat { get; set; }

        [MaxLength(500)]
        public string? PublicationTitle { get; set; }

        [MaxLength(500)]
        public string? PublicationJournalTitle { get; set; }

        [Range(1900, 2100)]
        public int? PublicationYear { get; set; }

        public int? PublicationVolume { get; set; }

        [MaxLength(100)]
        public string? PublicationIssue { get; set; }

        [Range(0, int.MaxValue)]
        public int? PublicationPagesFrom { get; set; }

        [Range(0, int.MaxValue)]
        public int? PublicationPagesTo { get; set; }

        [MaxLength(50)]
        public string? PublicationISBN { get; set; }

        public int? PublicationUniNumber { get; set; }
        public int? PublicationNAAS { get; set; }
        public int? PublicationImpact { get; set; }

        [Url]
        [MaxLength(1000)]
        public string? PublicationWebLink { get; set; }

        [MaxLength(1000)]
        public string? PublicationCover { get; set; }

        [MaxLength(1000)]
        public string? PublicationWhole { get; set; }

        [Range(0, int.MaxValue)]
        public int? Funds { get; set; }

        [MaxLength(1000)]
        public string? SponsorDetails { get; set; }

        public DateOnly? PermissionLetterDate { get; set; }

        [MaxLength(1000)]
        public string? PermissionLetterUrl { get; set; }
    }

    // ==================== PUBLISHER DETAILS DTOs ====================

    /// <summary>
    /// DTO for returning publisher details (Read operations)
    /// </summary>
    public class PublisherDetailsDto
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public string? PublisherBrochure { get; set; }
        public string? PublisherName { get; set; }
        public string? PublisherInstitutionName { get; set; }
        public string? PublisherAddress { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO for creating/updating publisher details (Phase 2)
    /// </summary>
    public class PublisherDetailsCreateDto
    {
        [MaxLength(1000)]
        public string? PublisherBrochure { get; set; }

        [MaxLength(200, ErrorMessage = "Publisher name cannot exceed 200 characters")]
        public string? PublisherName { get; set; }

        [MaxLength(300, ErrorMessage = "Institution name cannot exceed 300 characters")]
        public string? PublisherInstitutionName { get; set; }

        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string? PublisherAddress { get; set; }
    }

    // ==================== EXTENSION LITERATURE DTOs ====================

    /// <summary>
    /// DTO for returning extension literature (Read operations)
    /// </summary>
    public class ExtensionLiteratureDto
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public DateOnly? Date { get; set; }
        public string? AmountPerCopy { get; set; }
        public string? NumberOfCopies { get; set; }
        public string? TotalAmount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    /// <summary>
    /// DTO for creating/updating extension literature (Phase 3)
    /// </summary>
    public class ExtensionLiteratureCreateDto
    {
        public DateOnly? Date { get; set; }

        [MaxLength(50, ErrorMessage = "Amount per copy cannot exceed 50 characters")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid number")]
        public string? AmountPerCopy { get; set; }

        [MaxLength(50, ErrorMessage = "Number of copies cannot exceed 50 characters")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Number of copies must be a whole number")]
        public string? NumberOfCopies { get; set; }

        [MaxLength(50, ErrorMessage = "Total amount cannot exceed 50 characters")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Total amount must be a valid number")]
        public string? TotalAmount { get; set; }
    }
}