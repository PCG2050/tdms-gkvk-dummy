
using Application.Models.DataTables.STU;
using Domain.Entities.STU;

namespace Application.Mapper.DataTable.STU
{
    [Mapper]
    public partial class StuProgramMapper
    {
        // ============================
        // MAIN PROGRAM DETAILS MAPPINGS
        // ============================

        public partial StuProgramDetailsDto MapToDto(StuProgramDetails entity);
        public partial StuProgramDetailsCompleteDto MapToCompleteDto(StuProgramDetails entity);
        public partial StuProgramDetails MapToEntity(StuProgramCreateDto dto);

        // ============================
        // DEMOGRAPHICS
        // ============================

        public partial StuParticipantDemographicsDto MapToDto(StuParticipantDemographics entity);
        public partial StuParticipantDemographics MapToEntity(StuParticipantDemographicsCreateDto dto);

        // ============================
        // PROGRAM CONTENT
        // ============================

        public partial StuProgramContentDto MapToDto(StuProgramContentAndResources entity);
        public partial StuProgramContentAndResources MapToEntity(StuProgramContentCreateDto dto);

        // ============================
        // RESOURCE PERSON
        // ============================

        public partial StuResourcePersonDto MapToDto(StuResourcePerson entity);
        public partial StuResourcePerson MapToEntity(StuResourcePersonCreateDto dto);

        // ============================
        // TOPICS COVERED
        // ============================

        public partial StuTopicsCoveredDto MapToDto(StuTopicsCoveredInClass entity);
        public partial StuTopicsCoveredInClass MapToEntity(StuTopicsCoveredCreateDto dto);

        // ============================
        // TEACHING AIDS
        // ============================

        public partial StuTeachingAidsDto MapToDto(StuTeachingAidsDeveloped entity);
        public partial StuTeachingAidsDeveloped MapToEntity(StuTeachingAidsCreateDto dto);

        // ============================
        // ADVISORY SERVICES
        // ============================

        public partial StuAdvisoryServicesDto MapToDto(StuAdvisoryServices entity);
        public partial StuAdvisoryServices MapToEntity(StuAdvisoryServicesCreateDto dto);

        // ============================
        // REPORT
        // ============================

        public partial StuReportDto MapToDto(StuReport entity);
        public partial StuReport MapToEntity(StuReportCreateDto dto);

        // ============================
        // RECOMMENDATION
        // ============================

        public partial StuRecommendationDto MapToDto(StuRecommendation entity);
        public partial StuRecommendation MapToEntity(StuRecommendationCreateDto dto);

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

        [MapProperty(nameof(StuProgramDetails.UnitLocation), nameof(StuProgramDetailsDto.UnitName), Use = nameof(GetUnitName))]
        [MapProperty(nameof(StuProgramDetails.UnitLocation), nameof(StuProgramDetailsDto.DistrictName), Use = nameof(GetDistrictName))]
        [MapProperty(nameof(StuProgramDetails.UnitLocation), nameof(StuProgramDetailsDto.StateName), Use = nameof(GetStateName))]
        [MapProperty(nameof(StuProgramDetails.Category.Name), nameof(StuProgramDetailsDto.CategoryName))]
        [MapProperty(nameof(StuProgramDetails.ProgramType.Name), nameof(StuProgramDetailsDto.ProgramTypeName))]
        [MapProperty(nameof(StuProgramDetails.Theme.Name), nameof(StuProgramDetailsDto.ThemeName))]
        [MapProperty(nameof(StuProgramDetails.Region.Name), nameof(StuProgramDetailsDto.RegionName))]
        [MapProperty(nameof(StuProgramDetails.Mode.Name), nameof(StuProgramDetailsDto.ModeName))]
        [MapProperty(nameof(StuProgramDetails.SourceOfFund.Name), nameof(StuProgramDetailsDto.SourceOfFundName))]
        [MapProperty(nameof(StuProgramDetails.CreatedBy.FirstName), nameof(StuProgramDetailsDto.CreatedByName))]
        [MapProperty(nameof(StuProgramDetails.ApprovedBy.FirstName), nameof(StuProgramDetailsDto.ApprovedByName))]
        public partial StuProgramDetailsDto MapToDtoWithDetails(StuProgramDetails entity);

        // ============================
        // MANUAL UPDATE MAPPINGS
        // ============================

