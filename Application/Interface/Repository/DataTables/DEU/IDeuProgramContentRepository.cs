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

        Task<DeuProgramContentAndResources> CreateWithChildrenAsync(DeuProgramContentAndResources parent,
          List<DeuResourcePerson>? resourcePersons,
          List<DeuTopicsCoveredInClass>? topicsCovered,
          List<DeuTeachingAidsDeveloped>? teachingAids);

        Task<DeuProgramContentAndResources> UpdateWithChildrenAsync(DeuProgramContentAndResources parent,
            List<DeuResourcePerson>? resourcePersons,
            List<DeuTopicsCoveredInClass>? topicsCovered,
            List<DeuTeachingAidsDeveloped>? teachingAids);
    }
}