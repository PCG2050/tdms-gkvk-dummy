# Comprehensive Multi-Step Report System - Implementation Plan

## 📋 Executive Summary

This document provides a **practical, step-by-step implementation plan** for extending the existing dynamic report system to support comprehensive multi-step reports with complex layouts and merged columns.

### Current State
- ✅ Existing `DynamicReportService` returns simple table data (rows + columns)
- ✅ AdminReportController exposes `/api/admin/reports/dynamic/*` endpoints
- ✅ ReportColumnRegistry provides section configurations
- ✅ Angular frontend handles PDF generation with jsPDF

### Target State
- ✅ Support **6 report types** with multi-step wizards
- ✅ Complex data structures with **sub-sections** (Resource Persons, Topics, etc.)
- ✅ Support for **merged column headers** in PDF
- ✅ Template-based approach for reusability

---

## 🎯 Report Types Overview

### 1. **Program Reports** (7 Steppers)
   - Step 1: Program Organized
   - Step 2: Participant Demographics (with merged columns)
   - Step 3: Program Content (3 sub-sections: Resource Persons, Topics, Teaching Aids)
   - Step 4: Advisory Services (with Critical Inputs sub-table)
   - Step 5: Results (FLD & OFT for KVK/EEU only)
   - Step 6: Report
   - Step 7: Recommendations

### 2. **Publication Reports** (3 Steppers)
   - Step 1: Publications
   - Step 2: Authors
   - Step 3: Extension Literature

### 3. **Awards Report** (Single form with sub-sections)
   - Main: Type, Region
   - Sub-section 1: Achievements
   - Sub-section 2: Awards Received

### 4. **Consultancy/Social Media Services**
   - Single comprehensive form

### 5. **Services/Facilities** (with merged columns)
   - Main service details
   - Sub-section 1: Visitors (merged gender columns)
   - Sub-section 2: Accommodation

### 6. **Financial Status**
   - Budget table
   - Revolving Fund table

---

## 🏗️ Architecture: Hybrid Approach

### Backend Responsibilities
```
✅ Provide STRUCTURED data (not just flat rows)
✅ Query and join related tables (demographics, resource persons, topics, etc.)
✅ Calculate totals and aggregates
✅ Validate data completeness
✅ Apply role-based filtering
```

### Frontend Responsibilities
```
✅ Multi-step wizard UI with stepper
✅ Complex table layouts with merged columns
✅ PDF generation using jsPDF autoTable
✅ Step navigation and validation
✅ Preview before generate
```

---

## 📦 Backend Models (New)

### Location: `Application/Models/ComprehensiveReports/`

#### Request Model
```csharp
namespace Application.Models.ComprehensiveReports
{
    /// <summary>
    /// Request for generating comprehensive multi-step reports
    /// </summary>
    public class ComprehensiveReportRequest
    {
        [Required]
        public int UnitLocationId { get; set; }

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
        /// Optional: Filter by form status
        /// </summary>
        public string? FormStatus { get; set; } = "Approved";
    }
}
```

#### Response Models

