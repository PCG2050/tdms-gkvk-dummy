using Domain.Entities.Junction;

namespace Domain.Entities
{
    public abstract class ReportEntryBaseEntity:AuditableBaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Attachements { get; set; } = null; //comma seperated urls of assets
        public int UnitLocationId {  get; set; }
        public int OrganizationId { get; set; }
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        public Organization Organization { get; set; } = null!;
    }
}
