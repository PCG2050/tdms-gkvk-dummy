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

        // ModeAndOutreach Management
        Task<ServiceResult<ModeAndOutreachDto>> AddModeAndOutreachAsync(int consultingServiceId, ModeAndOutreachCreateDto dto);
        Task<ServiceResult<ModeAndOutreachDto>> UpdateModeAndOutreachAsync(int modeAndOutreachId, ModeAndOutreachCreateDto dto);
        Task<ServiceResult> DeleteModeAndOutreachAsync(int modeAndOutreachId);
        Task<ServiceResult<List<ModeAndOutreachDto>>> GetModeAndOutreachesAsync(int consultingServiceId);

        // Composite Create/Update with Children
        /// <summary>
        /// Create ConsultingService with all child entities (ModeAndOutreach) in a single transaction
        /// Solves the parent-child ID dependency - perfect for "Save & Next" button
        /// </summary>
        Task<ServiceResult<ConsultingServiceDto>> CreateWithChildrenAsync(ConsultingServiceWithChildrenCreateDto dto);

        /// <summary>
        /// Update ConsultingService with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
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
            int pageSize = 10);   




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