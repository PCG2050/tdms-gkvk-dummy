// ISametiAdvisoryServicesRepository.cs
using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiAdvisoryServicesRepository
    {
        Task<SametiAdvisoryServices?> GetByIdAsync(int id);
        Task<SametiAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<SametiAdvisoryServices> CreateAsync(SametiAdvisoryServices entity);
        Task<SametiAdvisoryServices> UpdateAsync(SametiAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}