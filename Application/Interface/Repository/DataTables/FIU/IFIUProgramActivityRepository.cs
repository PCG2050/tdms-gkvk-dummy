using Application.Models;
using Domain.Entities.FIU;

namespace Application.Interface.Repository.DataTables.FIU
{
    /// <summary>
    /// Repository interface for FIU Program Activities
    /// Handles data access operations with filtering and pagination
    /// </summary>
    public interface IFIUProgramActivityRepository
    {
        // ==========================================
        // CORE CRUD OPERATIONS
        // ==========================================

        Task<FIUProgramActivity> AddAsync(FIUProgramActivity entity);

        Task<FIUProgramActivity?> GetByIdAsync(int id);

        Task<FIUProgramActivity?> GetWithDetailsAsync(int id);

        Task<IEnumerable<FIUProgramActivity>> GetAllAsync();

        Task<FIUProgramActivity> UpdateAsync(FIUProgramActivity entity);

        Task<bool> DeleteAsync(int id);

        // ==========================================
        // PAGINATION & FILTERS
        // ==========================================

        /// <summary>
        /// Get paginated activities for specific trainers (by IDs)
        /// Supports filtering by date range, unit location, and search term
        /// </summary>
        Task<PaginatedResult<FIUProgramActivity>> GetPaginatedAsync(
            List<int> trainerIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get activities by status for specific trainers
        /// Status: "Draft", "Saved", "Pending", "Approved", "Rejected"
        /// </summary>
        Task<PaginatedResult<FIUProgramActivity>> GetByStatusAsync(
            List<int> trainerIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get count of activities by status for dashboard summary
        /// Returns aggregated counts for each status
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> trainerIds);

        /// <summary>
        /// Get activities by unit location
        /// </summary>
        Task<List<FIUProgramActivity>> GetByUnitLocationAsync(int unitLocationId);

        /// <summary>
        /// Get activities created by specific trainer
        /// </summary>
        Task<List<FIUProgramActivity>> GetByCreatedByAsync(int trainerId);
    }
}