// IStuReportRepository.cs
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuReportRepository
    {
        Task<StuReport?> GetByIdAsync(int id);
        Task<StuReport?> GetByProgramIdAsync(int programId);
        Task<StuReport> CreateAsync(StuReport entity);
        Task<StuReport> UpdateAsync(StuReport entity);
        Task DeleteAsync(int id);
    }
}