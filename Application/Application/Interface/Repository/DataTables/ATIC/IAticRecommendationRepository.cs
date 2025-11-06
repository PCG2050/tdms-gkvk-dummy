// IAticRecommendationRepository.cs
using Domain.Entities.ATIC;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticRecommendationRepository
    {
        Task<AticRecommendation?> GetByIdAsync(int id);
        Task<AticRecommendation?> GetByProgramIdAsync(int programId);
        Task<AticRecommendation> CreateAsync(AticRecommendation entity);
        Task<AticRecommendation> UpdateAsync(AticRecommendation entity);
        Task DeleteAsync(int id);
    }
}