using Domain.Entities.Junction;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public abstract class ReportEntryBaseEntity:AuditableBaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Attachements { get; set; } = null; //comma seperated urls of assets
        public int UnitLocationId {  get; set; }
        public int OrganizationId { get; set; }
        [JsonIgnore]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        [JsonIgnore]
        public Organization Organization { get; set; } = null!;
    }
}
