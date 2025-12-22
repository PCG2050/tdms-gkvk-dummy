
namespace Domain.Entities.SAMETI
{
    public class SametiProgramDetails : ReportEntryBaseEntity
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


        [MaxLength(250)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public int? ModeId { get; set; }
        [JsonIgnore]
        public Mode? Mode { get; set; }

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
        public SourceOfFund? SourceOfFund { get; set; }

        public int? Funds { get; set; }

        [MaxLength(100)]
        public int? StatusId { get; set; }
        [JsonIgnore]
        public Status? Status { get; set; }

        public decimal? TotalOutlayRs { get; set; }
        //this is for project dropdown it has to be linked to masterdata table
        public string? Copi { get; set; }



        [MaxLength(500)]
        public string? PiAddress { get; set; }

        [MaxLength(100)]
        public int? BatchNo { get; set; }

        public decimal? Area { get; set; }


        public string? OrganizerBroucherFile { get; set; }

        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }

        public int? SourceId { get; set; }
        [JsonIgnore]
        public ParticipatedSource? Source { get; set; }

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
        public string? ReportingVideo { get; set; }

        //New Fields
        public int? CollaboratorId { get; set; }
        [JsonIgnore]
        public Collaborator? Collaborator { get; set; }

        public string? CollaboratorOther { get; set; }

        public int? CollaborativeProgramOptionId { get; set; }
        [JsonIgnore]
        public CollaborativeProgramOption? CollaborativeProgramOption { get; set; }

        public string? CollaborativeProgramOptionOther { get; set; }

        public int? ParticipatedAsId { get; set; }
        [JsonIgnore]
        public Participant? ParticipatedAs { get; set; }

        public string? ParticipantFileUpload { get; set; }

        public ICollection<SametiParticipantDemographics>? ParticipantDemographics { get; set; }

        public ICollection<SametiProgramContentAndResources>? ProgramContent { get; set; }
        public SametiAdvisoryServices? AdvisoryServices { get; set; }
        public SametiRecommendation? Recommendations { get; set; }
        public SametiReport? Reports { get; set; }


    }
}
