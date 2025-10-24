// IAticTeachingAidsRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticTeachingAidsRepository
    {
        Task<AticTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<AticTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<AticTeachingAidsDeveloped> CreateAsync(AticTeachingAidsDeveloped entity);
        Task<AticTeachingAidsDeveloped> UpdateAsync(AticTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}