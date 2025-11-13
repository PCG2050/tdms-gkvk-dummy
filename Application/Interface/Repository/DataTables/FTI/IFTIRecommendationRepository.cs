// IFTIRecommendationRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIRecommendationRepository
    {
        Task<FTIRecommendation?> GetByIdAsync(int id);
        Task<FTIRecommendation?> GetByProgramIdAsync(int programId);
        Task<FTIRecommendation> CreateAsync(FTIRecommendation entity);
        Task<FTIRecommendation> UpdateAsync(FTIRecommendation entity);
        Task DeleteAsync(int id);
    }
}
