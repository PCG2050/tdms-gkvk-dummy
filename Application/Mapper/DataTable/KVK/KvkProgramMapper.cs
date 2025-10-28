// Application/Mapper/DataTable/KVK/KvkProgramMapper.cs
using Application.Models.DataTables.KVK;
using Domain.Entities.KVK;
using Riok.Mapperly.Abstractions;

namespace Application.Mapper.DataTable.KVK
{
    [Mapper]
    public partial class KvkProgramMapper
    {
        // ============================
        // PROGRAM DETAILS MAPPINGS
        // ============================

        public partial KvkProgramDetailsDto MapToDto(KvkProgramDetails entity);

        public partial KvkProgramDetails MapToEntity(KvkProgramCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkProgramDetails.Id))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.UpdatedById))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.FormStatus))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.FormStatusRemarks))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.ApprovedAt))]
        [MapperIgnoreTarget(nameof(KvkProgramDetails.ApprovedById))]
        public partial void MapUpdateDtoToEntity(KvkProgramUpdateDto dto, KvkProgramDetails entity);

        // Custom mapping for complete program
        public KvkProgramCompleteDto MapToCompleteDto(KvkProgramDetails entity)
        {
            var dto = new KvkProgramCompleteDto
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

        public partial KvkParticipantDemographicsDto MapToDto(KvkParticipantDemographics entity);

        public partial KvkParticipantDemographics MapToEntity(KvkParticipantDemographicsCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.Id))]
        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.KvkProgramDetailsId))]
        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkParticipantDemographics.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkParticipantDemographicsUpdateDto dto, KvkParticipantDemographics entity);

        // ============================
        // PROGRAM CONTENT MAPPINGS
        // ============================

        public KvkProgramContentDto MapToDto(KvkProgramContentAndResources entity)
        {
            return new KvkProgramContentDto
            {
                Id = entity.Id,
                KvkProgramDetailsId = entity.KvkProgramDetailsId ?? 0,             
                ResourcePersons = entity.ResourcePersons?.Select(MapToDto).ToList(),
                TopicsCovered = entity.TopicsCovered?.Select(MapToDto).ToList(),
                TeachingAids = entity.TeachingAids?.Select(MapToDto).ToList()
            };
        }

        public partial KvkProgramContentAndResources MapToEntity(KvkProgramContentCreateDto dto);
     

        // ============================
        // RESOURCE PERSON MAPPINGS
        // ============================

        public partial KvkResourcePersonDto MapToDto(KvkResourcePerson entity);

        public partial KvkResourcePerson MapToEntity(KvkResourcePersonCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkResourcePerson.Id))]
        [MapperIgnoreTarget(nameof(KvkResourcePerson.KvkProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(KvkResourcePerson.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkResourcePerson.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkResourcePerson.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkResourcePerson.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkResourcePersonUpdateDto dto, KvkResourcePerson entity);

        // ============================
        // TOPICS COVERED MAPPINGS
        // ============================

        public partial KvkTopicsCoveredDto MapToDto(KvkTopicsCoveredInClass entity);

        public partial KvkTopicsCoveredInClass MapToEntity(KvkTopicsCoveredCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.Id))]
        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.KvkProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkTopicsCoveredInClass.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkTopicsCoveredUpdateDto dto, KvkTopicsCoveredInClass entity);

        // ============================
        // TEACHING AIDS MAPPINGS
        // ============================

        public partial KvkTeachingAidsDto MapToDto(KvkTeachingAidsDeveloped entity);

        public partial KvkTeachingAidsDeveloped MapToEntity(KvkTeachingAidsCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.Id))]
        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.KvkProgramContentAndResourcesId))]
        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkTeachingAidsDeveloped.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkTeachingAidsUpdateDto dto, KvkTeachingAidsDeveloped entity);

        // ============================
        // ADVISORY SERVICES MAPPINGS
        // ============================

        public partial KvkAdvisoryServicesDto MapToDto(KvkAdvisoryServices entity);

        public partial KvkAdvisoryServices MapToEntity(KvkAdvisoryServicesCreateDto dto);

        // ============================
        // RESULT MAPPINGS (FLD/OFT)
        // ============================

        public KvkResultDto MapToDto(KvkResult entity)
        {
            return new KvkResultDto
            {
                Id = entity.Id,
                KvkProgramDetailsId = entity.KvkProgramDetailsId,
                UploadExcelUrl = entity.UploadExcelUrl,
                FldResults = entity.FldResults?.Select(MapToDto).ToList(),
                OftResults = entity.OftResults?.Select(MapToDto).ToList()
            };
        }

        public partial KvkFldResultDto MapToDto(KvkFldResult entity);

        public partial KvkFldResult MapToEntity(KvkFldResultCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkFldResult.Id))]
        [MapperIgnoreTarget(nameof(KvkFldResult.KvkResultId))]
        [MapperIgnoreTarget(nameof(KvkFldResult.UnitLocationId))]
        [MapperIgnoreTarget(nameof(KvkFldResult.OrganizationId))]
        [MapperIgnoreTarget(nameof(KvkFldResult.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkFldResult.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkFldResult.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkFldResult.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkFldResultUpdateDto dto, KvkFldResult entity);

        public partial KvkOftResultDto MapToDto(KvkOftResult entity);

        public partial KvkOftResult MapToEntity(KvkOftResultCreateDto dto);

        [MapperIgnoreTarget(nameof(KvkOftResult.Id))]
        [MapperIgnoreTarget(nameof(KvkOftResult.KvkResultId))]
        [MapperIgnoreTarget(nameof(KvkOftResult.CreatedAt))]
        [MapperIgnoreTarget(nameof(KvkOftResult.CreatedById))]
        [MapperIgnoreTarget(nameof(KvkOftResult.UpdatedAt))]
        [MapperIgnoreTarget(nameof(KvkOftResult.UpdatedById))]
        public partial void MapUpdateDtoToEntity(KvkOftResultUpdateDto dto, KvkOftResult entity);

        // ============================
        // REPORT MAPPINGS
        // ============================

        public partial KvkReportDto MapToDto(KvkReport entity);

        public partial KvkReport MapToEntity(KvkReportCreateDto dto);

        // ============================
        // RECOMMENDATION MAPPINGS
        // ============================

        public partial KvkRecommendationDto MapToDto(KvkRecommendation entity);

        public partial KvkRecommendation MapToEntity(KvkRecommendationCreateDto dto);
    }
}