```csharp
namespace Application.Models.ComprehensiveReports
{
    /// <summary>
    /// Comprehensive report response with structured data for all sections
    /// </summary>
    public class ComprehensiveReportResponse
    {
        // Metadata
        public ReportMetadata Metadata { get; set; } = new();

        // Program Report Sections (null if not requested)
        public ProgramOrganizedData? ProgramOrganized { get; set; }
        public ParticipantDemographicsData? ParticipantDemographics { get; set; }
        public ProgramContentData? ProgramContent { get; set; }
        public AdvisoryServicesData? AdvisoryServices { get; set; }
        public ResultsData? Results { get; set; }
        public ReportData? Report { get; set; }
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
    }

    // ========================================
    // METADATA
    // ========================================

    public class ReportMetadata
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public int UnitLocationId { get; set; }
        public string UnitLocationName { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty;
        public DateTimeOffset GeneratedAt { get; set; }
        public int TotalEntries { get; set; }
    }

    // ========================================
    // PROGRAM REPORT SECTIONS
    // ========================================

    public class ProgramOrganizedData
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ProgramType { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
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
    }

    public class ParticipantDemographicsData
    {
        public List<ParticipantCategory> Participants { get; set; } = new();
        public ParticipantTotals Totals { get; set; } = new();
    }

    public class ParticipantCategory
    {
        public string CategoryName { get; set; } = string.Empty;
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
    }

    public class TopicCoveredData
    {
        public DateOnly Date { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public class TeachingAidData
    {
        public string TypeOfAidUsed { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
    }

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
    }

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
    }

    public class OFTResultData
    {
        public string DetailsOfTechnology { get; set; } = string.Empty;
        public int NumberOfTrials { get; set; }
        public decimal Yield { get; set; }
        public string? Remarks { get; set; }
    }

    public class ReportData
    {
        public DateOnly ReportingDate { get; set; }
        public string SignificantOutcome { get; set; } = string.Empty;
        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }
    }

    public class RecommendationData
    {
        public string? ProblemsIdentified { get; set; }
        public string? Recommendations { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? Outcome { get; set; }
    }

    // ========================================
    // PUBLICATION REPORT SECTIONS
    // ========================================

    public class PublicationData
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string? JournalName { get; set; }
        public string? ISSN { get; set; }
        public DateOnly? PublicationDate { get; set; }
        public int? TotalPages { get; set; }
    }

    public class AuthorData
    {
        public int PublicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsPrimaryAuthor { get; set; }
    }

    public class ExtensionLiteratureData
    {
        public List<ExtensionLiteratureItem> Items { get; set; } = new();
        public decimal TotalAmountGenerated { get; set; }
        public int TotalCopiesSold { get; set; }
    }

    public class ExtensionLiteratureItem
    {
        public string PublicationTitle { get; set; } = string.Empty;
        public decimal AmountPerCopy { get; set; }
        public int CopiesSoldOrDistributed { get; set; }
        public decimal AmountGenerated { get; set; } // Calculated
    }

    // ========================================
    // AWARDS REPORT
    // ========================================

    public class AwardsData
    {
        public string Type { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
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
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string RelatedToDiscipline { get; set; } = string.Empty;
        public string Beneficiaries { get; set; } = string.Empty;
        public int TotalNoOfBeneficiaries { get; set; }
        public decimal? AmountGenerated { get; set; }
        public DateOnly? Date { get; set; }
    }

    // ========================================
    // SERVICES/FACILITIES REPORT
    // ========================================

    public class ServicesFacilitiesData
    {
        public List<ServiceItem> Services { get; set; } = new();
        public VisitorsData Visitors { get; set; } = new();
        public List<AccommodationData> Accommodation { get; set; } = new();
    }

    public class ServiceItem
    {
        public string Category { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public string SourceOfFunds { get; set; } = string.Empty;
        public string Components { get; set; } = string.Empty;
        public string Units { get; set; } = string.Empty;
        public int Number { get; set; }
        public decimal AmountGenerated { get; set; }
    }

    public class VisitorsData
    {
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
    }

    public class AccommodationData
    {
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total { get; set; }
        public int NoOfDays { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }

    // ========================================
    // FINANCIAL STATUS REPORT
    // ========================================

    public class FinancialStatusData
    {
        public BudgetData Budget { get; set; } = new();
        public RevolvingFundData RevolvingFund { get; set; } = new();
        public List<BankAccountData> BankAccounts { get; set; } = new();
    }

    public class BudgetData
    {
        public decimal Sanctioned { get; set; }
        public decimal Released { get; set; }
        public decimal Expenditure { get; set; }
        public decimal Balance { get; set; } // Calculated: released - expenditure
    }

    public class RevolvingFundData
    {
        public decimal OpeningBalance { get; set; }
        public decimal Receipts { get; set; }
        public decimal Expenditure { get; set; }
        public decimal ClosingBalance { get; set; } // Calculated
    }

    public class BankAccountData
    {
        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public decimal Balance { get; set; }
    }
}
```

