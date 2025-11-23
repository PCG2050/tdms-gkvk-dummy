using System;
using System.Collections.Generic;

namespace Application.Models.Reports
{
    // ========================================
    // FILTER REQUEST
    // ========================================
    public class AdminReportFilterDto
    {
        public int UnitLocationId { get; set; }
        public int Month { get; set; }  // 1-12
        public int Year { get; set; }
    }

    // ========================================
    // MAIN REPORT RESPONSE (Single object with all tables)
    // ========================================
    public class AdminReportResponseDto
    {
        // Report metadata
        public string UnitName { get; set; } = string.Empty;
        public string UnitLocationName { get; set; } = string.Empty;
        public string MonthName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime GeneratedAt { get; set; }
        public int TotalEntries { get; set; }

        // All tables data
        public List<ReportProgramDto> Programs { get; set; } = new();
        public List<ReportPublicationDto> Publications { get; set; } = new();
        public List<ReportNominationDto> Nominations { get; set; } = new();
        public List<ReportConsultancyDto> Consultancies { get; set; } = new();
        public List<ReportServiceDto> Services { get; set; } = new();
        public List<ReportOtherActivityDto> OtherActivities { get; set; } = new();

        // NEW: FIU Activities (only populated for FIU unit)
        public ReportFIUActivitiesDto? FIUActivities { get; set; }

        public ReportASMActivitiesDto? ASMActivities { get;set; }
    }

    // ========================================
    // FILTER OPTIONS (for initial load)
    // ========================================
    public class ReportFilterOptionsDto
    {
        public List<UnitWithLocationsDto> Units { get; set; } = new();
        public List<int> Years { get; set; } = new();
    }

    // ========== NEW: FIU DTOs ==========
    public class ReportFIUActivitiesDto
    {
        public List<ReportFIUActivityItemDto> Activities { get; set; } = new();
        public int TotalActivities { get; set; }
        public int TotalCount { get; set; }

        public int TotalEntries { get; set; }
    }

    public class ReportFIUActivityItemDto
    {
        public int SlNo { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    /// <summary>
    /// ASM Monthly Visitor Summary for Reports
    /// </summary>
    public class ASMVisitorSummaryDto
    {
        public int TotalFarmers { get; set; }
        public int TotalStudents { get; set; }
        public int TotalPublic { get; set; }
        public int TotalVisitors { get; set; }
    }

    /// <summary>
    /// ASM Report Display Item (for table rows)
    /// </summary>
    public class ASMVisitorReportItemDto
    {
        public int SlNo { get; set; }
        public string Particulars { get; set; } = string.Empty;
        public int NoOfVisitors { get; set; }
    }

    /// <summary>
    /// Complete ASM Report Data
    /// </summary>
    public class ReportASMActivitiesDto
    {
        public List<ASMVisitorReportItemDto> Visitors { get; set; } = new();
        public int TotalVisitors { get; set; }
        public int TotalEntries { get; set; }  // Number of DB entries
    }




    public class LocationOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }

    // ========================================
    // TABLE DTOs (simplified for display)
    // ========================================

    public class ReportProgramDto
    {
        public string ProgramType { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string DateFrom { get; set; } = string.Empty;
        public string DateTo { get; set; } = string.Empty;  
        public string Duration { get; set; } = string.Empty;
        public int Participants { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class ReportPublicationDto
    {
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Pages { get; set; }   = string.Empty;

        public string Mode { get; set; } = string.Empty;
        
        public int CopiesSold { get; set; } = 0;
    }

    public class ReportNominationDto
    {
        public string Type { get; set; } = string.Empty;
        public string AwardName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }

    public class ReportConsultancyDto
    {
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
    }

    public class ReportServiceDto
    {
        public string Category { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        public string Theme { get; set; } = string.Empty;
        public int Quantity { get; set; }

        public string Unit { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        // Hostel data (only populated when CategoryId == 1)
        public List<ReportHostelDto>? HostelData { get; set; }
        //public string Discipline { get; set; } = string.Empty;
        //public string Particular { get; set; } = string.Empty;
        //public DateOnly Date { get; set; }
    }

    public class ReportHostelDto
    {
        public string Date { get; set; } = string.Empty;
        public int TotalMale { get; set; }
        public int TotalFemale { get; set; }
        public int DurationOfStay { get; set; }
        public decimal AmountGenerated { get; set; }
    }

    public class ReportOtherActivityDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}