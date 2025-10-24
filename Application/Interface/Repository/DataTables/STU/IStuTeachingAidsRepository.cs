// IStuTeachingAidsRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuTeachingAidsRepository
    {
        Task<StuTeachingAidsDeveloped?> GetByIdAsync(int id);
        Task<List<StuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId);
        Task<StuTeachingAidsDeveloped> CreateAsync(StuTeachingAidsDeveloped entity);
        Task<StuTeachingAidsDeveloped> UpdateAsync(StuTeachingAidsDeveloped entity);
        Task DeleteAsync(int id);
    }
}