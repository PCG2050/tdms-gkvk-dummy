

namespace Domain.Entities
{
    public class ReportRecord:AuditableBaseEntity
    {
        public required string Title { get; set; }
        public DateTimeOffset ReportStartTime { get; set; }
        public DateTimeOffset ReportEndTime { get; set; }
    }
}
