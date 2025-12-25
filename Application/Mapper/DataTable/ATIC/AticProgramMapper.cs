// Application/Mapper/DataTable/ATIC/AticProgramMapper.cs
using Application.Models.DataTables.ATIC;
using Domain.Entities.ATIC;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.ATIC
{
    [Mapper]
    public partial class AticProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        [MapProperty(nameof(AticProgramDetails.EventNames.Name), nameof(AticProgramDetailsDto.EventNamesName))]
        [MapProperty(nameof(AticProgramDetails.VAPOptions.Name), nameof(AticProgramDetailsDto.VAPOptionsName))]
        [MapProperty(nameof(AticProgramDetails.TargetFarmers.Name), nameof(AticProgramDetailsDto.TargetFarmersName))]
        [MapProperty(nameof(AticProgramDetails.ZoneOptions.Name), nameof(AticProgramDetailsDto.ZonesOptionsName))]
        [MapProperty(nameof(AticProgramDetails.Place.Name), nameof(AticProgramDetailsDto.PlaceName))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.EventNames))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.VAPOptions))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.TargetFarmers))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.ZoneOptions))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.Place))]
        public partial AticProgramDetailsDto MapToDto(AticProgramDetails entity);

        public partial AticProgramDetails MapToEntity(AticProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(AticProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.ApprovedById))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.EventNames))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.VAPOptions))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.TargetFarmers))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.ZoneOptions))]
        [MapperIgnoreTarget(nameof(AticProgramDetails.Place))]
        public partial void MapUpdateDtoToEntity(AticProgramUpdateDto dto, AticProgramDetails entity);

        // Custom mapping for complete program
        public AticProgramCompleteDto MapToCompleteDto(AticProgramDetails entity)
        {
            var dto = new AticProgramCompleteDto
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

        public partial AticParticipantDemographicsDto MapToDto(AticParticipantDemographics entity);

        public partial AticParticipantDemographics MapToEntity(AticParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(AticParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(AticParticipantDemographics.AticProgramDetailsId))]
        [MapperIgnoreTarget(nameof(AticParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(AticParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticParticipantDemographicsUpdateDto dto, AticParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public AticProgramContentDto MapToDto(AticProgramContentAndResources entity)
        {
            return new AticProgramContentDto
            {
                Id = entity.Id,
                AticProgramDetailsId = entity.AticProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial AticProgramContentAndResources MapToEntity(AticProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        [MapProperty(nameof(AticResourcePerson.ResourceType.Name), nameof(AticResourcePersonDto.ResourceTypeName))]
        [MapProperty(nameof(AticResourcePerson.Responsibility.Name), nameof(AticResourcePersonDto.ResponsibilityName))]
        public partial AticResourcePersonDto MapToDto(AticResourcePerson entity);

        public partial AticResourcePerson MapToEntity(AticResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(AticResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.AticProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.UpdatedById))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.ResourceType))]
        [MapperIgnoreTarget(nameof(AticResourcePerson.Responsibility))]
        public partial void MapUpdateDtoToEntity(AticResourcePersonUpdateDto dto, AticResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial AticTopicsCoveredDto MapToDto(AticTopicsCoveredInClass entity);

        public partial AticTopicsCoveredInClass MapToEntity(AticTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.AticProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticTopicsCoveredUpdateDto dto, AticTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial AticTeachingAidsDto MapToDto(AticTeachingAidsDeveloped entity);

        public partial AticTeachingAidsDeveloped MapToEntity(AticTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.AticProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticTeachingAidsUpdateDto dto, AticTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial AticAdvisoryServicesDto MapToDto(AticAdvisoryServices entity);

        public partial AticAdvisoryServices MapToEntity(AticAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(AticAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(AticAdvisoryServices.AticProgramDetailsId))]
        [MapperIgnoreTarget(nameof(AticAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(AticAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticAdvisoryServicesUpdateDto dto, AticAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial AticReportDto MapToDto(AticReport entity);

        public partial AticReport MapToEntity(AticReportCreateDto dto);

        [MapperIgnoreTarget(nameof(AticReport.Id))]
        [MapperIgnoreTarget(nameof(AticReport.AticProgramDetailsId))]
        [MapperIgnoreTarget(nameof(AticReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticReport.CreatedById))]
        [MapperIgnoreTarget(nameof(AticReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticReportUpdateDto dto, AticReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial AticRecommendationDto MapToDto(AticRecommendation entity);

        public partial AticRecommendation MapToEntity(AticRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(AticRecommendation.Id))]
        [MapperIgnoreTarget(nameof(AticRecommendation.AticProgramDetailsId))]
        [MapperIgnoreTarget(nameof(AticRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(AticRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(AticRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(AticRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(AticRecommendationUpdateDto dto, AticRecommendation entity);
    }
}