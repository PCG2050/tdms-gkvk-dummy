// IAticTopicsCoveredRepository.cs
using Domain.Entities.ATIC;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticTopicsCoveredRepository
    {
        Task<AticTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<AticTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<AticTopicsCoveredInClass> CreateAsync(AticTopicsCoveredInClass entity);
        Task<AticTopicsCoveredInClass> UpdateAsync(AticTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}