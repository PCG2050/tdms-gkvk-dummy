// Application/Mapper/DataTable/IBTVA/IbtvaProgramMapper.cs
using Application.Models.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.IBTVA
{
    [Mapper]
    public partial class IbtvaProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial IbtvaProgramDetailsDto MapToDto(IbtvaProgramDetails entity);

        public partial IbtvaProgramDetails MapToEntity(IbtvaProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(IbtvaProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(IbtvaProgramUpdateDto dto, IbtvaProgramDetails entity);

        // Custom mapping for complete program
        public IbtvaProgramCompleteDto MapToCompleteDto(IbtvaProgramDetails entity)
        {
            var dto = new IbtvaProgramCompleteDto
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

        public partial IbtvaParticipantDemographicsDto MapToDto(IbtvaParticipantDemographics entity);

        public partial IbtvaParticipantDemographics MapToEntity(IbtvaParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.IbtvaProgramDetailsId))]
        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaParticipantDemographicsUpdateDto dto, IbtvaParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public IbtvaProgramContentDto MapToDto(IbtvaProgramContentAndResources entity)
        {
            return new IbtvaProgramContentDto
            {
                Id = entity.Id,
                IbtvaProgramDetailsId = entity.IbtvaProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial IbtvaProgramContentAndResources MapToEntity(IbtvaProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        [MapProperty(nameof(IbtvaResourcePerson.ResourceType.Name), nameof(IbtvaResourcePersonDto.ResourceTypeName))]
        [MapProperty(nameof(IbtvaResourcePerson.Responsibility.Name), nameof(IbtvaResourcePersonDto.ResponsibilityName))]
        public partial IbtvaResourcePersonDto MapToDto(IbtvaResourcePerson entity);

        public partial IbtvaResourcePerson MapToEntity(IbtvaResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.IbtvaProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.UpdatedById))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.ResourceType))]
        [MapperIgnoreTarget(nameof(IbtvaResourcePerson.Responsibility))]
        public partial void MapUpdateDtoToEntity(IbtvaResourcePersonUpdateDto dto, IbtvaResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial IbtvaTopicsCoveredDto MapToDto(IbtvaTopicsCoveredInClass entity);

        public partial IbtvaTopicsCoveredInClass MapToEntity(IbtvaTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.IbtvaProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaTopicsCoveredUpdateDto dto, IbtvaTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial IbtvaTeachingAidsDto MapToDto(IbtvaTeachingAidsDeveloped entity);

        public partial IbtvaTeachingAidsDeveloped MapToEntity(IbtvaTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.IbtvaProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaTeachingAidsUpdateDto dto, IbtvaTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial IbtvaAdvisoryServicesDto MapToDto(IbtvaAdvisoryServices entity);

        public partial IbtvaAdvisoryServices MapToEntity(IbtvaAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.IbtvaProgramDetailsId))]
        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaAdvisoryServicesUpdateDto dto, IbtvaAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial IbtvaReportDto MapToDto(IbtvaReport entity);

        public partial IbtvaReport MapToEntity(IbtvaReportCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaReport.Id))]
        [MapperIgnoreTarget(nameof(IbtvaReport.IbtvaProgramDetailsId))]
        [MapperIgnoreTarget(nameof(IbtvaReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaReport.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaReportUpdateDto dto, IbtvaReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial IbtvaRecommendationDto MapToDto(IbtvaRecommendation entity);

        public partial IbtvaRecommendation MapToEntity(IbtvaRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(IbtvaRecommendation.Id))]
        [MapperIgnoreTarget(nameof(IbtvaRecommendation.IbtvaProgramDetailsId))]
        [MapperIgnoreTarget(nameof(IbtvaRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(IbtvaRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(IbtvaRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(IbtvaRecommendationUpdateDto dto, IbtvaRecommendation entity);
    }
}