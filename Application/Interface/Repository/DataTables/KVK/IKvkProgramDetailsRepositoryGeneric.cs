using Application.Interface.Repository;
using Domain.Entities.KVK;

namespace Application.Interface.Repository.DataTables.KVK
{
    /// <summary>
    /// KVK Program Details Repository Interface
    /// Extends generic repository - no additional methods needed!
    /// All CRUD, pagination, filtering inherited from IGenericProgramRepository
    /// </summary>
    public interface IKvkProgramDetailsRepository : IGenericProgramRepository<KvkProgramDetails>
    {
        // ✅ All methods inherited from IGenericProgramRepository<KvkProgramDetails>:
        // - GetQueryable()
        // - GetAllAsync()
        // - GetByIdAsync(int id)
        // - GetWithDetailsAsync(int id)
        // - CreateAsync(KvkProgramDetails entity)
        // - UpdateAsync(KvkProgramDetails entity)
        // - DeleteAsync(int id)
        // - GetPaginatedAsync(...)
        // - GetByStatusAsync(...)
        // - GetStatusSummaryAsync(...)

        // Add KVK-specific methods here ONLY if needed
        // Example:
        // Task<List<KvkProgramDetails>> GetProgramsWithResultsAsync(int unitLocationId);
    }
}
