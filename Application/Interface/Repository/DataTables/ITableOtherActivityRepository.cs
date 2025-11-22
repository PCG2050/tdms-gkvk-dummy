using Application.Models.DataTables;
using Domain.Entities.GenericTables;

namespace Application.Interface.Repository.DataTables
{
    /// <summary>
    /// Repository interface for TableOtherActivity
    /// Provides data access operations with support for pagination, filtering, and status tracking
    /// </summary>
    public interface ITableOtherActivityRepository
    {
        // ============================
        // CORE CRUD OPERATIONS
        // ============================
        Task<List<TableOtherActivity>> GetAllAsync();
        Task<TableOtherActivity?> GetByIdAsync(int id);

        /// <summary>
        /// Get activity with related entities (creator, approver, unit location, organization)
        /// </summary>
        Task<TableOtherActivity?> GetByIdWithDetailsAsync(int id);

        Task AddAsync(TableOtherActivity entity);
        Task UpdateAsync(TableOtherActivity entity);
        Task DeleteAsync(TableOtherActivity entity);
        Task SaveChangesAsync();

        // ============================
        // PAGINATION & FILTERS
        // ============================

        /// <summary>
        /// Get paginated activities with optional filters
        /// </summary>
        /// <param name="unitLocationIds">List of accessible unit location IDs (based on user role)</param>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="startDate">Filter by start date (inclusive)</param>
        /// <param name="endDate">Filter by end date (inclusive)</param>
        /// <param name="unitLocationId">Filter by specific unit location</param>
        /// <param name="searchTerm">Search in title and description</param>
        Task<PaginatedResult<TableOtherActivity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get activities by status (Draft, Pending, Approved, Rejected)
        /// </summary>
        Task<PaginatedResult<TableOtherActivity>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get activities created by specific user (for trainer history)
        /// </summary>
        Task<PaginatedResult<TableOtherActivity>> GetByCreatorAsync(
            int creatorId,
            int pageNumber = 1,
            int pageSize = 20);

        // ============================
        // DASHBOARD & STATISTICS
        // ============================

        /// <summary>
        /// Get count of activities grouped by status
        /// Returns dictionary: { "Draft": 5, "Pending": 3, "Approved": 12, "Rejected": 1 }
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}