        /// <summary>
        /// Maps StuProgramUpdateDto to StuProgramDetails entity (only non-null values)
        /// </summary>
        public static void MapUpdateDtoToEntity(StuProgramUpdateDto dto, StuProgramDetails entity)
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
            if (dto.PiAddress != null) entity.PiAddress = dto.PiAddress;
            if (dto.BatchNo.HasValue) entity.BatchNo = dto.BatchNo;
            if (dto.Area.HasValue) entity.Area = dto.Area;
            if (dto.OrganizerBroucherFile != null) entity.OrganizerBroucherFile = dto.OrganizerBroucherFile;
            if (dto.OrganizerInstitutionName != null) entity.OrganizerInstitutionName = dto.OrganizerInstitutionName;
            if (dto.OrganizerInstitutionAddress != null) entity.OrganizerInstitutionAddress = dto.OrganizerInstitutionAddress;
            if (dto.SourceId.HasValue) entity.SourceId = dto.SourceId;
            if (dto.OtherSourceOfInformation != null) entity.OtherSourceOfInformation = dto.OtherSourceOfInformation;
            if (dto.SourceOfTitle != null) entity.SourceOfTitle = dto.SourceOfTitle;
            if (dto.ProposalDate.HasValue) entity.ProposalDate = dto.ProposalDate;
            if (dto.ProposalUploadFile != null) entity.ProposalUploadFile = dto.ProposalUploadFile;
            if (dto.UniversitySanctionLetterDate.HasValue) entity.UniversitySanctionLetterDate = dto.UniversitySanctionLetterDate;
            if (dto.UniversitySanctionLetterUploadFile != null) entity.UniversitySanctionLetterUploadFile = dto.UniversitySanctionLetterUploadFile;
            if (dto.ProjectSanctionDate.HasValue) entity.ProjectSanctionDate = dto.ProjectSanctionDate;
            if (dto.ProjectSanctionFile != null) entity.ProjectSanctionFile = dto.ProjectSanctionFile;
            if (dto.UniImplDate.HasValue) entity.UniImplDate = dto.UniImplDate;
            if (dto.UniImplLetterFile != null) entity.UniImplLetterFile = dto.UniImplLetterFile;
            if (dto.FundReleaseYear != null) entity.FundReleaseYear = dto.FundReleaseYear;
            if (dto.FundAmount.HasValue) entity.FundAmount = dto.FundAmount;
            if (dto.FundReleaseDate.HasValue) entity.FundReleaseDate = dto.FundReleaseDate;
            if (dto.FundReleaseFile != null) entity.FundReleaseFile = dto.FundReleaseFile;
            if (dto.FundsSanctionLetterDate.HasValue) entity.FundsSanctionLetterDate = dto.FundsSanctionLetterDate;
            if (dto.FundsSanctionLetterUploadFile != null) entity.FundsSanctionLetterUploadFile = dto.FundsSanctionLetterUploadFile;
            if (dto.ReportingVideo != null) entity.ReportingVideo = dto.ReportingVideo;
        }

        /// <summary>
        /// Maps StuParticipantDemographicsUpdateDto to StuParticipantDemographics entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuParticipantDemographicsUpdateDto dto, StuParticipantDemographics entity)
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
        /// Maps StuProgramContentUpdateDto to StuProgramContentAndResources entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuProgramContentUpdateDto dto, StuProgramContentAndResources entity)
        {
            // Currently the update DTO has only Id, but keeping for future expansion
            // Add mappings here if more fields are added to the update DTO
        }

        /// <summary>
        /// Maps StuResourcePersonUpdateDto to StuResourcePerson entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuResourcePersonUpdateDto dto, StuResourcePerson entity)
        {
            if (dto.Name != null) entity.Name = dto.Name;
            if (dto.Designation != null) entity.Designation = dto.Designation;
            if (dto.ResourceType.HasValue) entity.ResourceType = dto.ResourceType;
            if (dto.Responsibility.HasValue) entity.Responsibility = dto.Responsibility;
            if (dto.InstitutionOrDepartment != null) entity.InstitutionOrDepartment = dto.InstitutionOrDepartment;
        }

        /// <summary>
        /// Maps StuTopicsCoveredUpdateDto to StuTopicsCoveredInClass entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuTopicsCoveredUpdateDto dto, StuTopicsCoveredInClass entity)
        {
            if (dto.Date.HasValue) entity.Date = dto.Date;
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.PhotoUpload != null) entity.PhotoUpload = dto.PhotoUpload;
        }

        /// <summary>
        /// Maps StuTeachingAidsUpdateDto to StuTeachingAidsDeveloped entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuTeachingAidsUpdateDto dto, StuTeachingAidsDeveloped entity)
        {
            if (dto.TypeOfAidId.HasValue) entity.TypeOfAidId = dto.TypeOfAidId;
            if (dto.OtherTypeOfAid != null) entity.OtherTypeOfAid = dto.OtherTypeOfAid;
            if (dto.Purpose != null) entity.Purpose = dto.Purpose;
            if (dto.Number.HasValue) entity.Number = dto.Number.Value;
        }

        /// <summary>
        /// Maps StuAdvisoryServicesUpdateDto to StuAdvisoryServices entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuAdvisoryServicesUpdateDto dto, StuAdvisoryServices entity)
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
        /// Maps StuReportUpdateDto to StuReport entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuReportUpdateDto dto, StuReport entity)
        {
            if (dto.ReportingYear != null) entity.ReportingYear = dto.ReportingYear;
            if (dto.ReportDate.HasValue) entity.ReportDate = dto.ReportDate;
            if (dto.ProgressReport != null) entity.ProgressReport = dto.ProgressReport;
            if (dto.GeoTaggedPhoto != null) entity.GeoTaggedPhoto = dto.GeoTaggedPhoto;
            if (dto.ReportingVideo != null) entity.ReportingVideo = dto.ReportingVideo;
            if (dto.Outcome != null) entity.Outcome = dto.Outcome;
            if (dto.TestingCompletionDate.HasValue) entity.TestingCompletionDate = dto.TestingCompletionDate;
            if (dto.TestingCompletionLetter != null) entity.TestingCompletionLetter = dto.TestingCompletionLetter;
            if (dto.ProjectCompletionDate.HasValue) entity.ProjectCompletionDate = dto.ProjectCompletionDate;
            if (dto.ProjectCompletionLetter != null) entity.ProjectCompletionLetter = dto.ProjectCompletionLetter;
            if (dto.TypeOfReport != null) entity.TypeOfReport = dto.TypeOfReport;
            if (dto.SpclReport != null) entity.SpclReport = dto.SpclReport;
        }

        /// <summary>
        /// Maps StuRecommendationUpdateDto to StuRecommendation entity
        /// </summary>
        public static void MapUpdateDtoToEntity(StuRecommendationUpdateDto dto, StuRecommendation entity)
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
