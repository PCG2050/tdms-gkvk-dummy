
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Mapper.DataTable.EEU
{
    [Mapper]
    public partial class EeuProgramMapper
    {
        // ============================
        // MAIN PROGRAM DETAILS MAPPINGS
        // ============================

        public partial EeuProgramDetailsDto MapToDto(EeuProgramDetails entity);
        public partial EeuProgramDetailsCompleteDto MapToCompleteDto(EeuProgramDetails entity);
        public partial EeuProgramDetails MapToEntity(EeuProgramCreateDto dto);

        // ============================
        // DEMOGRAPHICS
        // ============================

        public partial EeuParticipantDemographicsDto MapToDto(EeuParticipantDemographics entity);
        public partial EeuParticipantDemographics MapToEntity(EeuParticipantDemographicsCreateDto dto);

        // ============================
        // PROGRAM CONTENT
        // ============================

        public partial EeuProgramContentDto MapToDto(EeuProgramContentAndResources entity);
        public partial EeuProgramContentAndResources MapToEntity(EeuProgramContentCreateDto dto);

        // ============================
        // RESOURCE PERSON
        // ============================

        public partial EeuResourcePersonDto MapToDto(EeuResourcePerson entity);
        public partial EeuResourcePerson MapToEntity(EeuResourcePersonCreateDto dto);

        // ============================
        // TOPICS COVERED
        // ============================

        public partial EeuTopicsCoveredDto MapToDto(EeuTopicsCoveredInClass entity);
        public partial EeuTopicsCoveredInClass MapToEntity(EeuTopicsCoveredCreateDto dto);

        // ============================
        // TEACHING AIDS
        // ============================

        public partial EeuTeachingAidsDto MapToDto(EeuTeachingAidsDeveloped entity);
        public partial EeuTeachingAidsDeveloped MapToEntity(EeuTeachingAidsCreateDto dto);

        // ============================
        // ADVISORY SERVICES
        // ============================

        public partial EeuAdvisoryServicesDto MapToDto(EeuAdvisoryServices entity);
        public partial EeuAdvisoryServices MapToEntity(EeuAdvisoryServicesCreateDto dto);

        // ============================
        // REPORT
        // ============================

        public partial EeuReportDto MapToDto(EeuReport entity);
        public partial EeuReport MapToEntity(EeuReportCreateDto dto);

        // ============================
        // RECOMMENDATION
        // ============================

        public partial EeuRecommendationDto MapToDto(EeuRecommendation entity);
        public partial EeuRecommendation MapToEntity(EeuRecommendationCreateDto dto);

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

        [MapProperty(nameof(EeuProgramDetails.UnitLocation), nameof(EeuProgramDetailsDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(EeuProgramDetails.UnitLocation), nameof(EeuProgramDetailsDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(EeuProgramDetails.UnitLocation), nameof(EeuProgramDetailsDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(EeuProgramDetails.Category.Name), nameof(EeuProgramDetailsDto.CategoryName))]
        [MapProperty(nameof(EeuProgramDetails.ProgramType.Name), nameof(EeuProgramDetailsDto.ProgramTypeName))]
        [MapProperty(nameof(EeuProgramDetails.Theme.Name), nameof(EeuProgramDetailsDto.ThemeName))]
        [MapProperty(nameof(EeuProgramDetails.Region.Name), nameof(EeuProgramDetailsDto.RegionName))]
        [MapProperty(nameof(EeuProgramDetails.Mode.Name), nameof(EeuProgramDetailsDto.ModeName))]
        [MapProperty(nameof(EeuProgramDetails.SourceOfFund.Name), nameof(EeuProgramDetailsDto.SourceOfFundName))]
        [MapProperty(nameof(EeuProgramDetails.CreatedBy.FirstName), nameof(EeuProgramDetailsDto.CreatedByName))]
        [MapProperty(nameof(EeuProgramDetails.ApprovedBy.FirstName), nameof(EeuProgramDetailsDto.ApprovedByName))]
        public partial EeuProgramDetailsDto MapToDtoWithDetails(EeuProgramDetails entity);

        // ============================
        // MANUAL UPDATE MAPPINGS
        // ============================

        /// <summary>
        /// Maps EeuProgramUpdateDto to EeuProgramDetails entity (only non-null values)
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuProgramUpdateDto dto, EeuProgramDetails entity)
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
            if (dto.StatusId.HasValue) entity.StatusId = dto.StatusId;
            if (dto.TotalOutlayRs.HasValue) entity.TotalOutlayRs = dto.TotalOutlayRs;
            if (dto.Copi != null) entity.Copi = dto.Copi;
            if (dto.BatchNo.HasValue) entity.BatchNo = dto.BatchNo;
            if (dto.OrganizerBroucherFile != null) entity.OrganizerBroucherFile = dto.OrganizerBroucherFile;
            if (dto.OrganizerInstitutionName != null) entity.OrganizerInstitutionName = dto.OrganizerInstitutionName;
            if (dto.OrganizerInstitutionAddress != null) entity.OrganizerInstitutionAddress = dto.OrganizerInstitutionAddress;
            if (dto.SourceId.HasValue) entity.SourceId = dto.SourceId;
            if (dto.ProposalDate.HasValue) entity.ProposalDate = dto.ProposalDate;
            if (dto.ProposalUploadFile != null) entity.ProposalUploadFile = dto.ProposalUploadFile;
            if (dto.UniversitySanctionLetterDate.HasValue) entity.UniversitySanctionLetterDate = dto.UniversitySanctionLetterDate;
            if (dto.UniversitySanctionLetterUploadFile != null) entity.UniversitySanctionLetterUploadFile = dto.UniversitySanctionLetterUploadFile;
            if (dto.FundsSanctionLetterDate.HasValue) entity.FundsSanctionLetterDate = dto.FundsSanctionLetterDate;
            if (dto.FundsSanctionLetterUploadFile != null) entity.FundsSanctionLetterUploadFile = dto.FundsSanctionLetterUploadFile;
        }

        /// <summary>
        /// Maps EeuParticipantDemographicsUpdateDto to EeuParticipantDemographics entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuParticipantDemographicsUpdateDto dto, EeuParticipantDemographics entity)
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
        /// Maps EeuProgramContentUpdateDto to EeuProgramContentAndResources entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuProgramContentUpdateDto dto, EeuProgramContentAndResources entity)
        {
            // Currently the update DTO has only Id, but keeping for future expansion
            // Add mappings here if more fields are added to the update DTO
        }

        /// <summary>
        /// Maps EeuResourcePersonUpdateDto to EeuResourcePerson entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuResourcePersonUpdateDto dto, EeuResourcePerson entity)
        {
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Designation != null) entity.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) entity.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) entity.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) entity.InstitutionOrDepartment = dto.InstitutionOrDepartment;
        }

        /// <summary>
        /// Maps EeuTopicsCoveredUpdateDto to EeuTopicsCoveredInClass entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuTopicsCoveredUpdateDto dto, EeuTopicsCoveredInClass entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PhotoUpload != null) entity.PhotoUpload = dto.PhotoUpload;
        }

        /// <summary>
        /// Maps EeuTeachingAidsUpdateDto to EeuTeachingAidsDeveloped entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuTeachingAidsUpdateDto dto, EeuTeachingAidsDeveloped entity)
        {
            if (dto.TypeOfAidId.HasValue) entity.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) entity.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) entity.Purpose = dto.Purpose;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
        }

        /// <summary>
        /// Maps EeuAdvisoryServicesUpdateDto to EeuAdvisoryServices entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuAdvisoryServicesUpdateDto dto, EeuAdvisoryServices entity)
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
        /// Maps EeuReportUpdateDto to EeuReport entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuReportUpdateDto dto, EeuReport entity)
        {
            if (dto.ProgressReportReportingYear != null) entity.ProgressReportReportingYear = dto.ProgressReportReportingYear;
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.UploadPhoto != null) entity.UploadPhoto = dto.UploadPhoto;
            if (dto.PhotosGeotaggedPhotoOrUploadPhoto != null) entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
            if (dto.UploadVideo != null) entity.UploadVideo = dto.UploadVideo;
            if (dto.SignificantOutcome != null) entity.SignificantOutcome = dto.SignificantOutcome;
        }

        /// <summary>
        /// Maps EeuRecommendationUpdateDto to EeuRecommendation entity
        /// </summary>
        public static void MapUpdateDtoToEntity(EeuRecommendationUpdateDto dto, EeuRecommendation entity)
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
