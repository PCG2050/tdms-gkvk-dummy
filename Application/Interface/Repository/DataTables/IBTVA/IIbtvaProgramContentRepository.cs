// IIbtvaProgramContentRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaProgramContentRepository
    {
        Task<IbtvaProgramContentAndResources?> GetByIdAsync(int id);
        Task<IbtvaProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<IbtvaProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<IbtvaProgramContentAndResources> CreateAsync(IbtvaProgramContentAndResources entity);
        Task<IbtvaProgramContentAndResources> UpdateAsync(IbtvaProgramContentAndResources entity);
        Task DeleteAsync(int id);

        // Hybrid pattern methods for bulk create/update operations
        Task<IbtvaProgramContentAndResources> CreateWithChildrenAsync(
            IbtvaProgramContentAndResources parent,
            List<IbtvaResourcePerson>? resourcePersons,
            List<IbtvaTopicsCoveredInClass>? topicsCovered,
            List<IbtvaTeachingAidsDeveloped>? teachingAids);

        Task<IbtvaProgramContentAndResources> UpdateWithChildrenAsync(
            IbtvaProgramContentAndResources parent,
            List<IbtvaResourcePerson>? resourcePersons,
            List<IbtvaTopicsCoveredInClass>? topicsCovered,
            List<IbtvaTeachingAidsDeveloped>? teachingAids);
    }
}