---

## 🔧 Backend Implementation

### Step 1: Create Service Interface

**File**: `Application/Interface/Services/Reports/IComprehensiveReportService.cs`

```csharp
using Application.Models.ComprehensiveReports;

namespace Application.Interface.Services.Reports
{
    public interface IComprehensiveReportService
    {
        /// <summary>
        /// Generate comprehensive multi-step report
        /// </summary>
        Task<ServiceResult<ComprehensiveReportResponse>> GenerateReportAsync(
            ComprehensiveReportRequest request);

        /// <summary>
        /// Get available report types for a unit
        /// </summary>
        Task<ServiceResult<List<string>>> GetAvailableReportTypesAsync(int unitId);
    }
}
```

### Step 2: Implement Service

**File**: `Infrastructure/Services/Reports/ComprehensiveReportService.cs`

```csharp
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services;
using Application.Interface.Services.Reports;
using Application.Models.ComprehensiveReports;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Infrastructure.Services.Reports
{
    public class ComprehensiveReportService : IComprehensiveReportService
    {
        // Inject all necessary repositories
        private readonly IFTIProgramDetailsRepository _ftiProgramRepository;
        private readonly ISTUProgramDetailsRepository _stuProgramRepository;
        // ... (inject repositories for all 11 units)

        private readonly IPublicationRepository _publicationRepository;
        private readonly IConsultingServiceRepository _consultancyRepository;
        private readonly ITableServiceRepository _serviceRepository;
        private readonly IFinancialBudgetRepository _financialRepository;
        private readonly INominationRewardRepository _nominationRepository;

        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ICurrentUserService _currentUserService;

        public ComprehensiveReportService(
            IFTIProgramDetailsRepository ftiProgramRepository,
            // ... other injections
            IOrganizationUnitRepository organizationUnitRepository,
            ICurrentUserService currentUserService)
        {
            _ftiProgramRepository = ftiProgramRepository;
            // ... assign other repositories
            _organizationUnitRepository = organizationUnitRepository;
            _currentUserService = currentUserService;
        }

        public async Task<ServiceResult<ComprehensiveReportResponse>> GenerateReportAsync(
            ComprehensiveReportRequest request)
        {
            try
            {
                // Get unit location details
                var unitLocation = await _organizationUnitRepository.GetByIdAsync(request.UnitLocationId);
                if (unitLocation == null)
                    return ServiceResult<ComprehensiveReportResponse>.Failure("Unit location not found");

                var response = new ComprehensiveReportResponse
                {
                    Metadata = new ReportMetadata
                    {
                        UnitId = unitLocation.UnitId,
                        UnitName = unitLocation.Unit?.Name ?? "Unknown",
                        UnitLocationId = request.UnitLocationId,
                        UnitLocationName = $"{unitLocation.District?.Name}, {unitLocation.District?.State?.Name}",
                        District = unitLocation.District?.Name ?? "",
                        State = unitLocation.District?.State?.Name ?? "",
                        Month = request.Month,
                        Year = request.Year,
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(request.Month),
                        ReportType = request.ReportType,
                        GeneratedAt = DateTimeOffset.UtcNow
                    }
                };

                // Build report based on type
                switch (request.ReportType.ToLower())
                {
                    case "program":
                        await BuildProgramReportAsync(response, request, unitLocation.UnitId);
                        break;

                    case "publication":
                        await BuildPublicationReportAsync(response, request);
                        break;

                    case "awards":
                        await BuildAwardsReportAsync(response, request);
                        break;

                    case "consultancy":
                        await BuildConsultancyReportAsync(response, request);
                        break;

                    case "services":
                        await BuildServicesReportAsync(response, request);
                        break;

                    case "financial":
                        await BuildFinancialReportAsync(response, request);
                        break;

                    default:
                        return ServiceResult<ComprehensiveReportResponse>.Failure(
                            $"Unknown report type: {request.ReportType}");
                }

                return ServiceResult<ComprehensiveReportResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<ComprehensiveReportResponse>.Failure(
                    $"Failed to generate report: {ex.Message}");
            }
        }

        public async Task<ServiceResult<List<string>>> GetAvailableReportTypesAsync(int unitId)
        {
            var reportTypes = new List<string>();

            // All units except FIU and ASM support program reports
            if (unitId != UnitConstants.FIU_UNIT_ID && unitId != UnitConstants.ASM_UNIT_ID)
            {
                reportTypes.Add("program");
            }

            // All units support these
            reportTypes.Add("publication");
            reportTypes.Add("awards");
            reportTypes.Add("consultancy");
            reportTypes.Add("services");
            reportTypes.Add("financial");

            return ServiceResult<List<string>>.Success(reportTypes);
        }

        // ========================================
        // PROGRAM REPORT BUILDER
        // ========================================

        private async Task BuildProgramReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request,
            int unitId)
        {
            // Get the appropriate repository based on unit
            var programDetails = await GetProgramDetailsByUnitAsync(
                unitId,
                request.UnitLocationId,
                request.Month,
                request.Year,
                request.FormStatus,
                request.ReportEntryId);

            if (programDetails == null)
                return;

            // Step 1: Program Organized
            response.ProgramOrganized = await BuildProgramOrganizedAsync(programDetails);

            // Step 2: Participant Demographics
            response.ParticipantDemographics = await BuildParticipantDemographicsAsync(
                programDetails, unitId);

            // Step 3: Program Content
            response.ProgramContent = await BuildProgramContentAsync(programDetails, unitId);

            // Step 4: Advisory Services
            response.AdvisoryServices = await BuildAdvisoryServicesAsync(programDetails, unitId);

            // Step 5: Results (KVK/EEU only)
            if (unitId == UnitConstants.KVK_UNIT_ID || unitId == UnitConstants.EEU_UNIT_ID)
            {
                response.Results = await BuildResultsAsync(programDetails, unitId);
            }

            // Step 6: Report
            response.Report = await BuildReportSectionAsync(programDetails, unitId);

            // Step 7: Recommendations
            response.Recommendations = await BuildRecommendationsAsync(programDetails, unitId);
        }

        private async Task<object?> GetProgramDetailsByUnitAsync(
            int unitId,
            int unitLocationId,
            int month,
            int year,
            string? formStatus,
            int? reportEntryId)
        {
            // Build query based on unit
            IQueryable<object> query = unitId switch
            {
                UnitConstants.FTI_UNIT_ID => _ftiProgramRepository.GetQueryable()
                    .Where(p => p.UnitLocationId == unitLocationId),

                // Add other units...

                _ => null
            };

            if (query == null)
                return null;

            // Apply filters (this is a simplified example - needs proper typing)
            // In reality, you'd cast to the specific type and use proper LINQ

            // If specific entry requested
            if (reportEntryId.HasValue)
            {
                // Get specific entry by ID
                // return await query.FirstOrDefaultAsync(p => p.Id == reportEntryId.Value);
            }

            // Otherwise filter by month/year and status
            // return await query
            //     .Where(p => p.StartDate.Month == month && p.StartDate.Year == year)
            //     .Where(p => p.FormStatus == formStatus)
            //     .Include(...)  // Include all related entities
            //     .FirstOrDefaultAsync();

            return null; // Placeholder
        }

        private async Task<ProgramOrganizedData> BuildProgramOrganizedAsync(object programDetails)
        {
            // Map from entity to DTO
            // This is unit-specific mapping
            return new ProgramOrganizedData();
        }

        private async Task<ParticipantDemographicsData> BuildParticipantDemographicsAsync(
            object programDetails, int unitId)
        {
            var data = new ParticipantDemographicsData();

            // Query participant demographics based on unit
            // Each unit has different demographic structure
            // Example for FTI:
            // var demographics = await _context.FTIParticipantDemographics
            //     .Where(d => d.ProgramDetailsId == programId)
            //     .ToListAsync();

            // Map to common structure with totals
            data.Participants = new List<ParticipantCategory>
            {
                // Map each category with male/female/total/hostel
            };

            // Calculate totals
            data.Totals = new ParticipantTotals
            {
                TotalMale = data.Participants.Sum(p => p.MaleTotal),
                TotalFemale = data.Participants.Sum(p => p.FemaleTotal),
                GrandTotal = data.Participants.Sum(p => p.GrandTotal),
                TotalHostel = data.Participants.Sum(p => p.StayedInHostel)
            };

            return data;
        }

        private async Task<ProgramContentData> BuildProgramContentAsync(
            object programDetails, int unitId)
        {
            var data = new ProgramContentData();

            // Query resource persons, topics, teaching aids from database
            // Map to DTOs

            return data;
        }

        private async Task<AdvisoryServicesData> BuildAdvisoryServicesAsync(
            object programDetails, int unitId)
        {
            // Query advisory services and critical inputs
            return new AdvisoryServicesData();
        }

        private async Task<ResultsData> BuildResultsAsync(object programDetails, int unitId)
        {
            // Query FLD and OFT results (KVK/EEU only)
            return new ResultsData();
        }

        private async Task<ReportData> BuildReportSectionAsync(object programDetails, int unitId)
        {
            // Map report section
            return new ReportData();
        }

        private async Task<RecommendationData> BuildRecommendationsAsync(
            object programDetails, int unitId)
        {
            // Map recommendations
            return new RecommendationData();
        }

        // ========================================
        // PUBLICATION REPORT BUILDER
        // ========================================

        private async Task BuildPublicationReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request)
        {
            var query = _publicationRepository.GetQueryable()
                .Where(p => p.UnitLocationId == request.UnitLocationId
                    && p.CreatedAt.Month == request.Month
                    && p.CreatedAt.Year == request.Year)
                .Include(p => p.Category)
                .Include(p => p.Authors)
                .Include(p => p.ExtensionLiterature);

            if (!string.IsNullOrEmpty(request.FormStatus))
                query = query.Where(p => p.FormStatus == request.FormStatus);

            if (request.ReportEntryId.HasValue)
                query = query.Where(p => p.Id == request.ReportEntryId.Value);

            var publications = await query.ToListAsync();

            // Step 1: Publications
            response.Publications = publications.Select(p => new PublicationData
            {
                Id = p.Id,
                Title = p.Title ?? "",
                Category = p.Category?.Name ?? "",
                Mode = p.Mode ?? "",
                Region = p.Region ?? "",
                JournalName = p.JournalName,
                ISSN = p.ISSN,
                PublicationDate = p.PublishedDate,
                TotalPages = p.TotalPages
            }).ToList();

            // Step 2: Authors
            response.Authors = publications
                .SelectMany(p => p.Authors?.Select(a => new AuthorData
                {
                    PublicationId = p.Id,
                    Name = a.Name ?? "",
                    Institution = a.Institution ?? "",
                    Address = a.Address ?? "",
                    IsPrimaryAuthor = a.IsPrimaryAuthor
                }) ?? Enumerable.Empty<AuthorData>())
                .ToList();

            // Step 3: Extension Literature
            response.ExtensionLiterature = new ExtensionLiteratureData
            {
                Items = publications
                    .SelectMany(p => p.ExtensionLiterature?.Select(e => new ExtensionLiteratureItem
                    {
                        PublicationTitle = e.Title ?? "",
                        AmountPerCopy = e.AmountPerCopy,
                        CopiesSoldOrDistributed = e.CopiesSold,
                        AmountGenerated = e.AmountPerCopy * e.CopiesSold
                    }) ?? Enumerable.Empty<ExtensionLiteratureItem>())
                    .ToList()
            };

            response.ExtensionLiterature.TotalAmountGenerated =
                response.ExtensionLiterature.Items.Sum(i => i.AmountGenerated);
            response.ExtensionLiterature.TotalCopiesSold =
                response.ExtensionLiterature.Items.Sum(i => i.CopiesSoldOrDistributed);
        }

        // ========================================
        // OTHER REPORT BUILDERS (Simplified)
        // ========================================

        private async Task BuildAwardsReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request)
        {
            // Query nominations/rewards repository
            // Map to AwardsData structure
        }

        private async Task BuildConsultancyReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request)
        {
            var query = _consultancyRepository.GetQueryable()
                .Where(c => c.UnitLocationId == request.UnitLocationId
                    && c.CreatedAt.Month == request.Month
                    && c.CreatedAt.Year == request.Year);

            if (!string.IsNullOrEmpty(request.FormStatus))
                query = query.Where(c => c.FormStatus == request.FormStatus);

            var consultancies = await query.ToListAsync();

            response.Consultancies = consultancies.Select(c => new ConsultancyData
            {
                Id = c.Id,
                Category = c.Category?.Name ?? "",
                Title = c.Title ?? "",
                // ... map other fields
            }).ToList();
        }

        private async Task BuildServicesReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request)
        {
            // Query services, visitors, accommodation
            // Map to ServicesFacilitiesData
        }

        private async Task BuildFinancialReportAsync(
            ComprehensiveReportResponse response,
            ComprehensiveReportRequest request)
        {
            // Query financial budget repository
            // Map to FinancialStatusData with budget and revolving fund
        }
    }
}
```

