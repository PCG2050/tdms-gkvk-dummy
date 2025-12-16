using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.DEU
{
    // ==================== PROGRAM DETAILS (Section A) ====================
    public class DeuProgramDetailsDto
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
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }
        public decimal? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? SourceName { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
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
    public class DeuProgramCreateDto
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
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }
        public decimal? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
        public string? Attachements { get; set; }
    }

    public class DeuProgramUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
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
        public string? PiAddress { get; set; }
        public int? BatchNo { get; set; }
        public int? Area { get; set; }
        public string? OrganizerBroucherFile { get; set; }
        public string? OrganizerInstitutionName { get; set; }
        public string? OrganizerInstitutionAddress { get; set; }
        public int? SourceId { get; set; }
        public string? OtherSourceOfInformation { get; set; }
        public string? SourceOfTitle { get; set; }
        public DateOnly? ProposalDate { get; set; }
        public string? ProposalUploadFile { get; set; }
        public DateOnly? UniversitySanctionLetterDate { get; set; }
        public string? UniversitySanctionLetterUploadFile { get; set; }
        public DateOnly? ProjectSanctionDate { get; set; }
        public string? ProjectSanctionFile { get; set; }
        public DateOnly? UniImplDate { get; set; }
        public string? UniImplLetterFile { get; set; }
        public string? FundReleaseYear { get; set; }
        public double? FundAmount { get; set; }
        public DateOnly? FundReleaseDate { get; set; }
        public string? FundReleaseFile { get; set; }
        public DateOnly? FundsSanctionLetterDate { get; set; }
        public string? FundsSanctionLetterUploadFile { get; set; }
        public string? ReportingVideo { get; set; }
        public string? Attachements { get; set; }
    }




    public class DeuProgramCompleteDto
    {
        public DeuProgramDetailsDto ProgramDetails { get; set; }
        public List<DeuParticipantDemographicsDto>? Demographics { get; set; }
        public List<DeuProgramContentDto>? ProgramContent { get; set; }
        public DeuAdvisoryServicesDto? AdvisoryServices { get; set; }

        public DeuReportDto? Report { get; set; } // For other categories
        public DeuRecommendationDto? Recommendation { get; set; }
    }
    /// <summary>
    /// Lightweight DTO for list/search results
    /// </summary>
    public class DeuProgramListItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? CategoryName { get; set; }
        public string? TypeName { get; set; }
        public string? Location { get; set; }
        public string FormStatus { get; set; } = "Draft";
        public string? CreatedByName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? UnitName { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS (Section B) ====================

    public class DeuParticipantDemographicsDto
    {
        public int Id { get; set; }
        public int DeuProgramDetailsId { get; set; }
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


    public class DeuParticipantDemographicsCreateDto
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

    public class DeuParticipantDemographicsUpdateDto : IUpdateDto
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
    public class DeuProgramContentDto
    {
        public int Id { get; set; }
        public int DeuProgramDetailsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<DeuResourcePersonDto>? ResourcePersons { get; set; }
        public List<DeuTopicsCoveredDto>? TopicsCovered { get; set; }
        public List<DeuTeachingAidsDto>? TeachingAids { get; set; }
    }

    public class DeuProgramContentCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    /// <summary>
    /// Composite DTO for creating DeuProgramContentAndResources along with all child entities in a single transaction
    /// </summary>
    public class DeuProgramContentWithChildrenCreateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<DeuResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<DeuTopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<DeuTeachingAidsCreateDto>? TeachingAids { get; set; }
    }

    /// <summary>
    /// Hybrid item for Resource Person - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class DeuResourcePersonHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    /// <summary>
    /// Hybrid item for Topics Covered
    /// </summary>
    public class DeuTopicsCoveredHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Teaching Aids
    /// </summary>
    public class DeuTeachingAidsHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating DeuProgramContentAndResources with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id: CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class DeuProgramContentWithChildrenUpdateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id: update it
        // If item has no Id: create it
        // If existing item not in array: delete it
        public List<DeuResourcePersonHybridDto>? ResourcePersons { get; set; }
        public List<DeuTopicsCoveredHybridDto>? TopicsCovered { get; set; }
        public List<DeuTeachingAidsHybridDto>? TeachingAids { get; set; }
    }

    // Resource Person DTOs
    public class DeuResourcePersonDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class DeuResourcePersonCreateDto
    {
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class DeuResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    // Topics Covered DTOs
    public class DeuTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class DeuTopicsCoveredCreateDto
    {
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class DeuTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    // Teaching Aids DTOs
    public class DeuTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? TypeOfAidName { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class DeuTeachingAidsCreateDto
    {
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class DeuTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ============================
    // ADVISORY SERVICES DTOs
    // ============================
    public class DeuAdvisoryServicesDto
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

    public class DeuAdvisoryServicesCreateDto
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

    public class DeuAdvisoryServicesUpdateDto : IUpdateDto
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

    public class DeuReportDto
    {
        public int Id { get; set; }
       

        //public string? ReportingYear { get; set; }
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }

    public class DeuReportCreateDto
    {
       
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }

    public class DeuReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
       
        public DateOnly? ReportDate { get; set; }

        public string? ProgressReport { get; set; }

        public string? GeoTaggedPhoto { get; set; }
        public string? ReportingVideo { get; set; }

        public string? Outcome { get; set; }
        public DateOnly? TestingCompletionDate { get; set; }
        public string? TestingCompletionLetter { get; set; }

        public DateOnly? ProjectCompletionDate { get; set; }
        public string? ProjectCompletionLetter { get; set; }

        public string? TypeOfReport { get; set; }
        public string? SpclReport { get; set; }
    }


    // ==================== RECOMMENDATIONS (Section F) ====================

    public class DeuRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }

        public string? UploadVideoUrl { get; set; }
    }

    public class DeuRecommendationCreateDto
    {
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }

        public string? UploadVideoUrl { get; set; }
    }

    public class DeuRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }

        public string? UploadVideoUrl { get; set; }
    }
}