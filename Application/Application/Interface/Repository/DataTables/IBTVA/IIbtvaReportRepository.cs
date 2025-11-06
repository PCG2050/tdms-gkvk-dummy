// IIbtvaReportRepository.cs
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaReportRepository
    {
        Task<IbtvaReport?> GetByIdAsync(int id);
        Task<IbtvaReport?> GetByProgramIdAsync(int programId);
        Task<IbtvaReport> CreateAsync(IbtvaReport entity);
        Task<IbtvaReport> UpdateAsync(IbtvaReport entity);
        Task DeleteAsync(int id);
    }
}