// IEeuTopicsCoveredRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkTopicsCoveredRepository
    {
        Task<KvkTopicsCoveredInClass> CreateAsync(KvkTopicsCoveredInClass entity);
        Task<KvkTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<KvkTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<KvkTopicsCoveredInClass> UpdateAsync(KvkTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}