namespace Domain.Entities.FTI
{
<<<<<<< Updated upstream
    public  class FTIReport : AuditableBaseEntity
    {
        [Required]
        public int FTIProgramDetailsID { get; set; }

        [ForeignKey(nameof(FTIProgramDetailsID))]
        public FTIProgramDetails ProgramDetails { get; set; }
=======
    public class FtiReport : AuditableBaseEntity
    {
        [Required]
        public int? FtiProgramDetailsId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(FtiProgramDetailsId))]
        public FtiProgramDetails? ProgramDetails { get; set; }
>>>>>>> Stashed changes

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
    }
}
