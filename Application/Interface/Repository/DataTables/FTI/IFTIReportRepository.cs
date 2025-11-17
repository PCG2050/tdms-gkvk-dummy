// IFtiReportRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiReportRepository
    {
        Task<FtiReport?> GetByIdAsync(int id);
        Task<FtiReport?> GetByProgramIdAsync(int programId);
        Task<FtiReport> CreateAsync(FtiReport entity);
        Task<FtiReport> UpdateAsync(FtiReport entity);
        Task DeleteAsync(int id);
    }
}
