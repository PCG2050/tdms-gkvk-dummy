namespace Domain.Entities
{
    public abstract class ReportEntryBaseEntity:AuditableBaseEntity
    {
        public int ReportRecordId { get; set; }
        public ReportRecord ReportRecord { get; set; } = null!;
        public int UnitId {  get; set; }
        public Unit Unit { get; set; } = null!;
    }
}
