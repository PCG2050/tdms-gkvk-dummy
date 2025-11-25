// Application/Mapper/DataTable/EEU/EeuProgramMapper.cs
using Application.Models.DataTables.EEU;
using Application.Models.DataTables.STU;
using Domain.Entities.EEU;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.EEU
{
    [Mapper]
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

                Report = entity.Reports != null ? MapToDto(entity.Reports) : null,
                Recommendation = entity.Recommendations != null ? MapToDto(entity.Recommendations) : null
            };

            return dto;
        }

        // ============================
        // DEMOGRAPHICS MAPPINGS
        // ============================

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
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial EeuProgramContentAndResources MapToEntity(EeuProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial EeuResourcePersonDto MapToDto(EeuResourcePerson entity);

        public partial EeuResourcePerson MapToEntity(EeuResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.EeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuResourcePerson.UpdatedById))]
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
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial EeuAdvisoryServicesDto MapToDto(EeuAdvisoryServices entity);

        public partial EeuAdvisoryServices MapToEntity(EeuAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.EeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(EeuAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(EeuAdvisoryServicesUpdateDto dto, EeuAdvisoryServices entity);


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