// IFTIProgramContentRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIProgramContentRepository
    {
        Task<FTIProgramContentAndResources?> GetByIdAsync(int id);
        Task<FTIProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<FTIProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<FTIProgramContentAndResources> CreateAsync(FTIProgramContentAndResources entity);
        Task<FTIProgramContentAndResources> UpdateAsync(FTIProgramContentAndResources entity);
        Task DeleteAsync(int id);
    }
}
