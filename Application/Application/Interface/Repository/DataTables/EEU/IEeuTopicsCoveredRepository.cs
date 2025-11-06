// IEeuTopicsCoveredRepository.cs
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuTopicsCoveredRepository
    {
        Task<EeuTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<EeuTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<EeuTopicsCoveredInClass> CreateAsync(EeuTopicsCoveredInClass entity);
        Task<EeuTopicsCoveredInClass> UpdateAsync(EeuTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}