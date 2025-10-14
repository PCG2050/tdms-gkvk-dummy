using Application.Models.DataTables.IBTVA;

namespace Application.Interface.Services.DataTables.IBTVA
{
    // Phase 1: Program
    public interface IIbtvaProgramService
    {
        Task<IbtvaProgramDetailsDto> CreateAsync(IbtvaProgramCreateDto dto, int trainerId, int organizationId);
        Task<IbtvaProgramDetailsDto?> GetByIdAsync(int id, int trainerId);
        Task<IEnumerable<IbtvaProgramDetailsDto>> GetAllAsync(int trainerId);
        Task<IbtvaProgramDetailsDto?> UpdateAsync(int id, IbtvaProgramUpdateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int trainerId);
        Task<bool> SubmitAsync(int id, int trainerId);
    }

    // Phase 2: Demographics
    public interface IIbtvaDemographicsService
    {
        Task<DemographicsDto> CreateAsync(int programId, DemographicsCreateDto dto, int trainerId);
        Task<DemographicsDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<DemographicsDto>> GetAllByProgramAsync(int programId);
        Task<DemographicsDto?> UpdateAsync(int id, int programId, DemographicsCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 3: Content
    public interface IIbtvaContentService
    {
        Task<ContentResourcesDto> CreateAsync(int programId, ContentResourcesCreateDto dto, int trainerId);
        Task<ContentResourcesDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<ContentResourcesDto>> GetAllByProgramAsync(int programId);
        Task<ContentResourcesDto?> UpdateAsync(int id, int programId, ContentResourcesCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaResourcePersonService
    {
        Task<ResourcePersonDto> CreateAsync(int programId, ResourcePersonCreateDto dto, int trainerId);
        Task<ResourcePersonDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<ResourcePersonDto>> GetAllByProgramAsync(int programId);
        Task<ResourcePersonDto?> UpdateAsync(int id, int programId, ResourcePersonCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaTopicService
    {
        Task<TopicDto> CreateAsync(int programId, TopicCreateDto dto, int trainerId);
        Task<TopicDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<TopicDto>> GetAllByProgramAsync(int programId);
        Task<TopicDto?> UpdateAsync(int id, int programId, TopicCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaTeachingAidService
    {
        Task<TeachingAidDto> CreateAsync(int programId, TeachingAidCreateDto dto, int trainerId);
        Task<TeachingAidDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<TeachingAidDto>> GetAllByProgramAsync(int programId);
        Task<TeachingAidDto?> UpdateAsync(int id, int programId, TeachingAidCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 4: Advisory
    public interface IIbtvaAdvisoryService
    {
        Task<AdvisoryDto> CreateAsync(int programId, AdvisoryCreateDto dto, int trainerId);
        Task<AdvisoryDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<AdvisoryDto>> GetAllByProgramAsync(int programId);
        Task<AdvisoryDto?> UpdateAsync(int id, int programId, AdvisoryCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 5: Reports
    public interface IIbtvaReportService
    {
        Task<ReportDto> CreateAsync(int programId, ReportCreateDto dto, int trainerId);
        Task<ReportDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<ReportDto>> GetAllByProgramAsync(int programId);
        Task<ReportDto?> UpdateAsync(int id, int programId, ReportCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 6: Recommendations
    public interface IIbtvaRecommendationService
    {
        Task<RecommendationDto> CreateAsync(int programId, RecommendationCreateDto dto, int trainerId);
        Task<RecommendationDto?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<RecommendationDto>> GetAllByProgramAsync(int programId);
        Task<RecommendationDto?> UpdateAsync(int id, int programId, RecommendationCreateDto dto, int trainerId);
        Task<bool> DeleteAsync(int id, int programId);
    }
}