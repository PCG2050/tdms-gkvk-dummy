// IDeuProgramContentRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuProgramContentRepository
    {
        Task<DeuProgramContentAndResources?> GetByIdAsync(int id);
        Task<DeuProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<DeuProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<DeuProgramContentAndResources> CreateAsync(DeuProgramContentAndResources entity);
        Task<DeuProgramContentAndResources> UpdateAsync(DeuProgramContentAndResources entity);
        Task DeleteAsync(int id);
    }
}