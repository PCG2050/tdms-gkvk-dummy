using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    // Phase 1: Program
    public interface IIbtvaProgramRepository
    {
        Task<IbtvaProgramDetails> CreateAsync(IbtvaProgramDetails program);
        Task<IbtvaProgramDetails?> GetByIdAsync(int id, int trainerId);
        Task<IEnumerable<IbtvaProgramDetails>> GetAllAsync(int trainerId);
        Task<IbtvaProgramDetails?> UpdateAsync(IbtvaProgramDetails program);
        Task<bool> DeleteAsync(int id, int trainerId);
        Task<bool> SubmitAsync(int id, int trainerId);
    }

    // Phase 2: Demographics
    public interface IIbtvaDemographicsRepository
    {
        Task<IbtvaParticipantDemographics> CreateAsync(IbtvaParticipantDemographics demographic);
        Task<IbtvaParticipantDemographics?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaParticipantDemographics>> GetAllByProgramAsync(int programId);
        Task<IbtvaParticipantDemographics?> UpdateAsync(IbtvaParticipantDemographics demographic);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 3: Content & Resources
    public interface IIbtvaContentRepository
    {
        Task<IbtvaProgramContentAndResources> CreateAsync(IbtvaProgramContentAndResources content);
        Task<IbtvaProgramContentAndResources?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaProgramContentAndResources>> GetAllByProgramAsync(int programId);
        Task<IbtvaProgramContentAndResources?> UpdateAsync(IbtvaProgramContentAndResources content);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaResourcePersonRepository
    {
        Task<IbtvaResourcePerson> CreateAsync(IbtvaResourcePerson person);
        Task<IbtvaResourcePerson?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaResourcePerson>> GetAllByProgramAsync(int programId);
        Task<IbtvaResourcePerson?> UpdateAsync(IbtvaResourcePerson person);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaTopicRepository
    {
        Task<IbtvaTopicsCoveredInClass> CreateAsync(IbtvaTopicsCoveredInClass topic);
        Task<IbtvaTopicsCoveredInClass?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaTopicsCoveredInClass>> GetAllByProgramAsync(int programId);
        Task<IbtvaTopicsCoveredInClass?> UpdateAsync(IbtvaTopicsCoveredInClass topic);
        Task<bool> DeleteAsync(int id, int programId);
    }

    public interface IIbtvaTeachingAidRepository
    {
        Task<IbtvaTeachingAidsDeveloped> CreateAsync(IbtvaTeachingAidsDeveloped aid);
        Task<IbtvaTeachingAidsDeveloped?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaTeachingAidsDeveloped>> GetAllByProgramAsync(int programId);
        Task<IbtvaTeachingAidsDeveloped?> UpdateAsync(IbtvaTeachingAidsDeveloped aid);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 4: Advisory
    public interface IIbtvaAdvisoryRepository
    {
        Task<IbtvaAdvisoryServices> CreateAsync(IbtvaAdvisoryServices advisory);
        Task<IbtvaAdvisoryServices?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaAdvisoryServices>> GetAllByProgramAsync(int programId);
        Task<IbtvaAdvisoryServices?> UpdateAsync(IbtvaAdvisoryServices advisory);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 5: Reports
    public interface IIbtvaReportRepository
    {
        Task<IbtvaReport> CreateAsync(IbtvaReport report);
        Task<IbtvaReport?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaReport>> GetAllByProgramAsync(int programId);
        Task<IbtvaReport?> UpdateAsync(IbtvaReport report);
        Task<bool> DeleteAsync(int id, int programId);
    }

    // Phase 6: Recommendations
    public interface IIbtvaRecommendationRepository
    {
        Task<IbtvaRecommendation> CreateAsync(IbtvaRecommendation recommendation);
        Task<IbtvaRecommendation?> GetByIdAsync(int id, int programId);
        Task<IEnumerable<IbtvaRecommendation>> GetAllByProgramAsync(int programId);
        Task<IbtvaRecommendation?> UpdateAsync(IbtvaRecommendation recommendation);
        Task<bool> DeleteAsync(int id, int programId);
    }
}