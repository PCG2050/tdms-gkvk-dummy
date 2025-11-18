using Application.Models;
using Application.Models.DataTables.FIU;
using Application.Models.DataTables.FIU.Application.Models.FIU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.FIU
{
    /// <summary>
    /// Service interface for FIU Program Activities
    /// Handles CRUD operations and FormStatus workflow management
    /// </summary>
    public interface IFIUProgramActivityService
    {
        // CRUD Operations
        Task<ServiceResult<FIUProgramActivityResponseDto>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> DeleteAsync(int id);

        // Batch Operations
        Task<ServiceResult<FIUProgramActivityBatchResultDto>> CreateBatchAsync(FIUProgramActivityBatchCreateDto batchCreateDto);
        Task<ServiceResult<FIUProgramActivityBatchResultDto>> UpdateBatchAsync(FIUProgramActivityBatchUpdateDto batchUpdateDto);

        // Workflow
        Task<ServiceResult<FIUProgramActivityResponseDto>> ApproveAsync(int id, string? remarks);
        Task<ServiceResult<FIUProgramActivityResponseDto>> RejectAsync(int id, string remarks);

        // Queries
        Task<ServiceResult<PaginatedResult<FIUProgramActivityResponseDto>>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? activityId = null,
            string? status = null);

        Task<ServiceResult<List<FIUProgramActivityResponseDto>>> GetMyActivitiesAsync();
        Task<ServiceResult<List<FIUProgramActivityResponseDto>>> GetPendingApprovalsAsync();

        // Master Data
        Task<ServiceResult<List<FIUActivityDto>>> GetAvailableActivitiesAsync();

        // Statistics
        Task<ServiceResult<Dictionary<string, int>>> GetStatsByActivityTypeAsync();
        Task<ServiceResult<Dictionary<string, int>>> GetStatsByStatusAsync();

        // Monthly Report
        Task<ServiceResult<FIUMonthlyReportDto>> GetMonthlyReportAsync(FIUReportRequestDto requestDto);


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
    }
}
