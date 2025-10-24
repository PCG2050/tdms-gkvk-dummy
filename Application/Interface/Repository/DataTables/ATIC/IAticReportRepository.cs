// IAticReportRepository.cs
using Domain.Entities.ATIC;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticReportRepository
    {
        Task<AticReport?> GetByIdAsync(int id);
        Task<AticReport?> GetByProgramIdAsync(int programId);
        Task<AticReport> CreateAsync(AticReport entity);
        Task<AticReport> UpdateAsync(AticReport entity);
        Task DeleteAsync(int id);
    }
}