
using Application.Models;
using Application.Models.DataTables.ATIC;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.ATIC
{
    public interface IAticProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<AticProgramDetailsDto>> CreateProgramAsync(AticProgramCreateDto dto);
        Task<ServiceResult<AticProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<AticProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<AticProgramDetailsDto>> UpdateProgramAsync(int id, AticProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<AticParticipantDemographicsDto>> AddDemographicsAsync(int programId, AticParticipantDemographicsCreateDto dto);
        Task<ServiceResult<AticParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, AticParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<AticParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create AticProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<AticProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, AticProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update AticProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<AticProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, AticProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<AticProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<AticProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<AticAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, AticAdvisoryServicesCreateDto dto);
        Task<ServiceResult<AticAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<AticReportDto>> AddOrUpdateReportAsync(int programId, AticReportCreateDto dto);
        Task<ServiceResult<AticReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<AticRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, AticRecommendationCreateDto dto);
        Task<ServiceResult<AticRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT
        // ============================

        /// <summary>
        /// Submit program for approval (Trainer role - changes status from Draft to Pending)
        /// </summary>
        Task<ServiceResult> SubmitForApprovalAsync(int programId);

        /// <summary>
        /// Approve program (UnitHead/Admin roles - changes status from Pending to Approved)
        /// </summary>
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);

        /// <summary>
        /// Reject program (UnitHead/Admin roles - changes status from Pending to Rejected)
        /// </summary>
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // LISTING & FILTERING
        // ============================

        /// <summary>
        /// Search and filter ATIC programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<AticProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<AticProgramListItemDto>> GetByStatusAsync(
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
