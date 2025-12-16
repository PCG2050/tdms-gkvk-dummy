using System;
using System.Collections.Generic;

namespace Application.Models.DynamicReporting
{
    /// <summary>
    /// Unified DTO for program details across all units
    /// </summary>
    public class UnifiedProgramDetailsDto
    {
        // ===== UNIT IDENTIFICATION =====
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int Id { get; set; }

        // ===== COMMON FIELDS FROM ReportEntryBaseEntity =====
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; } = string.Empty;
        public string FormStatus { get; set; } = string.Empty;
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public string? ApprovedByName { get; set; }
        public string? Attachements { get; set; }

        // ===== COMMON PROGRAM FIELDS =====
        public string? Title { get; set; }
        public string? ProgramType { get; set; }
        public string? Category { get; set; }
        public string? CategoryOther { get; set; }
        public string? Type { get; set; }
        public string? TypeOther { get; set; }
        public string? Theme { get; set; }
        public string? ThemeOther { get; set; }
        public string? ThematicArea { get; set; }
        public string? ThematicAreaOther { get; set; }
        public string? Mode { get; set; }
        public string? Duration { get; set; }
        public string? Location { get; set; }
        public string? Region { get; set; }
        public string? RegionOther { get; set; }
        public string? Status { get; set; }
        public int? TPNo { get; set; }
        public int? BatchNo { get; set; }
        public decimal? Area { get; set; }

        // ===== FUNDING DETAILS =====
        public string? SourceOfFund { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public int? Funds { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseYear { get; set; }

        // ===== SPONSORED INFORMATION =====
        public string? SponsoredOrganizationName { get; set; }

        // ===== ORGANIZER INFORMATION =====
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }

        // ===== PROPOSAL & SANCTION INFORMATION =====
        public DateOnly? ProposalDate { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }

        // ===== PI INFORMATION =====
        public string? Copi { get; set; }
        public string? PiAddress { get; set; }

        // ===== AUDIT FIELDS =====
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedByName { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedByName { get; set; }

        // ===== UNIT-SPECIFIC FIELDS =====
        /// <summary>
        /// Additional fields specific to certain units (e.g., T01-T05 for KVK/EEU)
        /// </summary>
        public Dictionary<string, object>? AdditionalFields { get; set; }

        // ===== AGGREGATED CHILD DATA =====
        public int? TotalParticipants { get; set; }
        public int? MaleParticipants { get; set; }
        public int? FemaleParticipants { get; set; }
        public int? ResourcePersonCount { get; set; }
        public int? TopicsCoveredCount { get; set; }
    }

    /// <summary>
    /// Unified DTO for participant demographics across all units
    /// </summary>
    public class UnifiedParticipantDemographicsDto
    {
        // ===== UNIT & PROGRAM IDENTIFICATION =====
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string? ProgramTitle { get; set; }
        public DateOnly ProgramStartDate { get; set; }
        public DateOnly ProgramEndDate { get; set; }

        // ===== PARTICIPANT CATEGORY =====
        public string? ParticipantCategory { get; set; }

        // ===== MALE DEMOGRAPHICS =====
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }

        // ===== MALE HOSTEL DATA =====
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }

        // ===== FEMALE DEMOGRAPHICS =====
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }

        // ===== FEMALE HOSTEL DATA =====
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }

        // ===== COMPUTED TOTALS =====
        public int TotalMale => Male_SC + Male_ST + Male_OBC + Male_GEN;
        public int TotalFemale => Female_SC + Female_ST + Female_OBC + Female_GEN;
        public int Total => TotalMale + TotalFemale;
        public int TotalSC => Male_SC + Female_SC;
        public int TotalST => Male_ST + Female_ST;
        public int TotalOBC => Male_OBC + Female_OBC;
        public int TotalGEN => Male_GEN + Female_GEN;

        // ===== LOCATION & ORGANIZATION =====
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }

    /// <summary>
    /// Unified DTO for advisory services across all units
    /// </summary>
    public class UnifiedAdvisoryServicesDto
    {
        // ===== UNIT & PROGRAM IDENTIFICATION =====
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int Id { get; set; }
        public int ProgramId { get; set; }
        public string? ProgramTitle { get; set; }
        public DateOnly ProgramStartDate { get; set; }
        public DateOnly ProgramEndDate { get; set; }

        // ===== ADVISORY METRICS =====
        public int NoOfFacebookSMS { get; set; }
        public int NoOfSMSSentToRegisteredFarmers { get; set; }
        public int NoOfWhatsappGroups { get; set; }
        public int NoOfWhatsappSMS { get; set; }
        public int NoOfAnsweredWhatsappQueries { get; set; }
        public int NoOfPhoneCalls { get; set; }
        public int NoOfFaceToFaceDiscussions { get; set; }
        public int NoOfGroupDiscussions { get; set; }
        public int NoOfEmailsSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }

        // ===== COMPUTED TOTALS =====
        public int TotalContacts =>
            NoOfFacebookSMS +
            NoOfSMSSentToRegisteredFarmers +
            NoOfWhatsappSMS +
            NoOfPhoneCalls +
            NoOfFaceToFaceDiscussions +
            NoOfEmailsSent;

        // ===== LOCATION & ORGANIZATION =====
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }

    /// <summary>
    /// DTO for aggregated report results
    /// </summary>
    public class AggregatedReportDto
    {
        /// <summary>
        /// Value(s) of the group-by field(s)
        /// </summary>
        public required string GroupByValue { get; set; }

        /// <summary>
        /// Number of records in this group
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// Calculated metrics for this group
        /// Key = metric name (e.g., "totalParticipants", "avgDuration")
        /// Value = calculated value
        /// </summary>
        public required Dictionary<string, object> Metrics { get; set; }

        /// <summary>
        /// Additional group-by dimension values
        /// </summary>
        public Dictionary<string, object>? Dimensions { get; set; }
    }

    /// <summary>
    /// DTO for training programmes (FTI, STU, IBTVA, EEU)
    /// </summary>
    public class UnifiedTrainingProgrammeDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int Id { get; set; }

        // Common fields
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string TrainingTitle { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }

        // FTI-specific
        public string? OrganisationName { get; set; }

        // Status tracking
        public string FormStatus { get; set; } = string.Empty;
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }

    /// <summary>
    /// DTO for ASM visitor details
    /// </summary>
    public class UnifiedVisitorDetailsDto
    {
        public int Id { get; set; }
        public string? InstituteName { get; set; }
        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }
        public int TotalVisitors => FarmersCount + StudentsCount + PublicCount;
        public DateOnly SubmittedDate { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // Status tracking
        public string FormStatus { get; set; } = string.Empty;
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }

    /// <summary>
    /// DTO for ATIC sales
    /// </summary>
    public class UnifiedSalesDto
    {
        public int Id { get; set; }
        public string Details { get; set; } = string.Empty;
        public string QuantityType { get; set; } = string.Empty;
        public double Quantity { get; set; }

        // Common fields
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string FormStatus { get; set; } = string.Empty;
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }
    }

    /// <summary>
    /// DTO for FIU program activities
    /// </summary>
    public class UnifiedProgramActivityDto
    {
        public int Id { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public string ActivityCategory { get; set; } = string.Empty;
        public int Number { get; set; }
        public string? UploadMediaUrl { get; set; }
        public string? Remarks { get; set; }

        // Status tracking
        public string FormStatus { get; set; } = string.Empty;
        public DateTimeOffset? SubmittedAt { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public int OrganizationId { get; set; }
        public string? OrganizationName { get; set; }

        // Audit
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedByName { get; set; }
    }
}
