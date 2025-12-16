// ISametiResourcePersonRepository.cs
using Domain.Entities.SAMETI;
    
namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiResourcePersonRepository
    {
        Task<SametiResourcePerson?> GetByIdAsync(int id);
        Task<List<SametiResourcePerson>> GetByContentIdAsync(int contentId);
        Task<SametiResourcePerson> CreateAsync(SametiResourcePerson entity);
        Task<SametiResourcePerson> UpdateAsync(SametiResourcePerson entity);
        Task DeleteAsync(int id);
    }
}