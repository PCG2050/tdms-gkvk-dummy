// IFtiTopicsCoveredRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiTopicsCoveredRepository
    {
        Task<FtiTopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<FtiTopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<FtiTopicsCoveredInClass> CreateAsync(FtiTopicsCoveredInClass entity);
        Task<FtiTopicsCoveredInClass> UpdateAsync(FtiTopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}
