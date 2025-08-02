using Application.Interface;

namespace Application.Models.DataTables
{
    public class EeuTrainingProgrammeDto:EeuTrainingProgrammeCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    public class EeuTrainingProgrammeCreateDto : BaseEntryCreateDto
    {
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
    }

    public class EeuTrainingProgrammeUpdateDto:BaseEntryUpdateDto
    {
        public string? TrainingTitle { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? TrainingCount { get; set; }
        public int? ParticipantCount { get; set; }
    }
}