### Step 3: Register Service

**File**: `WebApi/Program.cs` (or dependency injection configuration)

```csharp
services.AddScoped<IComprehensiveReportService, ComprehensiveReportService>();
```

### Step 4: Add Controller Endpoint

**File**: `WebApi/Controllers/Reports/AdminReportController.cs`

Add to existing controller:

```csharp
/// <summary>
/// Generate comprehensive multi-step report
/// POST /api/admin/reports/comprehensive
/// </summary>
[HttpPost("comprehensive")]
public async Task<IActionResult> GenerateComprehensiveReport(
    [FromBody] ComprehensiveReportRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await _comprehensiveReportService.GenerateReportAsync(request);

    if (!result.IsSuccess)
        return BadRequest(new { message = result.ErrorMessage });

    return Ok(result.Data);
}

/// <summary>
/// Get available report types for a unit
/// GET /api/admin/reports/comprehensive/available-types?unitId=1
/// </summary>
[HttpGet("comprehensive/available-types")]
public async Task<IActionResult> GetAvailableReportTypes([FromQuery] int unitId)
{
    var result = await _comprehensiveReportService.GetAvailableReportTypesAsync(unitId);

    if (!result.IsSuccess)
        return BadRequest(new { message = result.ErrorMessage });

    return Ok(result.Data);
}
```

