// IFTIResourcePersonRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIResourcePersonRepository
    {
        Task<FTIResourcePerson?> GetByIdAsync(int id);
        Task<List<FTIResourcePerson>> GetByContentIdAsync(int contentId);
        Task<FTIResourcePerson> CreateAsync(FTIResourcePerson entity);
        Task<FTIResourcePerson> UpdateAsync(FTIResourcePerson entity);
        Task DeleteAsync(int id);
    }
}
