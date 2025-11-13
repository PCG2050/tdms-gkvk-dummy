// IKvkProgramContentRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkProgramContentRepository
    {
        Task<KvkProgramContentAndResources> CreateAsync(KvkProgramContentAndResources entity);
        Task<KvkProgramContentAndResources?> GetByIdAsync(int id);
        Task<KvkProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<KvkProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<KvkProgramContentAndResources> UpdateAsync(KvkProgramContentAndResources entity);
        Task DeleteAsync(int id);

        /// <summary>
        /// Create parent with all child entities in a single transaction
        /// Handles transaction management internally
        /// </summary>
        Task<KvkProgramContentAndResources> CreateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids);

        /// <summary>
        /// Update parent with all child entities using hybrid pattern in a single transaction
        /// - Creates new children (without Id)
        /// - Updates existing children (with Id)
        /// - Deletes children not in lists
        /// Handles transaction management internally
        /// </summary>
        Task<KvkProgramContentAndResources> UpdateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids);
    }
}