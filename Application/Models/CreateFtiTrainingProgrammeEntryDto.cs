namespace Application.Models
{
    public class CreateFtiTrainingProgrammeEntryDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public required string OrganisationName { get; set; }
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
    }
}
