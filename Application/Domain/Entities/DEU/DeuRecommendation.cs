
namespace Domain.Entities.DEU
{
    public class DeuRecommendation :AuditableBaseEntity
    {
        [Required]
        public int? DeuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(DeuProgramDetailsId))]
        public DeuProgramDetails? ProgramDetails { get; set; }

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
    }
}
