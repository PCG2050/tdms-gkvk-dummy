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
    /// - Draft: Initial state, can edit (auto-set on creation)
    /// - Saved: Auto-saved state (not used for OtherActivity - goes from Draft → Pending)
    /// - Pending: Submitted for Unit Head approval (trainer submits)
    /// - Approved: Unit Head approved, cannot edit
    /// - Rejected: Unit Head rejected with remarks, can edit and resubmit
    /// 
    /// PERMISSIONS:
    /// - Trainers: Create, Edit (Draft/Rejected only), Submit, View own entries
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
        /// Initial status: Draft
        /// Auto-sets CreatedById, CreatedAt, and OrganizationId from current user
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> CreateAsync(TableOtherActivityCreateDto createDto);

        /// <summary>
        /// Get OtherActivity by ID with permission check
        /// Returns entry with full details including creator and approver names
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> GetByIdAsync(int id);

        /// <summary>
        /// Update OtherActivity (Only for Draft or Rejected status)
        /// Trainers can only update their own entries
        /// Cannot update entries in Pending or Approved status
        /// </summary>
        Task<ServiceResult<TableOtherActivityDto>> UpdateAsync(int id, TableOtherActivityUpdateDto updateDto);

        /// <summary>
        /// Delete OtherActivity (Only Draft status)
        /// Trainers can only delete their own Draft entries
        /// Once submitted, entries cannot be deleted
        /// </summary>
        Task<ServiceResult> DeleteAsync(int id);

        // ============================
        // SUBMISSION & APPROVAL WORKFLOW
        // ============================

        /// <summary>
        /// Submit OtherActivity for approval (Trainer only)
        /// Changes status: Draft → Pending
        /// Only the creator can submit
        /// </summary>
        Task<ServiceResult> SubmitForApprovalAsync(int id);

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
        Task<PaginatedResult<TableOtherActivityDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 20);

        /// <summary>
        /// Get pending approvals for Unit Head
        /// Shows all entries in Pending status for their unit locations
        /// </summary>
        Task<PaginatedResult<TableOtherActivityDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 20);
    }
}