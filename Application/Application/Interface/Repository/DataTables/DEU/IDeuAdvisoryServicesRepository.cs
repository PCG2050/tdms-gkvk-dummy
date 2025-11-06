// IDeuAdvisoryServicesRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuAdvisoryServicesRepository
    {
        Task<DeuAdvisoryServices?> GetByIdAsync(int id);
        Task<DeuAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<DeuAdvisoryServices> CreateAsync(DeuAdvisoryServices entity);
        Task<DeuAdvisoryServices> UpdateAsync(DeuAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}