// IIbtvaTopicsCoveredRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaTopicsCoveredRepository
    {
        Task<IbtvaTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<IbtvaTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<IbtvaTopicsCoveredInClass> CreateAsync(IbtvaTopicsCoveredInClass entity);
        Task<IbtvaTopicsCoveredInClass> UpdateAsync(IbtvaTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}