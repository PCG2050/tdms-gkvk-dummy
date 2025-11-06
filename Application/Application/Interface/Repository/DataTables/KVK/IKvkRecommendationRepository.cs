// IEeuRecommendationRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkRecommendationRepository
    {
        Task<KvkRecommendation?> GetByIdAsync(int id);
        Task<KvkRecommendation?> GetByProgramIdAsync(int programId);
        Task<KvkRecommendation> CreateAsync(KvkRecommendation entity);
        Task<KvkRecommendation> UpdateAsync(KvkRecommendation entity);
        Task DeleteAsync(int id);
    }
}