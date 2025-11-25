
using Application.Models;
using Application.Models.DataTables.DEU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.DEU
{
    public interface IDeuProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<DeuProgramDetailsDto>> CreateProgramAsync(DeuProgramCreateDto dto);
        Task<ServiceResult<DeuProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<DeuProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<DeuProgramDetailsDto>> UpdateProgramAsync(int id, DeuProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<DeuParticipantDemographicsDto>> AddDemographicsAsync(int programId, DeuParticipantDemographicsCreateDto dto);
        Task<ServiceResult<DeuParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, DeuParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<DeuParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create DeuProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<DeuProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, DeuProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update DeuProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<DeuProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, DeuProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<DeuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<DeuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<DeuAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, DeuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<DeuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<DeuReportDto>> AddOrUpdateReportAsync(int programId, DeuReportCreateDto dto);
        Task<ServiceResult<DeuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<DeuRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, DeuRecommendationCreateDto dto);
        Task<ServiceResult<DeuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter DEU programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<DeuProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<DeuProgramListItemDto>> GetByStatusAsync(
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
