using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables
{
    public interface IPublicationService
    {
        // ===== PHASE 1: Publication Details =====

        /// <summary>
        /// Create new publication (Phase 1) - Returns publicationId for subsequent phases
        /// Status: Draft
        /// </summary>
        Task<ServiceResult<PublicationDto>> CreatePhase1Async(PublicationCreateDto createDto);

        /// <summary>
        /// Update publication Phase 1 data (when going back to edit)
        /// Can only update Draft or Rejected publications
        /// </summary>
        Task<ServiceResult<PublicationDto>> UpdateAsync(int id, PublicationUpdateDto updateDto);

        // ===== PHASE 2: Publisher Details (Optional) =====

        /// <summary>
        /// Add publisher details to publication (Phase 2)
        /// Optional - can be skipped
        /// </summary>
        Task<ServiceResult<PublisherDetailsDto>> AddPublisherDetailsAsync(
            int publicationId,
            PublisherDetailsCreateDto dto);

        /// <summary>
        /// Update existing publisher details (when editing)
        /// </summary>
        Task<ServiceResult<PublisherDetailsDto>> UpdatePublisherDetailsAsync(
            int publisherDetailsId,
            PublisherDetailsCreateDto dto);

        // ===== PHASE 3: Extension Literature (Multiple entries) =====

        /// <summary>
        /// Add extension literature entry (Phase 3)
        /// Can add multiple entries
        /// </summary>
        Task<ServiceResult<ExtensionLiteratureDto>> AddExtensionLiteratureAsync(
            int publicationId,
            ExtensionLiteratureCreateDto dto);

        /// <summary>
        /// Update extension literature entry
        /// </summary>
        Task<ServiceResult<ExtensionLiteratureDto>> UpdateExtensionLiteratureAsync(
            int extensionLiteratureId,
            ExtensionLiteratureCreateDto dto);

        /// <summary>
        /// Delete extension literature entry
        /// </summary>
        Task<ServiceResult> DeleteExtensionLiteratureAsync(int extensionLiteratureId);

        /// <summary>
        /// Get all extension literature entries for a publication
        /// Used in Phase 3 to display existing entries
        /// </summary>
        Task<ServiceResult<List<ExtensionLiteratureDto>>> GetExtensionLiteraturesAsync(
            int publicationId);

        // ===== FINAL SUBMISSION =====

        /// <summary>
        /// Submit publication for approval (Only submission point)
        /// Changes status: Draft → Pending
        /// Can only submit Draft publications
        /// </summary>
        Task<ServiceResult> SubmitForApprovalAsync(int publicationId);

        // ===== HISTORY & VIEWING =====

        /// <summary>
        /// Get single publication by ID
        /// For viewing publication details
        /// </summary>
        Task<ServiceResult<PublicationDto>> GetByIdAsync(int id);

        /// <summary>
        /// Get complete publication with all phases data
        /// Used when editing from history - loads Phase 1, 2, and 3 data
        /// </summary>
        Task<ServiceResult<CompletePublicationDto>> GetCompletePublicationAsync(int id);

        /// <summary>
        /// Delete publication
        /// Can only delete Draft publications
        /// </summary>
        Task<ServiceResult> DeleteAsync(int id);

        /// <summary>
        /// Get paginated publications for history page
        /// Automatically filters by user's accessible unit locations
        /// </summary>
        Task<PaginatedResult<PublicationDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        // ===== STATUS-BASED QUERIES =====

        /// <summary>
        /// Get publications by status (Draft, Pending, Approved, Rejected)
        /// Used for filtering in history page
        /// </summary>
        Task<PaginatedResult<PublicationDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get count of publications by status
        /// Used for dashboard summary
        /// Returns: { "Draft": 5, "Pending": 8, "Approved": 12, "Rejected": 2 }
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync();

        // ===== UNIT HEAD OPERATIONS =====

        /// <summary>
        /// Approve publication (Unit Head only)
        /// Changes status: Pending → Approved
        /// </summary>
        Task<ServiceResult> ApprovePublicationAsync(int publicationId, string? remarks = null);

        /// <summary>
        /// Reject publication with remarks (Unit Head only)
        /// Changes status: Pending → Rejected
        /// Remarks are required for rejection
        /// </summary>
        Task<ServiceResult> RejectPublicationAsync(int publicationId, string remarks);

        /// <summary>
        /// Get publications grouped by unit (Unit Head view)
        /// Shows all publications from units managed by the Unit Head
        /// </summary>
        //Task<object> GetGroupedByUnitAsync(int pageNumber = 1, int pageSize = 10);
        Task<object> GetGroupedByUnitAsync(PaginationRequest pagination);

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for Unit Head and Admin with pagination
        /// </summary>
        Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? createdByIdFilter = null);

    }
}
