// ISametiProgramContentRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiProgramContentRepository
    {
        Task<SametiProgramContentAndResources?> GetByIdAsync(int id);
        Task<SametiProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<SametiProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<SametiProgramContentAndResources> CreateAsync(SametiProgramContentAndResources entity);
        Task<SametiProgramContentAndResources> UpdateAsync(SametiProgramContentAndResources entity);
        Task DeleteAsync(int id);

        // Hybrid pattern methods for bulk create/update operations
        Task<SametiProgramContentAndResources> CreateWithChildrenAsync(
            SametiProgramContentAndResources parent,
            List<SametiResourcePerson>? resourcePersons,
            List<SametiTopicsCoveredInClass>? topicsCovered,
            List<SametiTeachingAidsDeveloped>? teachingAids);

        Task<SametiProgramContentAndResources> UpdateWithChildrenAsync(
            SametiProgramContentAndResources parent,
            List<SametiResourcePerson>? resourcePersons,
            List<SametiTopicsCoveredInClass>? topicsCovered,
            List<SametiTeachingAidsDeveloped>? teachingAids);
    }
}