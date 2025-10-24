// IEeuAdvisoryServicesRepository.cs
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuAdvisoryServicesRepository
    {
        Task<EeuAdvisoryServices?> GetByIdAsync(int id);
        Task<EeuAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<EeuAdvisoryServices> CreateAsync(EeuAdvisoryServices entity);
        Task<EeuAdvisoryServices> UpdateAsync(EeuAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}