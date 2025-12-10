// IEeuProgramContentRepository.cs
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

        Task<KvkProgramContentAndResources> CreateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids,
            List<KvkFieldVisit>? fieldVisits,
            List<KvkFieldDay>? fieldDays,
            List<KvkFarmerScientistInteraction>? farmerScientistInteractions);

        Task<KvkProgramContentAndResources> UpdateWithChildrenAsync(
            KvkProgramContentAndResources parent,
            List<KvkResourcePerson>? resourcePersons,
            List<KvkTopicsCoveredInClass>? topicsCovered,
            List<KvkTeachingAidsDeveloped>? teachingAids,
            List<KvkFieldVisit>? fieldVisits,
            List<KvkFieldDay>? fieldDays,
            List<KvkFarmerScientistInteraction>? farmerScientistInteractions);

    }
}