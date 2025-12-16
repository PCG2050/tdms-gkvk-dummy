// ISametiReportRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiReportRepository
    {
        Task<SametiReport?> GetByIdAsync(int id);
        Task<SametiReport?> GetByProgramIdAsync(int programId);
        Task<SametiReport> CreateAsync(SametiReport entity);
        Task<SametiReport> UpdateAsync(SametiReport entity);
        Task DeleteAsync(int id);
    }
}