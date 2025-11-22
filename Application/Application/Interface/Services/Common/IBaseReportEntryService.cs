using Application.Models;

namespace Application.Interface.Services.Common
{
    /// <summary>
    /// Generic service interface for all ReportEntryBaseEntity types.
    /// Provides common CRUD operations, workflow management, and permission-aware queries.
    /// </summary>
    /// <typeparam name="TDto">Read DTO type</typeparam>
    /// <typeparam name="TCreateDto">Create DTO type</typeparam>
    /// <typeparam name="TUpdateDto">Update DTO type</typeparam>
    public interface IBaseReportEntryService<TDto, TCreateDto, TUpdateDto>
        where TDto : IBaseDto
        where TCreateDto : ICreateDto
        where TUpdateDto : IUpdateDto
    {
        // ============================
        // CRUD OPERATIONS
        // ============================

        /// <summary>
        /// Creates a new record with Draft status
        /// </summary>
        Task<ServiceResult<TDto>> CreateAsync(TCreateDto dto);

        /// <summary>
        /// Gets a record by ID (with permission check)
        /// </summary>
        Task<ServiceResult<TDto>> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing record (only allowed in Draft status)
        /// </summary>
        Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto dto);

        /// <summary>
        /// Deletes a record (only allowed in Draft status)
        /// </summary>
        Task<ServiceResult> DeleteAsync(int id);

        // ============================
        // WORKFLOW OPERATIONS
        // ============================

        /// <summary>
        /// Submits a record for approval (Draft → Pending)
        /// </summary>
        Task<ServiceResult> SubmitForApprovalAsync(int id);

        /// <summary>
        /// Approves a record (Pending → Approved)
        /// </summary>
        Task<ServiceResult> ApproveAsync(int id, string? remarks = null);

        /// <summary>
        /// Rejects a record (Pending → Rejected)
        /// </summary>
        Task<ServiceResult> RejectAsync(int id, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================

        /// <summary>
        /// Gets paginated results with optional filtering
        /// </summary>
        Task<PaginatedResult<TDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        /// <summary>
        /// Gets paginated results filtered by status
        /// </summary>
        Task<PaginatedResult<TDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Gets status summary (count by status)
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync();
    }
}
