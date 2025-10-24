// IAticProgramDetailsRepository.cs


namespace Application.Interface.Repository.DataTables.ATIC
{
    public interface IAticProgramDetailsRepository
    {
        Task<AticProgramDetails?> GetByIdAsync(int id);
        Task<AticProgramDetails?> GetWithDetailsAsync(int id);
        Task<AticProgramDetails> CreateAsync(AticProgramDetails entity);
        Task<AticProgramDetails> UpdateAsync(AticProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<AticProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<AticProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}