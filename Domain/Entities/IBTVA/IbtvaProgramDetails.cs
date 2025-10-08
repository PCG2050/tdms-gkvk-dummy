using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.IBTVA
{
    public class IbtvaProgramDetails : ReportEntryBaseEntity
    {
        public int? ProgramTypeId { get; set; }
        [JsonIgnore]
        public ProgramType? ProgramType { get; set; }
        // Foreign keys & “Other” text fields

        public int? CategoryId { get; set; }

        [JsonIgnore]
        public ProgramCategory? Category { get; set; }

        [MaxLength(200)]
        public string? CategoryOther { get; set; }

        [MaxLength(100)]
        public int? TypeId { get; set; }
        [JsonIgnore]
        public InfoType? Type { get; set; }


        [MaxLength(200)]
        public string? TypeOther { get; set; }

        [MaxLength(150)]
        public int? ThemeId { get; set; }
        [JsonIgnore]
        public Theme? Theme { get; set; }

        [MaxLength(200)]
        public string? ThemeOther { get; set; }

        [MaxLength(150)]
        public int? ThematicAreaId { get; set; }
        [JsonIgnore]
        public ThematicArea? ThematicArea { get; set; }
        [MaxLength(200)]
        public string? ThematicAreaOther { get; set; }

        [MaxLength(200)]
        public int? SponsoredOrganization { get; set; }

        [MaxLength(200)]
        public string? SponsoredOrganizationName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(100)]
        public int? Mode { get; set; }

        // Dates
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(100)]
        public string? Duration { get; set; }

        [MaxLength(150)]
        public int? RegionId { get; set; }

        [JsonIgnore]
        public Region? Region { get; set; }

        [MaxLength(200)]
        public string? RegionOther { get; set; }

        [MaxLength(100)]
        public int? TPNo { get; set; }

        [MaxLength(250)]
        public string? Location { get; set; }


        public int? SourceOfFundId { get; set; }

        [JsonIgnore]
        public SourceOfFund SourceOfFund { get; set; }

        public int? Funds { get; set; }

        [MaxLength(100)]
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }

        public decimal? TotalOutlayRs { get; set; }
        //this is for project dropdown it has to be linked to masterdata table
        public string? Copi { get; set; }



        [MaxLength(100)]
        public int? BatchNo { get; set; }


        public string? OrganizerBroucherFile { get; set; }

        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }

        public int? SourceId { get; set; }
        [JsonIgnore]
        public ParticipatedSource? Source { get; set; }

        // Proposal details
        public DateTime? ProposalDate { get; set; }
        [MaxLength(500)]
        public string? ProposalUploadFile { get; set; }

        public DateTime? UniversitySanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? UniversitySanctionLetterUploadFile { get; set; }

        public DateTime? FundsSanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? FundsSanctionLetterUploadFile { get; set; }

        public ICollection<IbtvaRecommendation> Recommendations { get; set; }

        public ICollection<IbtvaReport> Reports { get; set; }

        public ICollection<IbtvaAdvisoryServices> AdvisoryServices { get; set; }
    }
}
