// IFtiResourcePersonRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiResourcePersonRepository
    {
        Task<FtiResourcePerson?> GetByIdAsync(int id);
        Task<List<FtiResourcePerson>> GetByContentIdAsync(int contentId);
        Task<FtiResourcePerson> CreateAsync(FtiResourcePerson entity);
        Task<FtiResourcePerson> UpdateAsync(FtiResourcePerson entity);
        Task DeleteAsync(int id);
    }
}
