using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.ATIC
{
    // ==================== PROGRAM DETAILS (Section A) ====================
    public class AticProgramDetailsDto
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
    public class AticProgramCreateDto
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
        public int? Area { get; set; }
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


    }

    public class AticProgramUpdateDto : IUpdateDto
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

    }




    public class AticProgramDetailsCompleteDto : AticProgramDetailsDto
    {
        public List<AticParticipantDemographicsDto>? Demographics { get; set; }
        public List<AticProgramContentDto>? ProgramContent { get; set; }
        public AticAdvisoryServicesDto? AdvisoryServices { get; set; }
        public AticReportDto? Reports { get; set; }
        public AticRecommendationDto? Recommendations { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS (Section B) ====================

    public class AticParticipantDemographicsDto
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


    public class AticParticipantDemographicsCreateDto
    {
        [Required]
        public int AticProgramDetailsId { get; set; }
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

    public class AticParticipantDemographicsUpdateDto : IUpdateDto
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
    
    public class AticProgramContentCreateDto
    {
        [Required]
        public int AticProgramDetailsId { get; set; }
   
    }

    public class AticProgramContentUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
       
    }

    public class AticProgramContentDto
    {
        public int Id { get; set; }
       
        public List<AticResourcePersonDto> ResourcePersons { get; set; }
        public List<AticTopicsCoveredDto> TopicsCovered { get; set; }
        public List<AticTeachingAidsDto> TeachingAids { get; set; }
    }

    // ==================== RESOURCE PERSONS (Section C - Subsection) ====================
    
    public class AticResourcePersonCreateDto
    {
        [Required]
        public int AticProgramContentAndResourcesId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class AticResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class AticResourcePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ResourceType { get; set; }
        public string Responsibility { get; set; }
    }

    // ==================== TOPICS COVERED (Section C - Subsection) ====================
    
    public class AticTopicsCoveredCreateDto
    {
        [Required]
        public int AticProgramContentAndResourcesId { get; set; }
        
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class AticTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class AticTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string PhotoUpload { get; set; }
    }

    // ==================== TEACHING AIDS (Section C - Subsection) ====================
    
    public class AticTeachingAidsCreateDto
    {
        [Required]
        public int AticProgramContentAndResourcesId { get; set; }
        
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    public class AticTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class AticTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ==================== COMPOSITE DTOs - HYBRID PATTERN ====================

    /// <summary>
    /// Composite DTO for creating AticProgramContentAndResources with all child entities in a single transaction
    /// </summary>
    public class AticProgramContentWithChildrenCreateDto
    {
        // Parent fields
        public string? Title { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<AticResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<AticTopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<AticTeachingAidsCreateDto>? TeachingAids { get; set; }
    }

    /// <summary>
    /// Hybrid item for Resource Person - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class AticResourcePersonHybridDto
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
    public class AticTopicsCoveredHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Teaching Aids
    /// </summary>
    public class AticTeachingAidsHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating AticProgramContentAndResources with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id (null): CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class AticProgramContentWithChildrenUpdateDto
    {
        // Parent fields
        public string? Title { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id: update it
        // If item has no Id (null): create it
        // If existing item not in array: delete it
        public List<AticResourcePersonHybridDto>? ResourcePersons { get; set; }
        public List<AticTopicsCoveredHybridDto>? TopicsCovered { get; set; }
        public List<AticTeachingAidsHybridDto>? TeachingAids { get; set; }
    }

    // ==================== ADVISORY SERVICES (Section D) ====================
    
    public class AticAdvisoryServicesCreateDto
    {
        [Required]
        public int AticProgramDetailsId { get; set; }

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

    public class AticAdvisoryServicesUpdateDto : IUpdateDto
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

    public class AticAdvisoryServicesDto
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

    // ==================== REPORTS (Section E) ====================
    
    public class AticReportCreateDto
    {
        [Required]
        public int AticProgramDetailsId { get; set; }
        
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class AticReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class AticReportDto
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
    
    public class AticRecommendationCreateDto
    {
        [Required]
        public int AticProgramDetailsId { get; set; }
        
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class AticRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class AticRecommendationDto
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