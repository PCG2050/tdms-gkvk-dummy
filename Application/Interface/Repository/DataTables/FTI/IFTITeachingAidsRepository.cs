// IFTITeachingAidsRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTITeachingAidsRepository
    {
        Task<FTITeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<FTITeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<FTITeachingAidsDeveloped> CreateAsync(FTITeachingAidsDeveloped entity);
        Task<FTITeachingAidsDeveloped> UpdateAsync(FTITeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}
