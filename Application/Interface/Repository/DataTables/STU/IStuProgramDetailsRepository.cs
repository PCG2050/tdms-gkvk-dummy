// IStuProgramDetailsRepository.cs


using Domain.Entities.ASM;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuProgramDetailsRepository
    {
        IQueryable<StuProgramDetails> GetQueryable();
        Task<List<StuProgramDetails>> GetAllAsync();
        Task<StuProgramDetails?> GetByIdAsync(int id);
        Task<StuProgramDetails?> GetWithDetailsAsync(int id);
        Task<StuProgramDetails> CreateAsync(StuProgramDetails entity);
        Task<StuProgramDetails> UpdateAsync(StuProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<StuProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<StuProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);

        /// <summary>
        /// Get programs created by a specific user (for history)
        /// </summary>
        Task<PaginatedResult<StuProgramDetails>> GetByCreatorIdAsync(
            int creatorId,
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for unit head (status = Pending, optionally filtered by unit location)
        /// </summary>
        Task<PaginatedResult<StuProgramDetails>> GetPendingApprovalsAsync(
            List<int> unitLocationIds,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get programs by trainer and unit location
        /// </summary>
        Task<PaginatedResult<StuProgramDetails>> GetByTrainerAndUnitLocationAsync(
            int trainerId,
            int? unitLocationId = null,
            List<int>? accessibleUnitLocationIds = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}