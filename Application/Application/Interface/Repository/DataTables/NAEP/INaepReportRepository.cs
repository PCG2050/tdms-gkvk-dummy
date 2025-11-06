// INaepReportRepository.cs
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepReportRepository
    {
        Task<NaepReport?> GetByIdAsync(int id);
        Task<NaepReport?> GetByProgramIdAsync(int programId);
        Task<NaepReport> CreateAsync(NaepReport entity);
        Task<NaepReport> UpdateAsync(NaepReport entity);
        Task DeleteAsync(int id);
    }
}