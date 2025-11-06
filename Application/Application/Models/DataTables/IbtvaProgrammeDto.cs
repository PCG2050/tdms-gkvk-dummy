namespace Application.Models.DataTables
{
    public class IbtvaProgrammeDto: IbtvaProgrammeCreateDto
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
    }
    public class IbtvaProgrammeCreateDto : BaseEntryCreateDto
    {
        public required string TrainingTitle { get; set; }
        public TimeSpan Duration { get; set; }
        public int ParticipantCount { get; set; }
    }
    public class IbtvaProgrammeUpdateDto : BaseEntryUpdateDto
    {
        public string? TrainingTitle { get; set; }
        public TimeSpan? Duration { get; set; }
        public int? ParticipantCount { get; set; }
    }
}
