using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.FTI
{
    // ==================== PROGRAM DETAILS (Section A) ====================

    /// <summary>
    /// Display DTO for FTI Program Details with all fields and display names
    /// </summary>
    public class FTIProgramDetailsDto
    {
        public int Id { get; set; }

        // --- Base Fields (same as CreateDto) ---

        public string? Title { get; set; }
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

        // --- Derived / Display Fields ---

        public int UnitLocationId { get; set; }

        public string UnitLocationName {get; set; }
        public string? CategoryName { get; set; }
        public string? ProgramTypeName { get; set; }
        public string? ThemeName { get; set; }
        public string? RegionName { get; set; }
        public string? ModeName { get; set; }
        public string? SourceOfFundName { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string? StateName { get; set; }

        // --- Metadata ---
        public string FormStatus { get; set; } = "Draft";

        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? CreatedByName { get; set; }

        public DateTimeOffset? ApprovedAt { get; set; }
        public int? ApprovedById { get; set; }
        public string? ApprovedByName { get; set; }
    }

    /// <summary>
    /// Create DTO for creating new FTI programs
    /// </summary>
    public class FTIProgramCreateDto
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        [Required]
        public int UnitLocationId { get; set; }

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


        [MaxLength(250)]
        public string? Title { get; set; }

        public int? Mode { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? NoOfCourses { get; set; }
        public string? Attachments { get; set; }


    }

    /// <summary>
    /// Update DTO for updating FTI programs with all fields nullable
    /// </summary>
    public class FTIProgramUpdateDto : IUpdateDto
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
        public int? Mode { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? NoOfCourses { get; set; }
        public string? Attachments { get; set; }
        public int? StatusId { get; set; }
        public decimal? TotalOutlayRs { get; set; }
        public string? Copi { get; set; }
        public int? BatchNo { get; set; }
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

    }




    /// <summary>
    /// Complete DTO including all child collections for FTI Program
    /// </summary>
    public class FTIProgramDetailsCompleteDto : FTIProgramDetailsDto
    {
        public List<FTIParticipantDemographicsDto>? Demographics { get; set; }
        public List<FTIProgramContentDto>? ProgramContent { get; set; }
        public FTIAdvisoryServicesDto? AdvisoryServices { get; set; }
        public FTIReportDto? Reports { get; set; }
        public FTIRecommendationDto? Recommendations { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS (Section B) ====================

    /// <summary>
    /// Display DTO for FTI Participant Demographics
    /// </summary>
    public class FTIParticipantDemographicsDto
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

    /// <summary>
    /// Create DTO for FTI Participant Demographics
    /// </summary>
    public class FTIParticipantDemographicsCreateDto
    {
        [Required]
        public int FTIProgramDetailsId { get; set; }
        public int? ParticipantId { get; set; }

        // Male counts
        public int? Male_SC { get; set; }
        public int? Male_ST { get; set; }
        public int? Male_OBC { get; set; }
        public int? Male_GEN { get; set; }

        // Male hostel
        public int? SC_Male_StayedInHostel { get; set; }
        public int? ST_Male_StayedInHostel { get; set; }
        public int? OBC_Male_StayedInHostel { get; set; }
        public int? GEN_Male_StayedInHostel { get; set; }

        // Female counts
        public int? Female_SC { get; set; }
        public int? Female_ST { get; set; }
        public int? Female_OBC { get; set; }
        public int? Female_GEN { get; set; }

        // Female hostel
        public int? SC_Female_StayedInHostel { get; set; }
        public int? ST_Female_StayedInHostel { get; set; }
        public int? OBC_Female_StayedInHostel { get; set; }
        public int? GEN_Female_StayedInHostel { get; set; }

        public int? Total { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Participant Demographics
    /// </summary>
    public class FTIParticipantDemographicsUpdateDto : IUpdateDto
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



    // ==================== PROGRAM CONTENT (Section C) ====================

    /// <summary>
    /// Create DTO for FTI Program Content
    /// </summary>
    public class FTIProgramContentCreateDto
    {
        [Required]
        public int FTIProgramDetailsId { get; set; }

    }

    /// <summary>
    /// Update DTO for FTI Program Content
    /// </summary>
    public class FTIProgramContentUpdateDto : IUpdateDto
    {
        public int Id { get; set; }

    }

    /// <summary>
    /// Display DTO for FTI Program Content with child collections
    /// </summary>
    public class FTIProgramContentDto
    {
        public int Id { get; set; }

        public List<FTIResourcePersonDto> ResourcePersons { get; set; }
        public List<FTITopicsCoveredDto> TopicsCovered { get; set; }
        public List<FTITeachingAidsDto> TeachingAids { get; set; }
    }

    // ==================== RESOURCE PERSONS (Section C - Subsection) ====================

    /// <summary>
    /// Create DTO for FTI Resource Person
    /// </summary>
    public class FTIResourcePersonCreateDto
    {
        [Required]
        public int FTIProgramContentAndResourcesId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Resource Person
    /// </summary>
    public class FTIResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    /// <summary>
    /// Display DTO for FTI Resource Person
    /// </summary>
    public class FTIResourcePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ResourceType { get; set; }
        public string Responsibility { get; set; }
    }

    // ==================== TOPICS COVERED (Section C - Subsection) ====================

    /// <summary>
    /// Create DTO for FTI Topics Covered
    /// </summary>
    public class FTITopicsCoveredCreateDto
    {
        [Required]
        public int FTIProgramContentAndResourcesId { get; set; }

        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Topics Covered
    /// </summary>
    public class FTITopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Display DTO for FTI Topics Covered
    /// </summary>
    public class FTITopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string PhotoUpload { get; set; }
    }

    // ==================== TEACHING AIDS (Section C - Subsection) ====================

    /// <summary>
    /// Create DTO for FTI Teaching Aids
    /// </summary>
    public class FTITeachingAidsCreateDto
    {
        [Required]
        public int FTIProgramContentAndResourcesId { get; set; }

        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Teaching Aids
    /// </summary>
    public class FTITeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Display DTO for FTI Teaching Aids
    /// </summary>
    public class FTITeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ==================== COMPOSITE DTOs - HYBRID PATTERN ====================

    /// <summary>
    /// Composite DTO for creating FTIProgramContentAndResources with all child entities in a single transaction
    /// </summary>
    public class FTIProgramContentWithChildrenCreateDto
    {
        // Parent fields (none for ProgramContent)

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<FTIResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<FTITopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<FTITeachingAidsCreateDto>? TeachingAids { get; set; }
    }

    /// <summary>
    /// Hybrid item for Resource Person - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class FTIResourcePersonHybridDto
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
    public class FTITopicsCoveredHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Teaching Aids
    /// </summary>
    public class FTITeachingAidsHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating FTIProgramContentAndResources with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id (null): CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class FTIProgramContentWithChildrenUpdateDto
    {
        // Parent fields (none for ProgramContent)

        // Child collections - Hybrid Pattern
        // If item has Id: update it
        // If item has no Id (null): create it
        // If existing item not in array: delete it
        public List<FTIResourcePersonHybridDto>? ResourcePersons { get; set; }
        public List<FTITopicsCoveredHybridDto>? TopicsCovered { get; set; }
        public List<FTITeachingAidsHybridDto>? TeachingAids { get; set; }
    }

    // ==================== ADVISORY SERVICES (Section D) ====================

    /// <summary>
    /// Create DTO for FTI Advisory Services
    /// </summary>
    public class FTIAdvisoryServicesCreateDto
    {
        [Required]
        public int FTIProgramDetailsId { get; set; }

        public int NoOfFacebookSMS { get; set; }
        public int NoOfSMSSentToRegisteredFarmers { get; set; }
        public int NoOfWhatsappGroups { get; set; }
        public int NoOfWhatsappSMS { get; set; }
        public int NoOfAnsweredWhatsappQueries { get; set; }
        public int NoOfPhoneCalls { get; set; }
        public int NoOfFaceToFaceDiscussions { get; set; }
        public int NoOfGroupDiscussions { get; set; }
        public int NoOfEmailsSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Advisory Services
    /// </summary>
    public class FTIAdvisoryServicesUpdateDto : IUpdateDto
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

    /// <summary>
    /// Display DTO for FTI Advisory Services
    /// </summary>
    public class FTIAdvisoryServicesDto
    {
        public int Id { get; set; }
        public int NoOfFacebookSMS { get; set; }
        public int NoOfSMSSentToRegisteredFarmers { get; set; }
        public int NoOfWhatsappGroups { get; set; }
        public int NoOfWhatsappSMS { get; set; }
        public int NoOfAnsweredWhatsappQueries { get; set; }
        public int NoOfPhoneCalls { get; set; }
        public int NoOfFaceToFaceDiscussions { get; set; }
        public int NoOfGroupDiscussions { get; set; }
        public int NoOfEmailsSent { get; set; }
        public int NoOfNewspaperCoverage { get; set; }
        public int NoOfBeneficiaries { get; set; }
    }

    // ==================== REPORTS (Section E) ====================

    /// <summary>
    /// Create DTO for FTI Report
    /// </summary>
    public class FTIReportCreateDto
    {
        [Required]
        public int FTIProgramDetailsId { get; set; }

        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Report
    /// </summary>
    public class FTIReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    /// <summary>
    /// Display DTO for FTI Report
    /// </summary>
    public class FTIReportDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    // ==================== RECOMMENDATIONS (Section F) ====================

    /// <summary>
    /// Create DTO for FTI Recommendation
    /// </summary>
    public class FTIRecommendationCreateDto
    {
        [Required]
        public int FTIProgramDetailsId { get; set; }

        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    /// <summary>
    /// Update DTO for FTI Recommendation
    /// </summary>
    public class FTIRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    /// <summary>
    /// Display DTO for FTI Recommendation
    /// </summary>
    public class FTIRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }
}
