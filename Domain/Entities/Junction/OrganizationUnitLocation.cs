using System.Text.Json.Serialization;

namespace Domain.Entities.Junction
{
    public class OrganizationUnitLocation:AuditableBaseEntity
    {
        public int OrganizationId { get; set; }
        public int UnitId { get; set; }
        public int DistrictId { get; set; }
        public Organization Organization { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
        public District District { get; set; } = null!;

        [JsonIgnore]
        public ICollection<TrainerAssignment> TrainerAssignments { get; set; } = [];
        [JsonIgnore]
        public ICollection<UnitHeadAssignment> UnitHeadAssignments { get; set; } = []; // NEW

    }
}
