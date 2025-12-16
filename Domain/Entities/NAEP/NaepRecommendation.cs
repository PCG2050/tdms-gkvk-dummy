
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;


namespace Domain.Entities.NAEP
{
    public class NaepRecommendation : AuditableBaseEntity
    {

        [Required]
        public int? NaepProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(NaepProgramDetailsId))]
        public NaepProgramDetails? ProgramDetails { get; set; }

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
