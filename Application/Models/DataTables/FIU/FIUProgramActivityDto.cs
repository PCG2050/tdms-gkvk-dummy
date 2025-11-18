using System.ComponentModel.DataAnnotations;


namespace Application.Models.DataTables.FIU
{
    public class FIUProgramActivityCreateDto
    {
        [Required(ErrorMessage = "Unit location is required")]
        public int UnitLocationId { get; set; }

        [Required(ErrorMessage = "Activity type is required")]
        public int FIUActivitiesId { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [Range(1, 100000, ErrorMessage = "Number must be between 1 and 100000")]
        public int Number { get; set; }

        [MaxLength(500, ErrorMessage = "Upload media URL cannot exceed 500 characters")]
        public string? UploadMediaUrl { get; set; }

        [MaxLength(2000, ErrorMessage = "Remarks cannot exceed 2000 characters")]
        public string? Remarks { get; set; }
    }


    public class FIUProgramActivityUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Activity type is required")]
        public int FIUActivitiesId { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [Range(1, 100000, ErrorMessage = "Number must be between 1 and 100000")]
        public int Number { get; set; }

        [MaxLength(500)]
        public string? UploadMediaUrl { get; set; }

        [MaxLength(2000)]
        public string? Remarks { get; set; }
    }

    namespace Application.Models.FIU
    {
        public class FIUProgramActivityResponseDto
        {
            public int Id { get; set; }

            // Location Info
            public int UnitLocationId { get; set; }
            public string UnitLocationName { get; set; } = string.Empty;
            public string DistrictName { get; set; } = string.Empty;
            public string StateName { get; set; } = string.Empty;

            // Organization Info
            public int OrganizationId { get; set; }
            public string OrganizationName { get; set; } = string.Empty;

            // Activity Info (From Master Data)
            public int FIUActivitiesId { get; set; }
            public string ActivityName { get; set; } = string.Empty;
            public string ActivityCategory { get; set; } = string.Empty;
            public string? UnitOfMeasurement { get; set; }

            // Activity Data
            public int Number { get; set; }
            public string? UploadMediaUrl { get; set; }
            public string? Remarks { get; set; }

            // Status Info
            public string FormStatus { get; set; } = "Draft";
            public string? FormStatusRemarks { get; set; }
            public DateTimeOffset? SubmittedAt { get; set; }
            public DateTimeOffset? ApprovedAt { get; set; }

            // Audit Info
            public int CreatedById { get; set; }
            public string CreatedByName { get; set; } = string.Empty;
            public DateTimeOffset CreatedAt { get; set; }

            public int? ApprovedById { get; set; }
            public string? ApprovedByName { get; set; }

            public int? UpdatedById { get; set; }
            public string? UpdatedByName { get; set; }
            public DateTimeOffset? UpdatedAt { get; set; }
        }
    }

    public class FIUActivityDto
    {
        public int Id { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public string? ActivityDescription { get; set; }
        public string ActivityCategory { get; set; } = string.Empty;
        public string? UnitOfMeasurement { get; set; }
        public bool RequiresMediaUpload { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// Monthly summary report for FIU activities
    /// </summary>
    public class FIUMonthlyReportDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; } = string.Empty;
        public List<FIUActivitySummaryDto> Activities { get; set; } = new();
        public int TotalActivities { get; set; }
        public int TotalCount { get; set; }
    }

    public class FIUActivitySummaryDto
    {
        public int SlNo { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public int Count { get; set; }
    }


    public class FIUReportRequestDto
    {
        [Required(ErrorMessage = "Year is required")]
        [Range(2020, 2100, ErrorMessage = "Year must be between 2020 and 2100")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Month is required")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int Month { get; set; }

        public int? UnitLocationId { get; set; }
    }

    /// <summary>
    /// DTO for batch creating FIU activities
    /// All entries are automatically created with status "Pending"
    /// </summary>
    public class FIUProgramActivityBatchCreateDto
    {
        [Required]
        public List<FIUProgramActivityCreateDto> Activities { get; set; } = new();
    }

    /// <summary>
    /// DTO for batch updating FIU activities
    /// All entries are automatically set to status "Pending" after update
    /// </summary>
    public class FIUProgramActivityBatchUpdateDto
    {
        [Required]
        public List<FIUProgramActivityUpdateDto> Activities { get; set; } = new();
    }

    /// <summary>
    /// DTO for batch operation results
    /// </summary>
    public class FIUProgramActivityBatchResultDto
    {
        public List<Application.Models.FIU.FIUProgramActivityResponseDto> SuccessfulEntries { get; set; } = new();
        public List<FIUBatchErrorDto> FailedEntries { get; set; } = new();
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }

    /// <summary>
    /// DTO for individual batch operation errors
    /// </summary>
    public class FIUBatchErrorDto
    {
        public int Index { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public object? OriginalData { get; set; }
    }

}
