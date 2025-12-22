using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.ComprehensiveReports
{
    /// <summary>
    /// Request for generating comprehensive multi-step reports
    /// </summary>
    public class ComprehensiveReportRequest
    {
        /// <summary>
        /// Single unit location ID OR list for multi-unit reports
        /// Use this OR UnitId, not both
        /// </summary>
        public List<int>? UnitLocationIds { get; set; }

        /// <summary>
        /// Unit ID to get ALL unit locations for that unit type
        /// Use this OR UnitLocationIds, not both
        /// </summary>
        public int? UnitId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Report type: "program", "publication", "awards", "consultancy", "services", "financial"
        /// </summary>
        [Required]
        public string ReportType { get; set; } = string.Empty;

        /// <summary>
        /// Optional: Specific report entry ID (for single entry reports)
        /// If not provided, returns all entries for the month/year
        /// </summary>
        public int? ReportEntryId { get; set; }

        /// <summary>
        /// Optional: Filter by form status (default: "Approved")
        /// </summary>
        public string? FormStatus { get; set; } = "Approved";

        /// <summary>
        /// Whether to include sub-section data (demographics, resource persons, etc.)
        /// </summary>
        public bool IncludeSubSections { get; set; } = true;
    }

    /// <summary>
    /// Simplified request for generating reports for ALL locations of a unit type
    /// </summary>
    public class AllUnitReportRequest
    {
        /// <summary>
        /// Unit ID (e.g., 10 for KVK, 9 for EEU) - generates report for ALL locations of this unit type
        /// </summary>
        [Required]
        public int UnitId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        /// <summary>
        /// Report type: "program", "publication", "awards", "consultancy", "services", "financial"
        /// </summary>
        [Required]
        public string ReportType { get; set; } = string.Empty;

        /// <summary>
        /// Optional: Filter by form status (default: "Approved")
        /// </summary>
        public string? FormStatus { get; set; } = "Approved";

        /// <summary>
        /// Whether to include sub-section data (demographics, resource persons, etc.)
        /// </summary>
        public bool IncludeSubSections { get; set; } = true;
    }

    /// <summary>
    /// Comprehensive report response with structured data for all sections
    /// </summary>
    public class ComprehensiveReportResponse
    {
        // Metadata
        public ReportMetadata Metadata { get; set; } = new();

        // Program Report Sections (null if not requested)
        public List<ProgramOrganizedData>? Programs { get; set; }
        public ParticipantDemographicsData? ParticipantDemographics { get; set; }
        public ProgramContentData? ProgramContent { get; set; }
        public AdvisoryServicesData? AdvisoryServices { get; set; }
        public ResultsData? Results { get; set; }
        public ReportSectionData? Report { get; set; }
        public RecommendationData? Recommendations { get; set; }

        // Publication Report Sections (null if not requested)
        public List<PublicationData>? Publications { get; set; }
        public List<AuthorData>? Authors { get; set; }
        public ExtensionLiteratureData? ExtensionLiterature { get; set; }

        // Awards Report (null if not requested)
        public AwardsData? Awards { get; set; }

        // Consultancy Report (null if not requested)
        public List<ConsultancyData>? Consultancies { get; set; }

        // Services/Facilities Report (null if not requested)
        public ServicesFacilitiesData? ServicesFacilities { get; set; }

        // Financial Status Report (null if not requested)
        public FinancialStatusData? FinancialStatus { get; set; }

        // FIU Activities (null if not FIU unit)
        public FIUActivitiesData? FIUActivities { get; set; }

        // ASM Activities (null if not ASM unit)
        public ASMActivitiesData? ASMActivities { get; set; }
    }

    // ========================================
    // METADATA
    // ========================================

    public class ReportMetadata
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public List<UnitLocationInfo> UnitLocations { get; set; } = new();
        public string District { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public DateTimeOffset GeneratedAt { get; set; }
        public int TotalEntries { get; set; }
        public bool IsMultiUnitReport { get; set; }
    }

    public class UnitLocationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }

    // ========================================
    // PROGRAM REPORT SECTIONS (7 Steppers)
    // ========================================

    /// <summary>
    /// Step 1: Program Organized
    /// </summary>
    public class ProgramOrganizedData
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ProgramType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string ThematicArea { get; set; } = string.Empty;
        public string? SponsoredOrganization { get; set; }
        public string? Collaborator { get; set; }
        public bool CollaborativeProgram { get; set; }
        public string Mode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Location { get; set; } = string.Empty;
        public string? TpNo { get; set; }
        public string SourceOfFund { get; set; } = string.Empty;
        public decimal TotalOutlay { get; set; }
        public string FormStatus { get; set; } = string.Empty;
        public int TotalParticipants { get; set; }

        // Nested data for single-program reports
        public List<ParticipantCategory>? Demographics { get; set; }
        public ProgramContentData? Content { get; set; }
        public AdvisoryServicesData? AdvisoryServices { get; set; }
        public ResultsData? Results { get; set; }
        public ReportSectionData? ReportSection { get; set; }
        public RecommendationData? Recommendations { get; set; }
    }

    /// <summary>
    /// Step 2: Participant Demographics
    /// </summary>
    public class ParticipantDemographicsData
    {
        public List<ParticipantCategory> Participants { get; set; } = new();
        public ParticipantTotals Totals { get; set; } = new();
    }

    public class ParticipantCategory
    {
        public string CategoryName { get; set; } = string.Empty;
        public int MaleSC { get; set; }
        public int FemaleSC { get; set; }
        public int MaleST { get; set; }
        public int FemaleST { get; set; }
        public int MaleOBC { get; set; }
        public int FemaleOBC { get; set; }
        public int MaleGEN { get; set; }
        public int FemaleGEN { get; set; }
        public int MaleTotal { get; set; }
        public int FemaleTotal { get; set; }
        public int GrandTotal { get; set; }
        public int StayedInHostel { get; set; }
    }

    public class ParticipantTotals
    {
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public int GrandTotal { get; set; }
        public int TotalHostel { get; set; }
    }

    /// <summary>
    /// Step 3: Program Content and Resources
    /// </summary>
    public class ProgramContentData
    {
        public List<ResourcePersonData> ResourcePersons { get; set; } = new();
        public List<TopicCoveredData> TopicsCovered { get; set; } = new();
        public List<TeachingAidData> TeachingAids { get; set; } = new();
    }

    public class ResourcePersonData
    {
        public string Name { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string ResourcePersonType { get; set; } = string.Empty; // Internal/External
        public string Institution { get; set; } = string.Empty;
    }

    public class TopicCoveredData
    {
        public DateOnly? Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }

    public class TeachingAidData
    {
        public string TypeOfAidUsed { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public int? Quantity { get; set; }
    }

    /// <summary>
    /// Step 4: Advisory Services
    /// </summary>
    public class AdvisoryServicesData
    {
        // Main metrics
        public int NoOfFacebookSMS { get; set; }
        public int NoOfWhatsAppSMS { get; set; }
        public int NoOfWhatsAppQueries { get; set; }
        public int NoOfPhoneCalls { get; set; }
        public int NoOfFaceToFaceDiscussions { get; set; }
        public int NoOfEmailsSent { get; set; }
        public int NoOfBeneficiaries { get; set; }

        // Sub-table: Critical Inputs Distributed
        public List<CriticalInputData> CriticalInputsDistributed { get; set; } = new();
    }

    public class CriticalInputData
    {
        public string Component { get; set; } = string.Empty;
        public int Number { get; set; }
        public string? Unit { get; set; }
    }

    /// <summary>
    /// Step 5: Results (KVK/EEU only)
    /// </summary>
    public class ResultsData
    {
        public List<FLDResultData> FLDResults { get; set; } = new();
        public List<OFTResultData> OFTResults { get; set; } = new();
    }

    public class FLDResultData
    {
        public string DetailsOfDemonstration { get; set; } = string.Empty;
        public string Crop { get; set; } = string.Empty;
        public string Variety { get; set; } = string.Empty;
        public decimal Area { get; set; }
        public int FarmersInvolved { get; set; }
        public decimal Yield { get; set; }
        public string? Remarks { get; set; }
    }

    public class OFTResultData
    {
        public string DetailsOfTechnology { get; set; } = string.Empty;
        public int NumberOfTrials { get; set; }
        public decimal Yield { get; set; }
        public string? Remarks { get; set; }
    }

    /// <summary>
    /// Step 6: Report
    /// </summary>
    public class ReportSectionData
    {
        public DateOnly ReportingDate { get; set; }
        public string SignificantOutcome { get; set; } = string.Empty;
        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }
        public string? ProgressReport { get; set; }
    }

    /// <summary>
    /// Step 7: Recommendations
    /// </summary>
    public class RecommendationData
    {
        public string? ProblemsIdentified { get; set; }
        public string? Recommendations { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? Outcome { get; set; }
        public string? VideoUrl { get; set; }
    }

    // ========================================
    // PUBLICATION REPORT SECTIONS (3 Steppers)
    // ========================================

    public class PublicationData
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string? JournalName { get; set; }
        public string? ISSN { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public int? TotalPages { get; set; }
        public string? WebLink { get; set; }
        public string FormStatus { get; set; } = string.Empty;

        // Nested authors for single publication view
        public List<AuthorData>? Authors { get; set; }
        public List<ExtensionLiteratureItem>? ExtensionLiterature { get; set; }
    }

    public class AuthorData
    {
        public int PublicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsPrimaryAuthor { get; set; }
        public int Order { get; set; }
    }

    public class ExtensionLiteratureData
    {
        public List<ExtensionLiteratureItem> Items { get; set; } = new();
        public decimal TotalAmountGenerated { get; set; }
        public int TotalCopiesSold { get; set; }
    }

    public class ExtensionLiteratureItem
    {
        public int PublicationId { get; set; }
        public string PublicationTitle { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }
        public decimal AmountPerCopy { get; set; }
        public int CopiesSoldOrDistributed { get; set; }
        public decimal AmountGenerated { get; set; } // Calculated
    }

    // ========================================
    // AWARDS REPORT
    // ========================================

    public class AwardsData
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string FormStatus { get; set; } = string.Empty;
        public List<AchievementData> Achievements { get; set; } = new();
        public List<AwardReceivedData> AwardsReceived { get; set; } = new();
    }

    public class AchievementData
    {
        public string Name { get; set; } = string.Empty;
        public string Achievement { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }
    }

    public class AwardReceivedData
    {
        public string AwardingAgency { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Contribution { get; set; } = string.Empty;
        public DateOnly? AwardDate { get; set; }
    }

    // ========================================
    // CONSULTANCY REPORT
    // ========================================

    public class ConsultancyData
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string RelatedToDiscipline { get; set; } = string.Empty;
        public string Beneficiaries { get; set; } = string.Empty;
        public int TotalNoOfBeneficiaries { get; set; }
        public decimal? AmountGenerated { get; set; }
        public DateOnly? Date { get; set; }
        public string FormStatus { get; set; } = string.Empty;

        // Demographics breakdown
        public int MaleSC { get; set; }
        public int FemaleSC { get; set; }
        public int MaleST { get; set; }
        public int FemaleST { get; set; }
        public int MaleOBC { get; set; }
        public int FemaleOBC { get; set; }
        public int MaleGEN { get; set; }
        public int FemaleGEN { get; set; }
    }

    // ========================================
    // SERVICES/FACILITIES REPORT
    // ========================================

    public class ServicesFacilitiesData
    {
        public List<ServiceItem> Services { get; set; } = new();
        public VisitorsData Visitors { get; set; } = new();
        public List<AccommodationData> Accommodation { get; set; } = new();
        public decimal TotalAmountGenerated { get; set; }
    }

    public class ServiceItem
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string SourceOfFunds { get; set; } = string.Empty;
        public string Components { get; set; } = string.Empty;
        public string Units { get; set; } = string.Empty;
        public int Number { get; set; }
        public decimal AmountGenerated { get; set; }
        public string FormStatus { get; set; } = string.Empty;
    }

    public class VisitorsData
    {
        public int MaleSC { get; set; }
        public int FemaleSC { get; set; }
        public int MaleST { get; set; }
        public int FemaleST { get; set; }
        public int MaleOBC { get; set; }
        public int FemaleOBC { get; set; }
        public int MaleGEN { get; set; }
        public int FemaleGEN { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
    }

    public class AccommodationData
    {
        public int Id { get; set; }
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public int MaleSC { get; set; }
        public int FemaleSC { get; set; }
        public int MaleST { get; set; }
        public int FemaleST { get; set; }
        public int MaleOBC { get; set; }
        public int FemaleOBC { get; set; }
        public int MaleGEN { get; set; }
        public int FemaleGEN { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
        public int NoOfDays { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Village { get; set; }
        public string? Taluk { get; set; }
    }

    // ========================================
    // FINANCIAL STATUS REPORT
    // ========================================

    public class FinancialStatusData
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string FormStatus { get; set; } = string.Empty;

        public List<BudgetData> Budgets { get; set; } = new();
        public List<RevolvingFundData> RevolvingFunds { get; set; } = new();
        public List<BankAccountData> BankAccounts { get; set; } = new();

        // Summary totals
        public decimal TotalSanctioned { get; set; }
        public decimal TotalReleased { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal TotalBalance { get; set; }
    }

    public class BudgetData
    {
        public int Id { get; set; }
        public string Particulars { get; set; } = string.Empty;
        public string? ABAC { get; set; }
        public string? DAC { get; set; }
        public decimal Sanctioned { get; set; }
        public decimal Released { get; set; }
        public decimal Expenditure { get; set; }
        public decimal Balance { get; set; } // Calculated: released - expenditure
        public decimal? Percentage { get; set; }
    }

    public class RevolvingFundData
    {
        public int Id { get; set; }
        public string? YearMonth { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Receipts { get; set; }
        public decimal Expenditure { get; set; }
        public decimal ClosingBalance { get; set; } // Calculated
    }

    public class BankAccountData
    {
        public int Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string LocationBranch { get; set; } = string.Empty;
        public string? BranchCode { get; set; }
        public string AccountName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string? MICRNumber { get; set; }
        public string IFSCCode { get; set; } = string.Empty;
    }

    // ========================================
    // FIU ACTIVITIES (Special for FIU Unit)
    // ========================================

    public class FIUActivitiesData
    {
        public List<FIUActivityItem> Activities { get; set; } = new();
        public int TotalActivities { get; set; }
        public int TotalCount { get; set; }
    }

    public class FIUActivityItem
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public int Count { get; set; }
        public DateOnly? Date { get; set; }
        public string? Description { get; set; }
    }

    // ========================================
    // ASM ACTIVITIES (Special for ASM Unit)
    // ========================================

    public class ASMActivitiesData
    {
        public List<ASMVisitorItem> Visitors { get; set; } = new();
        public int TotalVisitors { get; set; }
        public int TotalEntries { get; set; }

        // Category breakdown
        public int TotalFarmers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalPublic { get; set; }
    }

    public class ASMVisitorItem
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public string Particulars { get; set; } = string.Empty;
        public int NoOfVisitors { get; set; }
        public DateOnly? Date { get; set; }
        public string? Category { get; set; }
    }
}
