// IEeuTopicsCoveredRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuTopicsCoveredRepository
    {
        Task<EeuTopicsCoveredInClass> CreateAsync(EeuTopicsCoveredInClass entity);
        Task<EeuTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<EeuTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<EeuTopicsCoveredInClass> UpdateAsync(EeuTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}