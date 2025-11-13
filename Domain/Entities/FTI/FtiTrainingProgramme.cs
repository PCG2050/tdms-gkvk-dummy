namespace Domain.Entities.FTI
{
    public class FtiTrainingProgramme:ReportEntryBaseEntity
    {
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount { get; set; }
    }
}
