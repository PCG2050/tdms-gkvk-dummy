// IEeuResourcePersonRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuResourcePersonRepository
    {
        Task<EeuResourcePerson> CreateAsync(EeuResourcePerson entity);
        Task<EeuResourcePerson?> GetByIdAsync(int id);
        Task<List<EeuResourcePerson>> GetByContentIdAsync(int contentId);
        Task<EeuResourcePerson> UpdateAsync(EeuResourcePerson entity);
        Task DeleteAsync(int id);
    }
}