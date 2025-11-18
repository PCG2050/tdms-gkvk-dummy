using Application.Interface;
using System.ComponentModel.DataAnnotations;


namespace Application.Models.DataTables.ASM
{
  
    public class ASMVisitorDetailsDto
    {
        public int Id { get; set; }

        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        [StringLength(200)]
        public string? InstituteName { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }

        public int TotalVisitors => FarmersCount + StudentsCount + PublicCount;

        public DateOnly SubmittedDate { get; set; }

        // Status tracking
        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        // Approval tracking
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        // Audit fields
        public DateTimeOffset CreatedAt { get; set; }
        public int CreatedById { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public int? UpdatedById { get; set; }

        // Additional display fields (from joins)
        public string? UnitLocationName { get; set; }
        public string? OrganizationName { get; set; }
        public string? CreatedByName { get; set; }
        public string? ApprovedByName { get; set; }
    }

    public class ASMVisitorDetailsCreateDto
    {
        [Required]
        public int UnitLocationId { get; set; }

        [StringLength(200)]
        public string? InstituteName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Farmers count must be a positive number")]
        public int FarmersCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Students count must be a positive number")]
        public int StudentsCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Public count must be a positive number")]
        public int PublicCount { get; set; }
    }

    /// <summary>
    /// DTO for updating existing ASMVisitorDetails entries
    /// </summary>
    public class ASMVisitorDetailsUpdateDto :IUpdateDto
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string? InstituteName { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Farmers count must be a positive number")]
        public int? FarmersCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Students count must be a positive number")]
        public int? StudentsCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Public count must be a positive number")]
        public int? PublicCount { get; set; }
    }

    /// <summary>
    /// DTO for batch creating ASMVisitorDetails entries
    /// All entries are automatically created with status "Pending"
    /// </summary>
    public class ASMVisitorDetailsBatchCreateDto
    {
        /// <summary>
        /// List of visitor details to create
        /// </summary>
        [Required]
        public List<ASMVisitorDetailsCreateDto> VisitorDetails { get; set; } = new();
    }

    /// <summary>
    /// DTO for batch updating ASMVisitorDetails entries
    /// All entries are automatically set to status "Pending" after update
    /// </summary>
    public class ASMVisitorDetailsBatchUpdateDto
    {
        /// <summary>
        /// List of visitor details to update
        /// </summary>
        [Required]
        public List<ASMVisitorDetailsUpdateDto> VisitorDetails { get; set; } = new();
    }

    /// <summary>
    /// DTO for batch operation results
    /// </summary>
    public class ASMVisitorDetailsBatchResultDto
    {
        public List<ASMVisitorDetailsDto> SuccessfulEntries { get; set; } = new();
        public List<BatchErrorDto> FailedEntries { get; set; } = new();
        public int TotalProcessed { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
    }

    /// <summary>
    /// DTO for individual batch operation errors
    /// </summary>
    public class BatchErrorDto
    {
        public int Index { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public object? OriginalData { get; set; }
    }
}