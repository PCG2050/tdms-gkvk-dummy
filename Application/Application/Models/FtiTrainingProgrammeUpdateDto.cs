using Application.Interface;

namespace Application.Models
{
    public class FtiTrainingProgrammeUpdateDto:IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? OrganisationName { get; set; }
        public string? TrainingTitle { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? TrainingCount { get; set; }
        public int? ParticipantCount { get; set; }
    }
}
