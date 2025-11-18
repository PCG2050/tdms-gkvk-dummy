// Application/Interface/Services/DataTables/IAticProgramService.cs

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
        Task<ServiceResult<AticProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
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
        Task<ServiceResult<AticProgramContentDto>> AddProgramContentAsync(int programId, AticProgramContentCreateDto dto);
        Task<ServiceResult<AticProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<AticProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // Hybrid pattern methods for bulk create/update operations
        Task<ServiceResult<AticProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, AticProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<AticProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, AticProgramContentWithChildrenUpdateDto dto);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<AticAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, AticAdvisoryServicesCreateDto dto);
        Task<ServiceResult<AticAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, AticAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<AticAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<AticReportDto>> AddReportAsync(int programId, AticReportCreateDto dto);
        Task<ServiceResult<AticReportDto>> UpdateReportAsync(int reportId, AticReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<AticReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<AticRecommendationDto>> AddRecommendationAsync(int programId, AticRecommendationCreateDto dto);
        Task<ServiceResult<AticRecommendationDto>> UpdateRecommendationAsync(int recommendationId, AticRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<AticRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<AticProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<AticProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

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

        Task<PaginatedResult<AticProgramDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        Task<PaginatedResult<AticProgramDetailsDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}