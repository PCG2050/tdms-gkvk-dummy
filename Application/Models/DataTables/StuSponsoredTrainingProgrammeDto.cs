namespace Application.Models.DataTables
{
    public class StuSponsoredTrainingProgrammeDto : StuSponsoredTrainingProgrammeCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }

    public class StuSponsoredTrainingProgrammeCreateDto
    {
        public int UnitLocationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string SponsoredOrganization { get; set; }
        public string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
        public string Attachements { get; set; }
    }

    public class StuSponsoredTrainingProgrammeUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? SponsorOrganization { get; set; }
        public string? TrainingTitle { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? TrainingCount { get; set; }
        public int? ParticipantCount { get; set; }
        public string? Attachements { get; set; }
    }
}
