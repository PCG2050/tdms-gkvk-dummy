using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ASM
{
    public class ASMVisitorDetails : AuditableBaseEntity
    {
    
        [StringLength(200)]
        public string? InstituteName { get; set; } 

        public int FarmersCount { get; set; }
        public int StudentsCount { get; set; }
        public int PublicCount { get; set; }       
        public DateOnly SubmittedDate { get; set; }

        [Required]
        public int UnitLocationId { get; set; }

        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation? UnitLocation { get; set; }

        [Required]
        public int OrganizationId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(OrganizationId))]
        public Organization? Organization { get; set; }
        

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
