using Domain.Entities.Junction;

namespace Domain.Entities
{
    public abstract class ReportEntryBaseEntity:AuditableBaseEntity
    {
        public DateOnly Date { get; set; }
        public int UnitLocationId {  get; set; }
        public int OrganizationId { get; set; }
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
    }
}
