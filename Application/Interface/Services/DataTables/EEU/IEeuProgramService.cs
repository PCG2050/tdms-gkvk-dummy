// Application/Interface/Services/DataTables/IEeuProgramService.cs

using Application.Models.DataTables.DEU;
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
        Task<ServiceResult<EeuProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
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
        Task<ServiceResult<EeuProgramContentDto>> AddProgramContentAsync(int programId, EeuProgramContentCreateDto dto);
        Task<ServiceResult<EeuProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, EeuProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<EeuProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, EeuProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<EeuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<EeuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<EeuAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, EeuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<EeuAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, EeuAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<EeuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<EeuReportDto>> AddReportAsync(int programId, EeuReportCreateDto dto);
        Task<ServiceResult<EeuReportDto>> UpdateReportAsync(int reportId, EeuReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<EeuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<EeuRecommendationDto>> AddRecommendationAsync(int programId, EeuRecommendationCreateDto dto);
        Task<ServiceResult<EeuRecommendationDto>> UpdateRecommendationAsync(int recommendationId, EeuRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<EeuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<EeuProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<EeuProgramDetailsDto>> GetByStatusAsync(
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

        /// <summary>
        /// Get programs by trainer ID with optional unit location filter
        /// </summary>
        Task<PaginatedResult<EeuProgramDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}