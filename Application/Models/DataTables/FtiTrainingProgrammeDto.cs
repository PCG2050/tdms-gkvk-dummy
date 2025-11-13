using Application.Interface;

namespace Application.Models.DataTables
{
    public class FtiTrainingProgrammeDto:FtiTrainingProgrammeCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class FtiTrainingProgrammeCreateDto : BaseEntryCreateDto
    {
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
    }

    public class FtiTrainingProgrammeUpdateDto:BaseEntryUpdateDto
    {
        public string? TrainingTitle { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? TrainingCount { get; set; }
        public int? ParticipantCount { get; set; }
    }
}
