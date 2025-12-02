using Domain.Entities.MasterData;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.GenericProgram
{
    /// <summary>
    /// Generic base entity for all unit program details (FTI, STU, ATIC, DEU, EEU, NAEP, KVK)
    /// Contains common fields shared across all programs
    /// </summary>
    public abstract class ProgramDetailsBase<TContent, TDemographics, TAdvisory, TReport, TRecommendation> : ReportEntryBaseEntity
        where TContent : class
        where TDemographics : class
        where TAdvisory : class
        where TReport : class
        where TRecommendation : class
    {
        // ============================
        // COMMON FOREIGN KEYS
        // ============================

        public int? ProgramTypeId { get; set; }
        [JsonIgnore]
        public ProgramType? ProgramType { get; set; }

        public int? CategoryId { get; set; }
        [JsonIgnore]
        public ProgramCategory? Category { get; set; }

        [MaxLength(200)]
        public string? CategoryOther { get; set; }

        public int? TypeId { get; set; }
        [JsonIgnore]
        public InfoType? Type { get; set; }

        [MaxLength(200)]
        public string? TypeOther { get; set; }

        public int? ThemeId { get; set; }
        [JsonIgnore]
        public Theme? Theme { get; set; }

        [MaxLength(200)]
        public string? ThemeOther { get; set; }

        public int? ThematicAreaId { get; set; }
        [JsonIgnore]
        public ThematicArea? ThematicArea { get; set; }

        [MaxLength(200)]
        public string? ThematicAreaOther { get; set; }

        public int? SponsoredOrganization { get; set; }

        [MaxLength(200)]
        public string? SponsoredOrganizationName { get; set; }

        // ============================
        // PROGRAM DETAILS
        // ============================

        [MaxLength(250)]
        public string? Title { get; set; }

        public int? ModeId { get; set; }
        [JsonIgnore]
        public Mode? Mode { get; set; }

        [MaxLength(100)]
        public string? Duration { get; set; }

        public int? RegionId { get; set; }
        [JsonIgnore]
        public Region? Region { get; set; }

        [MaxLength(200)]
        public string? RegionOther { get; set; }

        public int? TPNo { get; set; }

        [MaxLength(250)]
        public string? Location { get; set; }

        // ============================
        // FUNDING
        // ============================

        public int? SourceOfFundId { get; set; }
        [JsonIgnore]
        public SourceOfFund? SourceOfFund { get; set; }

        public int? Funds { get; set; }

        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }

        public decimal? TotalOutlayRs { get; set; }

        // ============================
        // PI/CO-PI DETAILS
        // ============================

        public string? Copi { get; set; }

        [MaxLength(500)]
        public string? PiAddress { get; set; }

        public int? BatchNo { get; set; }

        public decimal? Area { get; set; }

        // ============================
        // ORGANIZER DETAILS
        // ============================

        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }

        // ============================
        // SOURCE OF INFORMATION
        // ============================

        public int? SourceId { get; set; }
        [JsonIgnore]
        public ParticipatedSource? Source { get; set; }

        [MaxLength(500)]
        public string? OtherSourceOfInformation { get; set; }

        [MaxLength(500)]
        public string? SourceOfTitle { get; set; }

        // ============================
        // PROPOSAL & SANCTION DETAILS
        // ============================

        public DateOnly? ProposalDate { get; set; }
        [MaxLength(500)]
        public string? ProposalUploadFile { get; set; }

        public DateOnly? UniversitySanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? UniversitySanctionLetterUploadFile { get; set; }

        public DateOnly? ProjectSanctionDate { get; set; }
        [MaxLength(500)]
        public string? ProjectSanctionFile { get; set; }

        public DateOnly? UniImplDate { get; set; }
        [MaxLength(500)]
        public string? UniImplLetterFile { get; set; }

        // ============================
        // FUND RELEASE DETAILS
        // ============================

        [MaxLength(100)]
        public string? FundReleaseYear { get; set; }

        public double? FundAmount { get; set; }

        public DateOnly? FundReleaseDate { get; set; }
        [MaxLength(500)]
        public string? FundReleaseFile { get; set; }

        public DateOnly? FundsSanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? FundsSanctionLetterUploadFile { get; set; }

        // ============================
        // REPORTING
        // ============================

        [MaxLength(500)]
        public string? ReportingVideo { get; set; }

        // ============================
        // NAVIGATION PROPERTIES (Generic)
        // ============================

        public ICollection<TDemographics>? ParticipantDemographics { get; set; }
        public ICollection<TContent>? ProgramContent { get; set; }
        public TAdvisory? AdvisoryServices { get; set; }
        public TRecommendation? Recommendations { get; set; }
        public TReport? Reports { get; set; }
    }
}
