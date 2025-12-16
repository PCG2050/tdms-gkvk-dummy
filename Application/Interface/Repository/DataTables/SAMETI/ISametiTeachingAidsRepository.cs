// ISametiTeachingAidsRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiTeachingAidsRepository
    {
        Task<SametiTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<SametiTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<SametiTeachingAidsDeveloped> CreateAsync(SametiTeachingAidsDeveloped entity);
        Task<SametiTeachingAidsDeveloped> UpdateAsync(SametiTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}