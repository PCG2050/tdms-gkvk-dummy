using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.STU
{
    // ==================== PROGRAM DETAILS (Section A) ====================
    public class StuProgramDetailsDto
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

        public string? UnitLocationName { get; set; }
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
    public class StuProgramCreateDto
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

    public class StuProgramUpdateDto : IUpdateDto
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

       

    }




    public class StuProgramDetailsCompleteDto : StuProgramDetailsDto
    {
        public List<StuParticipantDemographicsDto>? Demographics { get; set; }
        public List<StuProgramContentDto>? ProgramContent { get; set; }
        public StuAdvisoryServicesDto? AdvisoryServices { get; set; }
        public StuReportDto? Reports { get; set; }
        public StuRecommendationDto? Recommendations { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS (Section B) ====================

    public class StuParticipantDemographicsDto
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


    public class StuParticipantDemographicsCreateDto
    {
        [Required]
        public int StuProgramDetailsId { get; set; }
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

    public class StuParticipantDemographicsUpdateDto : IUpdateDto
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

    public class StuProgramContentCreateDto
    {
        [Required]
        public int StuProgramDetailsId { get; set; }

    }

    public class StuProgramContentUpdateDto : IUpdateDto
    {
        public int Id { get; set; }

    }

    public class StuProgramContentDto
    {
        public int Id { get; set; }

        public List<StuResourcePersonDto> ResourcePersons { get; set; }
        public List<StuTopicsCoveredDto> TopicsCovered { get; set; }
        public List<StuTeachingAidsDto> TeachingAids { get; set; }
    }

    // ==================== RESOURCE PERSONS (Section C - Subsection) ====================

    public class StuResourcePersonCreateDto
    {
        [Required]
        public int StuProgramContentAndResourcesId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class StuResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? InstitutionOrDepartment { get; set; }
    }

    public class StuResourcePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ResourceType { get; set; }
        public string Responsibility { get; set; }
    }

    // ==================== TOPICS COVERED (Section C - Subsection) ====================

    public class StuTopicsCoveredCreateDto
    {
        [Required]
        public int StuProgramContentAndResourcesId { get; set; }

        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class StuTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Title { get; set; }
        public string? PhotoUpload { get; set; }
    }

    public class StuTopicsCoveredDto
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Title { get; set; }
        public string PhotoUpload { get; set; }
    }

    // ==================== TEACHING AIDS (Section C - Subsection) ====================

    public class StuTeachingAidsCreateDto
    {
        [Required]
        public int StuProgramContentAndResourcesId { get; set; }

        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int Number { get; set; }
    }

    public class StuTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    public class StuTeachingAidsDto
    {
        public int Id { get; set; }
        public int? TypeOfAidId { get; set; }
        public string? OtherTypeOfAid { get; set; }
        public string? Purpose { get; set; }
        public int? Number { get; set; }
    }

    // ==================== ADVISORY SERVICES (Section D) ====================

    public class StuAdvisoryServicesCreateDto
    {
        [Required]
        public int StuProgramDetailsId { get; set; }

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

    public class StuAdvisoryServicesUpdateDto : IUpdateDto
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

    public class StuAdvisoryServicesDto
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

    public class StuReportCreateDto
    {
        [Required]
        public int StuProgramDetailsId { get; set; }

        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class StuReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class StuReportDto
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

    public class StuRecommendationCreateDto
    {
        [Required]
        public int StuProgramDetailsId { get; set; }

        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class StuRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class StuRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    // ==================== HISTORY & APPROVALS ====================

    /// <summary>
    /// DTO for trainer/unit head submission history
    /// </summary>
    public class TrainerHistoryItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string FormStatus { get; set; }
        public string? FormStatusRemarks { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ApprovedAt { get; set; }
        public string? ApprovedByName { get; set; }
        public string? ProgramTypeName { get; set; }
    }

    /// <summary>
    /// DTO for pending approvals shown to unit heads
    /// </summary>
    public class PendingApprovalItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int UnitLocationId { get; set; }
        public string? UnitLocationName { get; set; }
        public string? UnitName { get; set; }
        public string? DistrictName { get; set; }
        public string FormStatus { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string? CreatedByName { get; set; }
        public int CreatedById { get; set; }
        public string? ProgramTypeName { get; set; }
    }
}