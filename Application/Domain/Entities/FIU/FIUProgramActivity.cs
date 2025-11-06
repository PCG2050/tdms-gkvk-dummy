using Domain.Entities.Junction;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FIU
{
    public class FIUProgramActivity : AuditableBaseEntity
    {
        [Required]
        public int UnitLocationId { get; set; }

        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation? UnitLocation { get; set; }

       
        public int OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }
        public int? FIUActivitiesId { get; set; }

        [ForeignKey(nameof(FIUActivitiesId))]
        public FIUActivity? FIUActivities { get; set; } 

        public int? Number { get; set; }

        public string? UploadMediaUrl { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
  

        // ===== STATUS TRACKING FIELDS =====
        // Status: "Draft", "Pending", "Approved", "Rejected"
        [MaxLength(50)]
        public string FormStatus { get; set; } = "Draft";

        [MaxLength(1000)]
        public string? FormStatusRemarks { get; set; }

        // Approval tracking
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }

        [JsonIgnore]
        public User? ApprovedBy { get; set; }

    }
}
