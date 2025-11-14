using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Mapper.DataTable.FTI
{
    [Mapper]
    public partial class FTIMapper
    {
        // ============================
        // MAIN PROGRAM DETAILS MAPPINGS
        // ============================

        public partial FTIProgramDetailsDto MapToDto(FTIProgramDetails entity);
        public partial FTIProgramDetailsCompleteDto MapToCompleteDto(FTIProgramDetails entity);
        public partial FTIProgramDetails MapToEntity(FTIProgramCreateDto dto);

        // ============================
        // DEMOGRAPHICS
        // ============================

        public partial FTIParticipantDemographicsDto MapToDto(FTIParticipantDemographics entity);
        public partial FTIParticipantDemographics MapToEntity(FTIParticipantDemographicsCreateDto dto);

        // ============================
        // PROGRAM CONTENT
        // ============================

        public partial FTIProgramContentDto MapToDto(FTIProgramContentAndResources entity);
        public partial FTIProgramContentAndResources MapToEntity(FTIProgramContentCreateDto dto);

        // ============================
        // RESOURCE PERSON
        // ============================

        public partial FTIResourcePersonDto MapToDto(FTIResourcePerson entity);
        public partial FTIResourcePerson MapToEntity(FTIResourcePersonCreateDto dto);

        // ============================
        // TOPICS COVERED
        // ============================

        public partial FTITopicsCoveredDto MapToDto(FTITopicsCoveredInClass entity);
        public partial FTITopicsCoveredInClass MapToEntity(FTITopicsCoveredCreateDto dto);

        // ============================
        // TEACHING AIDS
        // ============================

        public partial FTITeachingAidsDto MapToDto(FTITeachingAidsDeveloped entity);
        public partial FTITeachingAidsDeveloped MapToEntity(FTITeachingAidsCreateDto dto);

        // ============================
        // ADVISORY SERVICES
        // ============================

        public partial FTIAdvisoryServicesDto MapToDto(FTIAdvisoryServices entity);
        public partial FTIAdvisoryServices MapToEntity(FTIAdvisoryServicesCreateDto dto);

        // ============================
        // REPORT
        // ============================

        public partial FTIReportDto MapToDto(FTIReport entity);
        public partial FTIReport MapToEntity(FTIReportCreateDto dto);

        // ============================
        // RECOMMENDATION
        // ============================

        public partial FTIRecommendationDto MapToDto(FTIRecommendation entity);
        public partial FTIRecommendation MapToEntity(FTIRecommendationCreateDto dto);

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

        [MapProperty(nameof(FTIProgramDetails.UnitLocation), nameof(FTIProgramDetailsDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(FTIProgramDetails.UnitLocation), nameof(FTIProgramDetailsDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(FTIProgramDetails.UnitLocation), nameof(FTIProgramDetailsDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(FTIProgramDetails.Category.Name), nameof(FTIProgramDetailsDto.CategoryName))]
        [MapProperty(nameof(FTIProgramDetails.ProgramType.Name), nameof(FTIProgramDetailsDto.ProgramTypeName))]
        [MapProperty(nameof(FTIProgramDetails.Theme.Name), nameof(FTIProgramDetailsDto.ThemeName))]
        [MapProperty(nameof(FTIProgramDetails.Region.Name), nameof(FTIProgramDetailsDto.RegionName))]
        [MapProperty(nameof(FTIProgramDetails.Mode.Name), nameof(FTIProgramDetailsDto.ModeName))]
        [MapProperty(nameof(FTIProgramDetails.SourceOfFund.Name), nameof(FTIProgramDetailsDto.SourceOfFundName))]
        [MapProperty(nameof(FTIProgramDetails.CreatedBy.FirstName), nameof(FTIProgramDetailsDto.CreatedByName))]
        [MapProperty(nameof(FTIProgramDetails.ApprovedBy.FirstName), nameof(FTIProgramDetailsDto.ApprovedByName))]
        public partial FTIProgramDetailsDto MapToDtoWithDetails(FTIProgramDetails entity);

        // ============================
        // MANUAL UPDATE MAPPINGS
        // ============================

        /// <summary>
        /// Maps FTIProgramUpdateDto to FTIProgramDetails entity (only non-null values)
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIProgramUpdateDto dto, FTIProgramDetails entity)
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
        /// Maps FTIParticipantDemographicsUpdateDto to FTIParticipantDemographics entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIParticipantDemographicsUpdateDto dto, FTIParticipantDemographics entity)
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
        /// Maps FTIProgramContentUpdateDto to FTIProgramContentAndResources entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIProgramContentUpdateDto dto, FTIProgramContentAndResources entity)
        {
            // Currently the update DTO has only Id, but keeping for future expansion
            // Add mappings here if more fields are added to the update DTO
        }

        /// <summary>
        /// Maps FTIResourcePersonUpdateDto to FTIResourcePerson entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIResourcePersonUpdateDto dto, FTIResourcePerson entity)
        {
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Designation != null) entity.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) entity.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) entity.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) entity.InstitutionOrDepartment = dto.InstitutionOrDepartment;
        }

        /// <summary>
        /// Maps FTITopicsCoveredUpdateDto to FTITopicsCoveredInClass entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTITopicsCoveredUpdateDto dto, FTITopicsCoveredInClass entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PhotoUpload != null) entity.PhotoUpload = dto.PhotoUpload;
        }

        /// <summary>
        /// Maps FTITeachingAidsUpdateDto to FTITeachingAidsDeveloped entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTITeachingAidsUpdateDto dto, FTITeachingAidsDeveloped entity)
        {
            if (dto.TypeOfAidId.HasValue) entity.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) entity.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) entity.Purpose = dto.Purpose;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
        }

        /// <summary>
        /// Maps FTIAdvisoryServicesUpdateDto to FTIAdvisoryServices entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIAdvisoryServicesUpdateDto dto, FTIAdvisoryServices entity)
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
        /// Maps FTIReportUpdateDto to FTIReport entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIReportUpdateDto dto, FTIReport entity)
        {
            if (dto.ProgressReportReportingYear != null) entity.ProgressReportReportingYear = dto.ProgressReportReportingYear;
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.UploadPhoto != null) entity.UploadPhoto = dto.UploadPhoto;
            if (dto.PhotosGeotaggedPhotoOrUploadPhoto != null) entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
            if (dto.UploadVideo != null) entity.UploadVideo = dto.UploadVideo;
            if (dto.SignificantOutcome != null) entity.SignificantOutcome = dto.SignificantOutcome;
        }

        /// <summary>
        /// Maps FTIRecommendationUpdateDto to FTIRecommendation entity
        /// </summary>
        public static void MapUpdateDtoToEntity(FTIRecommendationUpdateDto dto, FTIRecommendation entity)
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
