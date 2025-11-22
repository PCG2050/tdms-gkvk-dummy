// Application/Mapper/DataTable/STU/StuProgramMapper.cs
using Application.Models.DataTables.STU;
using Domain.Entities.STU;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.STU
{
    [Mapper]
    public partial class StuProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial StuProgramDetailsDto MapToDto(StuProgramDetails entity);

        public partial StuProgramDetails MapToEntity(StuProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(StuProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(StuProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(StuProgramUpdateDto dto, StuProgramDetails entity);

        // Custom mapping for complete program
        public StuProgramCompleteDto MapToCompleteDto(StuProgramDetails entity)
        {
            var dto = new StuProgramCompleteDto
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

        public partial StuParticipantDemographicsDto MapToDto(StuParticipantDemographics entity);

        public partial StuParticipantDemographics MapToEntity(StuParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(StuParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(StuParticipantDemographics.StuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(StuParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(StuParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuParticipantDemographicsUpdateDto dto, StuParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public StuProgramContentDto MapToDto(StuProgramContentAndResources entity)
        {
            return new StuProgramContentDto
            {
                Id = entity.Id,
                StuProgramDetailsId = entity.StuProgramDetailsId ?? 0,
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial StuProgramContentAndResources MapToEntity(StuProgramContentCreateDto dto);


        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial StuResourcePersonDto MapToDto(StuResourcePerson entity);

        public partial StuResourcePerson MapToEntity(StuResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(StuResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(StuResourcePerson.StuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(StuResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(StuResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuResourcePerson.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuResourcePersonUpdateDto dto, StuResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial StuTopicsCoveredDto MapToDto(StuTopicsCoveredInClass entity);

        public partial StuTopicsCoveredInClass MapToEntity(StuTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.StuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuTopicsCoveredUpdateDto dto, StuTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial StuTeachingAidsDto MapToDto(StuTeachingAidsDeveloped entity);

        public partial StuTeachingAidsDeveloped MapToEntity(StuTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.StuProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuTeachingAidsUpdateDto dto, StuTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial StuAdvisoryServicesDto MapToDto(StuAdvisoryServices entity);

        public partial StuAdvisoryServices MapToEntity(StuAdvisoryServicesCreateDto dto);

        [MapperIgnoreTarget(nameof(StuAdvisoryServices.Id))]
        [MapperIgnoreTarget(nameof(StuAdvisoryServices.StuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(StuAdvisoryServices.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuAdvisoryServices.CreatedById))]
        [MapperIgnoreTarget(nameof(StuAdvisoryServices.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuAdvisoryServices.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuAdvisoryServicesUpdateDto dto, StuAdvisoryServices entity);


        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial StuReportDto MapToDto(StuReport entity);

        public partial StuReport MapToEntity(StuReportCreateDto dto);

        [MapperIgnoreTarget(nameof(StuReport.Id))]
        [MapperIgnoreTarget(nameof(StuReport.StuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(StuReport.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuReport.CreatedById))]
        [MapperIgnoreTarget(nameof(StuReport.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuReport.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuReportUpdateDto dto, StuReport entity);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial StuRecommendationDto MapToDto(StuRecommendation entity);

        public partial StuRecommendation MapToEntity(StuRecommendationCreateDto dto);

        [MapperIgnoreTarget(nameof(StuRecommendation.Id))]
        [MapperIgnoreTarget(nameof(StuRecommendation.StuProgramDetailsId))]
        [MapperIgnoreTarget(nameof(StuRecommendation.CreatedAt))]
        [MapperIgnoreTarget(nameof(StuRecommendation.CreatedById))]
        [MapperIgnoreTarget(nameof(StuRecommendation.UpdatedAt))]
        [MapperIgnoreTarget(nameof(StuRecommendation.UpdatedById))]
        public partial void MapUpdateDtoToEntity(StuRecommendationUpdateDto dto, StuRecommendation entity);
    }
}