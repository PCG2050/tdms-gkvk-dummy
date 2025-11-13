// IFtiProgramContentRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiProgramContentRepository
    {
        Task<FtiProgramContentAndResources?> GetByIdAsync(int id);
        Task<FtiProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<FtiProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<FtiProgramContentAndResources> CreateAsync(FtiProgramContentAndResources entity);
        Task<FtiProgramContentAndResources> UpdateAsync(FtiProgramContentAndResources entity);
        Task DeleteAsync(int id);

        Task<FtiProgramContentAndResources> CreateWithChildrenAsync(FtiProgramContentAndResources parent,
          List<FtiResourcePerson>? resourcePersons,
          List<FtiTopicsCoveredInClass>? topicsCovered,
          List<FtiTeachingAidsDeveloped>? teachingAids);

        Task<FtiProgramContentAndResources> UpdateWithChildrenAsync(FtiProgramContentAndResources parent,
            List<FtiResourcePerson>? resourcePersons,
            List<FtiTopicsCoveredInClass>? topicsCovered,
            List<FtiTeachingAidsDeveloped>? teachingAids);
    }
}
