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

        /// <summary>
        /// Get programs by trainer and optionally filter by unit location
        /// </summary>
        Task<PaginatedResult<KvkProgramDetails>> GetByTrainerAndUnitLocationAsync(
            int trainerId,
            int? unitLocationId = null,
            List<int>? accessibleUnitLocationIds = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}