// IEeuRecommendationRepository.cs
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuRecommendationRepository
    {
        Task<EeuRecommendation?> GetByIdAsync(int id);
        Task<EeuRecommendation?> GetByProgramIdAsync(int programId);
        Task<EeuRecommendation> CreateAsync(EeuRecommendation entity);
        Task<EeuRecommendation> UpdateAsync(EeuRecommendation entity);
        Task DeleteAsync(int id);
    }
}