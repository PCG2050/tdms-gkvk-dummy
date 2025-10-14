// =================================================================
// File: Application/Mappers/IbtvaProgramMapper.cs
// =================================================================
using Application.Models.DataTables;
using Application.Models.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers.IBTVA
{
    /// <summary>
    /// Mapperly mapper for IBTVA Program and all related entities (Phases 1-6)
    /// </summary>
    [Mapper]
    public partial class IbtvaProgramMapper
    {
        // ========== PHASE 1: Program Details ==========

        /// <summary>
        /// Map CreateDto to Program Entity
        /// </summary>
        public partial IbtvaProgramDetails MapToEntity(IbtvaProgramCreateDto dto);

        /// <summary>
        /// Map Program Entity to Dto
        /// </summary>
        [MapProperty(nameof(IbtvaProgramDetails.CreatedAt), nameof(IbtvaProgramDetailsDto.CreatedAt), Use = nameof(MapDateTimeOffset))]
        [MapProperty(nameof(IbtvaProgramDetails.UpdatedAt), nameof(IbtvaProgramDetailsDto.UpdatedAt), Use = nameof(MapNullableDateTimeOffset))]
        public partial IbtvaProgramDetailsDto MapToDto(IbtvaProgramDetails entity);

        /// <summary>
        /// Map UpdateDto to existing Program Entity
        /// </summary>
        public partial void MapUpdateToEntity(IbtvaProgramUpdateDto dto, IbtvaProgramDetails entity);

        // ========== PHASE 2: Demographics ==========

        /// <summary>
        /// Map Demographics CreateDto to Entity
        /// </summary>
        public partial IbtvaParticipantDemographics MapToEntity(DemographicsCreateDto dto);

        /// <summary>
        /// Map Demographics Entity to Dto
        /// </summary>
        public partial DemographicsDto MapToDto(IbtvaParticipantDemographics entity);

        // ========== PHASE 3: Content & Resources ==========

        /// <summary>
        /// Map Content CreateDto to Entity
        /// </summary>
        public partial IbtvaProgramContentAndResources MapToEntity(ContentResourcesCreateDto dto);

        /// <summary>
        /// Map Content Entity to Dto
        /// </summary>
        public partial ContentResourcesDto MapToDto(IbtvaProgramContentAndResources entity);

        /// <summary>
        /// Map ResourcePerson CreateDto to Entity
        /// </summary>
        public partial IbtvaResourcePerson MapToEntity(ResourcePersonCreateDto dto);

        /// <summary>
        /// Map ResourcePerson Entity to Dto
        /// </summary>
        public partial ResourcePersonDto MapToDto(IbtvaResourcePerson entity);

        /// <summary>
        /// Map Topic CreateDto to Entity
        /// </summary>
        public partial IbtvaTopicsCoveredInClass MapToEntity(TopicCreateDto dto);

        /// <summary>
        /// Map Topic Entity to Dto
        /// </summary>
        public partial TopicDto MapToDto(IbtvaTopicsCoveredInClass entity);

        /// <summary>
        /// Map TeachingAid CreateDto to Entity
        /// </summary>
        public partial IbtvaTeachingAidsDeveloped MapToEntity(TeachingAidCreateDto dto);

        /// <summary>
        /// Map TeachingAid Entity to Dto
        /// </summary>
        public partial TeachingAidDto MapToDto(IbtvaTeachingAidsDeveloped entity);

        // ========== PHASE 4: Advisory Services ==========

        /// <summary>
        /// Map Advisory CreateDto to Entity
        /// </summary>
        public partial IbtvaAdvisoryServices MapToEntity(AdvisoryCreateDto dto);

        /// <summary>
        /// Map Advisory Entity to Dto
        /// </summary>
        public partial AdvisoryDto MapToDto(IbtvaAdvisoryServices entity);

        // ========== PHASE 5: Reports ==========

        /// <summary>
        /// Map Report CreateDto to Entity
        /// </summary>
        public partial IbtvaReport MapToEntity(ReportCreateDto dto);

        /// <summary>
        /// Map Report Entity to Dto
        /// </summary>
        public partial ReportDto MapToDto(IbtvaReport entity);

        // ========== PHASE 6: Recommendations ==========

        /// <summary>
        /// Map Recommendation CreateDto to Entity
        /// </summary>
        public partial IbtvaRecommendation MapToEntity(RecommendationCreateDto dto);

        /// <summary>
        /// Map Recommendation Entity to Dto
        /// </summary>
        public partial RecommendationDto MapToDto(IbtvaRecommendation entity);

        // ========== Helper Methods ==========

        /// <summary>
        /// Convert DateTimeOffset to DateTime
        /// </summary>
        private DateTime MapDateTimeOffset(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.DateTime;
        }

        /// <summary>
        /// Convert nullable DateTimeOffset to nullable DateTime
        /// </summary>
        private DateTime? MapNullableDateTimeOffset(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset?.DateTime;
        }

        // ========== Phase 1 Helper Methods with Timestamps ==========

        /// <summary>
        /// Create Program Entity with automatic CreatedAt and initial status
        /// </summary>
        public IbtvaProgramDetails CreateProgramEntity(
            IbtvaProgramCreateDto dto,
            int trainerId,
            int organizationId)
        {
            var entity = MapToEntity(dto);
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.CreatedById = trainerId;
            entity.OrganizationId = organizationId;
            entity.FormStatus = "Saved";
            entity.CurrentPhase = 1;
            return entity;
        }

        /// <summary>
        /// Update Program Entity with automatic UpdatedAt
        /// </summary>
        public void UpdateProgramEntity(
            IbtvaProgramUpdateDto dto,
            IbtvaProgramDetails entity,
            int trainerId)
        {
            MapUpdateToEntity(dto, entity);
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = trainerId;
        }

        // ========== Phase 2 Helper Methods ==========

        /// <summary>
        /// Create Demographics Entity with metadata
        /// </summary>
        public IbtvaParticipantDemographics CreateDemographicsEntity(
            DemographicsCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Update Demographics Entity with metadata
        /// </summary>
        public void UpdateDemographicsEntity(
            DemographicsCreateDto dto,
            IbtvaParticipantDemographics entity,
            int trainerId)
        {
            var updated = MapToEntity(dto);
            entity.ParticipantId = updated.ParticipantId;
            entity.Male_SC = updated.Male_SC;
            entity.Male_ST = updated.Male_ST;
            entity.Male_OBC = updated.Male_OBC;
            entity.Male_GEN = updated.Male_GEN;
            entity.Female_SC = updated.Female_SC;
            entity.Female_ST = updated.Female_ST;
            entity.Female_OBC = updated.Female_OBC;
            entity.Female_GEN = updated.Female_GEN;
            entity.Total = updated.Total;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = trainerId;
        }

        // ========== Phase 3 Helper Methods ==========

        /// <summary>
        /// Create Content Entity with metadata
        /// </summary>
        public IbtvaProgramContentAndResources CreateContentEntity(
            ContentResourcesCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Create ResourcePerson Entity with metadata
        /// </summary>
        public IbtvaResourcePerson CreateResourcePersonEntity(
            ResourcePersonCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramContentAndResourcesId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Create Topic Entity with metadata
        /// </summary>
        public IbtvaTopicsCoveredInClass CreateTopicEntity(
            TopicCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramContentAndResourcesId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Create TeachingAid Entity with metadata
        /// </summary>
        public IbtvaTeachingAidsDeveloped CreateTeachingAidEntity(
            TeachingAidCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramContentAndResourcesId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        // ========== Phase 4 Helper Methods ==========

        /// <summary>
        /// Create Advisory Entity with metadata
        /// </summary>
        public IbtvaAdvisoryServices CreateAdvisoryEntity(
            AdvisoryCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Update Advisory Entity with metadata
        /// </summary>
        public void UpdateAdvisoryEntity(
            AdvisoryCreateDto dto,
            IbtvaAdvisoryServices entity,
            int trainerId)
        {
            var updated = MapToEntity(dto);
            entity.NoOfBeneficiaries = updated.NoOfBeneficiaries;
            entity.NoOfFaceToFaceDiscussions = updated.NoOfFaceToFaceDiscussions;
            entity.NoOfGroupDiscussions = updated.NoOfGroupDiscussions;
            entity.NoOfPhoneCalls = updated.NoOfPhoneCalls;
            entity.NoOfWhatsappSMS = updated.NoOfWhatsappSMS;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = trainerId;
        }

        // ========== Phase 5 Helper Methods ==========

        /// <summary>
        /// Create Report Entity with metadata
        /// </summary>
        public IbtvaReport CreateReportEntity(
            ReportCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            // Map ReportDto fields to IbtvaReport fields
            entity.ProgressReportReportingYear = dto.ReportTitle;
            entity.Date = dto.ReportDate;
            entity.UploadPhoto = dto.ReportFileUrl;
            entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.ReportFileUrl;
            entity.UploadVideo = dto.ReportFileUrl;
            entity.SignificantOutcome = dto.ReportTitle;

            return entity;
        }

        /// <summary>
        /// Update Report Entity with metadata
        /// </summary>
        public void UpdateReportEntity(
            ReportCreateDto dto,
            IbtvaReport entity,
            int trainerId)
        {
            entity.ProgressReportReportingYear = dto.ReportTitle;
            entity.Date = dto.ReportDate;
            entity.UploadPhoto = dto.ReportFileUrl;
            entity.PhotosGeotaggedPhotoOrUploadPhoto = dto.ReportFileUrl;
            entity.UploadVideo = dto.ReportFileUrl;
            entity.SignificantOutcome = dto.ReportTitle;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = trainerId;
        }

        // ========== Phase 6 Helper Methods ==========

        /// <summary>
        /// Create Recommendation Entity with metadata
        /// </summary>
        public IbtvaRecommendation CreateRecommendationEntity(
            RecommendationCreateDto dto,
            int programId,
            int trainerId)
        {
            var entity = MapToEntity(dto);
            entity.IbtvaProgramDetailsId = programId;
            entity.CreatedById = trainerId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return entity;
        }

        /// <summary>
        /// Update Recommendation Entity with metadata
        /// </summary>
        public void UpdateRecommendationEntity(
            RecommendationCreateDto dto,
            IbtvaRecommendation entity,
            int trainerId)
        {
            var updated = MapToEntity(dto);
            entity.Recommendation = updated.Recommendation;
            entity.UpdatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = trainerId;
        }
    }
}

// =================================================================
// USAGE EXAMPLES:
// =================================================================
/*
// Phase 1: Program
var program = _mapper.CreateProgramEntity(createDto, trainerId, organizationId);
_mapper.UpdateProgramEntity(updateDto, existingProgram, trainerId);
var programDto = _mapper.MapToDto(programEntity);

// Phase 2: Demographics
var demographic = _mapper.CreateDemographicsEntity(createDto, programId, trainerId);
_mapper.UpdateDemographicsEntity(updateDto, existingDemographic, trainerId);
var demographicDto = _mapper.MapToDto(demographicEntity);

// Phase 3: Content
var content = _mapper.CreateContentEntity(createDto, programId, trainerId);
var resourcePerson = _mapper.CreateResourcePersonEntity(createDto, programId, trainerId);
var topic = _mapper.CreateTopicEntity(createDto, programId, trainerId);
var teachingAid = _mapper.CreateTeachingAidEntity(createDto, programId, trainerId);

// Phase 4: Advisory
var advisory = _mapper.CreateAdvisoryEntity(createDto, programId, trainerId);
_mapper.UpdateAdvisoryEntity(updateDto, existingAdvisory, trainerId);

// Phase 5: Reports
var report = _mapper.CreateReportEntity(createDto, programId, trainerId);
_mapper.UpdateReportEntity(updateDto, existingReport, trainerId);

// Phase 6: Recommendations
var recommendation = _mapper.CreateRecommendationEntity(createDto, programId, trainerId);
_mapper.UpdateRecommendationEntity(updateDto, existingRecommendation, trainerId);
*/