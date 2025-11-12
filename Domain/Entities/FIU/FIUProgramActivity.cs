
using Domain.Entities.Junction;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.FIU
{
    [Table("FIUProgramActivities")]
    public class FIUProgramActivity : AuditableBaseEntity
    {          

        // Foreign Keys
        [Required]
        public int UnitLocationId { get; set; }

        [Required]
        public int OrganizationId { get; set; }

        [Required]
        public int FIUActivitiesId { get; set; }

        // Activity Data
        [Required]
        [Range(1, 100000, ErrorMessage = "Number must be between 1 and 100000")]
        public int Number { get; set; }

        [MaxLength(500)]
        public string? UploadMediaUrl { get; set; }

        [MaxLength(2000)]
        public string? Remarks { get; set; }

        // Status Management
        [Required]
        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        public DateTimeOffset? SubmittedAt { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        // Navigation Properties (NO [JsonIgnore] - Clean Architecture!)
        [ForeignKey(nameof(UnitLocationId))]
        public virtual OrganizationUnitLocation UnitLocation { get; set; } = null!;

        [ForeignKey(nameof(OrganizationId))]
        public virtual Organization Organization { get; set; } = null!;

        [ForeignKey(nameof(FIUActivitiesId))]
        public virtual FIUActivity FIUActivity { get; set; } = null!;

        [ForeignKey(nameof(ApprovedById))]
        public virtual User? ApprovedBy { get; set; }
    }
}