using Domain.Entities.Junction;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.GenericTables.Service
{
    public class TblService : AuditableBaseEntity
    {

        [Required]
        public int UnitLocationId { get; set; }

        public int OrganizationId { get; set; }
        [JsonIgnore]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        [JsonIgnore]
        public Organization Organization { get; set; } = null!;

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // Foreign keys
        public int? CategoryId { get; set; }

        public ServiceCategory? Category { get; set; }
        [MaxLength(200)]
        public string? OtherCategory { get; set; }

        public int? ThemeId { get; set; }

        public ServiceTheme? Theme { get; set; }
        [MaxLength(200)]
        public string? OtherTheme { get; set; }

        [MaxLength(250)]
        public string? CropPlantProductName { get; set; }

        [MaxLength(150)]
        public string? Variety { get; set; }

        public int? SourceOfFundId { get; set; }

        public SourceOfFund? SourceOfFund { get; set; }

        [MaxLength(200)]
        public string? OtherSourceOfFund { get; set; }

        [MaxLength(250)]
        public string? Component { get; set; }

        public int? QuantityUnitId { get; set; }

        public QuantityUnit? QuantityUnit { get; set; }

        public int Number { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountGenerated { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AmountReleased { get; set; }

        //Training Hall category
        public DateOnly? Date { get; set; }

        [MaxLength(250)]
        public string? RentedTo { get; set; }

        [MaxLength(250)]
        public string? TitleOfActivityConducted { get; set; }

        // New visitor and participation fields
        public int? VisitorId { get; set; }
        [ForeignKey(nameof(VisitorId))]
        public Visitor? Visitor { get; set; }

        public int? ParticipationTypeId { get; set; }
        [ForeignKey(nameof(ParticipationTypeId))]
        public ParticipationType? ParticipationType { get; set; }

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

        // Navigation
        public ICollection<TableHostel>? Hostels { get; set; }
        public ICollection<RevolvingFundStatus>? RevolvingFundStatuses { get; set; }
        public ICollection<VisitorDetail>? VisitorDetails { get; set; }


    }
}
