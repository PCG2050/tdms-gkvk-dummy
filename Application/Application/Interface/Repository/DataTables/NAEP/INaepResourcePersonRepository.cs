// INaepResourcePersonRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepResourcePersonRepository
    {
        Task<NaepResourcePerson?> GetByIdAsync(int id);
        Task<List<NaepResourcePerson>> GetByContentIdAsync(int contentId);
        Task<NaepResourcePerson> CreateAsync(NaepResourcePerson entity);
        Task<NaepResourcePerson> UpdateAsync(NaepResourcePerson entity);
        Task DeleteAsync(int id);
    }
}