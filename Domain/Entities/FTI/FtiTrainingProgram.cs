namespace Domain.Entities.FTI
{
    public class FtiTrainingProgram:ReportEntryBaseEntity
    {
        public required string OrganisationName { get; set; }
        public required string TrainingTitle { get; set; }
        public DateOnly Date {  get; set; }
        public TimeSpan Duration { get; set; }
        public int TrainingCount { get; set; }
        public int ParticipantCount {  get; set; }
    }
}
