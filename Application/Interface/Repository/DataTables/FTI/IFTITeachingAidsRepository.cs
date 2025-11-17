// IFtiTeachingAidsRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiTeachingAidsRepository
    {
        Task<FtiTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<FtiTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<FtiTeachingAidsDeveloped> CreateAsync(FtiTeachingAidsDeveloped entity);
        Task<FtiTeachingAidsDeveloped> UpdateAsync(FtiTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}
