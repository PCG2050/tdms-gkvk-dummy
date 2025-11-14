// IAticProgramContentRepository.cs

using Domain.Entities.ATIC;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticProgramContentRepository
    {
        Task<AticProgramContentAndResources?> GetByIdAsync(int id);
        Task<AticProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<AticProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<AticProgramContentAndResources> CreateAsync(AticProgramContentAndResources entity);
        Task<AticProgramContentAndResources> UpdateAsync(AticProgramContentAndResources entity);
        Task DeleteAsync(int id);

        // Hybrid pattern methods for bulk create/update operations
        Task<AticProgramContentAndResources> CreateWithChildrenAsync(
            AticProgramContentAndResources parent,
            List<AticResourcePerson>? resourcePersons,
            List<AticTopicsCoveredInClass>? topicsCovered,
            List<AticTeachingAidsDeveloped>? teachingAids);

        Task<AticProgramContentAndResources> UpdateWithChildrenAsync(
            AticProgramContentAndResources parent,
            List<AticResourcePerson>? resourcePersons,
            List<AticTopicsCoveredInClass>? topicsCovered,
            List<AticTeachingAidsDeveloped>? teachingAids);
    }
}