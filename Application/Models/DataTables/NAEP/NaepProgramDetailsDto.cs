using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.NAEP
{
    // ==================== PROGRAM DETAILS (Section A) ====================
    public class NaepProgramDetailsDto
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

        public string? UnitLocationName {get; set; }
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
    public class NaepProgramCreateDto
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

    public class NaepProgramUpdateDto : IUpdateDto
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




    public class NaepProgramDetailsCompleteDto : NaepProgramDetailsDto
    {
        public List<NaepParticipantDemographicsDto>? Demographics { get; set; }
        public List<NaepProgramContentDto>? ProgramContent { get; set; }
        public NaepAdvisoryServicesDto? AdvisoryServices { get; set; }
        public NaepReportDto? Reports { get; set; }
        public NaepRecommendationDto? Recommendations { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS (Section B) ====================

    public class NaepParticipantDemographicsDto
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


    public class NaepParticipantDemographicsCreateDto
    {
        [Required]
        public int NaepProgramDetailsId { get; set; }
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

    public class NaepParticipantDemographicsUpdateDto : IUpdateDto
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
    
    public class NaepProgramContentCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

    }

    public class NaepProgramContentUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
       
    }

    public class NaepProgramContentDto
    {
        public int Id { get; set; }
       
        public List<NaepResourcePersonDto> ResourcePersons { get; set; }
        public List<NaepTopicsCoveredDto> TopicsCovered { get; set; }
        public List<NaepTeachingAidsDto> TeachingAids { get; set; }
    }

    public class NaepProgramContentWithChildrenCreateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections (optional - can be null or empty if UI doesn't have data yet)
        public List<NaepResourcePersonCreateDto>? ResourcePersons { get; set; }
        public List<NaepTopicsCoveredCreateDto>? TopicsCovered { get; set; }
        public List<NaepTeachingAidsCreateDto>? TeachingAids { get; set; }
    }

    /// <summary>
    /// Hybrid item for Resource Person - can be new (no Id) or existing (has Id)
    /// Used in update operations to support create/update in one call
    /// </summary>
    public class NaepResourcePersonHybridDto
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
    public class NaepTopicsCoveredHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    /// <summary>
    /// Hybrid item for Teaching Aids
    /// </summary>
    public class NaepTeachingAidsHybridDto
    {
        public int? Id { get; set; }  // null = create new, has value = update existing
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    /// <summary>
    /// Composite DTO for updating NaepProgramContentAndResources with all child entities in a single transaction
    /// Uses Hybrid Pattern:
    /// - Items WITH Id: UPDATE existing
    /// - Items WITHOUT Id: CREATE new
    /// - Items in DB but NOT in arrays: DELETE
    /// </summary>
    public class NaepProgramContentWithChildrenUpdateDto
    {
        // Parent fields
        public string? Title { get; set; }
        public string? Description { get; set; }

        // Child collections - Hybrid Pattern
        // If item has Id: update it
        // If item has no Id: create it
        // If existing item not in array: delete it
        public List<NaepResourcePersonHybridDto>? ResourcePersons { get; set; }
        public List<NaepTopicsCoveredHybridDto>? TopicsCovered { get; set; }
        public List<NaepTeachingAidsHybridDto>? TeachingAids { get; set; }
    }


    // ==================== RESOURCE PERSONS (Section C - Subsection) ====================

    public class NaepResourcePersonCreateDto
    {
        [Required]
        public int NaepProgramContentAndResourcesId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class NaepResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class NaepResourcePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ResourceType { get; set; }
        public string Responsibility { get; set; }
    }

    // ==================== TOPICS COVERED (Section C - Subsection) ====================
    
    public class NaepTopicsCoveredCreateDto
    {
        [Required]
        public int NaepProgramContentAndResourcesId { get; set; }
        
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class NaepTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class NaepTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string PhotoUpload { get; set; }
    }

    // ==================== TEACHING AIDS (Section C - Subsection) ====================
    
    public class NaepTeachingAidsCreateDto
    {
        [Required]
        public int NaepProgramContentAndResourcesId { get; set; }
        
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    public class NaepTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class NaepTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ==================== ADVISORY SERVICES (Section D) ====================
    
    public class NaepAdvisoryServicesCreateDto
    {
        [Required]
        public int NaepProgramDetailsId { get; set; }
        
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

    public class NaepAdvisoryServicesUpdateDto : IUpdateDto
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

    public class NaepAdvisoryServicesDto
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
    
    public class NaepReportCreateDto
    {
        [Required]
        public int NaepProgramDetailsId { get; set; }
        
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class NaepReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class NaepReportDto
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
    
    public class NaepRecommendationCreateDto
    {
        [Required]
        public int NaepProgramDetailsId { get; set; }
        
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class NaepRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class NaepRecommendationDto
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