---

## 🎨 Frontend Implementation Guide

### Angular Service

**File**: `src/app/services/comprehensive-report.service.ts`

```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComprehensiveReportService {
  private baseUrl = '/api/admin/reports/comprehensive';

  constructor(private http: HttpClient) {}

  generateReport(request: ComprehensiveReportRequest): Observable<ComprehensiveReportResponse> {
    return this.http.post<ComprehensiveReportResponse>(this.baseUrl, request);
  }

  getAvailableReportTypes(unitId: number): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/available-types?unitId=${unitId}`);
  }
}

// TypeScript interfaces matching backend models
export interface ComprehensiveReportRequest {
  unitLocationId: number;
  month: number;
  year: number;
  reportType: 'program' | 'publication' | 'awards' | 'consultancy' | 'services' | 'financial';
  reportEntryId?: number;
  formStatus?: string;
}

export interface ComprehensiveReportResponse {
  metadata: ReportMetadata;
  programOrganized?: ProgramOrganizedData;
  participantDemographics?: ParticipantDemographicsData;
  programContent?: ProgramContentData;
  advisoryServices?: AdvisoryServicesData;
  results?: ResultsData;
  report?: ReportData;
  recommendations?: RecommendationData;
  publications?: PublicationData[];
  authors?: AuthorData[];
  extensionLiterature?: ExtensionLiteratureData;
  awards?: AwardsData;
  consultancies?: ConsultancyData[];
  servicesFacilities?: ServicesFacilitiesData;
  financialStatus?: FinancialStatusData;
}

