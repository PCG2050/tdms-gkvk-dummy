

namespace Domain.Entities.FTI
{
    public class FtiRecommendation : AuditableBaseEntity
    {
        // FK to parent KVK program
        [Required]
<<<<<<< Updated upstream
        public int FTIProgramDetailsID { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails ProgramDetails { get; set; }
=======
        public int? FtiProgramDetailsId { get; set; }

        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
>>>>>>> Stashed changes

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
    }
}
