// Application/Mapper/DataTable/FTI/FtiProgramMapper.cs
using Application.Models.DataTables.FTI;
using Application.Models.DataTables.STU;
using Domain.Entities.FTI;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.FTI
{
    [Mapper]
    public partial class FtiProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial FtiProgramDetailsDto MapToDto(FtiProgramDetails entity);

        public partial FtiProgramDetails MapToEntity(FtiProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(FtiProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(FtiProgramUpdateDto dto, FtiProgramDetails entity);

        // Custom mapping for complete program
        public FtiProgramCompleteDto MapToCompleteDto(FtiProgramDetails entity)
        {
            var dto = new FtiProgramCompleteDto
            {
                ProgramDetails = MapToDto(entity),
                Demographics = entity.ParticipantDemographics?.Select(MapToDto).ToList(),
                ProgramContent = entity.ProgramContent?.Select(MapToDto).ToList(),
                AdvisoryServices = entity.AdvisoryServices != null ? MapToDto(entity.AdvisoryServices) : null,

                Report = entity.Reports != null ? MapToDto(entity.Reports) : null,
                Recommendation = entity.Recommendations != null ? MapToDto(entity.Recommendations) : null
            };

            return dto;
        }

        // ============================
        // DEMOGRAPHICS MAPPINGS
        // ============================

        public partial FtiParticipantDemographicsDto MapToDto(FtiParticipantDemographics entity);

        public partial FtiParticipantDemographics MapToEntity(FtiParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.FtiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiParticipantDemographicsUpdateDto dto, FtiParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public FtiProgramContentDto MapToDto(FtiProgramContentAndResources entity)
        {
            return new FtiProgramContentDto
            {
                Id = entity.Id,
                FtiProgramDetailsId = entity.FtiProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial FtiProgramContentAndResources MapToEntity(FtiProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial FtiResourcePersonDto MapToDto(FtiResourcePerson entity);

        public partial FtiResourcePerson MapToEntity(FtiResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(FtiResourcePerson.FtiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(FtiResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiResourcePerson.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiResourcePersonUpdateDto dto, FtiResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial FtiTopicsCoveredDto MapToDto(FtiTopicsCoveredInClass entity);

        public partial FtiTopicsCoveredInClass MapToEntity(FtiTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.FtiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiTopicsCoveredUpdateDto dto, FtiTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial FtiTeachingAidsDto MapToDto(FtiTeachingAidsDeveloped entity);

        public partial FtiTeachingAidsDeveloped MapToEntity(FtiTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.FtiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiTeachingAidsUpdateDto dto, FtiTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial FtiAdvisoryServicesDto MapToDto(FtiAdvisoryServices entity);

        public partial FtiAdvisoryServices MapToEntity(FtiAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.FtiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiAdvisoryServicesUpdateDto dto, FtiAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial FtiReportDto MapToDto(FtiReport entity);

        public partial FtiReport MapToEntity(FtiReportCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiReport.Id))]
        [MapperIgnoreTarget(nameof(FtiReport.FtiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(FtiReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiReport.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiReportUpdateDto dto, FtiReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial FtiRecommendationDto MapToDto(FtiRecommendation entity);

        public partial FtiRecommendation MapToEntity(FtiRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(FtiRecommendation.Id))]
        [MapperIgnoreTarget(nameof(FtiRecommendation.FtiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(FtiRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(FtiRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(FtiRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(FtiRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(FtiRecommendationUpdateDto dto, FtiRecommendation entity);
    }
}