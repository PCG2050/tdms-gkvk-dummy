// IStuTopicsCoveredRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuTopicsCoveredRepository
    {
        Task<StuTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<StuTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<StuTopicsCoveredInClass> CreateAsync(StuTopicsCoveredInClass entity);
        Task<StuTopicsCoveredInClass> UpdateAsync(StuTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}