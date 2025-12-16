
using Application.Models;
using Application.Models.DataTables.EEU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.EEU
{
    public interface IEeuProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<EeuProgramDetailsDto>> CreateProgramAsync(EeuProgramCreateDto dto);
        Task<ServiceResult<EeuProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<EeuProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<EeuProgramDetailsDto>> UpdateProgramAsync(int id, EeuProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<EeuParticipantDemographicsDto>> AddDemographicsAsync(int programId, EeuParticipantDemographicsCreateDto dto);
        Task<ServiceResult<EeuParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, EeuParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<EeuParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create EeuProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<EeuProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, EeuProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update EeuProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<EeuProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, EeuProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<EeuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<EeuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<EeuAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, EeuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<EeuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        /// <summary>
        /// Create EeuAdvisoryServices along with all child entities (CriticalInputsDistributed) in a single transaction
        /// </summary>
        Task<ServiceResult<EeuAdvisoryServicesDto>> AddAdvisoryServicesWithChildrenAsync(int programId, EeuAdvisoryServicesHybridCreateDto dto);

        /// <summary>
        /// Update EeuAdvisoryServices with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing capabilities
        /// </summary>
        Task<ServiceResult<EeuAdvisoryServicesDto>> UpdateAdvisoryServicesWithChildrenAsync(int advisoryServicesId, EeuAdvisoryServicesHybridUpdateDto dto);


        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24)
        // ============================
        Task<ServiceResult<EeuResultDto>> GetResultByIdAsync(int resultId);
        Task<ServiceResult> UpdateResultExcelAsync(int resultId, string excelUrl);

        // E3: Composite Create/Update for Results with Children
        /// <summary>
        /// Create EeuResult along with all child entities (FldResults and OftResults) in a single transaction
        /// Solves the parent-child ID dependency issue by creating parent first, then using its ID for children
        /// Perfect for "Save & Next" button that needs to save all data at once
        /// </summary>
        Task<ServiceResult<EeuResultDto>> CreateResultWithChildrenAsync(int programId, EeuResultWithChildrenCreateDto dto);

        /// <summary>
        /// Update EeuResult with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing capabilities
        /// </summary>
        Task<ServiceResult<EeuResultDto>> UpdateResultWithChildrenAsync(int resultId, EeuResultWithChildrenUpdateDto dto);

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<EeuReportDto>> AddOrUpdateReportAsync(int programId, EeuReportCreateDto dto);
        Task<ServiceResult<EeuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<EeuRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, EeuRecommendationCreateDto dto);
        Task<ServiceResult<EeuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter EEU programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<EeuProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<EeuProgramListItemDto>> GetByStatusAsync(
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
