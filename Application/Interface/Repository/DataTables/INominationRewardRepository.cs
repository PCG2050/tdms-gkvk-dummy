

namespace Application.Interface.Repository.DataTables
{
    public interface INominationRewardRepository
    {
        IQueryable<NominationReward> GetQueryable();
        Task<NominationReward?> GetByIdAsync(int id);
        Task<NominationReward?> GetWithDetailsAsync(int id); // includes child collections
        Task<NominationReward> AddAsync(NominationReward entity);
        Task<NominationReward> UpdateAsync(NominationReward entity);
        Task DeleteAsync(int id);

        Task<List<NominationReward>> GetAllAsync();

        Task<PaginatedResult<NominationReward>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? typeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<NominationReward>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);

    }
}
