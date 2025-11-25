
using Application.Models;
using Application.Models.DataTables.NAEP;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.NAEP
{
    public interface INaepProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<NaepProgramDetailsDto>> CreateProgramAsync(NaepProgramCreateDto dto);
        Task<ServiceResult<NaepProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<NaepProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<NaepProgramDetailsDto>> UpdateProgramAsync(int id, NaepProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<NaepParticipantDemographicsDto>> AddDemographicsAsync(int programId, NaepParticipantDemographicsCreateDto dto);
        Task<ServiceResult<NaepParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, NaepParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<NaepParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create NaepProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<NaepProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, NaepProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update NaepProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<NaepProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, NaepProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<NaepProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<NaepProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<NaepAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, NaepAdvisoryServicesCreateDto dto);
        Task<ServiceResult<NaepAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<NaepReportDto>> AddOrUpdateReportAsync(int programId, NaepReportCreateDto dto);
        Task<ServiceResult<NaepReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<NaepRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, NaepRecommendationCreateDto dto);
        Task<ServiceResult<NaepRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter NAEP programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<NaepProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<NaepProgramListItemDto>> GetByStatusAsync(
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
