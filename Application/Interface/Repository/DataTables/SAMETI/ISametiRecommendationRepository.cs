// ISametiRecommendationRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiRecommendationRepository
    {
        Task<SametiRecommendation?> GetByIdAsync(int id);
        Task<SametiRecommendation?> GetByProgramIdAsync(int programId);
        Task<SametiRecommendation> CreateAsync(SametiRecommendation entity);
        Task<SametiRecommendation> UpdateAsync(SametiRecommendation entity);
        Task DeleteAsync(int id);
    }
}