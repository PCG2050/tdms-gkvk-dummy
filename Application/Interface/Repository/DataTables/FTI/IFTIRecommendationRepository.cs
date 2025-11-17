// IFtiRecommendationRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiRecommendationRepository
    {
        Task<FtiRecommendation?> GetByIdAsync(int id);
        Task<FtiRecommendation?> GetByProgramIdAsync(int programId);
        Task<FtiRecommendation> CreateAsync(FtiRecommendation entity);
        Task<FtiRecommendation> UpdateAsync(FtiRecommendation entity);
        Task DeleteAsync(int id);
    }
}
