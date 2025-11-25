
using Application.Models;
using Application.Models.DataTables.IBTVA;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.IBTVA
{
    public interface IIbtvaProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<IbtvaProgramDetailsDto>> CreateProgramAsync(IbtvaProgramCreateDto dto);
        Task<ServiceResult<IbtvaProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<IbtvaProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<IbtvaProgramDetailsDto>> UpdateProgramAsync(int id, IbtvaProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<IbtvaParticipantDemographicsDto>> AddDemographicsAsync(int programId, IbtvaParticipantDemographicsCreateDto dto);
        Task<ServiceResult<IbtvaParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, IbtvaParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<IbtvaParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create IbtvaProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<IbtvaProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, IbtvaProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update IbtvaProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<IbtvaProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, IbtvaProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<IbtvaProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<IbtvaProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<IbtvaAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, IbtvaAdvisoryServicesCreateDto dto);
        Task<ServiceResult<IbtvaAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<IbtvaReportDto>> AddOrUpdateReportAsync(int programId, IbtvaReportCreateDto dto);
        Task<ServiceResult<IbtvaReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<IbtvaRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, IbtvaRecommendationCreateDto dto);
        Task<ServiceResult<IbtvaRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter IBTVA programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<IbtvaProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<IbtvaProgramListItemDto>> GetByStatusAsync(
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
