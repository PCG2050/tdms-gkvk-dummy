
using Application.Models;
using Application.Models.DataTables.KVK;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.KVK
{
    public interface IKvkProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<KvkProgramDetailsDto>> CreateProgramAsync(KvkProgramCreateDto dto);
        Task<ServiceResult<KvkProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<KvkProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<KvkProgramDetailsDto>> UpdateProgramAsync(int id, KvkProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<KvkParticipantDemographicsDto>> AddDemographicsAsync(int programId, KvkParticipantDemographicsCreateDto dto);
        Task<ServiceResult<KvkParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, KvkParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<KvkParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create KvkProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<KvkProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, KvkProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update KvkProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<KvkProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, KvkProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<KvkProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<KvkProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<KvkAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, KvkAdvisoryServicesCreateDto dto);
        Task<ServiceResult<KvkAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24)
        // ============================
        Task<ServiceResult<KvkResultDto>> GetResultByIdAsync(int resultId);
        Task<ServiceResult> UpdateResultExcelAsync(int resultId, string excelUrl);

        // E3: Composite Create/Update for Results with Children
        /// <summary>
        /// Create KvkResult along with all child entities (FldResults and OftResults) in a single transaction
        /// Solves the parent-child ID dependency issue by creating parent first, then using its ID for children
        /// Perfect for "Save & Next" button that needs to save all data at once
        /// </summary>
        Task<ServiceResult<KvkResultDto>> CreateResultWithChildrenAsync(int programId, KvkResultWithChildrenCreateDto dto);

        /// <summary>
        /// Update KvkResult with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing capabilities
        /// </summary>
        Task<ServiceResult<KvkResultDto>> UpdateResultWithChildrenAsync(int resultId, KvkResultWithChildrenUpdateDto dto);

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<KvkReportDto>> AddOrUpdateReportAsync(int programId, KvkReportCreateDto dto);
        Task<ServiceResult<KvkReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<KvkRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, KvkRecommendationCreateDto dto);
        Task<ServiceResult<KvkRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // LISTING & FILTERING
        // ============================

        /// <summary>
        /// Search and filter KVK programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<KvkProgramListItemDto>> GetPaginatedAsync(
            int pageNumber,
            int pageSize,
            DateOnly? startDate,
            DateOnly? endDate,
            int? categoryId,
            string? searchTerm,
            string? formStatus,
            int? createdById,
            int? unitLocationId);

        /// <summary>
        /// Get programs by status with pagination
        /// </summary>
        Task<PaginatedResult<KvkProgramListItemDto>> GetByStatusAsync(
            string status,
            int pageNumber,
            int pageSize);

        /// <summary>
        /// Get summary of programs grouped by status
        /// </summary>
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
    }
}