// ... (add all other interfaces from backend models)
```

### Angular Component with Stepper

**File**: `src/app/pages/comprehensive-report/comprehensive-report.component.ts`

```typescript
import { Component, OnInit } from '@angular/core';
import { ComprehensiveReportService, ComprehensiveReportRequest, ComprehensiveReportResponse } from '../../services/comprehensive-report.service';
import jsPDF from 'jspdf';
import autoTable from 'jspdf-autotable';

@Component({
  selector: 'app-comprehensive-report',
  templateUrl: './comprehensive-report.component.html'
})
export class ComprehensiveReportComponent implements OnInit {
  // Form data
  selectedUnitLocationId: number = 0;
  selectedMonth: number = 1;
  selectedYear: number = new Date().getFullYear();
  selectedReportType: string = 'program';

  // Stepper
  currentStep: number = 0;
  steps: string[] = [];

  // Report data
  reportData: ComprehensiveReportResponse | null = null;
  isLoading: boolean = false;

  constructor(private reportService: ComprehensiveReportService) {}

  ngOnInit() {
    this.updateSteps();
  }

  updateSteps() {
    switch (this.selectedReportType) {
      case 'program':
        this.steps = [
          'Program Organized',
          'Participant Demographics',
          'Program Content',
          'Advisory Services',
          'Results',
          'Report',
          'Recommendations'
        ];
        break;
      case 'publication':
        this.steps = ['Publications', 'Authors', 'Extension Literature'];
        break;
      // ... other cases
    }
  }

