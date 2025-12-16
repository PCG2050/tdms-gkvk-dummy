// ISametiProgramDetailsRepository.cs


using Domain.Entities.SAMETI;

namespace Application.Interface.Repository.DataTables.SAMETI
{
    public interface ISametiProgramDetailsRepository
    {
        IQueryable<SametiProgramDetails> GetQueryable();
        Task<List<SametiProgramDetails>> GetAllAsync();
        Task<SametiProgramDetails?> GetByIdAsync(int id);
        Task<SametiProgramDetails?> GetWithDetailsAsync(int id);
        Task<SametiProgramDetails> CreateAsync(SametiProgramDetails entity);
        Task<SametiProgramDetails> UpdateAsync(SametiProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<SametiProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<SametiProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}