// INaepProgramContentRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepProgramContentRepository
    {
        Task<NaepProgramContentAndResources?> GetByIdAsync(int id);
        Task<NaepProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<NaepProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<NaepProgramContentAndResources> CreateAsync(NaepProgramContentAndResources entity);
        Task<NaepProgramContentAndResources> UpdateAsync(NaepProgramContentAndResources entity);
        Task DeleteAsync(int id);

        Task<NaepProgramContentAndResources> CreateWithChildrenAsync(NaepProgramContentAndResources parent,
          List<NaepResourcePerson>? resourcePersons,
          List<NaepTopicsCoveredInClass>? topicsCovered,
          List<NaepTeachingAidsDeveloped>? teachingAids);

        Task<NaepProgramContentAndResources> UpdateWithChildrenAsync(NaepProgramContentAndResources parent,
            List<NaepResourcePerson>? resourcePersons,
            List<NaepTopicsCoveredInClass>? topicsCovered,
            List<NaepTeachingAidsDeveloped>? teachingAids);

    }
}