// ISametiTopicsCoveredRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiTopicsCoveredRepository
    {
        Task<SametiTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<SametiTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<SametiTopicsCoveredInClass> CreateAsync(SametiTopicsCoveredInClass entity);
        Task<SametiTopicsCoveredInClass> UpdateAsync(SametiTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}