  async loadReportData() {
    this.isLoading = true;

    const request: ComprehensiveReportRequest = {
      unitLocationId: this.selectedUnitLocationId,
      month: this.selectedMonth,
      year: this.selectedYear,
      reportType: this.selectedReportType as any
    };

    this.reportService.generateReport(request).subscribe({
      next: (data) => {
        this.reportData = data;
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Error loading report:', err);
        this.isLoading = false;
      }
    });
  }

  nextStep() {
    if (this.currentStep < this.steps.length - 1) {
      this.currentStep++;
    }
  }

  previousStep() {
    if (this.currentStep > 0) {
      this.currentStep--;
    }
  }

  generatePDF() {
    if (!this.reportData) return;

    const doc = new jsPDF({
      orientation: 'landscape',
      unit: 'mm',
      format: 'a4'
    });

    // Add header
    this.addReportHeader(doc);

    // Add sections based on report type
    switch (this.selectedReportType) {
      case 'program':
        this.addProgramReportSections(doc);
        break;
      case 'publication':
        this.addPublicationReportSections(doc);
        break;
      // ... other cases
    }

    // Save PDF
    const filename = `${this.selectedReportType}_report_${this.selectedMonth}_${this.selectedYear}.pdf`;
    doc.save(filename);
  }

  private addReportHeader(doc: jsPDF) {
    doc.setFontSize(16);
    doc.text(`${this.reportData!.metadata.unitName} - ${this.reportData!.metadata.reportType.toUpperCase()} REPORT`,
      doc.internal.pageSize.width / 2, 15, { align: 'center' });

    doc.setFontSize(10);
    doc.text(`${this.reportData!.metadata.unitLocationName}`,
      doc.internal.pageSize.width / 2, 22, { align: 'center' });
    doc.text(`${this.reportData!.metadata.monthName} ${this.reportData!.metadata.year}`,
      doc.internal.pageSize.width / 2, 27, { align: 'center' });
  }

