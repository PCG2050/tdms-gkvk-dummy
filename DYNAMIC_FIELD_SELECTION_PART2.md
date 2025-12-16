# Dynamic Field Selection - Part 2: Backend Implementation

## Backend Service Implementation

### IDynamicReportConfigurationService.cs
```csharp
namespace Application.Interface
{
    public interface IDynamicReportConfigurationService
    {
        /// <summary>
        /// Get report configuration with field metadata for a specific unit
        /// </summary>
        Task<DynamicReportConfigurationResponse> GetConfigurationAsync(
            int unitId,
            bool includeTemplates = false
        );

        /// <summary>
        /// Validate field selection for a section
        /// </summary>
        Task<ValidationResult> ValidateFieldSelectionAsync(
            int unitId,
            string sectionKey,
            List<string> selectedFields
        );

        /// <summary>
        /// Save a report template
        /// </summary>
        Task<int> SaveTemplateAsync(SaveTemplateRequest request, int userId);

        /// <summary>
        /// Get a report template
        /// </summary>
        Task<ReportTemplate> GetTemplateAsync(int templateId);

        /// <summary>
        /// Get templates for a unit
        /// </summary>
        Task<List<ReportTemplate>> GetTemplatesAsync(int unitId, int userId);
    }
}
```

### DynamicReportConfigurationService.cs
```csharp
using Application.Configuration;
using Application.Constants;
using Application.Interface;
using Application.Models.DynamicReporting;

namespace Infracture.Services
{
    public class DynamicReportConfigurationService : IDynamicReportConfigurationService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        public DynamicReportConfigurationService(
            ICurrentUserService currentUserService,
            ApplicationDbContext context
        )
        {
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<DynamicReportConfigurationResponse> GetConfigurationAsync(
            int unitId,
            bool includeTemplates = false
        )
        {
            var unitConfig = UnitConfigurations.GetConfiguration(unitId);

            var sections = BuildSectionConfigurations(unitId, unitConfig);

            var response = new DynamicReportConfigurationResponse
            {
                Unit = new UnitInfo
                {
                    UnitId = unitId,
                    UnitName = unitConfig.UnitName,
                    RecordCount = 0
                },
                Sections = sections,
                GlobalSettings = new ReportGlobalSettings
                {
                    MaxSectionsAllowed = 10,
                    AllowSaveAsTemplate = true,
                    AllowExport = true,
                    ExportFormats = new List<string> { "PDF", "Excel", "CSV" },
                    DefaultPageSize = 50,
                    PageSizeOptions = new List<int> { 10, 25, 50, 100, 200 }
                }
            };

            if (includeTemplates)
            {
                response.SavedTemplates = await GetTemplatesAsync(unitId, _currentUserService.UserId);
            }

            return response;
        }

        private List<SectionConfiguration> BuildSectionConfigurations(
            int unitId,
            UnitReportConfiguration unitConfig
        )
        {
            var sections = new List<SectionConfiguration>();

            // Build configurations based on unit type
            if (unitConfig.ProgramDetailsType != null)
            {
                sections.Add(BuildProgramDetailsSection(unitId));
            }

            if (unitConfig.ParticipantDemographicsType != null)
            {
                sections.Add(BuildParticipantDemographicsSection(unitId));
            }

            if (unitConfig.AdvisoryServicesType != null)
            {
                sections.Add(BuildAdvisoryServicesSection(unitId));
            }

            if (unitConfig.ReportType != null)
            {
                sections.Add(BuildReportSection(unitId));
            }

            if (unitConfig.HasResults)
            {
                sections.Add(BuildResultsSection(unitId));
            }

            if (unitConfig.IsSalesUnit)
            {
                sections.Add(BuildSalesSection(unitId));
            }

            if (unitConfig.IsVisitorUnit)
            {
                sections.Add(BuildVisitorDetailsSection(unitId));
            }

            if (unitConfig.IsActivityUnit)
            {
                sections.Add(BuildProgramActivitiesSection(unitId));
            }

            // Sort by display order
            return sections.OrderBy(s => s.DisplayOrder).ToList();
        }

        private SectionConfiguration BuildProgramDetailsSection(int unitId)
        {
            // Common fields for all training-based units
            var commonFields = new List<FieldMetadata>
            {
                new()
                {
                    FieldKey = "id",
                    DisplayName = "ID",
                    DataType = FieldDataType.Integer,
                    IsRequired = true,
                    IsDefaultSelected = true,
                    Category = "Identification",
                    ColumnWidth = "5%",
                    DisplayOrder = 1,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "title",
                    DisplayName = "Program Title",
                    DataType = FieldDataType.String,
                    IsRequired = true,
                    IsDefaultSelected = true,
                    Category = "Basic Information",
                    ColumnWidth = "20%",
                    Description = "Title of the training program or project",
                    DisplayOrder = 2,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "startDate",
                    DisplayName = "Start Date",
                    DataType = FieldDataType.Date,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Basic Information",
                    ColumnWidth = "8%",
                    Description = "Program start date",
                    DisplayOrder = 3,
                    IsSortable = true,
                    IsAggregatable = false,
                    FormatString = "dd/MM/yyyy"
                },
                new()
                {
                    FieldKey = "endDate",
                    DisplayName = "End Date",
                    DataType = FieldDataType.Date,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Basic Information",
                    ColumnWidth = "8%",
                    Description = "Program end date",
                    DisplayOrder = 4,
                    IsSortable = true,
                    IsAggregatable = false,
                    FormatString = "dd/MM/yyyy"
                },
                new()
                {
                    FieldKey = "duration",
                    DisplayName = "Duration",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Basic Information",
                    ColumnWidth = "8%",
                    Description = "Duration of the program",
                    DisplayOrder = 5,
                    IsSortable = false,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "location",
                    DisplayName = "Location",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Basic Information",
                    ColumnWidth = "12%",
                    Description = "Program location",
                    DisplayOrder = 6,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "programType",
                    DisplayName = "Program Type",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Classification",
                    ColumnWidth = "10%",
                    Description = "Type of program",
                    DisplayOrder = 10,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "category",
                    DisplayName = "Category",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Classification",
                    ColumnWidth = "10%",
                    Description = "Program category",
                    DisplayOrder = 11,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "theme",
                    DisplayName = "Theme",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Classification",
                    ColumnWidth = "10%",
                    Description = "Program theme",
                    DisplayOrder = 12,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "thematicArea",
                    DisplayName = "Thematic Area",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Classification",
                    ColumnWidth = "10%",
                    Description = "Thematic area of focus",
                    DisplayOrder = 13,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "mode",
                    DisplayName = "Mode of Delivery",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Delivery",
                    ColumnWidth = "8%",
                    Description = "Mode of program delivery (Online, Offline, Hybrid)",
                    DisplayOrder = 20,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "region",
                    DisplayName = "Region",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Location Details",
                    ColumnWidth = "10%",
                    Description = "Geographic region",
                    DisplayOrder = 30,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "sourceOfFund",
                    DisplayName = "Funding Source",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Financial",
                    ColumnWidth = "10%",
                    Description = "Source of funding",
                    DisplayOrder = 40,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "totalOutlayRs",
                    DisplayName = "Total Outlay (₹)",
                    DataType = FieldDataType.Currency,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Financial",
                    ColumnWidth = "10%",
                    Description = "Total project outlay in Rupees",
                    DisplayOrder = 41,
                    IsSortable = true,
                    IsAggregatable = true,
                    FormatString = "₹#,##0.00"
                },
                new()
                {
                    FieldKey = "fundAmount",
                    DisplayName = "Fund Amount (₹)",
                    DataType = FieldDataType.Currency,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Financial",
                    ColumnWidth = "10%",
                    Description = "Amount of funds received",
                    DisplayOrder = 42,
                    IsSortable = true,
                    IsAggregatable = true,
                    FormatString = "₹#,##0.00"
                },
                new()
                {
                    FieldKey = "fundReleaseDate",
                    DisplayName = "Fund Release Date",
                    DataType = FieldDataType.Date,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Financial",
                    ColumnWidth = "10%",
                    Description = "Date when funds were released",
                    DisplayOrder = 43,
                    IsSortable = true,
                    IsAggregatable = false,
                    FormatString = "dd/MM/yyyy"
                },
                new()
                {
                    FieldKey = "status",
                    DisplayName = "Status",
                    DataType = FieldDataType.Enum,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Status",
                    ColumnWidth = "8%",
                    Description = "Current program status",
                    DisplayOrder = 50,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "formStatus",
                    DisplayName = "Form Status",
                    DataType = FieldDataType.Enum,
                    IsRequired = true,
                    IsDefaultSelected = true,
                    Category = "Status",
                    ColumnWidth = "8%",
                    Description = "Form approval status",
                    DisplayOrder = 51,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "totalParticipants",
                    DisplayName = "Total Participants",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Participants",
                    ColumnWidth = "8%",
                    Description = "Total number of participants",
                    DisplayOrder = 60,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "maleParticipants",
                    DisplayName = "Male Participants",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Participants",
                    ColumnWidth = "8%",
                    Description = "Number of male participants",
                    DisplayOrder = 61,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "femaleParticipants",
                    DisplayName = "Female Participants",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Participants",
                    ColumnWidth = "8%",
                    Description = "Number of female participants",
                    DisplayOrder = 62,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "sponsoredOrganizationName",
                    DisplayName = "Sponsor Organization",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Organization",
                    ColumnWidth = "12%",
                    Description = "Name of sponsoring organization",
                    DisplayOrder = 70,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "organizerInstitutionName",
                    DisplayName = "Organizer Institution",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Organization",
                    ColumnWidth = "12%",
                    Description = "Name of organizing institution",
                    DisplayOrder = 71,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "createdAt",
                    DisplayName = "Created Date",
                    DataType = FieldDataType.DateTime,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Audit",
                    ColumnWidth = "10%",
                    Description = "Record creation date",
                    DisplayOrder = 90,
                    IsSortable = true,
                    IsAggregatable = false,
                    FormatString = "dd/MM/yyyy HH:mm"
                },
                new()
                {
                    FieldKey = "createdByName",
                    DisplayName = "Created By",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = false,
                    Category = "Audit",
                    ColumnWidth = "10%",
                    Description = "User who created the record",
                    DisplayOrder = 91,
                    IsSortable = true,
                    IsAggregatable = false
                }
            };

            // Add unit-specific fields
            if (unitId == UnitConstants.FTI_UNIT_ID || unitId == UnitConstants.STU_UNIT_ID)
            {
                commonFields.AddRange(new[]
                {
                    new FieldMetadata
                    {
                        FieldKey = "tpNo",
                        DisplayName = "TP No",
                        DataType = FieldDataType.Integer,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Identification",
                        ColumnWidth = "6%",
                        Description = "Training Program Number",
                        DisplayOrder = 7,
                        IsSortable = true,
                        IsAggregatable = false
                    },
                    new FieldMetadata
                    {
                        FieldKey = "batchNo",
                        DisplayName = "Batch No",
                        DataType = FieldDataType.Integer,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Identification",
                        ColumnWidth = "6%",
                        Description = "Batch Number",
                        DisplayOrder = 8,
                        IsSortable = true,
                        IsAggregatable = false
                    }
                });
            }

            // Add KVK/EEU specific fields
            if (unitId == UnitConstants.KVK_UNIT_ID || unitId == UnitConstants.EEU_UNIT_ID)
            {
                commonFields.AddRange(new[]
                {
                    new FieldMetadata
                    {
                        FieldKey = "stageOfCrop",
                        DisplayName = "Stage of Crop",
                        DataType = FieldDataType.String,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Technology",
                        ColumnWidth = "10%",
                        Description = "Current stage of crop",
                        DisplayOrder = 80,
                        IsSortable = true,
                        IsAggregatable = false
                    },
                    new FieldMetadata
                    {
                        FieldKey = "noOfDemos",
                        DisplayName = "No. of Demonstrations",
                        DataType = FieldDataType.Integer,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Technology",
                        ColumnWidth = "8%",
                        Description = "Number of demonstrations conducted",
                        DisplayOrder = 81,
                        IsSortable = true,
                        IsAggregatable = true
                    },
                    new FieldMetadata
                    {
                        FieldKey = "noOfTrails",
                        DisplayName = "No. of Trials",
                        DataType = FieldDataType.Integer,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Technology",
                        ColumnWidth = "8%",
                        Description = "Number of trials conducted",
                        DisplayOrder = 82,
                        IsSortable = true,
                        IsAggregatable = true
                    },
                    new FieldMetadata
                    {
                        FieldKey = "noOfVisits",
                        DisplayName = "No. of Visits",
                        DataType = FieldDataType.Integer,
                        IsRequired = false,
                        IsDefaultSelected = false,
                        Category = "Technology",
                        ColumnWidth = "8%",
                        Description = "Number of field visits",
                        DisplayOrder = 83,
                        IsSortable = true,
                        IsAggregatable = true
                    }
                });
            }

            // Define field categories
            var categories = new List<FieldCategory>
            {
                new()
                {
                    CategoryKey = "Identification",
                    DisplayName = "Identification",
                    DisplayOrder = 1,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Basic Information",
                    DisplayName = "Basic Information",
                    DisplayOrder = 2,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Classification",
                    DisplayName = "Classification",
                    DisplayOrder = 3,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Delivery",
                    DisplayName = "Delivery Mode",
                    DisplayOrder = 4,
                    IsCollapsedByDefault = true
                },
                new()
                {
                    CategoryKey = "Location Details",
                    DisplayName = "Location Details",
                    DisplayOrder = 5,
                    IsCollapsedByDefault = true
                },
                new()
                {
                    CategoryKey = "Financial",
                    DisplayName = "Financial Information",
                    DisplayOrder = 6,
                    IsCollapsedByDefault = true
                },
                new()
                {
                    CategoryKey = "Status",
                    DisplayName = "Status Information",
                    DisplayOrder = 7,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Participants",
                    DisplayName = "Participant Information",
                    DisplayOrder = 8,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Organization",
                    DisplayName = "Organization Details",
                    DisplayOrder = 9,
                    IsCollapsedByDefault = true
                },
                new()
                {
                    CategoryKey = "Technology",
                    DisplayName = "Technology Details",
                    DisplayOrder = 10,
                    IsCollapsedByDefault = true
                },
                new()
                {
                    CategoryKey = "Audit",
                    DisplayName = "Audit Information",
                    DisplayOrder = 99,
                    IsCollapsedByDefault = true
                }
            };

            return new SectionConfiguration
            {
                SectionKey = "programDetails",
                DisplayName = "Program Details",
                Description = "Detailed information about training programs and projects",
                IconClass = "bi-file-text",
                DisplayOrder = 1,
                AvailableFields = commonFields.OrderBy(f => f.DisplayOrder).ToList(),
                RequiredFields = new List<string> { "id", "title", "formStatus" },
                DefaultSelectedFields = commonFields
                    .Where(f => f.IsDefaultSelected)
                    .Select(f => f.FieldKey)
                    .ToList(),
                MaxFieldsAllowed = 0,
                SupportsPagination = true,
                SupportsSorting = true,
                SupportsFiltering = true,
                FieldCategories = categories
            };
        }

        private SectionConfiguration BuildParticipantDemographicsSection(int unitId)
        {
            var fields = new List<FieldMetadata>
            {
                new()
                {
                    FieldKey = "id",
                    DisplayName = "ID",
                    DataType = FieldDataType.Integer,
                    IsRequired = true,
                    IsDefaultSelected = true,
                    Category = "Identification",
                    ColumnWidth = "5%",
                    DisplayOrder = 1,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "programTitle",
                    DisplayName = "Program Title",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Basic",
                    ColumnWidth = "15%",
                    Description = "Associated program title",
                    DisplayOrder = 2,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "participantCategory",
                    DisplayName = "Participant Category",
                    DataType = FieldDataType.String,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Basic",
                    ColumnWidth = "12%",
                    Description = "Category of participants",
                    DisplayOrder = 3,
                    IsSortable = true,
                    IsAggregatable = false
                },
                new()
                {
                    FieldKey = "male_SC",
                    DisplayName = "Male SC",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Male Demographics",
                    ColumnWidth = "6%",
                    Description = "Male participants from Scheduled Caste",
                    DisplayOrder = 10,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "male_ST",
                    DisplayName = "Male ST",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Male Demographics",
                    ColumnWidth = "6%",
                    Description = "Male participants from Scheduled Tribe",
                    DisplayOrder = 11,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "male_OBC",
                    DisplayName = "Male OBC",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Male Demographics",
                    ColumnWidth = "6%",
                    Description = "Male participants from Other Backward Classes",
                    DisplayOrder = 12,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "male_GEN",
                    DisplayName = "Male GEN",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Male Demographics",
                    ColumnWidth = "6%",
                    Description = "Male participants from General category",
                    DisplayOrder = 13,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "female_SC",
                    DisplayName = "Female SC",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Female Demographics",
                    ColumnWidth = "6%",
                    Description = "Female participants from Scheduled Caste",
                    DisplayOrder = 20,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "female_ST",
                    DisplayName = "Female ST",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Female Demographics",
                    ColumnWidth = "6%",
                    Description = "Female participants from Scheduled Tribe",
                    DisplayOrder = 21,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "female_OBC",
                    DisplayName = "Female OBC",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Female Demographics",
                    ColumnWidth = "6%",
                    Description = "Female participants from Other Backward Classes",
                    DisplayOrder = 22,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "female_GEN",
                    DisplayName = "Female GEN",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Female Demographics",
                    ColumnWidth = "6%",
                    Description = "Female participants from General category",
                    DisplayOrder = 23,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "totalMale",
                    DisplayName = "Total Male",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Totals",
                    ColumnWidth = "6%",
                    Description = "Total male participants",
                    DisplayOrder = 30,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "totalFemale",
                    DisplayName = "Total Female",
                    DataType = FieldDataType.Integer,
                    IsRequired = false,
                    IsDefaultSelected = true,
                    Category = "Totals",
                    ColumnWidth = "6%",
                    Description = "Total female participants",
                    DisplayOrder = 31,
                    IsSortable = true,
                    IsAggregatable = true
                },
                new()
                {
                    FieldKey = "total",
                    DisplayName = "Grand Total",
                    DataType = FieldDataType.Integer,
                    IsRequired = true,
                    IsDefaultSelected = true,
                    Category = "Totals",
                    ColumnWidth = "6%",
                    Description = "Total number of participants",
                    DisplayOrder = 32,
                    IsSortable = true,
                    IsAggregatable = true
                }
            };

            var categories = new List<FieldCategory>
            {
                new()
                {
                    CategoryKey = "Identification",
                    DisplayName = "Identification",
                    DisplayOrder = 1,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Basic",
                    DisplayName = "Basic Information",
                    DisplayOrder = 2,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Male Demographics",
                    DisplayName = "Male Participant Demographics",
                    DisplayOrder = 3,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Female Demographics",
                    DisplayName = "Female Participant Demographics",
                    DisplayOrder = 4,
                    IsCollapsedByDefault = false
                },
                new()
                {
                    CategoryKey = "Totals",
                    DisplayName = "Total Counts",
                    DisplayOrder = 5,
                    IsCollapsedByDefault = false
                }
            };

            return new SectionConfiguration
            {
                SectionKey = "participantDemographics",
                DisplayName = "Participant Demographics",
                Description = "Detailed demographic breakdown of program participants",
                IconClass = "bi-people",
                DisplayOrder = 2,
                AvailableFields = fields,
                RequiredFields = new List<string> { "id", "total" },
                DefaultSelectedFields = fields
                    .Where(f => f.IsDefaultSelected)
                    .Select(f => f.FieldKey)
                    .ToList(),
                MaxFieldsAllowed = 0,
                SupportsPagination = true,
                SupportsSorting = true,
                SupportsFiltering = false,
                FieldCategories = categories
            };
        }

        // ... Additional section builders for AdvisoryServices, Reports, etc.

        public async Task<ValidationResult> ValidateFieldSelectionAsync(
            int unitId,
            string sectionKey,
            List<string> selectedFields
        )
        {
            var config = await GetConfigurationAsync(unitId, false);
            var section = config.Sections.FirstOrDefault(s => s.SectionKey == sectionKey);

            if (section == null)
            {
                return new ValidationResult
                {
                    IsValid = false,
                    Errors = new List<string> { $"Section '{sectionKey}' not found for unit {unitId}" }
                };
            }

            var errors = new List<string>();
            var warnings = new List<string>();

            // Check required fields
            var missingRequired = section.RequiredFields
                .Where(rf => !selectedFields.Contains(rf))
                .ToList();

            if (missingRequired.Any())
            {
                errors.Add($"Missing required fields: {string.Join(", ", missingRequired)}");
            }

            // Check max fields
            if (section.MaxFieldsAllowed > 0 && selectedFields.Count > section.MaxFieldsAllowed)
            {
                errors.Add($"Too many fields selected. Maximum allowed: {section.MaxFieldsAllowed}");
            }

            // Check for invalid fields
            var availableFieldKeys = section.AvailableFields.Select(f => f.FieldKey).ToHashSet();
            var invalidFields = selectedFields
                .Where(sf => !availableFieldKeys.Contains(sf))
                .ToList();

            if (invalidFields.Any())
            {
                errors.Add($"Invalid fields: {string.Join(", ", invalidFields)}");
            }

            // Warnings for performance
            if (selectedFields.Count > 20)
            {
                warnings.Add("Selecting many fields may impact report generation performance");
            }

            return new ValidationResult
            {
                IsValid = errors.Count == 0,
                Errors = errors,
                Warnings = warnings
            };
        }

        // Template management methods...
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
    }
}
```

---

## Configuration for All 11 Units

(Continued in Part 3...)
