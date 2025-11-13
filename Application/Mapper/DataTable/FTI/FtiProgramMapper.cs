
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Mapper.DataTable.FTI
{
    [Mapper]
    public partial class FtiProgramMapper
    {
        // ============================
        // MAIN PROGRAM DETAILS MAPPINGS
        // ============================

        public partial FtiProgramDetailsDto MapToDto(FtiProgramDetails entity);
        public partial FtiProgramDetailsCompleteDto MapToCompleteDto(FtiProgramDetails entity);
        public partial FtiProgramDetails MapToEntity(FtiProgramCreateDto dto);

        // ============================
        // DEMOGRAPHICS
        // ============================

        public partial FtiParticipantDemographicsDto MapToDto(FtiParticipantDemographics entity);
        public partial FtiParticipantDemographics MapToEntity(FtiParticipantDemographicsCreateDto dto);

        // ============================
        // PROGRAM CONTENT
        // ============================

        public partial FtiProgramContentDto MapToDto(FtiProgramContentAndResources entity);
        public partial FtiProgramContentAndResources MapToEntity(FtiProgramContentCreateDto dto);

        // ============================
        // RESOURCE PERSON
        // ============================

        public partial FtiResourcePersonDto MapToDto(FtiResourcePerson entity);
        public partial FtiResourcePerson MapToEntity(FtiResourcePersonCreateDto dto);

        // ============================
        // TOPICS COVERED
        // ============================

        public partial FtiTopicsCoveredDto MapToDto(FtiTopicsCoveredInClass entity);
        public partial FtiTopicsCoveredInClass MapToEntity(FtiTopicsCoveredCreateDto dto);

        // ============================
        // TEACHING AIDS
        // ============================

        public partial FtiTeachingAidsDto MapToDto(FtiTeachingAidsDeveloped entity);
        public partial FtiTeachingAidsDeveloped MapToEntity(FtiTeachingAidsCreateDto dto);

        // ============================
        // ADVISORY SERVICES
        // ============================

        public partial FtiAdvisoryServicesDto MapToDto(FtiAdvisoryServices entity);
        public partial FtiAdvisoryServices MapToEntity(FtiAdvisoryServicesCreateDto dto);

        // ============================
        // REPORT
        // ============================

        public partial FtiReportDto MapToDto(FtiReport entity);
        public partial FtiReport MapToEntity(FtiReportCreateDto dto);

        // ============================
        // RECOMMENDATION
        // ============================

        public partial FtiRecommendationDto MapToDto(FtiRecommendation entity);
        public partial FtiRecommendation MapToEntity(FtiRecommendationCreateDto dto);

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

        [MapProperty(nameof(FtiProgramDetails.UnitLocation), nameof(FtiProgramDetailsDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(FtiProgramDetails.UnitLocation), nameof(FtiProgramDetailsDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(FtiProgramDetails.UnitLocation), nameof(FtiProgramDetailsDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(FtiProgramDetails.Category.Name), nameof(FtiProgramDetailsDto.CategoryName))]
        [MapProperty(nameof(FtiProgramDetails.ProgramType.Name), nameof(FtiProgramDetailsDto.ProgramTypeName))]
        [MapProperty(nameof(FtiProgramDetails.Theme.Name), nameof(FtiProgramDetailsDto.ThemeName))]
        [MapProperty(nameof(FtiProgramDetails.Region.Name), nameof(FtiProgramDetailsDto.RegionName))]
        [MapProperty(nameof(FtiProgramDetails.Mode.Name), nameof(FtiProgramDetailsDto.ModeName))]
        [MapProperty(nameof(FtiProgramDetails.SourceOfFund.Name), nameof(FtiProgramDetailsDto.SourceOfFundName))]
        [MapProperty(nameof(FtiProgramDetails.CreatedBy.FirstName), nameof(FtiProgramDetailsDto.CreatedByName))]
        [MapProperty(nameof(FtiProgramDetails.ApprovedBy.FirstName), nameof(FtiProgramDetailsDto.ApprovedByName))]
        public partial FtiProgramDetailsDto MapToDtoWithDetails(FtiProgramDetails entity);

        // ============================
        // MANUAL UPDATE MAPPINGS
        // ============================

        /// <summary>
        /// Maps FtiProgramUpdateDto to FtiProgramDetails entity (only non-null values)
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiProgramUpdateDto dto, FtiProgramDetails entity)
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
        /// Maps FtiParticipantDemographicsUpdateDto to FtiParticipantDemographics entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiParticipantDemographicsUpdateDto dto, FtiParticipantDemographics entity)
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
        /// Maps FtiProgramContentUpdateDto to FtiProgramContentAndResources entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiProgramContentUpdateDto dto, FtiProgramContentAndResources entity)
        {
            // Currently the update DTO has only Id, but keeping for future expansion
            // Add mappings here if more fields are added to the update DTO
        }

        /// <summary>
        /// Maps FtiResourcePersonUpdateDto to FtiResourcePerson entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiResourcePersonUpdateDto dto, FtiResourcePerson entity)
        {
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Designation != null) entity.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) entity.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) entity.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) entity.InstitutionOrDepartment = dto.InstitutionOrDepartment;
        }

        /// <summary>
        /// Maps FtiTopicsCoveredUpdateDto to FtiTopicsCoveredInClass entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiTopicsCoveredUpdateDto dto, FtiTopicsCoveredInClass entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PhotoUpload != null) entity.PhotoUpload = dto.PhotoUpload;
        }

        /// <summary>
        /// Maps FtiTeachingAidsUpdateDto to FtiTeachingAidsDeveloped entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiTeachingAidsUpdateDto dto, FtiTeachingAidsDeveloped entity)
        {
            if (dto.TypeOfAidId.HasValue) entity.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) entity.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) entity.Purpose = dto.Purpose;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
        }

        /// <summary>
        /// Maps FtiAdvisoryServicesUpdateDto to FtiAdvisoryServices entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiAdvisoryServicesUpdateDto dto, FtiAdvisoryServices entity)
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
        /// Maps FtiReportUpdateDto to FtiReport entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiReportUpdateDto dto, FtiReport entity)
        {
            if (dto.ProgressReportReportingYear != null) entity.ProgressReportReportingYear = dto.ProgressReportReportingYear;
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.UploadPhoto != null) entity.UploadPhoto = dto.UploadPhoto;
            if (dto.PhotosGeotaggedPhotoOrUploadPhoto != null) entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
            if (dto.UploadVideo != null) entity.UploadVideo = dto.UploadVideo;
            if (dto.SignificantOutcome != null) entity.SignificantOutcome = dto.SignificantOutcome;
        }

        /// <summary>
        /// Maps FtiRecommendationUpdateDto to FtiRecommendation entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FtiRecommendationUpdateDto dto, FtiRecommendation entity)
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
