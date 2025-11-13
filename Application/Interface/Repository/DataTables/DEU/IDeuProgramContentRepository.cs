// IDeuProgramContentRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuProgramContentRepository
    {
        Task<DeuProgramContentAndResources?> GetByIdAsync(int id);
        Task<DeuProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<DeuProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<DeuProgramContentAndResources> CreateAsync(DeuProgramContentAndResources entity);
        Task<DeuProgramContentAndResources> UpdateAsync(DeuProgramContentAndResources entity);
        Task DeleteAsync(int id);

        /// <summary>
        /// Create parent with all child entities in a single transaction
        /// Handles transaction management internally
        /// </summary>
        Task<DeuProgramContentAndResources> CreateWithChildrenAsync(
            DeuProgramContentAndResources parent,
            List<DeuResourcePerson>? resourcePersons,
            List<DeuTopicsCoveredInClass>? topicsCovered,
            List<DeuTeachingAidsDeveloped>? teachingAids);

        /// <summary>
        /// Update parent with all child entities using hybrid pattern in a single transaction
        /// - Creates new children (without Id)
        /// - Updates existing children (with Id)
        /// - Deletes children not in lists
        /// Handles transaction management internally
        /// </summary>
        Task<DeuProgramContentAndResources> UpdateWithChildrenAsync(
            DeuProgramContentAndResources parent,
            List<DeuResourcePerson>? resourcePersons,
            List<DeuTopicsCoveredInClass>? topicsCovered,
            List<DeuTeachingAidsDeveloped>? teachingAids);
    }
}