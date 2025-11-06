// IEeuProgramDetailsRepository.cs

using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    public interface IKvkProgramDetailsRepository
    {
        Task<KvkProgramDetails?> GetByIdAsync(int id);
        Task<KvkProgramDetails?> GetWithDetailsAsync(int id);
        Task<KvkProgramDetails> CreateAsync(KvkProgramDetails entity);
        Task<KvkProgramDetails> UpdateAsync(KvkProgramDetails entity);
        Task DeleteAsync(int id);
        Task<List<KvkProgramDetails>> GetByCreatedByIdAsync(int createdById);
        Task<List<KvkProgramDetails>> GetByUnitLocationIdsAsync(List<int> unitLocationIds);
    }
}