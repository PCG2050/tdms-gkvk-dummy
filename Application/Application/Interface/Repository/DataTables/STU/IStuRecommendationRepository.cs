// IStuRecommendationRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuRecommendationRepository
    {
        Task<StuRecommendation?> GetByIdAsync(int id);
        Task<StuRecommendation?> GetByProgramIdAsync(int programId);
        Task<StuRecommendation> CreateAsync(StuRecommendation entity);
        Task<StuRecommendation> UpdateAsync(StuRecommendation entity);
        Task DeleteAsync(int id);
    }
}