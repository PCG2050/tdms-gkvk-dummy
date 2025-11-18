using Domain.Entities.Junction;
using Domain.Entities.MasterData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.GenericTables
{
    public class Publication : AuditableBaseEntity
    {
        //Unitlocaiton and Organization References
        [Required]
        public int UnitLocationId { get; set; }
       
        public int OrganizationId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        [JsonIgnore]
        public Organization Organization { get; set; } = null!;

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }



        // Master Data References
        public int? CategoryId { get; set; }
       
        public PublicationCategory? Category { get; set; }

        [MaxLength(200)]
        public string? OtherPublication { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        public int? ModeId { get; set; }
       
        public Mode? Mode { get; set; }

        [MaxLength(200)]
        public string? ModePublication { get; set; }

        public int? RegionId { get; set; }
        [JsonIgnore]
        public Region? Region { get; set; }

        // Publication Details
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

        public int? PublicationPagesFrom { get; set; }
        public int? PublicationPagesTo { get; set; }

        [MaxLength(50)]
        public string? PublicationISBN { get; set; }

        public int? PublicationUniNumber { get; set; }
        public int? PublicationNAAS { get; set; }
        public int? PublicationImpact { get; set; }

        [Url]
        [MaxLength(1000)]
        public string? PublicationWebLink { get; set; }

        // File attachments
        [MaxLength(1000)]
        public string? PublicationCover { get; set; }

        [MaxLength(1000)]
        public string? PublicationWhole { get; set; }

        // Source and Funding
        public int? SourceId { get; set; }
        [JsonIgnore]
        public ParticipatedSource? Source { get; set; }

        public int? Funds { get; set; }

        [MaxLength(1000)]
        public string? SponsorDetails { get; set; }

        public DateOnly? PermissionLetterDate { get; set; }

        [MaxLength(1000)]
        public string? PermissionLetterUrl { get; set; }

        public DateOnly? PublicationDate { get; set; }

        // ===== STATUS TRACKING FIELDS =====
        // Status: "Draft", "Pending", "Approved", "Rejected"
        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        // Approval tracking
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        [JsonIgnore]
        public User? ApprovedBy { get; set; }
        // ==================================

        // Navigation Properties - Related Entities
        public ICollection<ExtensionLiterature> ExtensionLiteratures { get; set; } = new List<ExtensionLiterature>();
        public PublisherDetails? PublisherDetails { get; set; }

        // Many-to-many relationships for Newspapers and Magazines
        public ICollection<PublicationKannadaNewsPaper> PublicationKannadaNewsPapers { get; set; } = new List<PublicationKannadaNewsPaper>();
        public ICollection<PublicationEnglishNewsPaper> PublicationEnglishNewsPapers { get; set; } = new List<PublicationEnglishNewsPaper>();
        public ICollection<PublicationKannadaMagazine> PublicationKannadaMagazines { get; set; } = new List<PublicationKannadaMagazine>();
        public ICollection<PublicationEnglishMagazine> PublicationEnglishMagazines { get; set; } = new List<PublicationEnglishMagazine>();
    }

    public class PublisherDetails : AuditableBaseEntity
    {
        public int PublicationId { get; set; }

        [JsonIgnore]
        public Publication? Publication { get; set; }

        [MaxLength(1000)]
        public string? PublisherBrochure { get; set; }

        [MaxLength(200)]
        public string? PublisherName { get; set; }

        [MaxLength(300)]
        public string? PublisherInstitutionName { get; set; }

        [MaxLength(500)]
        public string? PublisherAddress { get; set; }
    }
    public class ExtensionLiterature : AuditableBaseEntity
    {
        public int PublicationId { get; set; }

        [JsonIgnore]
        public Publication? Publication { get; set; }

        public DateOnly? Date { get; set; }

        [MaxLength(50)]
        public string? AmountPerCopy { get; set; }

        [MaxLength(50)]
        public string? NumberOfCopies { get; set; }

        [MaxLength(50)]
        public string? TotalAmount { get; set; }
    }
}