  private addProgramReportSections(doc: jsPDF) {
    let startY = 35;

    // Section 1: Program Organized
    if (this.reportData!.programOrganized) {
      startY = this.addProgramOrganizedSection(doc, startY);
    }

    // Section 2: Participant Demographics (WITH MERGED COLUMNS)
    if (this.reportData!.participantDemographics) {
      startY = this.addParticipantDemographicsSection(doc, startY);
    }

    // Continue with other sections...
  }

  private addParticipantDemographicsSection(doc: jsPDF, startY: number): number {
    doc.setFontSize(12);
    doc.text('Participant Demographics', 14, startY);

    const demographics = this.reportData!.participantDemographics!;

    autoTable(doc, {
      startY: startY + 5,
      head: [
        [
          { content: 'Participant Category', rowSpan: 2, styles: { halign: 'center', valign: 'middle' } },
          { content: 'Gender Totals', colSpan: 2, styles: { halign: 'center' } }, // MERGED COLUMN
          { content: 'Grand Total', rowSpan: 2, styles: { halign: 'center', valign: 'middle' } },
          { content: 'Hostel Stayed', rowSpan: 2, styles: { halign: 'center', valign: 'middle' } }
        ],
        ['Male', 'Female'] // Sub-headers under Gender Totals
      ],
      body: [
        ...demographics.participants.map(p => [
          p.categoryName,
          p.maleTotal.toString(),
          p.femaleTotal.toString(),
          p.grandTotal.toString(),
          p.stayedInHostel.toString()
        ]),
        // Totals row
        [
          'TOTAL',
          demographics.totals.totalMale.toString(),
          demographics.totals.totalFemale.toString(),
          demographics.totals.grandTotal.toString(),
          demographics.totals.totalHostel.toString()
        ]
      ],
      theme: 'grid',
      styles: { fontSize: 9, cellPadding: 2 },
      headStyles: { fillColor: [41, 128, 185], halign: 'center' },
      footStyles: { fillColor: [52, 73, 94], fontStyle: 'bold' }
    });

    return (doc as any).lastAutoTable.finalY + 10;
  }

  // ... other section rendering methods
}
```

---

## 📋 Implementation Steps

### Week 1: Backend Foundation
1. ✅ Create `Application/Models/ComprehensiveReports/` folder
2. ✅ Add all request/response models
3. ✅ Create `IComprehensiveReportService` interface
4. ✅ Implement `ComprehensiveReportService` skeleton
5. ✅ Add controller endpoint
6. ✅ Test with Postman/Swagger

### Week 2: Backend Data Builders
1. ✅ Implement `BuildProgramReportAsync()` for one unit (e.g., FTI)
2. ✅ Implement demographic aggregation logic
3. ✅ Implement program content builders (resource persons, topics, aids)
4. ✅ Implement advisory services builder
5. ✅ Test program report generation end-to-end

### Week 3: Frontend Structure
1. ✅ Create comprehensive-report service
2. ✅ Create comprehensive-report component with stepper
3. ✅ Implement step navigation
4. ✅ Add data loading and display
5. ✅ Add basic table views for each section

### Week 4: PDF Generation
1. ✅ Implement PDF header
2. ✅ Implement participant demographics with merged columns
3. ✅ Implement other program sections
4. ✅ Test PDF generation with real data
5. ✅ Polish styling and formatting

### Week 5: Remaining Reports
1. ✅ Implement publication report
2. ✅ Implement awards report
3. ✅ Implement consultancy report
4. ✅ Implement services/facilities report
5. ✅ Implement financial status report

### Week 6: Testing & Deployment
1. ✅ Unit tests for backend services
2. ✅ Integration tests
3. ✅ User acceptance testing
4. ✅ Performance optimization
5. ✅ Deploy to staging
6. ✅ Production deployment

---

## 🚀 Next Steps

1. **Review this implementation plan** with the team
2. **Start with Week 1** backend models and endpoint
3. **Test the comprehensive endpoint** with a simple program report
4. **Iterate and expand** to other report types
5. **Coordinate with frontend team** for parallel development

---

**Status**: 🟢 Ready for Development
**Last Updated**: 2025-12-22
**Document Version**: 1.0
