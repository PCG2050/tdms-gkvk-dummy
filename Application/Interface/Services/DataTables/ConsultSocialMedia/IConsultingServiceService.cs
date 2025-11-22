using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.ConsultSocialMedia
{
    public interface IConsultingServiceService
    {
        // Main Consulting Service CRUD
        Task<ServiceResult<ConsultingServiceDto>> CreateAsync(ConsultingServiceCreateDto createDto);
        Task<ServiceResult<ConsultingServiceDto>> GetByIdAsync(int id);
        Task<ServiceResult<CompleteConsultingServiceDto>> GetCompleteConsultingServiceAsync(int id);
        Task<ServiceResult<ConsultingServiceDto>> UpdateAsync(int id, ConsultingServiceUpdateDto updateDto);
        Task<ServiceResult> DeleteAsync(int id);

        // Composite Create/Update with Children (Hybrid Pattern)
        /// <summary>
        /// Create ConsultingService with all child entities (ModeAndOutreach) in a single transaction
        /// </summary>
        Task<ServiceResult<ConsultingServiceDto>> CreateWithChildrenAsync(ConsultingServiceWithChildrenCreateDto dto);


        Task<ServiceResult<ConsultingServiceDto>> UpdateWithChildrenAsync(int consultingServiceId, ConsultingServiceWithChildrenUpdateDto dto);

        // Submission & Approval
        Task<ServiceResult> SubmitForApprovalAsync(int consultingServiceId);
        Task<ServiceResult> ApproveConsultingServiceAsync(int consultingServiceId, string? remarks = null);
        Task<ServiceResult> RejectConsultingServiceAsync(int consultingServiceId, string remarks);

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for Unit Head with pagination
        /// </summary>
        Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? createdByIdFilter = null);




        // Pagination & Filtering
        Task<PaginatedResult<ConsultingServiceDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<ConsultingServiceDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync();
    }
}