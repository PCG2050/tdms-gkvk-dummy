// IEeuProgramContentRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuProgramContentRepository
    {
        Task<EeuProgramContentAndResources> CreateAsync(EeuProgramContentAndResources entity);
        Task<EeuProgramContentAndResources?> GetByIdAsync(int id);
        Task<EeuProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<EeuProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<EeuProgramContentAndResources> UpdateAsync(EeuProgramContentAndResources entity);
        Task DeleteAsync(int id);

        Task<EeuProgramContentAndResources> CreateWithChildrenAsync(
            EeuProgramContentAndResources parent,
            List<EeuResourcePerson>? resourcePersons,
            List<EeuTopicsCoveredInClass>? topicsCovered,
            List<EeuTeachingAidsDeveloped>? teachingAids,
            List<EeuFieldVisit>? fieldVisits,
            List<EeuFieldDay>? fieldDays,
            List<EeuFarmerScientistInteraction>? farmerScientistInteractions);

        Task<EeuProgramContentAndResources> UpdateWithChildrenAsync(
            EeuProgramContentAndResources parent,
            List<EeuResourcePerson>? resourcePersons,
            List<EeuTopicsCoveredInClass>? topicsCovered,
            List<EeuTeachingAidsDeveloped>? teachingAids,
            List<EeuFieldVisit>? fieldVisits,
            List<EeuFieldDay>? fieldDays,
            List<EeuFarmerScientistInteraction>? farmerScientistInteractions);

    }
}