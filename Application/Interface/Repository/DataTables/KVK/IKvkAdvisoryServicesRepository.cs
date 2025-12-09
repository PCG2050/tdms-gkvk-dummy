// IKvkAdvisoryServicesRepository.cs
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkAdvisoryServicesRepository
    {
        IQueryable<KvkAdvisoryServices> GetQueryable();
        Task<KvkAdvisoryServices?> GetByIdAsync(int id);
        Task<KvkAdvisoryServices?> GetWithDetailsAsync(int id);
        Task<KvkAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<KvkAdvisoryServices> CreateAsync(KvkAdvisoryServices entity);
        Task<KvkAdvisoryServices> CreateWithChildrenAsync(
            KvkAdvisoryServices parent,
            List<KvkCriticalInputsDistributed>? criticalInputs);
        Task<KvkAdvisoryServices> UpdateAsync(KvkAdvisoryServices entity);
        Task<KvkAdvisoryServices> UpdateWithChildrenAsync(
            KvkAdvisoryServices parent,
            List<KvkCriticalInputsDistributed>? criticalInputs);
        Task DeleteAsync(int id);
    }
}