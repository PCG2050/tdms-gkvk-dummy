// Application/Interface/Services/DataTables/INaepProgramService.cs

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
        Task<ServiceResult<NaepProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
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
        Task<ServiceResult<NaepProgramContentDto>> AddProgramContentAsync(int programId, NaepProgramContentCreateDto dto);

        Task<ServiceResult<NaepProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, NaepProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<NaepProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, NaepProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<NaepProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<NaepProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<NaepAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, NaepAdvisoryServicesCreateDto dto);
        Task<ServiceResult<NaepAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, NaepAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<NaepAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<NaepReportDto>> AddReportAsync(int programId, NaepReportCreateDto dto);
        Task<ServiceResult<NaepReportDto>> UpdateReportAsync(int reportId, NaepReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<NaepReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<NaepRecommendationDto>> AddRecommendationAsync(int programId, NaepRecommendationCreateDto dto);
        Task<ServiceResult<NaepRecommendationDto>> UpdateRecommendationAsync(int recommendationId, NaepRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<NaepRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<NaepProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<NaepProgramDetailsDto>> GetByStatusAsync(
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
    }
}