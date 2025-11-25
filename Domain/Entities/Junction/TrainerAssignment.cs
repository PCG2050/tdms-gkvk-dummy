using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Junction
{
    public class TrainerAssignment:AuditableBaseEntity
    {
        public int UnitLocationId { get; set; }
        public int TrainerId { get; set; }
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        public User Trainer { get; set; } = null!;

        // Soft delete fields
        public bool IsDeactivated { get; set; } = false;
        public DateTimeOffset? DeactivatedAt { get; set; }
        public int? DeactivatedById { get; set; }
    }
}
