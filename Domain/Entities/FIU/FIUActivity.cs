using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.FIU
{
    /// <summary>
    /// Master data table for FIU media coordination activity types
    /// </summary>
    [Table("FIUActivities")]
    public class FIUActivity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string ActivityName { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? ActivityDescription { get; set; }

        [Required]
        [MaxLength(100)]
        public string ActivityCategory { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? UnitOfMeasurement { get; set; }

        public bool RequiresMediaUpload { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }
        
        // Navigation Property
        public virtual ICollection<FIUProgramActivity> ProgramActivities { get; set; }
            = new List<FIUProgramActivity>();
    }
}