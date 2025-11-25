
using Application.Models;
using Application.Models.DataTables.FTI;
using Application.Models.DataTables.STU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.FTI
{
    public interface IFtiProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<FtiProgramDetailsDto>> CreateProgramAsync(FtiProgramCreateDto dto);
        Task<ServiceResult<FtiProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<FtiProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<FtiProgramDetailsDto>> UpdateProgramAsync(int id, FtiProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<FtiParticipantDemographicsDto>> AddDemographicsAsync(int programId, FtiParticipantDemographicsCreateDto dto);
        Task<ServiceResult<FtiParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, FtiParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<FtiParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create FtiProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<FtiProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, FtiProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update FtiProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<FtiProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, FtiProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<FtiProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<FtiProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<FtiAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, FtiAdvisoryServicesCreateDto dto);
        Task<ServiceResult<FtiAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<FtiReportDto>> AddOrUpdateReportAsync(int programId, FtiReportCreateDto dto);
        Task<ServiceResult<FtiReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<FtiRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, FtiRecommendationCreateDto dto);
        Task<ServiceResult<FtiRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter FTI programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<FtiProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<FtiProgramListItemDto>> GetByStatusAsync(
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
