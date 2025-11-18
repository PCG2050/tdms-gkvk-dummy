using Application.Models;
using Application.Models.DataTables.ASM;

namespace Application.Interface.Services.DataTables.ASM
{
    public interface IASMVisitorDetailsService
    {
        // CRUD
        Task<ServiceResult<ASMVisitorDetailsDto>> AddAsync(ASMVisitorDetailsCreateDto createDto);
        Task<ServiceResult<ASMVisitorDetailsDto>> GetByIdAsync(int id);
        Task<ServiceResult<ASMVisitorDetailsDto>> UpdateAsync(int id, ASMVisitorDetailsUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);

        // Batch Operations
        Task<ServiceResult<ASMVisitorDetailsBatchResultDto>> AddBatchAsync(ASMVisitorDetailsBatchCreateDto batchCreateDto);
        Task<ServiceResult<ASMVisitorDetailsBatchResultDto>> UpdateBatchAsync(ASMVisitorDetailsBatchUpdateDto batchUpdateDto);

        // Workflow
        Task<ServiceResult> SubmitForApprovalAsync(int id);
        Task<ServiceResult> ApproveAsync(int id, string? remarks = null);
        Task<ServiceResult> RejectAsync(int id, string remarks);

        // Pagination & Filtering
        Task<PaginatedResult<ASMVisitorDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null);

        Task<PaginatedResult<ASMVisitorDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        // Dashboard & Statistics
        Task<ServiceResult<Dictionary<string, int>>> GetStatusSummaryAsync();

        // Extra (Trainer & Approval Dashboards)
        Task<PaginatedResult<ASMVisitorDetailsDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 20);

        Task<PaginatedResult<ASMVisitorDetailsDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 20);
    }
}
