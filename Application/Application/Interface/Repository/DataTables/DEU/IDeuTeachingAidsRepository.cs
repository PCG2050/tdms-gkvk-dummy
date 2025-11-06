// IDeuTeachingAidsRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuTeachingAidsRepository
    {
        Task<DeuTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<DeuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<DeuTeachingAidsDeveloped> CreateAsync(DeuTeachingAidsDeveloped entity);
        Task<DeuTeachingAidsDeveloped> UpdateAsync(DeuTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}