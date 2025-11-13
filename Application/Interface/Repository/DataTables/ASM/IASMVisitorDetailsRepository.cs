using Application.Models;
using Domain.Entities.ASM;

namespace Application.Interface.Repository.DataTables.ASM
{
    /// <summary>
    /// Repository interface for ASM Visitor Details
    /// Handles data access operations with filtering and pagination
    /// </summary>
    public interface IASMVisitorDetailsRepository
    {
        // ==========================================
        // CORE CRUD OPERATIONS
        // ==========================================

        IQueryable<ASMVisitorDetails> GetQueryable();

        Task<ASMVisitorDetails> AddAsync(ASMVisitorDetails entity);

        Task<ASMVisitorDetails?> GetByIdAsync(int id);

        Task<ASMVisitorDetails?> GetWithDetailsAsync(int id);

        Task<IEnumerable<ASMVisitorDetails>> GetAllAsync();

        Task<ASMVisitorDetails> UpdateAsync(ASMVisitorDetails entity);

        Task<bool> DeleteAsync(int id);

        // ==========================================
        // PAGINATION & FILTERS
        // ==========================================

        /// <summary>
        /// Get paginated visitor details for specific trainers (by IDs)
        /// Supports filtering by date range, unit location, and search term
        /// </summary>
        Task<PaginatedResult<ASMVisitorDetails>> GetPaginatedAsync(
            List<int> trainerIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get visitor details by status for specific trainers
        /// Status: "Draft", "Saved", "Pending", "Approved", "Rejected"
        /// </summary>
        Task<PaginatedResult<ASMVisitorDetails>> GetByStatusAsync(
            List<int> trainerIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get count of visitor details by status for dashboard summary
        /// Returns aggregated counts for each status
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> trainerIds);

        /// <summary>
        /// Get visitor details by unit location
        /// </summary>
        Task<List<ASMVisitorDetails>> GetByUnitLocationAsync(int unitLocationId);

        /// <summary>
        /// Get visitor details created by specific trainer
        /// </summary>
        Task<PaginatedResult<ASMVisitorDetails>> GetByCreatedByAsync(
    int trainerId,
    int pageNumber,
    int pageSize);

        //New MOntly Report
        Task<(ASMVisitorSummaryDto summary, int totalEntries)> GetMonthlyVisitorSummaryAsync(
       List<int> unitLocationIds,
       int year,
       int month);

    }
   
}