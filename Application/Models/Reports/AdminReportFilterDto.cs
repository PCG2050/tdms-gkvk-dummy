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
        public string UnitName { get; set; }
        public string UnitLocationName { get; set; }
        public string MonthName { get; set; }
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
    }

    // ========================================
    // FILTER OPTIONS (for initial load)
    // ========================================
    public class ReportFilterOptionsDto
    {
        public List<UnitWithLocationsDto> Units { get; set; } = new();
        public List<int> Years { get; set; } = new();
    }




    public class LocationOptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
    }

    // ========================================
    // TABLE DTOs (simplified for display)
    // ========================================

    public class ReportProgramDto
    {
        public string ProgramType { get; set; }
        public string Title { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Duration { get; set; }
        public int Participants { get; set; }
        public string Status { get; set; }
    }

    public class ReportPublicationDto
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Pages { get; set; }
    }

    public class ReportNominationDto
    {
        public string Type { get; set; }
        public string AwardName { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
    }

    public class ReportConsultancyDto
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
    }

    public class ReportServiceDto
    {
        public string Category { get; set; }
        public string Title { get; set; }
        public string Discipline { get; set; }
        public string Particular { get; set; }        
        public DateOnly Date { get; set; }
    }

    public class ReportOtherActivityDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}