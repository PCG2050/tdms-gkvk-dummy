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
        public string Discipline { get; set; } = string.Empty;
        public string Particular { get; set; } = string.Empty;        
        public DateOnly Date { get; set; }
    }

    public class ReportOtherActivityDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}