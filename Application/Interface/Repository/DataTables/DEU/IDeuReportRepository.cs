// IDeuReportRepository.cs


namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuReportRepository
    {
        Task<DeuReport?> GetByIdAsync(int id);
        Task<DeuReport?> GetByProgramIdAsync(int programId);
        Task<DeuReport> CreateAsync(DeuReport entity);
        Task<DeuReport> UpdateAsync(DeuReport entity);
        Task DeleteAsync(int id);
    }
}