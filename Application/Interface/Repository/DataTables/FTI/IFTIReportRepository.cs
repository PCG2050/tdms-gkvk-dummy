// IFTIReportRepository.cs
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIReportRepository
    {
        Task<FTIReport?> GetByIdAsync(int id);
        Task<FTIReport?> GetByProgramIdAsync(int programId);
        Task<FTIReport> CreateAsync(FTIReport entity);
        Task<FTIReport> UpdateAsync(FTIReport entity);
        Task DeleteAsync(int id);
    }
}
