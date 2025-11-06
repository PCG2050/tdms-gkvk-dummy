// INaepTeachingAidsRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP  
{
    public interface INaepTeachingAidsRepository
    {
        Task<NaepTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<NaepTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<NaepTeachingAidsDeveloped> CreateAsync(NaepTeachingAidsDeveloped entity);
        Task<NaepTeachingAidsDeveloped> UpdateAsync(NaepTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}