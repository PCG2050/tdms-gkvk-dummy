// IEeuReportRepository.cs
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuReportRepository
    {
        Task<EeuReport?> GetByIdAsync(int id);
        Task<EeuReport?> GetByProgramIdAsync(int programId);
        Task<EeuReport> CreateAsync(EeuReport entity);
        Task<EeuReport> UpdateAsync(EeuReport entity);
        Task DeleteAsync(int id);
    }
}