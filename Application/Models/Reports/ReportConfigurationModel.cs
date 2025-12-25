using System.Collections.Generic;

namespace Application.Models.Reports
{
    /// <summary>
    /// Response containing report configuration for a specific unit
    /// </summary>
    public class ReportConfigurationResponse
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public List<SectionDefinitionDto> AvailableSections { get; set; } = new();
        public List<string> DefaultSections { get; set; } = new();
    }

    /// <summary>
    /// Definition of a report section with available fields
    /// </summary>
    public class SectionDefinitionDto
    {
        public string SectionKey { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Icon { get; set; }
        public int? StepperNumber { get; set; }
        public string? Group { get; set; }
        public List<FieldDefinitionDto> AvailableFields { get; set; } = new();
        public List<string> DefaultFields { get; set; } = new();
        public bool IsAvailable { get; set; }
    }

    /// <summary>
    /// Definition of a field within a section
    /// </summary>
    public class FieldDefinitionDto
    {
        public string Key { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public bool DefaultSelected { get; set; }
        public string? Group { get; set; }
        public bool IsArray { get; set; }
    }
}
