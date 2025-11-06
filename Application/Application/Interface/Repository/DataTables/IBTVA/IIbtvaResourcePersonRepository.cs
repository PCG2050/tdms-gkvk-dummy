// IIbtvaResourcePersonRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaResourcePersonRepository
    {
        Task<IbtvaResourcePerson?> GetByIdAsync(int id);
        Task<List<IbtvaResourcePerson>> GetByContentIdAsync(int contentId);
        Task<IbtvaResourcePerson> CreateAsync(IbtvaResourcePerson entity);
        Task<IbtvaResourcePerson> UpdateAsync(IbtvaResourcePerson entity);
        Task DeleteAsync(int id);
    }
}