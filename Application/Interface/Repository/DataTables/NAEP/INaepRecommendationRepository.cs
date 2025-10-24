// INaepRecommendationRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepRecommendationRepository
    {
        Task<NaepRecommendation?> GetByIdAsync(int id);
        Task<NaepRecommendation?> GetByProgramIdAsync(int programId);
        Task<NaepRecommendation> CreateAsync(NaepRecommendation entity);
        Task<NaepRecommendation> UpdateAsync(NaepRecommendation entity);
        Task DeleteAsync(int id);
    }
}