// IDeuTopicsCoveredRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuTopicsCoveredRepository
    {
        Task<DeuTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<DeuTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<DeuTopicsCoveredInClass> CreateAsync(DeuTopicsCoveredInClass entity);
        Task<DeuTopicsCoveredInClass> UpdateAsync(DeuTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}