// Application/Mapper/DataTable/EEU/EeuProgramMapper.cs
using Application.Models.DataTables.EEU;
using Domain.Entities.EEU;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.EEU
{

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target, AllowNullPropertyAssignment = true)]
    public partial class EeuProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial EeuProgramDetailsDto MapToDto(EeuProgramDetails entity);

        public partial EeuProgramDetails MapToEntity(EeuProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(EeuProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(EeuProgramUpdateDto dto, EeuProgramDetails entity);

        // Custom mapping for complete program
        public EeuProgramCompleteDto MapToCompleteDto(EeuProgramDetails entity)
        {
            var dto = new EeuProgramCompleteDto
            {
                ProgramDetails = MapToDto(entity),
                Demographics = entity.ParticipantDemographics?.Select(MapToDto).ToList(),
                ProgramContent = entity.ProgramContent?.Select(MapToDto).ToList(),
                AdvisoryServices = entity.AdvisoryServices != null ? MapToDto(entity.AdvisoryServices) : null,
                Results = entity.Results != null ? MapToDto(entity.Results) : null,
                Report = entity.Reports != null ? MapToDto(entity.Reports) : null,
                Recommendation = entity.Recommendations != null ? MapToDto(entity.Recommendations) : null
            };

            return dto;
        }

        // ============================
        // DEMOGRAPHICS MAPPINGS
        // ============================

        //public partial EeuParticipantDemographicsDto MapToDto(EeuParticipantDemographics entity);

        //public partial EeuParticipantDemographics MapToEntity(EeuParticipantDemographicsCreateDto dto);

        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.Id))]
        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.EeuProgramDetailsId))]
        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.CreatedAt))]
        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.CreatedById))]
        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.UpdatedAt))]
        //[MapperIgnoreTarget(nameof(EeuParticipantDemographics.UpdatedById))]
        //public partial void MapUpdateDtoToEntity(EeuParticipantDemographicsUpdateDto dto, EeuParticipantDemographics entity);


        //public partial class EeuProgramMapper
        //{
        public partial EeuParticipantDemographicsDto MapToDto(EeuParticipantDemographics entity);
        public partial EeuParticipantDemographics MapToEntity(EeuParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.EeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuParticipantDemographicsUpdateDto dto, EeuParticipantDemographics entity);






        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public EeuProgramContentDto MapToDto(EeuProgramContentAndResources entity)
        {
            return new EeuProgramContentDto
            {
                Id = entity.Id,
                EeuProgramDetailsId = entity.EeuProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList(),
                FieldVisits = entity.FieldVisits?.Select(MapToDto).ToList(),
                FieldDays = entity.FieldDays?.Select(MapToDto).ToList(),
                FarmerScientistInteractions = entity.FarmerScientistInteractions?.Select(MapToDto).ToList()
            };
        }

        public partial EeuProgramContentAndResources MapToEntity(EeuProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        [MapProperty(nameof(EeuResourcePerson.ResourceType.Name), nameof(EeuResourcePersonDto.ResourceTypeName))]
        [MapProperty(nameof(EeuResourcePerson.Responsibility.Name), nameof(EeuResourcePersonDto.ResponsibilityName))]
        public partial EeuResourcePersonDto MapToDto(EeuResourcePerson entity);

        public partial EeuResourcePerson MapToEntity(EeuResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.UpdatedById))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.ResourceType))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.Responsibility))]
        public partial void MapUpdateDtoToEntity(EeuResourcePersonUpdateDto dto, EeuResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial EeuTopicsCoveredDto MapToDto(EeuTopicsCoveredInClass entity);

        public partial EeuTopicsCoveredInClass MapToEntity(EeuTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuTopicsCoveredUpdateDto dto, EeuTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial EeuTeachingAidsDto MapToDto(EeuTeachingAidsDeveloped entity);

        public partial EeuTeachingAidsDeveloped MapToEntity(EeuTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuTeachingAidsUpdateDto dto, EeuTeachingAidsDeveloped entity);

        // ============================
        // FIELD VISIT MAPPINGS
        // ============================

        public partial EeuFieldVisitDto MapToDto(EeuFieldVisit entity);

        public partial EeuFieldVisit MapToEntity(EeuFieldVisitCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuFieldVisit.Id))]
        [MapperIgnoreTarget(nameof(EeuFieldVisit.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuFieldVisit.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuFieldVisit.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuFieldVisit.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuFieldVisit.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuFieldVisitUpdateDto dto, EeuFieldVisit entity);

        // ============================
        // FIELD DAY MAPPINGS
        // ============================

        public partial EeuFieldDayDto MapToDto(EeuFieldDay entity);

        public partial EeuFieldDay MapToEntity(EeuFieldDayCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuFieldDay.Id))]
        [MapperIgnoreTarget(nameof(EeuFieldDay.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuFieldDay.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuFieldDay.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuFieldDay.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuFieldDay.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuFieldDayUpdateDto dto, EeuFieldDay entity);

        // ============================
        // FARMER SCIENTIST INTERACTION MAPPINGS
        // ============================

        public partial EeuFarmerScientistInteractionDto MapToDto(EeuFarmerScientistInteraction entity);

        public partial EeuFarmerScientistInteraction MapToEntity(EeuFarmerScientistInteractionCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.Id))]
        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuFarmerScientistInteraction.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuFarmerScientistInteractionUpdateDto dto, EeuFarmerScientistInteraction entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public EeuAdvisoryServicesDto MapToDto(EeuAdvisoryServices entity)
        {
            return new EeuAdvisoryServicesDto
            {
                Id = entity.Id,
                NoOfFacebookSMS = entity.NoOfFacebookSMS,
                NoOfSMSSentToRegisteredFarmers = entity.NoOfSMSSentToRegisteredFarmers,
                NoOfWhatsappGroups = entity.NoOfWhatsappGroups,
                NoOfWhatsappSMS = entity.NoOfWhatsappSMS,
                NoOfAnsweredWhatsappQueries = entity.NoOfAnsweredWhatsappQueries,
                NoOfPhoneCalls = entity.NoOfPhoneCalls,
                NoOfFaceToFaceDiscussions = entity.NoOfFaceToFaceDiscussions,
                NoOfGroupDiscussions = entity.NoOfGroupDiscussions,
                NoOfEmailsSent = entity.NoOfEmailsSent,
                NoOfNewspaperCoverage = entity.NoOfNewspaperCoverage,
                NoOfBeneficiaries = entity.NoOfBeneficiaries,
                CriticalInputsDistributed = entity.CriticalInputsDistributed?.Select(MapToDto).ToList()
            };
        }

        public partial EeuAdvisoryServices MapToEntity(EeuAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.EeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuAdvisoryServicesUpdateDto dto, EeuAdvisoryServices entity);

        // ============================
        // CRITICAL INPUTS DISTRIBUTED MAPPINGS
        // ============================

        public partial EeuCriticalInputsDistributedDto MapToDto(EeuCriticalInputsDistributed entity);

        public partial EeuCriticalInputsDistributed MapToEntity(EeuCriticalInputsDistributedCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.Id))]
        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.EeuAdvisoryServicesId))]
        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuCriticalInputsDistributed.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuCriticalInputsDistributedUpdateDto dto, EeuCriticalInputsDistributed entity);

        // ============================
        // RESULT MAPPINGS (FLD/OFT)
        // ============================

        public EeuResultDto MapToDto(EeuResult entity)
        {
            return new EeuResultDto
            {
                Id = entity.Id,
                EeuProgramDetailsId = entity.EeuProgramDetailsId,
                UploadExcelUrl = entity.UploadExcelUrl,
                FldResults = entity.FldResults?.Select(MapToDto).ToList(),
                OftResults = entity.OftResults?.Select(MapToDto).ToList()
            };
        }

        public partial EeuFldResultDto MapToDto(EeuFldResult entity);

        public partial EeuFldResult MapToEntity(EeuFldResultCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuFldResult.Id))]
        [MapperIgnoreTarget(nameof(EeuFldResult.EeuResultId))]
        [MapperIgnoreTarget(nameof(EeuFldResult.UnitLocationId))]
        [MapperIgnoreTarget(nameof(EeuFldResult.OrganizationId))]
        [MapperIgnoreTarget(nameof(EeuFldResult.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuFldResult.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuFldResult.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuFldResult.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuFldResultUpdateDto dto, EeuFldResult entity);

        public partial EeuOftResultDto MapToDto(EeuOftResult entity);

        public partial EeuOftResult MapToEntity(EeuOftResultCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuOftResult.Id))]
        [MapperIgnoreTarget(nameof(EeuOftResult.EeuResultId))]
        [MapperIgnoreTarget(nameof(EeuOftResult.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuOftResult.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuOftResult.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuOftResult.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuOftResultUpdateDto dto, EeuOftResult entity);

        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial EeuReportDto MapToDto(EeuReport entity);

        public partial EeuReport MapToEntity(EeuReportCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuReport.Id))]
        [MapperIgnoreTarget(nameof(EeuReport.EeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(EeuReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuReport.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuReportUpdateDto dto, EeuReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial EeuRecommendationDto MapToDto(EeuRecommendation entity);

        public partial EeuRecommendation MapToEntity(EeuRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuRecommendation.Id))]
        [MapperIgnoreTarget(nameof(EeuRecommendation.EeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(EeuRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuRecommendationUpdateDto dto, EeuRecommendation entity);
    }
}