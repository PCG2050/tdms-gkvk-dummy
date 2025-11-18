using Application.Models;
using Application.Models.DataTables;

namespace Application.Interface.Services.DataTables
{
    /// <summary>
    /// Service interface for TableOtherActivity
    /// 
    /// This is a GENERIC table used across ALL units for miscellaneous activities
    /// that don't fit into specific unit tables.
    /// 
    /// STATUS WORKFLOW:
    /// - Pending: Auto-submitted when created or updated, awaits Unit Head approval
    /// - Approved: Unit Head approved, cannot edit
    /// - Rejected: Unit Head rejected with remarks, can edit and resubmit
    /// - On create/update: Status automatically changes to Pending
    ///
    /// PERMISSIONS:
    /// - Trainers: Create, Edit (Pending/Rejected), View own entries
    /// - UnitHeads: Approve/Reject entries for their unit locations, View all in their units
    /// - Admins: Full access to all entries across all organizations
    /// </summary>
    public interface ITableOtherActivityService
    {
        // ============================
        // MAIN CRUD OPERATIONS
        // ============================

        /// <summary>
        /// Create a new OtherActivity entry (Trainer only)
        /// Status: Automatically set to Pending (auto-submit)
        /// Auto-sets CreatedById, CreatedAt, and OrganizationId from current user
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> CreateAsync(TableOtherActivityCreateDto createDto);

        /// <summary>
        /// Get OtherActivity by ID with permission check
        /// Returns entry with full details including creator and approver names
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> GetByIdAsync(int id);

        /// <summary>
        /// Update OtherActivity (Pending/Rejected status)
        /// Trainers can only update their own entries
        /// Status auto-changes to Pending on update
        /// Cannot update Approved entries
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> UpdateAsync(int id, TableOtherActivityUpdateDto updateDto);

        /// <summary>
        /// Delete OtherActivity (Pending/Rejected status)
        /// Trainers can only delete their own entries
        /// Cannot delete Approved entries
        /// </summary>
        Task<ServiceResult> DeleteAsync(int id);

        // ============================
        // APPROVAL WORKFLOW
        // ============================

        /// <summary>
        /// Approve OtherActivity (Unit Head only)
        /// Changes status: Pending → Approved
        /// Records approver ID and timestamp
        /// Optional approval remarks
        /// </summary>
        Task<ServiceResult> ApproveAsync(int id, string? remarks = null);

        /// <summary>
        /// Reject OtherActivity with remarks (Unit Head only)
        /// Changes status: Pending → Rejected
        /// Remarks are REQUIRED for rejection
        /// Trainer can then edit and resubmit
        /// </summary>
        Task<ServiceResult> RejectAsync(int id, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================

        /// <summary>
        /// Get paginated list with filters
        /// Auto-filters by user's accessible unit locations based on role
        /// - Trainers: Only their own entries
        /// - UnitHeads: All entries in their assigned unit locations
        /// - Admins: All entries in organization (or all if SuperAdmin)
        /// </summary>
        Task<PaginatedResult<TableOtherActivityDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get entries by specific status (for history/dashboard views)
        /// Useful for showing "My Pending Approvals", "My Drafts", etc.
        /// </summary>
        Task<PaginatedResult<TableOtherActivityDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        // ============================
        // DASHBOARD & STATISTICS
        // ============================

        /// <summary>
        /// Get status summary (count by status)
        /// Returns: { "Draft": 5, "Pending": 3, "Approved": 12, "Rejected": 1 }
        /// Filtered by user's accessible unit locations
        /// </summary>
        Task<ServiceResult<Dictionary<string, int>>> GetStatusSummaryAsync();

        /// <summary>
        /// Get trainer's submission history
        /// Shows all entries with status, submission date, approval date
        /// Sorted by most recent first
        /// </summary>
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for Unit Head
        /// Shows all entries in Pending status for their unit locations
        /// </summary>
        Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get table other activities by trainer ID (Unit Head and Admin only)
        /// </summary>
        Task<PaginatedResult<TableOtherActivityDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get unified history - own forms or trainer forms (for unit heads)
        /// </summary>
        Task<PaginatedResult<TableOtherActivityDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}