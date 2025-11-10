namespace Domain.Entities.STU
{
    public class StuReport : AuditableBaseEntity
    {
        [Required]
        public int? StuProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StuProgramDetailsId))]
        public StuProgramDetails? ProgramDetails { get; set; }

        // Report details
        [MaxLength(150)]
        public string? ProgressReportReportingYear { get; set; }

        public DateTime? Date { get; set; }

        // File / media paths or URLs
        [MaxLength(500)]
        public string? UploadPhoto { get; set; }

        [MaxLength(500)]
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }

        [MaxLength(500)]
        public string? UploadVideo { get; set; }

        [MaxLength(1000)]
        public string? SignificantOutcome { get; set; }
        public int? UnitLocationId { get; set; }

        public int? OrganizationId { get; set; }

    }
}
