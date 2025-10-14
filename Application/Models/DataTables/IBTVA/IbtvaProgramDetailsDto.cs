using Application.Interface;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.DataTables.IBTVA
{
    // ==================== PROGRAM DETAILS ====================

    public class IbtvaProgramDetailsCreateDto
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

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        public int? Mode { get; set; }
        public string? Duration { get; set; }
        public int? RegionId { get; set; }
        public string? RegionOther { get; set; }
        public int? TPNo { get; set; }
        public string? Location { get; set; }
        public int? SourceOfFundId { get; set; }
        public int? NoOfCourses { get; set; }
        public string? Attachments { get; set; }
        public string FormStatus { get; set; } = "Draft";
    }

    public class IbtvaProgramDetailsUpdateDto : IUpdateDto
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
        public string? FormStatus { get; set; }
    }

    public class IbtvaProgramDetailsDto
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public string FormStatus { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedByName { get; set; }
        public int? TotalParticipants { get; set; }
    }

    public class IbtvaProgramDetailsCompleteDto : IbtvaProgramDetailsDto
    {
        public List<IbtvaParticipantDemographicsDto> Demographics { get; set; }
        public IbtvaProgramContentDto ProgramContent { get; set; }
        public IbtvaAdvisoryServicesDto AdvisoryServices { get; set; }
        public List<IbtvaReportDto> Reports { get; set; }
        public List<IbtvaRecommendationDto> Recommendations { get; set; }
    }

    public class UpdateFormStatusDto
    {
        [Required]
        public string FormStatus { get; set; } // "Draft", "Pending", "Approved", "Rejected"
        public string? FormStatusRemarks { get; set; }
    }

    // ==================== PARTICIPANT DEMOGRAPHICS ====================

    public class IbtvaParticipantDemographicsCreateDto
    {
        [Required]
        public int IbtvaProgramDetailsId { get; set; }
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

    public class IbtvaParticipantDemographicsUpdateDto : IUpdateDto
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

    public class IbtvaParticipantDemographicsDto
    {
        public int Id { get; set; }
        public string ParticipantName { get; set; }
        public int MaleTotal { get; set; }
        public int FemaleTotal { get; set; }
        public int Total { get; set; }
    }

    // ==================== PROGRAM CONTENT ====================

    public class IbtvaProgramContentCreateDto
    {
        [Required]
        public int IbtvaProgramDetailsId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class IbtvaProgramContentUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class IbtvaProgramContentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IbtvaResourcePersonDto> ResourcePersons { get; set; }
        public List<IbtvaTopicsCoveredDto> TopicsCovered { get; set; }
        public List<IbtvaTeachingAidsDto> TeachingAids { get; set; }
    }

    // ==================== RESOURCE PERSONS ====================

    public class IbtvaResourcePersonCreateDto
    {
        [Required]
        public int IbtvaProgramContentAndResourcesId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? TopicsCovered { get; set; }
    }

    public class IbtvaResourcePersonUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public int? ResourceType { get; set; }
        public int? Responsibility { get; set; }
        public string? TopicsCovered { get; set; }
    }

    public class IbtvaResourcePersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ResourceType { get; set; }
        public string Responsibility { get; set; }
    }

    // ==================== TOPICS COVERED ====================

    public class IbtvaTopicsCoveredCreateDto
    {
        [Required]
        public int IbtvaProgramContentAndResourcesId { get; set; }

        [Required]
        public string TopicName { get; set; }

        public string? Description { get; set; }
    }

    public class IbtvaTopicsCoveredUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? TopicName { get; set; }
        public string? Description { get; set; }
    }

    public class IbtvaTopicsCoveredDto
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public string Description { get; set; }
    }

    // ==================== TEACHING AIDS ====================

    public class IbtvaTeachingAidsCreateDto
    {
        [Required]
        public int IbtvaProgramContentAndResourcesId { get; set; }

        [Required]
        public string AidType { get; set; }

        public string? Description { get; set; }
        public int? Quantity { get; set; }
    }

    public class IbtvaTeachingAidsUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? AidType { get; set; }
        public string? Description { get; set; }
        public int? Quantity { get; set; }
    }

    public class IbtvaTeachingAidsDto
    {
        public int Id { get; set; }
        public string AidType { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }

    // ==================== ADVISORY SERVICES ====================

    public class IbtvaAdvisoryServicesCreateDto
    {
        [Required]
        public int IbtvaProgramDetailsId { get; set; }

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

    public class IbtvaAdvisoryServicesUpdateDto : IUpdateDto
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

    public class IbtvaAdvisoryServicesDto
    {
        public int Id { get; set; }
        public int TotalDigitalOutreach { get; set; }
        public int TotalPhysicalInteractions { get; set; }
        public int NoOfBeneficiaries { get; set; }
    }

    // ==================== REPORTS ====================

    public class IbtvaReportCreateDto
    {
        [Required]
        public int IbtvaProgramDetailsId { get; set; }

        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class IbtvaReportUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProgressReportReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string? UploadPhoto { get; set; }
        public string? PhotosGeotaggedPhotoOrUploadPhoto { get; set; }
        public string? UploadVideo { get; set; }
        public string? SignificantOutcome { get; set; }
    }

    public class IbtvaReportDto
    {
        public int Id { get; set; }
        public string ReportingYear { get; set; }
        public DateTime? Date { get; set; }
        public string SignificantOutcome { get; set; }
        public bool HasPhotos { get; set; }
        public bool HasVideos { get; set; }
    }

    // ==================== RECOMMENDATIONS ====================

    public class IbtvaRecommendationCreateDto
    {
        [Required]
        public int IbtvaProgramDetailsId { get; set; }

        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class IbtvaRecommendationUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
        public string? SuccessStories { get; set; }
        public string? ImpactOutcome { get; set; }
    }

    public class IbtvaRecommendationDto
    {
        public int Id { get; set; }
        public string? ProblemsIdentified { get; set; }
        public string? Recommendation { get; set; }
        public string? ActionTaken { get; set; }
        public string? SignificantAchievement { get; set; }
    }
}