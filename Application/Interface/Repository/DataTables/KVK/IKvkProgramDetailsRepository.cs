// IEeuProgramDetailsRepository.cs

using Domain.Entities.ASM;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkProgramDetailsRepository
    {
        IQueryable<KvkProgramDetails> GetQueryable();
        Task<List<KvkProgramDetails>> GetAllAsync();
        Task<KvkProgramDetails?> GetByIdAsync(int id);
        Task<KvkProgramDetails?> GetWithDetailsAsync(int id);
        Task<KvkProgramDetails> CreateAsync(KvkProgramDetails entity);
        Task<KvkProgramDetails> UpdateAsync(KvkProgramDetails entity);
        Task DeleteAsync(int id);
        Task<List<KvkProgramDetails>> GetByCreatedByIdAsync(int createdById);
        Task<List<KvkProgramDetails>> GetByUnitLocationIdsAsync(List<int> unitLocationIds);
    }
}