
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;


namespace Domain.Entities.IBTVA
{
    public class IbtvaRecommendation : AuditableBaseEntity
    {

        [Required]
        public int? IbtvaProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IbtvaProgramDetailsId))]
        public IbtvaProgramDetails? ProgramDetails { get; set; }

        // Recommendation fields
        [MaxLength(1000)]
        public string? ProblemsIdentified { get; set; }

        [MaxLength(1000)]
        public string? Recommendation { get; set; }

        [MaxLength(1000)]
        public string? ActionTaken { get; set; }

        [MaxLength(1000)]
        public string? SignificantAchievement { get; set; }

        [MaxLength(1000)]
        public string? SuccessStories { get; set; }

        [MaxLength(1000)]
        public string? ImpactOutcome { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

        //new field addded 
        [MaxLength(1000)]
        public string? UploadVideoUrl { get; set; }
    }
}
