// IEeuAdvisoryServicesRepository.cs
using Domain.Entities.DEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkAdvisoryServicesRepository
    {
        Task<KvkAdvisoryServices?> GetByIdAsync(int id);
        Task<KvkAdvisoryServices?> GetByProgramIdAsync(int programId);
        Task<KvkAdvisoryServices> CreateAsync(KvkAdvisoryServices entity);
        Task<KvkAdvisoryServices> UpdateAsync(KvkAdvisoryServices entity);
        Task DeleteAsync(int id);
    }
}