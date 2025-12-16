namespace Domain.Entities.STU
{
    public class StuRecommendation : AuditableBaseEntity
    {
        // FK to parent KVK program
        [Required]
        public int? StuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StuProgramDetailsId))]
        public StuProgramDetails? ProgramDetails { get; set; }

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
