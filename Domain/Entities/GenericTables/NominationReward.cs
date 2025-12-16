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

namespace Domain.Entities.GenericTables
{

    public class NominationReward : AuditableBaseEntity
    {
        //Unitlocaiton and Organization References
        [Required]
        public int UnitLocationId { get; set; }

        public int OrganizationId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(UnitLocationId))]
        public OrganizationUnitLocation UnitLocation { get; set; } = null!;
        [JsonIgnore]
        public Organization Organization { get; set; } = null!;

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        // === Foreign Keys & Navigation Properties ===
        public int? TypeId { get; set; }

        public NominationType? Type { get; set; }

        public string? OtherType { get; set; }

        public int? RegionId { get; set; }

        public Region? Region { get; set; }

        public string? OtherRegion { get; set; }

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
        // ==================================

        // === Child Collections ===
        public ICollection<NominationRewardIFSFarmer> NominationRewardIFSFarmers { get; set; } = new List<NominationRewardIFSFarmer>();
        public ICollection<NominationRewardFarmerInnovation> NominationRewardFarmerInnovations { get; set; } = new List<NominationRewardFarmerInnovation>();
        public ICollection<NominationRewardOrganicFarmer> NominationRewardOrganicFarmers { get; set; } = new List<NominationRewardOrganicFarmer>();
        public ICollection<NominationRewardIFSEnterpreneur> NominationRewardIFSEntrepreneurs { get; set; } = new List<NominationRewardIFSEnterpreneur>();
        public ICollection<NominationRewardEntrepreneurInnovation> NominationRewardEntrepreneurInnovations { get; set; } = new List<NominationRewardEntrepreneurInnovation>();
        public ICollection<NominationRewardOrganicEntrepreneur> NominationRewardOrganicEntrepreneurs { get; set; } = new List<NominationRewardOrganicEntrepreneur>();
        public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
        public ICollection<AwardRecognition> AwardRecognitions { get; set; } = new List<AwardRecognition>();
        public ICollection<UniversitySanctionLetterPaperPoster> UniversitySanctionLetterPaperPosters { get; set; } = new List<UniversitySanctionLetterPaperPoster>();
        public ICollection<AwardPhoto> AwardPhotos { get; set; } = new List<AwardPhoto>();

    }

    // === Child Entities ===
    public class NominationRewardIFSFarmer : AuditableBaseEntity
    {
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }
        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = null!;
    }

    public class NominationRewardFarmerInnovation : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public string? DetailsOfInnovation { get; set; }

        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = null!;
    }

    public class NominationRewardOrganicFarmer : AuditableBaseEntity
    {
        public string? NameAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public string? CropsGrown { get; set; }

        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class NominationRewardIFSEnterpreneur : AuditableBaseEntity
    {
        public string? NameAddress { get; set; }
        public string? Phone { get; set; }
        public string? ComponentOfIFS { get; set; }

        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class NominationRewardEntrepreneurInnovation : AuditableBaseEntity
    {
        public string? Type { get; set; }
        public string? NameAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public string? DetailsOfInnovation { get; set; }

        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class NominationRewardOrganicEntrepreneur : AuditableBaseEntity
    {
        public string? NameAddress { get; set; }
        public int? PhoneNumber { get; set; }
        public string? CropsGrown { get; set; }

        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class Achievement : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? AchievementDetail { get; set; }
        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class AwardRecognition : AuditableBaseEntity
    {
        public string? AwardName { get; set; }

        public int? ContributionId { get; set; }

        public Contribution? Contribution { get; set; }
        public string? OtherContribution { get; set; }
        public string? AwardingAgency { get; set; }
        public string? InstitutionName { get; set; }
        public string? InstitutionAddress { get; set; }
        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class UniversitySanctionLetterPaperPoster : AuditableBaseEntity
    {
        public DateOnly? SanctionLetterDate { get; set; }
        public string? SanctionLetterFilePath { get; set; }
        public DateOnly? PaperDate { get; set; }
        public string? PaperFilePath { get; set; }
        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }

    public class AwardPhoto : AuditableBaseEntity
    {
        public string? AwardReceivingPhoto { get; set; }
        public string? AwardReceivingCertificate { get; set; }
        [Required]
        public int NominationRewardId { get; set; }
        [JsonIgnore]
        public NominationReward NominationReward { get; set; } = default!;
    }
}
