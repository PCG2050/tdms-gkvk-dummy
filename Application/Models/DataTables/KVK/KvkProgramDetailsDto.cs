
using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.KVK
{
    // ============================
    // PROGRAM DETAILS DTOs
    // ============================
    public class KvkProgramDetailsDto
    {
        public int Id { get; set; }
        public int UnitLocationId { get; set; }
        public int OrganizationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public string? ProgramTypeName { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeName { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaName { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? ModeName { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public string? SourceOfFundName { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public int? BatchNo { get; set; }
        public decimal Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? SourceName { get; set; }
        public DateTime? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateTime? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateTime? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? Attachements { get; set; }

        // Status tracking
        public string FormStatus { get; set; } = "Draft";
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }

        // Audit
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? CreatedByName { get; set; }

        // Navigation
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }
    }

    public class KvkProgramCreateDto
    {
        [Required]
        public int UnitLocationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public int? BatchNo { get; set; }
        public decimal Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public DateTime? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateTime? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateTime? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? Attachements { get; set; }
    }

    public class KvkProgramUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public int? ProgramTypeId { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryOther { get; set; }
        public int? TypeId { get; set; }
        public string? TypeOther { get; set; }
        public int? ThemeId { get; set; }
        public string? ThemeOther { get; set; }
        public int? ThematicAreaId { get; set; }
        public string? ThematicAreaOther { get; set; }
        public int? SponsoredOrganization { get; set; }
        public string? SponsoredOrganizationName { get; set; }
        public string? Title { get; set; }
        public int? ModeId { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? Funds { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public int? BatchNo { get; set; }
        public decimal? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public DateTime? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateTime? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateTime? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? Attachements { get; set; }
    }

    public class KvkProgramCompleteDto
    {
        public KvkProgramDetailsDto ProgramDetails { get; set; }
        public List<KvkParticipantDemographicsDto>? Demographics { get; set; }
        public List<KvkProgramContentDto>? ProgramContent { get; set; }
        public KvkAdvisoryServicesDto? AdvisoryServices { get; set; }
        public KvkResultDto? Results { get; set; } // For FLD/OFT categories
        public KvkReportDto? Report { get; set; } // For other categories
        public KvkRecommendationDto? Recommendation { get; set; }
    }

    // ============================
    // DEMOGRAPHICS DTOs
    // ============================
    public class KvkParticipantDemographicsDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public int? ParticipantId { get; set; }
        public string? ParticipantName { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    public class KvkParticipantDemographicsCreateDto
    {
        public int? ParticipantId { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    public class KvkParticipantDemographicsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? ParticipantId { get; set; }
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }
        public int? Total { get; set; }
    }

    // ============================
    // PROGRAM CONTENT DTOs
    // ============================
    public class KvkProgramContentDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<KvkResourcePersonDto>? ResourcePersons { get; set; }
        public List<KvkTopicsCoveredDto>? TopicsCovered { get; set; }
        public List<KvkTeachingAidsDto>? TeachingAids { get; set; }
    }

    public class KvkProgramContentCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    /// <summary>
    /// Composite DTO for creating KvkProgramContentAndResources along with all child entities in a single transaction
    /// </summary>
    public class KvkProgramContentWithChildrenCreateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<KvkResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<KvkTopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<KvkTeachingAidsCreateDto>? TeachingAids { get; set; }
    }

    // Resource Person DTOs
    public class KvkResourcePersonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class KvkResourcePersonCreateDto
    {
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class KvkResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    // Topics Covered DTOs
    public class KvkTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkTopicsCoveredCreateDto
    {
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class KvkTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    // Teaching Aids DTOs
    public class KvkTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? TypeOfAidName { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    public class KvkTeachingAidsCreateDto
    {
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    public class KvkTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    // ============================
    // ADVISORY SERVICES DTOs
    // ============================
    public class KvkAdvisoryServicesDto
    {
        public int Id { get; set; }
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }

    public class KvkAdvisoryServicesCreateDto
    {
        public int? NoOfFacebookSMS { get; set; }
        public int? NoOfSMSSentToRegisteredFarmers { get; set; }
        public int? NoOfWhatsappGroups { get; set; }
        public int? NoOfWhatsappSMS { get; set; }
        public int? NoOfAnsweredWhatsappQueries { get; set; }
        public int? NoOfPhoneCalls { get; set; }
        public int? NoOfFaceToFaceDiscussions { get; set; }
        public int? NoOfGroupDiscussions { get; set; }
        public int? NoOfEmailsSent { get; set; }
        public int? NoOfNewspaperCoverage { get; set; }
        public int? NoOfBeneficiaries { get; set; }
    }

    // ============================
    // RESULT DTOs (FLD/OFT)
    // ============================
    public class KvkResultDto
    {
        public int Id { get; set; }
        public int KvkProgramDetailsId { get; set; }
        public string? UploadExcelUrl { get; set; }
        public List<KvkFldResultDto>? FldResults { get; set; }
        public List<KvkOftResultDto>? OftResults { get; set; }
    }

    public class KvkFldResultDto
    {
        public int Id { get; set; }
        public int KvkResultId { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? DetailsOfDemoName { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkFldResultCreateDto
    {
        public int DetailsOfDemoId { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkFldResultUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int DetailsOfDemoId { get; set; }
        public int FldNumber { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultDto
    {
        public int Id { get; set; }
        public int KvkResultId { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? DetailsOfDemoName { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultCreateDto
    {
        public int DetailsOfDemoId { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    public class KvkOftResultUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int DetailsOfDemoId { get; set; }
        public string? Parameter1 { get; set; }
        public string? Observation1 { get; set; }
        public string? Parameter2 { get; set; }
        public string? Observation2 { get; set; }
        public string? Parameter3 { get; set; }
        public string? Observation3 { get; set; }
        public string? Parameter4 { get; set; }
        public string? Observation4 { get; set; }
        public string? Parameter5 { get; set; }
        public string? Observation5 { get; set; }
        public decimal? Yield { get; set; }
        public decimal? GrossCost { get; set; }
        public decimal? GrossReturns { get; set; }
        public decimal? NetReturns { get; set; }
        public string? BC { get; set; }
    }

    // ============================
    // REPORT DTOs (Non-FLD/OFT)
    // ============================
    public class KvkReportDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class KvkReportCreateDto
    {
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    // ============================
    // RECOMMENDATION DTOs
    // ============================
    public class KvkRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class KvkRecommendationCreateDto
    {
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }
}