// IStuProgramContentRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuProgramContentRepository
    {
        Task<StuProgramContentAndResources?> GetByIdAsync(int id);
        Task<StuProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<StuProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<StuProgramContentAndResources> CreateAsync(StuProgramContentAndResources entity);
        Task<StuProgramContentAndResources> UpdateAsync(StuProgramContentAndResources entity);
        Task DeleteAsync(int id);

        // Hybrid pattern methods for bulk create/update operations
        Task<StuProgramContentAndResources> CreateWithChildrenAsync(
            StuProgramContentAndResources parent,
            List<StuResourcePerson>? resourcePersons,
            List<StuTopicsCoveredInClass>? topicsCovered,
            List<StuTeachingAidsDeveloped>? teachingAids);

        Task<StuProgramContentAndResources> UpdateWithChildrenAsync(
            StuProgramContentAndResources parent,
            List<StuResourcePerson>? resourcePersons,
            List<StuTopicsCoveredInClass>? topicsCovered,
            List<StuTeachingAidsDeveloped>? teachingAids);
    }
}