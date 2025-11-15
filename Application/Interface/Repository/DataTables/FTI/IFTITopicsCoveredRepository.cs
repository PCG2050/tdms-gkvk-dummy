// IFTITopicsCoveredRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTITopicsCoveredRepository
    {
        Task<FTITopicsCoveredInClass?> GetByIdAsync(int id);
        Task<List<FTITopicsCoveredInClass>> GetByContentIdAsync(int contentId);
        Task<FTITopicsCoveredInClass> CreateAsync(FTITopicsCoveredInClass entity);
        Task<FTITopicsCoveredInClass> UpdateAsync(FTITopicsCoveredInClass entity);
        Task DeleteAsync(int id);
    }
}
