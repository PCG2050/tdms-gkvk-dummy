using Application.Models;
using Application.Models.DataTables.FIU;

namespace Application.Interface.Services.DataTables.FIU
{
    /// <summary>
    /// Service interface for FIU Program Activities
    /// Handles CRUD operations and FormStatus workflow management
    /// </summary>
    public interface IFIUProgramActivityService
    {
        // ==========================================
        // CRUD OPERATIONS
        // ==========================================

        /// <summary>
        /// Create a new FIU Program Activity
        /// Initial status: Draft
        /// </summary>
        Task<ServiceResult<FIUProgramActivityDto>> AddAsync(FIUProgramActivityDto dto);

        /// <summary>
        /// Get FIU Program Activity by ID
        /// Validates user has permission to view
        /// </summary>
        Task<ServiceResult<FIUProgramActivityDto>> GetByIdAsync(int id);

        /// <summary>
        /// Get all FIU Program Activities accessible to current user
        /// Filters based on role and permissions
        /// </summary>
        Task<IEnumerable<FIUProgramActivityDto>> GetAllAsync();

        /// <summary>
        /// Update existing FIU Program Activity
        /// Can only update Draft or Rejected entries
        /// Auto-resets Rejected entries to Draft on edit
        /// </summary>
        Task<ServiceResult<FIUProgramActivityDto>> UpdateAsync(int id, FIUProgramActivityDto dto);

        /// <summary>
        /// Delete FIU Program Activity
        /// Can only delete Draft entries
        /// </summary>
        Task<ServiceResult> DeleteAsync(int id);

        // ==========================================
        // FORM STATUS WORKFLOW
        // ==========================================

        /// <summary>
        /// Submit activity for approval
        /// Changes status: Draft → Pending
        /// Only trainers can submit their own entries
        /// </summary>
        Task<ServiceResult> SubmitForApprovalAsync(int id);

        /// <summary>
        /// Approve activity (UnitHead/Admin only)
        /// Changes status: Pending → Approved
        /// </summary>
        Task<ServiceResult> ApproveAsync(int id, string? remarks = null);

        /// <summary>
        /// Reject activity with remarks (UnitHead/Admin only)
        /// Changes status: Pending → Rejected
        /// Remarks are required for rejection
        /// </summary>
        Task<ServiceResult> RejectAsync(int id, string remarks);

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        /// <summary>
        /// Get paginated activities with optional filters
        /// Automatically filters based on user role and permissions
        /// </summary>
        Task<PaginatedResult<FIUProgramActivityDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get activities by specific status
        /// Used for filtering history page
        /// </summary>
        Task<PaginatedResult<FIUProgramActivityDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get count of activities by status for dashboard
        /// Returns: { "Draft": 5, "Pending": 8, "Approved": 12, "Rejected": 2 }
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync();
    }
}