using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    // ========================================
    // REQUEST DTOs
    // ========================================

    /// <summary>
    /// Request for generating a dynamic report with selected sections and columns
    /// </summary>
    public class DynamicReportRequest
    {
        [Required]
        public int UnitLocationId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        /// <summary>
        /// List of sections to include in the report with selected columns
        /// </summary>
        [Required]
        public List<SectionRequest> Sections { get; set; } = new();

        /// <summary>
        /// Output format for the report (future use)
        /// </summary>
        public string OutputFormat { get; set; } = "json"; // "json", "docx", "excel"
    }

    /// <summary>
    /// Represents a section (e.g., Programs, Services) with selected columns
    /// </summary>
    public class SectionRequest
    {
        /// <summary>
        /// Section identifier (e.g., "programs", "services", "fiuActivities")
        /// </summary>
        [Required]
        public string SectionKey { get; set; } = string.Empty;

        /// <summary>
        /// List of column keys to include in this section
        /// </summary>
        [Required]
        public List<string> SelectedColumns { get; set; } = new();

        /// <summary>
        /// Optional: Column to sort by
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Optional: Sort direction
        /// </summary>
        public string SortDirection { get; set; } = "asc"; // "asc" or "desc"
    }

    // ========================================
    // RESPONSE DTOs
    // ========================================

    /// <summary>
    /// Dynamic report response with flexible sections
    /// </summary>
    public class DynamicReportData
    {
        // Metadata
        public string UnitName { get; set; } = string.Empty;
        public string UnitLocationName { get; set; } = string.Empty;
        public string MonthName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public string GeneratedAt { get; set; } = string.Empty;
        public int TotalEntries { get; set; }

        /// <summary>
        /// Dynamic sections with their data
        /// </summary>
        public List<ReportSection> Sections { get; set; } = new();
    }

    /// <summary>
    /// Represents a single section in the report (e.g., Programs, Services)
    /// </summary>
    public class ReportSection
    {
        /// <summary>
        /// Section identifier
        /// </summary>
        public string SectionKey { get; set; } = string.Empty;

        /// <summary>
        /// Display name for the section
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Total number of records in this section
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Column metadata for this section
        /// </summary>
        public List<ColumnMetadata> Columns { get; set; } = new();

        /// <summary>
        /// Actual data rows (only selected columns)
        /// </summary>
        public List<Dictionary<string, object?>> Rows { get; set; } = new();

        /// <summary>
        /// Optional: CSS class for special styling (e.g., "fiu-activities")
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// Optional: Whether to show a total row
        /// </summary>
        public bool ShowTotal { get; set; }

        /// <summary>
        /// Optional: Total value (for FIU/ASM aggregated counts)
        /// </summary>
        public int? TotalValue { get; set; }
    }

    /// <summary>
    /// Metadata for a single column
    /// </summary>
    public class ColumnMetadata
    {
        /// <summary>
        /// Column key (property name in data)
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Display name for the column header
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Data type for formatting
        /// </summary>
        public string DataType { get; set; } = "string"; // "string", "number", "date", "decimal", "currency"

        /// <summary>
        /// Optional: Column width percentage
        /// </summary>
        public int? Width { get; set; }
    }

    // ========================================
    // CONFIGURATION DTOs
    // ========================================

    /// <summary>
    /// Report configuration response - lists available sections and columns
    /// </summary>
    public class ReportConfiguration
    {
        /// <summary>
        /// Available sections for this unit
        /// </summary>
        public List<SectionDefinition> AvailableSections { get; set; } = new();

        /// <summary>
        /// Section keys that are selected by default
        /// </summary>
        public List<string> DefaultSections { get; set; } = new();
    }

    /// <summary>
    /// Definition of a section with its available columns
    /// </summary>
    public class SectionDefinition
    {
        /// <summary>
        /// Section identifier
        /// </summary>
        public string SectionKey { get; set; } = string.Empty;

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Optional: Icon name
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// Available columns for this section
        /// </summary>
        public List<ColumnDefinition> AvailableColumns { get; set; } = new();

        /// <summary>
        /// Column keys that are selected by default
        /// </summary>
        public List<string> DefaultColumns { get; set; } = new();

        /// <summary>
        /// Whether this section is available for the current unit
        /// </summary>
        public bool IsAvailable { get; set; } = true;

        /// <summary>
        /// Optional: Group/category for UI organization
        /// </summary>
        public string? Group { get; set; }
    }

    /// <summary>
    /// Definition of a column
    /// </summary>
    public class ColumnDefinition
    {
        /// <summary>
        /// Column key
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Data type
        /// </summary>
        public string DataType { get; set; } = "string";

        /// <summary>
        /// Whether selected by default
        /// </summary>
        public bool DefaultSelected { get; set; } = true;

        /// <summary>
        /// Optional: Group for UI organization (e.g., "Basic Info", "Financial", "Demographics")
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// Whether this column can be used for filtering
        /// </summary>
        public bool IsFilterable { get; set; } = false;

        /// <summary>
        /// Whether this column can be used for sorting
        /// </summary>
        public bool IsSortable { get; set; } = true;

        /// <summary>
        /// Optional: Column width percentage
        /// </summary>
        public int? Width { get; set; }
    }
}
