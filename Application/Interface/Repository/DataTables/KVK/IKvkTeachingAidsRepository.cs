// IEeuTeachingAidsRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkTeachingAidsRepository
    {
        Task<KvkTeachingAidsDeveloped> CreateAsync(KvkTeachingAidsDeveloped entity);
        Task<KvkTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<KvkTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<KvkTeachingAidsDeveloped> UpdateAsync(KvkTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}