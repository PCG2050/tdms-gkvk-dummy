// IEeuAdvisoryServicesRepository.cs
using Domain.Entities.EEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuAdvisoryServicesRepository
    {
        IQueryable<EeuAdvisoryServices> GetQueryable();
        Task<EeuAdvisoryServices?> GetByIdAsync(int id);
        Task<EeuAdvisoryServices?> GetWithDetailsAsync(int id);
        Task<EeuAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<EeuAdvisoryServices> CreateAsync(EeuAdvisoryServices entity);
        Task<EeuAdvisoryServices> CreateWithChildrenAsync(
            EeuAdvisoryServices parent,
            List<EeuCriticalInputsDistributed>? criticalInputs);
        Task<EeuAdvisoryServices> UpdateAsync(EeuAdvisoryServices entity);
        Task<EeuAdvisoryServices> UpdateWithChildrenAsync(
            EeuAdvisoryServices parent,
            List<EeuCriticalInputsDistributed>? criticalInputs);
        Task DeleteAsync(int id);
    }
}