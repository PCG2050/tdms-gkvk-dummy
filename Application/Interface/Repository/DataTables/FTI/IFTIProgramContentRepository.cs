// IFTIProgramContentRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIProgramContentRepository
    {
        Task<FTIProgramContentAndResources?> GetByIdAsync(int id);
        Task<FTIProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<FTIProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<FTIProgramContentAndResources> CreateAsync(FTIProgramContentAndResources entity);
        Task<FTIProgramContentAndResources> UpdateAsync(FTIProgramContentAndResources entity);
        Task DeleteAsync(int id);

        // Hybrid pattern methods for bulk create/update operations
        Task<FTIProgramContentAndResources> CreateWithChildrenAsync(
            FTIProgramContentAndResources parent,
            List<FTIResourcePerson>? resourcePersons,
            List<FTITopicsCoveredInClass>? topicsCovered,
            List<FTITeachingAidsDeveloped>? teachingAids);

        Task<FTIProgramContentAndResources> UpdateWithChildrenAsync(
            FTIProgramContentAndResources parent,
            List<FTIResourcePerson>? resourcePersons,
            List<FTITopicsCoveredInClass>? topicsCovered,
            List<FTITeachingAidsDeveloped>? teachingAids);
    }
}
