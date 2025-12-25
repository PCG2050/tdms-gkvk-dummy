// Application/Mapper/DataTable/DEU/DeuProgramMapper.cs
using Application.Models.DataTables.DEU;
using Application.Models.DataTables.STU;
using Domain.Entities.DEU;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.DEU
{
    [Mapper]
    public partial class DeuProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial DeuProgramDetailsDto MapToDto(DeuProgramDetails entity);

        public partial DeuProgramDetails MapToEntity(DeuProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(DeuProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(DeuProgramUpdateDto dto, DeuProgramDetails entity);

        // Custom mapping for complete program
        public DeuProgramCompleteDto MapToCompleteDto(DeuProgramDetails entity)
        {
            var dto = new DeuProgramCompleteDto
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

        public partial DeuParticipantDemographicsDto MapToDto(DeuParticipantDemographics entity);

        public partial DeuParticipantDemographics MapToEntity(DeuParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.DeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuParticipantDemographicsUpdateDto dto, DeuParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public DeuProgramContentDto MapToDto(DeuProgramContentAndResources entity)
        {
            return new DeuProgramContentDto
            {
                Id = entity.Id,
                DeuProgramDetailsId = entity.DeuProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial DeuProgramContentAndResources MapToEntity(DeuProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        [MapProperty(nameof(DeuResourcePerson.ResourceType.Name), nameof(DeuResourcePersonDto.ResourceTypeName))]
        [MapProperty(nameof(DeuResourcePerson.Responsibility.Name), nameof(DeuResourcePersonDto.ResponsibilityName))]
        public partial DeuResourcePersonDto MapToDto(DeuResourcePerson entity);

        public partial DeuResourcePerson MapToEntity(DeuResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.DeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.UpdatedById))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.ResourceType))]
        [MapperIgnoreTarget(nameof(DeuResourcePerson.Responsibility))]
        public partial void MapUpdateDtoToEntity(DeuResourcePersonUpdateDto dto, DeuResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial DeuTopicsCoveredDto MapToDto(DeuTopicsCoveredInClass entity);

        public partial DeuTopicsCoveredInClass MapToEntity(DeuTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.DeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuTopicsCoveredUpdateDto dto, DeuTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial DeuTeachingAidsDto MapToDto(DeuTeachingAidsDeveloped entity);

        public partial DeuTeachingAidsDeveloped MapToEntity(DeuTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.DeuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuTeachingAidsUpdateDto dto, DeuTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial DeuAdvisoryServicesDto MapToDto(DeuAdvisoryServices entity);

        public partial DeuAdvisoryServices MapToEntity(DeuAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.DeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuAdvisoryServicesUpdateDto dto, DeuAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial DeuReportDto MapToDto(DeuReport entity);

        public partial DeuReport MapToEntity(DeuReportCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuReport.Id))]
        [MapperIgnoreTarget(nameof(DeuReport.DeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(DeuReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuReport.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuReportUpdateDto dto, DeuReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial DeuRecommendationDto MapToDto(DeuRecommendation entity);

        public partial DeuRecommendation MapToEntity(DeuRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(DeuRecommendation.Id))]
        [MapperIgnoreTarget(nameof(DeuRecommendation.DeuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(DeuRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(DeuRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(DeuRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(DeuRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(DeuRecommendationUpdateDto dto, DeuRecommendation entity);
    }
}