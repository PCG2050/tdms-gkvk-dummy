using Domain.Entities.FIU;
using Domain.Entities.KVK;
using Domain.Entities.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FTI
{
    public class FTIProgramDetails : AuditableBaseEntity
    {
        public int? ProgramTypeId { get; set; }
        // Foreign keys & “Other” text fields
        public int? CategoryId { get; set; }

        public ProgramCategory? Category { get; set; }

        [MaxLength(200)]
        public string? CategoryOther { get; set; }

        [MaxLength(100)]
        public int? TypeId { get; set; }

        public InfoType? Type { get; set; }

        
        [MaxLength(200)]
        public string? TypeOther { get; set; }

        public int? CollaboratorId { get; set; }

        public Collaborator? Collaborator { get; set; }

        public string? CollaboratorOther { get; set; }

        [MaxLength(150)]
        public int? Theme { get; set; }
        [MaxLength(200)]
        public string? ThemeOther { get; set; }

        [MaxLength(150)]
        public int? ThematicAreaId { get; set; }

        public ThematicArea? ThematicArea { get; set; }
        [MaxLength(200)]
        public string? ThematicAreaOther { get; set; }

        [MaxLength(200)]
        public int SponsoredOrganization { get; set; }

        [MaxLength(200)]
        public string? SponsoredOrganizationName { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public int? Mode { get; set; }

        // Dates
        public DateOnly? StartDate { get; set; }
        [Required]
        public DateOnly? EndDate { get; set; }

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

        public int? Area { get; set; }

        [MaxLength(500)]
        public string? PiAddress { get; set; }

        [MaxLength(500)]
        public string? OtherSourceOfInformation { get; set; }

        [MaxLength(500)]
        public string? SourceOfTitle { get; set; }

        // Proposal details
        public DateOnly? ProposalDate { get; set; }
        [MaxLength(500)]
        public string? ProposalUploadFile { get; set; }

        public DateOnly? UniversitySanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? UniversitySanctionLetterUploadFile { get; set; }

        public DateOnly? ProjectSanctionDate { get; set; }
        [MaxLength(500)]
        public string? ProjectSanctionFile { get; set; }

        public DateOnly? UniImplDate { get; set; }
        [MaxLength(500)]
        public string? UniImplLetterFile { get; set; }

        [MaxLength(100)]
        public string? FundReleaseYear { get; set; }

        public double? FundAmount { get; set; }

        public DateOnly? FundReleaseDate { get; set; }
        [MaxLength(500)]
        public string? FundReleaseFile { get; set; }

        public DateOnly? FundsSanctionLetterDate { get; set; }
        [MaxLength(500)]
        public string? FundsSanctionLetterUploadFile { get; set; }

        [MaxLength(500)]
        public string? UploadVideo { get; set; }

        [MaxLength(500)]
        public string? ReportingVideo { get; set; }

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

        public DateOnly? PaperPosterAbstractDate { get; set; }

        [MaxLength(500)]
        public string? PaperPosterAbstractLink { get; set; }


        public ICollection<FTIRecommendation> Recommendations { get; set; } = new List<FTIRecommendation>();
        public ICollection<FTIReport> Reports { get; set; } = new List<FTIReport>();
        public ICollection<FTIAdvisoryServices> AdvisoryServices { get; set; } = new List<FTIAdvisoryServices>();
        public ICollection<FTIParticipantDemographics> ParticipantDemographics { get; set; } = new List<FTIParticipantDemographics>();
        public ICollection<FTIProgramContentAndResources> ProgramContentAndResources { get; set; } = new List<FTIProgramContentAndResources>();
    }
}
