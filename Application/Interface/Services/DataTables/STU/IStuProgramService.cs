
using Application.Models;
using Application.Models.DataTables.STU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.STU
{
    public interface IStuProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<StuProgramDetailsDto>> CreateProgramAsync(StuProgramCreateDto dto);
        Task<ServiceResult<StuProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<StuProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<StuProgramDetailsDto>> UpdateProgramAsync(int id, StuProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<StuParticipantDemographicsDto>> AddDemographicsAsync(int programId, StuParticipantDemographicsCreateDto dto);
        Task<ServiceResult<StuParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, StuParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<StuParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Create StuProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<StuProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, StuProgramContentWithChildrenCreateDto dto);

        /// <summary>
        /// Update StuProgramContentAndResources with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        Task<ServiceResult<StuProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, StuProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<StuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<StuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<StuAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, StuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<StuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);


        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<StuReportDto>> AddOrUpdateReportAsync(int programId, StuReportCreateDto dto);
        Task<ServiceResult<StuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<StuRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, StuRecommendationCreateDto dto);
        Task<ServiceResult<StuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
        /// Search and filter STU programs with pagination (Admin/UnitHead)
        /// </summary>
        Task<PaginatedResult<StuProgramListItemDto>> GetPaginatedAsync(
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
        Task<PaginatedResult<StuProgramListItemDto>> GetByStatusAsync(
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
