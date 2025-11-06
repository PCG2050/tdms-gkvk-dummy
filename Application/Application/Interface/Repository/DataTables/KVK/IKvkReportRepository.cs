// IEeuReportRepository.cs
using Domain.Entities.EEU;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkReportRepository
    {
        Task<KvkReport?> GetByIdAsync(int id);
        Task<KvkReport?> GetByProgramIdAsync(int programId);
        Task<KvkReport> CreateAsync(KvkReport entity);
        Task<KvkReport> UpdateAsync(KvkReport entity);
        Task DeleteAsync(int id);
    }
}