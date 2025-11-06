// IDeuRecommendationRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuRecommendationRepository
    {
        Task<DeuRecommendation?> GetByIdAsync(int id);
        Task<DeuRecommendation?> GetByProgramIdAsync(int programId);
        Task<DeuRecommendation> CreateAsync(DeuRecommendation entity);
        Task<DeuRecommendation> UpdateAsync(DeuRecommendation entity);
        Task DeleteAsync(int id);
    }
}