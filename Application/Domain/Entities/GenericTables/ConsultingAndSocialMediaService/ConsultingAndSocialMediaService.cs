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

namespace Domain.Entities.GenericTables.ConsultingAndSocialMediaService
{
    public class ConsultingAndSocialMediaService : AuditableBaseEntity
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
        // Foreign Keys
        public int? CategoryId { get; set; }
        [JsonIgnore]
        public ConsultancyServicesCategory? Category { get; set; }
        public int? RelatedToId { get; set; }
        [JsonIgnore]
        public RelatedTo? RelatedTo { get; set; }

        [MaxLength(250)]
        public string? RelatedToDisciplineOther { get; set; }

        public int? ExtensionActivityId { get; set; }
        [JsonIgnore]
        public ExtensionWork? ExtensionActivity { get; set; }
        public int? ParticularsId { get; set; }
        [JsonIgnore]
        public Particular? Particulars { get; set; }

        [MaxLength(250)]
        public string? ParticularsOthers { get; set; }
        public int? ModeOrOutreachId { get; set; }
        [JsonIgnore]
        public ModeOutreach? ModeOutreach { get; set; }

        // Columns
        [MaxLength(250)]
        public string? Title { get; set; }
        
       
        [MaxLength(250)]
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(150)]
        public string? Publisher { get; set; }
        [MaxLength(500)]
        public string? WeblinkOrApplink { get; set; }
        [MaxLength(500)]
        public string? PublishedDocUrl { get; set; }

        //Counts Group modeOutreach
        public int PhoneCalls { get; set; }

        // Male categories
        public int Male_SC { get; set; }
        public int Male_ST { get; set; }
        public int Male_OBC { get; set; }
        public int Male_GEN { get; set; }

        // Female categories
        public int Female_SC { get; set; }
        public int Female_ST { get; set; }
        public int Female_OBC { get; set; }
        public int Female_GEN { get; set; }

        public int FacebookPostCount { get; set; }
        public int NoOfSms { get; set; }
        public int NoOfWhatsappGroup { get; set; }
        public int NoOfWhatsappMessage { get; set; }
        public int CountOfAnsweredQueries { get; set; }
        public int FaceToFace { get; set; }
        public int GroupDiscussion { get; set; }
        public int NoOfEmailSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
        public int NoOfPressVisit { get; set; }
        public int NoOfPressMeet { get; set; }
        public int NoOfPressCoverage { get; set; }
        public int NoOfTweets { get; set; }

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

        // Navigation to ModeAndOutreach details
        public ICollection<TableModeAndOutreach>? TableModeAndOutreaches { get; set; }
    }
}
