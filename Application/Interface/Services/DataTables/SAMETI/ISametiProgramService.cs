
using Application.Models;
using Application.Models.DataTables.SAMETI;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.SAMETI
{
    public interface ISametiProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<SametiProgramDetailsDto>> CreateProgramAsync(SametiProgramCreateDto dto);
        Task<ServiceResult<SametiProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<SametiProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<SametiProgramDetailsDto>> UpdateProgramAsync(int id, SametiProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<SametiParticipantDemographicsDto>> AddDemographicsAsync(int programId, SametiParticipantDemographicsCreateDto dto);
        Task<ServiceResult<SametiParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, SametiParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<SametiParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create SametiProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<SametiProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, SametiProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update SametiProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<SametiProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, SametiProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<SametiProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<SametiProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<SametiAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, SametiAdvisoryServicesCreateDto dto);
        Task<ServiceResult<SametiAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<SametiReportDto>> AddOrUpdateReportAsync(int programId, SametiReportCreateDto dto);
        Task<ServiceResult<SametiReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<SametiRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, SametiRecommendationCreateDto dto);
        Task<ServiceResult<SametiRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter SAMETI programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<SametiProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<SametiProgramListItemDto>> GetByStatusAsync(
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
