// IEeuTeachingAidsRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuTeachingAidsRepository
    {
        Task<EeuTeachingAidsDeveloped> CreateAsync(EeuTeachingAidsDeveloped entity);
        Task<EeuTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<EeuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<EeuTeachingAidsDeveloped> UpdateAsync(EeuTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}