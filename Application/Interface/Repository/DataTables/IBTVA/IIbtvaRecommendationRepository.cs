// IIbtvaRecommendationRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaRecommendationRepository
    {
        Task<IbtvaRecommendation?> GetByIdAsync(int id);
        Task<IbtvaRecommendation?> GetByProgramIdAsync(int programId);
        Task<IbtvaRecommendation> CreateAsync(IbtvaRecommendation entity);
        Task<IbtvaRecommendation> UpdateAsync(IbtvaRecommendation entity);
        Task DeleteAsync(int id);
    }
}