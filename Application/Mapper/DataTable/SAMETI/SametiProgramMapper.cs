// Application/Mapper/DataTable/SAMETI/SametiProgramMapper.cs
using Application.Models.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.SAMETI
{
    [Mapper]
    public partial class SametiProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial SametiProgramDetailsDto MapToDto(SametiProgramDetails entity);

        public partial SametiProgramDetails MapToEntity(SametiProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(SametiProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(SametiProgramUpdateDto dto, SametiProgramDetails entity);

        // Custom mapping for complete program
        public SametiProgramCompleteDto MapToCompleteDto(SametiProgramDetails entity)
        {
            var dto = new SametiProgramCompleteDto
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

        public partial SametiParticipantDemographicsDto MapToDto(SametiParticipantDemographics entity);

        public partial SametiParticipantDemographics MapToEntity(SametiParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.SametiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiParticipantDemographicsUpdateDto dto, SametiParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public SametiProgramContentDto MapToDto(SametiProgramContentAndResources entity)
        {
            return new SametiProgramContentDto
            {
                Id = entity.Id,
                SametiProgramDetailsId = entity.SametiProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial SametiProgramContentAndResources MapToEntity(SametiProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial SametiResourcePersonDto MapToDto(SametiResourcePerson entity);

        public partial SametiResourcePerson MapToEntity(SametiResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(SametiResourcePerson.SametiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(SametiResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiResourcePerson.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiResourcePersonUpdateDto dto, SametiResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial SametiTopicsCoveredDto MapToDto(SametiTopicsCoveredInClass entity);

        public partial SametiTopicsCoveredInClass MapToEntity(SametiTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.SametiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiTopicsCoveredUpdateDto dto, SametiTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial SametiTeachingAidsDto MapToDto(SametiTeachingAidsDeveloped entity);

        public partial SametiTeachingAidsDeveloped MapToEntity(SametiTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.SametiProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiTeachingAidsUpdateDto dto, SametiTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial SametiAdvisoryServicesDto MapToDto(SametiAdvisoryServices entity);

        public partial SametiAdvisoryServices MapToEntity(SametiAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.SametiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiAdvisoryServicesUpdateDto dto, SametiAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial SametiReportDto MapToDto(SametiReport entity);

        public partial SametiReport MapToEntity(SametiReportCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiReport.Id))]
        [MapperIgnoreTarget(nameof(SametiReport.SametiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(SametiReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiReport.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiReportUpdateDto dto, SametiReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial SametiRecommendationDto MapToDto(SametiRecommendation entity);

        public partial SametiRecommendation MapToEntity(SametiRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(SametiRecommendation.Id))]
        [MapperIgnoreTarget(nameof(SametiRecommendation.SametiProgramDetailsId))]
        [MapperIgnoreTarget(nameof(SametiRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(SametiRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(SametiRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(SametiRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(SametiRecommendationUpdateDto dto, SametiRecommendation entity);
    }
}