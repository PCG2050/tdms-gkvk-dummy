// Application/Mapper/DataTable/NAEP/NaepProgramMapper.cs
using Application.Models.DataTables.NAEP;
using Domain.Entities.NAEP;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.NAEP
{
    [Mapper]
    public partial class NaepProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial NaepProgramDetailsDto MapToDto(NaepProgramDetails entity);

        public partial NaepProgramDetails MapToEntity(NaepProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(NaepProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(NaepProgramUpdateDto dto, NaepProgramDetails entity);

        // Custom mapping for complete program
        public NaepProgramCompleteDto MapToCompleteDto(NaepProgramDetails entity)
        {
            var dto = new NaepProgramCompleteDto
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

        public partial NaepParticipantDemographicsDto MapToDto(NaepParticipantDemographics entity);

        public partial NaepParticipantDemographics MapToEntity(NaepParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.NaepProgramDetailsId))]
        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepParticipantDemographicsUpdateDto dto, NaepParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public NaepProgramContentDto MapToDto(NaepProgramContentAndResources entity)
        {
            return new NaepProgramContentDto
            {
                Id = entity.Id,
                NaepProgramDetailsId = entity.NaepProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial NaepProgramContentAndResources MapToEntity(NaepProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial NaepResourcePersonDto MapToDto(NaepResourcePerson entity);

        public partial NaepResourcePerson MapToEntity(NaepResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(NaepResourcePerson.NaepProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(NaepResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepResourcePerson.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepResourcePersonUpdateDto dto, NaepResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial NaepTopicsCoveredDto MapToDto(NaepTopicsCoveredInClass entity);

        public partial NaepTopicsCoveredInClass MapToEntity(NaepTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.NaepProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepTopicsCoveredUpdateDto dto, NaepTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial NaepTeachingAidsDto MapToDto(NaepTeachingAidsDeveloped entity);

        public partial NaepTeachingAidsDeveloped MapToEntity(NaepTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.NaepProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepTeachingAidsUpdateDto dto, NaepTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial NaepAdvisoryServicesDto MapToDto(NaepAdvisoryServices entity);

        public partial NaepAdvisoryServices MapToEntity(NaepAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.NaepProgramDetailsId))]
        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepAdvisoryServicesUpdateDto dto, NaepAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial NaepReportDto MapToDto(NaepReport entity);

        public partial NaepReport MapToEntity(NaepReportCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepReport.Id))]
        [MapperIgnoreTarget(nameof(NaepReport.NaepProgramDetailsId))]
        [MapperIgnoreTarget(nameof(NaepReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepReport.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepReportUpdateDto dto, NaepReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial NaepRecommendationDto MapToDto(NaepRecommendation entity);

        public partial NaepRecommendation MapToEntity(NaepRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(NaepRecommendation.Id))]
        [MapperIgnoreTarget(nameof(NaepRecommendation.NaepProgramDetailsId))]
        [MapperIgnoreTarget(nameof(NaepRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(NaepRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(NaepRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(NaepRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(NaepRecommendationUpdateDto dto, NaepRecommendation entity);
    }
}