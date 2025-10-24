// IDeuResourcePersonRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuResourcePersonRepository
    {
        Task<DeuResourcePerson?> GetByIdAsync(int id);
        Task<List<DeuResourcePerson>> GetByContentIdAsync(int contentId);
        Task<DeuResourcePerson> CreateAsync(DeuResourcePerson entity);
        Task<DeuResourcePerson> UpdateAsync(DeuResourcePerson entity);
        Task DeleteAsync(int id);
    }
}