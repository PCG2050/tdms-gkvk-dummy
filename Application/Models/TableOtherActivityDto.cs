using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables
{
    /// <summary>
    /// DTO for displaying TableOtherActivity with full details
    /// </summary>
    public class TableOtherActivityDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? UploadPath { get; set; }

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

    /// <summary>
    /// DTO for creating a new TableOtherActivity
    /// </summary>
    public class TableOtherActivityCreateDto
    {
        [Required]
        public int UnitLocationId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? UploadPath { get; set; }
    }

    /// <summary>
    /// DTO for updating an existing TableOtherActivity
    /// All fields are optional - only provided fields will be updated
    /// </summary>
    public class TableOtherActivityUpdateDto
    {
        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? UploadPath { get; set; }
    }
}