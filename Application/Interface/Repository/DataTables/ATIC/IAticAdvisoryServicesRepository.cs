// IAticAdvisoryServicesRepository.cs
using Domain.Entities.ATIC;

namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticAdvisoryServicesRepository
    {
        Task<AticAdvisoryServices?> GetByIdAsync(int id);
        Task<AticAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<AticAdvisoryServices> CreateAsync(AticAdvisoryServices entity);
        Task<AticAdvisoryServices> UpdateAsync(AticAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}