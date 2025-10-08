using Domain.Entities.FIU;
using Domain.Entities.FTI;
using Domain.Entities.KVK;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.STU
{
    public class STUProgramDetails : AuditableBaseEntity
    {
        public int ProgramTypeId { get; set; }
        // Foreign keys & “Other” text fields
        public int CategoryId { get; set; }
        [MaxLength(200)]
        public string? CategoryOther { get; set; }

        [MaxLength(100)]
        public int? Type { get; set; }
        [MaxLength(200)]
        public string? TypeOther { get; set; }

        [MaxLength(150)]
        public int? Theme { get; set; }
        [MaxLength(200)]
        public string? ThemeOther { get; set; }

        [MaxLength(150)]
        public int? ThematicArea { get; set; }
        [MaxLength(200)]
        public string? ThematicAreaOther { get; set; }

        [MaxLength(200)]
        public int? SponsoredOrganization { get; set; }

        [MaxLength(200)]
        public string? SponsoredOrganizationName { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public int? Mode { get; set; }

        // Dates
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }

        [MaxLength(100)]
        public string? Duration { get; set; }

        [MaxLength(150)]
        public int? Region { get; set; }
        [MaxLength(200)]
        public string? RegionOther { get; set; }

        [MaxLength(100)]
        public int? TPNo { get; set; }

        [MaxLength(250)]
        public string? Location { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalOutlayRs { get; set; }

        [MaxLength(100)]
        public int? Status { get; set; }

        [MaxLength(100)]
        public int? BatchNo { get; set; }

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

        [MaxLength(500)]
        public string? UploadVideo { get; set; }

        [MaxLength(100)]
        public int? SD { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AreaHa { get; set; }

        [MaxLength(500)]
        public string? OrganizerFileUpload { get; set; }

        [MaxLength(250)]
        public string? OrganizerInstitutionName { get; set; }

        [MaxLength(500)]
        public string? OrganizerAddress { get; set; }

        // Participation details
        [MaxLength(250)]
        public string? Participation { get; set; }

        [MaxLength(250)]
        public string? ParticipatedAs { get; set; }

        [MaxLength(500)]
        public string? ParticipationFileLink { get; set; }

        // Source of Information
        [MaxLength(250)]
        public string? SourceOfInformation { get; set; }

        // Thesis / Project / Paper / Others
        [MaxLength(500)]
        public string? TitleOfThesisOrProjectOrPaperOrOthers { get; set; }

        // Paper / Poster / Abstract details
        [MaxLength(100)]
        public string? PaperPosterAbstract { get; set; }

        public DateTime? PaperPosterAbstractDate { get; set; }

        [MaxLength(500)]
        public string? PaperPosterAbstractLink { get; set; }


        public ICollection<STURecommendation> Recommendations { get; set; }

        public ICollection<STUReport> Reports { get; set; }

        public ICollection<STUAdvisoryServices> AdvisoryServices { get; set; }
    }
}
