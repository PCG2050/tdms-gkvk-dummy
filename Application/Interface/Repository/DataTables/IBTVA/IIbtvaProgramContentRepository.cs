// IIbtvaProgramContentRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaProgramContentRepository
    {
        Task<IbtvaProgramContentAndResources?> GetByIdAsync(int id);
        Task<IbtvaProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<IbtvaProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<IbtvaProgramContentAndResources> CreateAsync(IbtvaProgramContentAndResources entity);
        Task<IbtvaProgramContentAndResources> UpdateAsync(IbtvaProgramContentAndResources entity);
        Task DeleteAsync(int id);
    }
}