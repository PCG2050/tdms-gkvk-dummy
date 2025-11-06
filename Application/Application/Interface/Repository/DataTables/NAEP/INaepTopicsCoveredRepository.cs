// INaepTopicsCoveredRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepTopicsCoveredRepository
    {
        Task<NaepTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<NaepTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<NaepTopicsCoveredInClass> CreateAsync(NaepTopicsCoveredInClass entity);
        Task<NaepTopicsCoveredInClass> UpdateAsync(NaepTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}