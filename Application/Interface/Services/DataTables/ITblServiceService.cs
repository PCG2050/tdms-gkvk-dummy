using Application.Models;
using Application.Services.Common;
using Domain.Entities.GenericTables.Service;

namespace Application.Interface.Services.DataTables
{
    public interface ITblServiceService
    {
        // ============================
        // MAIN TblService CRUD
        // ============================


        Task<ServiceResult<TblServicesDto>> CreateAsync(TblServiceCreateDto createDto);
        Task<ServiceResult<TblServicesDto>> GetByIdAsync(int id);
        Task<ServiceResult<CompleteTblServicesDto>> GetCompleteTblServiceAsync(int id);
        Task<ServiceResult<TblServicesDto>> UpdateAsync(int id, TblServiceUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);

        // ============================
        // CHILD ENTITY: TABLE HOSTEL
        // ============================

        Task<ServiceResult<TableHostelDto>> AddTableHostelAsync(int serviceId, TableHostelCreateDto dto);
        Task<ServiceResult<TableHostelDto>> UpdateTableHostelAsync(int tableHostelId, TableHostelCreateDto dto);
        Task<ServiceResult> DeleteTableHostelAsync(int tableHostelId);
        Task<ServiceResult<List<TableHostelDto>>> GetTableHostelsAsync(int serviceId);

        // ============================
        // CHILD ENTITY: REVOLVING FUND STATUS
        // ============================

        Task<ServiceResult<RevolvingFundStatusDto>> AddRevolvingFundStatusAsync(int serviceId, RevolvingFundStatusCreateDto dto);
        Task<ServiceResult<RevolvingFundStatusDto>> UpdateRevolvingFundStatusAsync(int fundStatusId, RevolvingFundStatusCreateDto dto);
        Task<ServiceResult> DeleteRevolvingFundStatusAsync(int fundStatusId);
        Task<ServiceResult<List<RevolvingFundStatusDto>>> GetRevolvingFundStatusesAsync(int serviceId);

        // ============================
        // CHILD ENTITY: VISITOR DETAILS
        // ============================

        Task<ServiceResult<VisitorDetailDto>> AddVisitorDetailAsync(int serviceId, VisitorDetailCreateDto dto);
        Task<ServiceResult<VisitorDetailDto>> UpdateVisitorDetailAsync(int visitorDetailId, VisitorDetailCreateDto dto);
        Task<ServiceResult> DeleteVisitorDetailAsync(int visitorDetailId);
        Task<ServiceResult<List<VisitorDetailDto>>> GetVisitorDetailsAsync(int serviceId);

        // ============================
        // COMPOSITE CREATE/UPDATE WITH CHILDREN
        // ============================

        /// <summary>
        /// Create TblService with all child entities (TableHostel, RevolvingFundStatus, VisitorDetail) in a single transaction
        /// Solves the parent-child ID dependency - perfect for "Save & Next" button
        /// Note: TblServiceCreateDto already has optional child lists, this method uses them properly
        /// </summary>
        Task<ServiceResult<TblServicesDto>> CreateWithChildrenAsync(TblServiceCreateDto dto);

        /// <summary>
        /// Update TblService with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<TblServicesDto>> UpdateWithChildrenAsync(int serviceId, TblServiceWithChildrenUpdateDto dto);

        // ============================
        // SUBMISSION & APPROVAL WORKFLOW
        // ============================

        Task<ServiceResult> SubmitForApprovalAsync(int serviceId);
        Task<ServiceResult> ApproveTblServiceAsync(int serviceId, string? remarks = null);
        Task<ServiceResult> RejectTblServiceAsync(int serviceId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================

        Task<PaginatedResult<TblServicesDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<TblServicesDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync();

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
            int pageSize = 10);

        /// <summary>
        /// Get tbl services by trainer ID (Unit Head and Admin only)
        /// </summary>
        Task<PaginatedResult<TblServicesDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get unified history - own forms or trainer forms (for unit heads)
        /// </summary>
        Task<PaginatedResult<TblServicesDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}
