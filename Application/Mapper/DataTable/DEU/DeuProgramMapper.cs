
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Mapper.DataTable.DEU
{
    [Mapper]
    public partial class DeuProgramMapper
    {
        // ============================
        // MAIN PROGRAM DETAILS MAPPINGS
        // ============================

        public partial DeuProgramDetailsDto MapToDto(DeuProgramDetails entity);
        public partial DeuProgramDetailsCompleteDto MapToCompleteDto(DeuProgramDetails entity);
        public partial DeuProgramDetails MapToEntity(DeuProgramCreateDto dto);

        // ============================
        // DEMOGRAPHICS
        // ============================

        public partial DeuParticipantDemographicsDto MapToDto(DeuParticipantDemographics entity);
        public partial DeuParticipantDemographics MapToEntity(DeuParticipantDemographicsCreateDto dto);

        // ============================
        // PROGRAM CONTENT
        // ============================

        public partial DeuProgramContentDto MapToDto(DeuProgramContentAndResources entity);
        public partial DeuProgramContentAndResources MapToEntity(DeuProgramContentCreateDto dto);

        // ============================
        // RESOURCE PERSON
        // ============================

        public partial DeuResourcePersonDto MapToDto(DeuResourcePerson entity);
        public partial DeuResourcePerson MapToEntity(DeuResourcePersonCreateDto dto);

        // ============================
        // TOPICS COVERED
        // ============================

        public partial DeuTopicsCoveredDto MapToDto(DeuTopicsCoveredInClass entity);
        public partial DeuTopicsCoveredInClass MapToEntity(DeuTopicsCoveredCreateDto dto);

        // ============================
        // TEACHING AIDS
        // ============================

        public partial DeuTeachingAidsDto MapToDto(DeuTeachingAidsDeveloped entity);
        public partial DeuTeachingAidsDeveloped MapToEntity(DeuTeachingAidsCreateDto dto);

        // ============================
        // ADVISORY SERVICES
        // ============================

        public partial DeuAdvisoryServicesDto MapToDto(DeuAdvisoryServices entity);
        public partial DeuAdvisoryServices MapToEntity(DeuAdvisoryServicesCreateDto dto);

        // ============================
        // REPORT
        // ============================

        public partial DeuReportDto MapToDto(DeuReport entity);
        public partial DeuReport MapToEntity(DeuReportCreateDto dto);

        // ============================
        // RECOMMENDATION
        // ============================

        public partial DeuRecommendationDto MapToDto(DeuRecommendation entity);
        public partial DeuRecommendation MapToEntity(DeuRecommendationCreateDto dto);

        // ============================
        // HELPER METHODS FOR NESTED PROPERTIES
        // ============================

        private string? GetUnitName(OrganizationUnitLocation? location)
            => location?.Unit?.Name;

        private string? GetDistrictName(OrganizationUnitLocation? location)
            => location?.District?.Name;

        private string? GetStateName(OrganizationUnitLocation? location)
            => location?.District?.State?.Name;

        // ============================
        // MAPPING WITH NAVIGATION DETAILS
        // ============================

        [MapProperty(nameof(DeuProgramDetails.UnitLocation), nameof(DeuProgramDetailsDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(DeuProgramDetails.UnitLocation), nameof(DeuProgramDetailsDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(DeuProgramDetails.UnitLocation), nameof(DeuProgramDetailsDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(DeuProgramDetails.Category.Name), nameof(DeuProgramDetailsDto.CategoryName))]
        [MapProperty(nameof(DeuProgramDetails.ProgramType.Name), nameof(DeuProgramDetailsDto.ProgramTypeName))]
        [MapProperty(nameof(DeuProgramDetails.Theme.Name), nameof(DeuProgramDetailsDto.ThemeName))]
        [MapProperty(nameof(DeuProgramDetails.Region.Name), nameof(DeuProgramDetailsDto.RegionName))]
        [MapProperty(nameof(DeuProgramDetails.Mode.Name), nameof(DeuProgramDetailsDto.ModeName))]
        [MapProperty(nameof(DeuProgramDetails.SourceOfFund.Name), nameof(DeuProgramDetailsDto.SourceOfFundName))]
        [MapProperty(nameof(DeuProgramDetails.CreatedBy.FirstName), nameof(DeuProgramDetailsDto.CreatedByName))]
        [MapProperty(nameof(DeuProgramDetails.ApprovedBy.FirstName), nameof(DeuProgramDetailsDto.ApprovedByName))]
        public partial DeuProgramDetailsDto MapToDtoWithDetails(DeuProgramDetails entity);

        // ============================
        // MANUAL UPDATE MAPPINGS
        // ============================

        /// <summary>
        /// Maps DeuProgramUpdateDto to DeuProgramDetails entity (only non-null values)
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuProgramUpdateDto dto, DeuProgramDetails entity)
        {
            if (dto.StartDate.HasValue) entity.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) entity.EndDate = dto.EndDate.Value;
            if (dto.ProgramTypeId.HasValue) entity.ProgramTypeId = dto.ProgramTypeId;
            if (dto.CategoryId.HasValue) entity.CategoryId = dto.CategoryId;
            if (dto.CategoryOther != null) entity.CategoryOther = dto.CategoryOther;
            if (dto.TypeId.HasValue) entity.TypeId = dto.TypeId;
            if (dto.TypeOther != null) entity.TypeOther = dto.TypeOther;
            if (dto.ThemeId.HasValue) entity.ThemeId = dto.ThemeId;
            if (dto.ThemeOther != null) entity.ThemeOther = dto.ThemeOther;
            if (dto.ThematicAreaId.HasValue) entity.ThematicAreaId = dto.ThematicAreaId;
            if (dto.ThematicAreaOther != null) entity.ThematicAreaOther = dto.ThematicAreaOther;
            if (dto.SponsoredOrganization.HasValue) entity.SponsoredOrganization = dto.SponsoredOrganization;
            if (dto.SponsoredOrganizationName != null) entity.SponsoredOrganizationName = dto.SponsoredOrganizationName;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.Mode.HasValue) entity.ModeId = dto.Mode;
            if (dto.Duration != null) entity.Duration = dto.Duration;
            if (dto.RegionId.HasValue) entity.RegionId = dto.RegionId;
            if (dto.RegionOther != null) entity.RegionOther = dto.RegionOther;
            if (dto.TPNo.HasValue) entity.TPNo = dto.TPNo;
            if (dto.Location != null) entity.Location = dto.Location;
            if (dto.SourceOfFundId.HasValue) entity.SourceOfFundId = dto.SourceOfFundId;
            if (dto.NoOfCourses.HasValue) entity.Funds = dto.NoOfCourses;
            if (dto.Attachments != null) entity.Attachements = dto.Attachments;
        }

        /// <summary>
        /// Maps DeuParticipantDemographicsUpdateDto to DeuParticipantDemographics entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuParticipantDemographicsUpdateDto dto, DeuParticipantDemographics entity)
        {
            if (dto.ParticipantId.HasValue) entity.ParticipantId = dto.ParticipantId;
            if (dto.Male_SC.HasValue) entity.Male_SC = dto.Male_SC;
            if (dto.Male_ST.HasValue) entity.Male_ST = dto.Male_ST;
            if (dto.Male_OBC.HasValue) entity.Male_OBC = dto.Male_OBC;
            if (dto.Male_GEN.HasValue) entity.Male_GEN = dto.Male_GEN;
            if (dto.SC_Male_StayedInHostel.HasValue) entity.SC_Male_StayedInHostel = dto.SC_Male_StayedInHostel;
            if (dto.ST_Male_StayedInHostel.HasValue) entity.ST_Male_StayedInHostel = dto.ST_Male_StayedInHostel;
            if (dto.OBC_Male_StayedInHostel.HasValue) entity.OBC_Male_StayedInHostel = dto.OBC_Male_StayedInHostel;
            if (dto.GEN_Male_StayedInHostel.HasValue) entity.GEN_Male_StayedInHostel = dto.GEN_Male_StayedInHostel;
            if (dto.Female_SC.HasValue) entity.Female_SC = dto.Female_SC;
            if (dto.Female_ST.HasValue) entity.Female_ST = dto.Female_ST;
            if (dto.Female_OBC.HasValue) entity.Female_OBC = dto.Female_OBC;
            if (dto.Female_GEN.HasValue) entity.Female_GEN = dto.Female_GEN;
            if (dto.SC_Female_StayedInHostel.HasValue) entity.SC_Female_StayedInHostel = dto.SC_Female_StayedInHostel;
            if (dto.ST_Female_StayedInHostel.HasValue) entity.ST_Female_StayedInHostel = dto.ST_Female_StayedInHostel;
            if (dto.OBC_Female_StayedInHostel.HasValue) entity.OBC_Female_StayedInHostel = dto.OBC_Female_StayedInHostel;
            if (dto.GEN_Female_StayedInHostel.HasValue) entity.GEN_Female_StayedInHostel = dto.GEN_Female_StayedInHostel;
            if (dto.Total.HasValue) entity.Total = dto.Total;
        }

        /// <summary>
        /// Maps DeuProgramContentUpdateDto to DeuProgramContentAndResources entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuProgramContentUpdateDto dto, DeuProgramContentAndResources entity)
        {
            // Currently the update DTO has only Id, but keeping for future expansion
            // Add mappings here if more fields are added to the update DTO
        }

        /// <summary>
        /// Maps DeuResourcePersonUpdateDto to DeuResourcePerson entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuResourcePersonUpdateDto dto, DeuResourcePerson entity)
        {
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Designation != null) entity.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) entity.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) entity.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) entity.InstitutionOrDepartment = dto.InstitutionOrDepartment;
        }

        /// <summary>
        /// Maps DeuTopicsCoveredUpdateDto to DeuTopicsCoveredInClass entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuTopicsCoveredUpdateDto dto, DeuTopicsCoveredInClass entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PhotoUpload != null) entity.PhotoUpload = dto.PhotoUpload;
        }

        /// <summary>
        /// Maps DeuTeachingAidsUpdateDto to DeuTeachingAidsDeveloped entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuTeachingAidsUpdateDto dto, DeuTeachingAidsDeveloped entity)
        {
            if (dto.TypeOfAidId.HasValue) entity.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) entity.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) entity.Purpose = dto.Purpose;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
        }

        /// <summary>
        /// Maps DeuAdvisoryServicesUpdateDto to DeuAdvisoryServices entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuAdvisoryServicesUpdateDto dto, DeuAdvisoryServices entity)
        {
            if (dto.NoOfFacebookSMS.HasValue) entity.NoOfFacebookSMS = dto.NoOfFacebookSMS.Value;
            if (dto.NoOfSMSSentToRegisteredFarmers.HasValue) entity.NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers.Value;
            if (dto.NoOfWhatsappGroups.HasValue) entity.NoOfWhatsappGroups = dto.NoOfWhatsappGroups.Value;
            if (dto.NoOfWhatsappSMS.HasValue) entity.NoOfWhatsappSMS = dto.NoOfWhatsappSMS.Value;
            if (dto.NoOfAnsweredWhatsappQueries.HasValue) entity.NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries.Value;
            if (dto.NoOfPhoneCalls.HasValue) entity.NoOfPhoneCalls = dto.NoOfPhoneCalls.Value;
            if (dto.NoOfFaceToFaceDiscussions.HasValue) entity.NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions.Value;
            if (dto.NoOfGroupDiscussions.HasValue) entity.NoOfGroupDiscussions = dto.NoOfGroupDiscussions.Value;
            if (dto.NoOfEmailsSent.HasValue) entity.NoOfEmailsSent = dto.NoOfEmailsSent.Value;
            if (dto.NoOfNewspaperCoverage.HasValue) entity.NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage.Value;
            if (dto.NoOfBeneficiaries.HasValue) entity.NoOfBeneficiaries = dto.NoOfBeneficiaries.Value;
        }

        /// <summary>
        /// Maps DeuReportUpdateDto to DeuReport entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuReportUpdateDto dto, DeuReport entity)
        {
            if (dto.ProgressReportReportingYear != null) entity.ProgressReportReportingYear = dto.ProgressReportReportingYear;
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.UploadPhoto != null) entity.UploadPhoto = dto.UploadPhoto;
            if (dto.PhotosGeotaggedPhotoOrUploadPhoto != null) entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
            if (dto.UploadVideo != null) entity.UploadVideo = dto.UploadVideo;
            if (dto.SignificantOutcome != null) entity.SignificantOutcome = dto.SignificantOutcome;
        }

        /// <summary>
        /// Maps DeuRecommendationUpdateDto to DeuRecommendation entity
        /// </summary>
        public static void MapUpdateDtoToEntity(DeuRecommendationUpdateDto dto, DeuRecommendation entity)
        {
            if (dto.ProblemsIdentified != null) entity.ProblemsIdentified = dto.ProblemsIdentified;
            if (dto.Recommendation != null) entity.Recommendation = dto.Recommendation;
            if (dto.ActionTaken != null) entity.ActionTaken = dto.ActionTaken;
            if (dto.SignificantAchievement != null) entity.SignificantAchievement = dto.SignificantAchievement;
            if (dto.SuccessStories != null) entity.SuccessStories = dto.SuccessStories;
            if (dto.ImpactOutcome != null) entity.ImpactOutcome = dto.ImpactOutcome;
        }
    }
}
