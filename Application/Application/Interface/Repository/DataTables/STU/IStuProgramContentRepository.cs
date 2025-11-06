// IStuProgramContentRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuProgramContentRepository
    {
        Task<StuProgramContentAndResources?> GetByIdAsync(int id);
        Task<StuProgramContentAndResources?> GetWithDetailsAsync(int id);
        Task<List<StuProgramContentAndResources>> GetByProgramIdAsync(int programId);
        Task<StuProgramContentAndResources> CreateAsync(StuProgramContentAndResources entity);
        Task<StuProgramContentAndResources> UpdateAsync(StuProgramContentAndResources entity);
        Task DeleteAsync(int id);
    }
}