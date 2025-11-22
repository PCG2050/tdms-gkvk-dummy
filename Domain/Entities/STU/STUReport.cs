namespace Domain.Entities.STU
{
    public class StuReport : AuditableBaseEntity
    {
        [Required]
        public int? StuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StuProgramDetailsId))]
        public StuProgramDetails? ProgramDetails { get; set; }

        [MaxLength(4)]
        public string? ReportingYear { get; set; }

        public DateOnly? ReportDate { get; set; }

        [MaxLength(500)]
        public string? ProgressReport { get; set; }

        [MaxLength(500)]
        public string? GeoTaggedPhoto { get; set; }

        // Video path or URL
        [MaxLength(500)]
        public string? ReportingVideo { get; set; }

        // Text explanation, can be long
        [MaxLength(2000)]
        public string? Outcome { get; set; }

        public DateOnly? TestingCompletionDate { get; set; }

        // File upload for testing completion
        [MaxLength(500)]
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }

        // File upload for completion certificate
        [MaxLength(500)]
        public string? ProjectCompletionLetter { get; set; }

        // Dropdown text — short
        [MaxLength(150)]
        public string? TypeOfReport { get; set; }

        // Special remarks or additional context
        [MaxLength(500)]
        public string? SpclReport { get; set; }

        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
