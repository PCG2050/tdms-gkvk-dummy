// IStuResourcePersonRepository.cs
using Domain.Entities.STU;
    
namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuResourcePersonRepository
    {
        Task<StuResourcePerson?> GetByIdAsync(int id);
        Task<List<StuResourcePerson>> GetByContentIdAsync(int contentId);
        Task<StuResourcePerson> CreateAsync(StuResourcePerson entity);
        Task<StuResourcePerson> UpdateAsync(StuResourcePerson entity);
        Task DeleteAsync(int id);
    }
}