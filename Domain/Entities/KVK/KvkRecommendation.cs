
namespace Domain.Entities.KVK
{
    public class KvkRecommendation : AuditableBaseEntity
    {

        [Required]
        public int? KvkProgramDetailsId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KvkProgramDetailsId))]
        public KvkProgramDetails? ProgramDetails { get; set; }

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
        public string? UploadVideoUrl { get; set; } 


    }
}
