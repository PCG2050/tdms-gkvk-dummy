using System;
using System.Collections.Generic;

namespace Application.Models.DynamicReporting
{
    /// <summary>
    /// Generic response model for dynamic reports
    /// </summary>
    /// <typeparam name="T">Type of data in the report</typeparam>
    public class DynamicReportResponse<T>
    {
        /// <summary>
        /// Metadata about the report
        /// </summary>
        public required ReportMetadata Metadata { get; set; }

        /// <summary>
        /// Report data
        /// </summary>
        public required List<T> Data { get; set; }

        /// <summary>
        /// Pagination information
        /// </summary>
        public required PaginationInfo Pagination { get; set; }

        /// <summary>
        /// Aggregated values (if aggregations were requested)
        /// </summary>
        public Dictionary<string, object>? Aggregations { get; set; }
    }

    /// <summary>
    /// Metadata about the generated report
    /// </summary>
    public class ReportMetadata
    {
        /// <summary>
        /// Information about units included in the report
        /// </summary>
        public required List<UnitInfo> Units { get; set; }

        /// <summary>
        /// Start date filter applied
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// End date filter applied
        /// </summary>
        public DateOnly? EndDate { get; set; }

        /// <summary>
        /// Timestamp when the report was generated
        /// </summary>
        public DateTimeOffset GeneratedAt { get; set; }

        /// <summary>
        /// User who generated the report
        /// </summary>
        public required string GeneratedBy { get; set; }

        /// <summary>
        /// Type of entity being reported
        /// </summary>
        public ReportEntityType EntityType { get; set; }

        /// <summary>
        /// Applied filters (for audit trail)
        /// </summary>
        public Dictionary<string, object>? AppliedFilters { get; set; }
    }

    /// <summary>
    /// Information about a unit in the report
    /// </summary>
    public class UnitInfo
    {
        /// <summary>
        /// Unit ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// Unit name
        /// </summary>
        public required string UnitName { get; set; }

        /// <summary>
        /// Number of records from this unit in the report
        /// </summary>
        public int RecordCount { get; set; }
    }

    /// <summary>
    /// Pagination information
    /// </summary>
    public class PaginationInfo
    {
        /// <summary>
        /// Current page number
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Number of records per page
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of records (across all pages)
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Whether there is a next page
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// Create pagination info from query results
        /// </summary>
        public static PaginationInfo Create(int totalRecords, int pageNumber, int pageSize)
        {
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PaginationInfo
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                HasPrevious = pageNumber > 1,
                HasNext = pageNumber < totalPages
            };
        }
    }

    /// <summary>
    /// Response model for report metadata endpoint
    /// </summary>
    public class ReportMetadataResponse
    {
        /// <summary>
        /// Available units with their capabilities
        /// </summary>
        public required List<UnitMetadata> Units { get; set; }

        /// <summary>
        /// Common filter fields available across all units
        /// </summary>
        public required List<string> CommonFilters { get; set; }

        /// <summary>
        /// Available form status values
        /// </summary>
        public required List<string> FormStatuses { get; set; }

        /// <summary>
        /// Available aggregation types
        /// </summary>
        public required List<string> AggregationTypes { get; set; }
    }

    /// <summary>
    /// Metadata about a specific unit's reporting capabilities
    /// </summary>
    public class UnitMetadata
    {
        /// <summary>
        /// Unit ID
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// Unit name
        /// </summary>
        public required string UnitName { get; set; }

        /// <summary>
        /// Available entity types for this unit
        /// </summary>
        public required List<string> AvailableEntities { get; set; }

        /// <summary>
        /// Common fields available in ProgramDetails
        /// </summary>
        public required List<string> CommonFields { get; set; }

        /// <summary>
        /// Unit-specific fields unique to this unit
        /// </summary>
        public required List<string> UnitSpecificFields { get; set; }

        /// <summary>
        /// Whether this unit has Results entity (FLD/OFT)
        /// </summary>
        public bool HasResults { get; set; }

        /// <summary>
        /// Whether this is a sales-tracking unit (ATIC)
        /// </summary>
        public bool IsSalesUnit { get; set; }

        /// <summary>
        /// Whether this is a visitor-tracking unit (ASM)
        /// </summary>
        public bool IsVisitorUnit { get; set; }

        /// <summary>
        /// Whether this is an activity-based unit (FIU)
        /// </summary>
        public bool IsActivityUnit { get; set; }
    }
}
