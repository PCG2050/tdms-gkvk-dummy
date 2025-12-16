using System;
using System.Collections.Generic;

namespace Application.Models.DynamicReporting
{
    /// <summary>
    /// Request model for dynamic report generation across units
    /// </summary>
    public class DynamicReportRequest
    {
        // ===== UNIT SELECTION =====
        /// <summary>
        /// Single unit ID for unit-specific reports
        /// </summary>
        public int? UnitId { get; set; }

        /// <summary>
        /// Multiple unit IDs for cross-unit reports
        /// </summary>
        public List<int>? UnitIds { get; set; }

        // ===== DATE RANGE FILTERING =====
        /// <summary>
        /// Filter records with StartDate >= this value
        /// </summary>
        public DateOnly? StartDate { get; set; }

        /// <summary>
        /// Filter records with EndDate <= this value
        /// </summary>
        public DateOnly? EndDate { get; set; }

        // ===== LOCATION FILTERING =====
        /// <summary>
        /// Filter by specific unit location IDs
        /// </summary>
        public List<int>? UnitLocationIds { get; set; }

        /// <summary>
        /// Filter by organization ID
        /// </summary>
        public int? OrganizationId { get; set; }

        // ===== STATUS FILTERING =====
        /// <summary>
        /// Filter by form status (Draft, Pending, Approved, Rejected)
        /// </summary>
        public List<string>? FormStatuses { get; set; }

        // ===== ENTITY TYPE SELECTION =====
        /// <summary>
        /// Type of entity to report on
        /// </summary>
        public ReportEntityType? EntityType { get; set; }

        /// <summary>
        /// Specific fields to include in the response (null = all fields)
        /// </summary>
        public List<string>? Fields { get; set; }

        // ===== AGGREGATION =====
        /// <summary>
        /// Aggregations to perform (e.g., sum, average, count)
        /// </summary>
        public List<AggregationConfig>? Aggregations { get; set; }

        /// <summary>
        /// Fields to group by for aggregated reports
        /// </summary>
        public List<string>? GroupBy { get; set; }

        // ===== PAGINATION =====
        /// <summary>
        /// Page number (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Number of records per page (max 1000)
        /// </summary>
        public int PageSize { get; set; } = 50;

        // ===== SORTING =====
        /// <summary>
        /// Field name to sort by
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (Ascending or Descending)
        /// </summary>
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

        // ===== VALIDATION =====
        public void Validate()
        {
            if (PageSize < 1 || PageSize > 1000)
                throw new ArgumentException("PageSize must be between 1 and 1000");

            if (PageNumber < 1)
                throw new ArgumentException("PageNumber must be >= 1");

            if (StartDate.HasValue && EndDate.HasValue && StartDate > EndDate)
                throw new ArgumentException("StartDate cannot be after EndDate");
        }
    }

    /// <summary>
    /// Type of entity to report on
    /// </summary>
    public enum ReportEntityType
    {
        /// <summary>
        /// Main program/project details
        /// </summary>
        ProgramDetails,

        /// <summary>
        /// Progress reports
        /// </summary>
        Report,

        /// <summary>
        /// Participant demographic information
        /// </summary>
        ParticipantDemographics,

        /// <summary>
        /// Advisory services metrics
        /// </summary>
        AdvisoryServices,

        /// <summary>
        /// Training programmes
        /// </summary>
        TrainingProgrammes,

        /// <summary>
        /// Results (FLD/OFT) - KVK and EEU only
        /// </summary>
        Results,

        /// <summary>
        /// Other activities
        /// </summary>
        OtherActivities,

        /// <summary>
        /// Sales data - ATIC only
        /// </summary>
        Sales,

        /// <summary>
        /// Visitor details - ASM only
        /// </summary>
        VisitorDetails,

        /// <summary>
        /// Program activities - FIU only
        /// </summary>
        ProgramActivities
    }

    /// <summary>
    /// Sort direction
    /// </summary>
    public enum SortDirection
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// Configuration for aggregation calculations
    /// </summary>
    public class AggregationConfig
    {
        /// <summary>
        /// Field name to aggregate
        /// </summary>
        public required string Field { get; set; }

        /// <summary>
        /// Type of aggregation
        /// </summary>
        public AggregationType Type { get; set; }

        /// <summary>
        /// Alias for the aggregated value in results (optional)
        /// </summary>
        public string? Alias { get; set; }
    }

    /// <summary>
    /// Type of aggregation to perform
    /// </summary>
    public enum AggregationType
    {
        /// <summary>
        /// Count of records
        /// </summary>
        Count,

        /// <summary>
        /// Sum of values
        /// </summary>
        Sum,

        /// <summary>
        /// Average of values
        /// </summary>
        Average,

        /// <summary>
        /// Minimum value
        /// </summary>
        Min,

        /// <summary>
        /// Maximum value
        /// </summary>
        Max,

        /// <summary>
        /// Count of distinct values
        /// </summary>
        CountDistinct
    }
}
