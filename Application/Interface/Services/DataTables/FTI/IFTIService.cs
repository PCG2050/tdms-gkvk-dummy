// Application/Interface/Services/DataTables/IFTIService.cs

using Application.Models.DataTables.FTI;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.FTI
{
    public interface IFTIService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<FTIProgramDetailsDto>> CreateProgramAsync(FTIProgramCreateDto dto);
        Task<ServiceResult<FTIProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<FTIProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<FTIProgramDetailsDto>> UpdateProgramAsync(int id, FTIProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<FTIParticipantDemographicsDto>> AddDemographicsAsync(int programId, FTIParticipantDemographicsCreateDto dto);
        Task<ServiceResult<FTIParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, FTIParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<FTIParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================
        Task<ServiceResult<FTIProgramContentDto>> AddProgramContentAsync(int programId, FTIProgramContentCreateDto dto);
        Task<ServiceResult<FTIProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<FTIProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // Hybrid pattern methods for bulk create/update operations
        Task<ServiceResult<FTIProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, FTIProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<FTIProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, FTIProgramContentWithChildrenUpdateDto dto);


        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<FTIAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, FTIAdvisoryServicesCreateDto dto);
        Task<ServiceResult<FTIAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, FTIAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<FTIAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<FTIReportDto>> AddReportAsync(int programId, FTIReportCreateDto dto);
        Task<ServiceResult<FTIReportDto>> UpdateReportAsync(int reportId, FTIReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<FTIReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<FTIRecommendationDto>> AddRecommendationAsync(int programId, FTIRecommendationCreateDto dto);
        Task<ServiceResult<FTIRecommendationDto>> UpdateRecommendationAsync(int recommendationId, FTIRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<FTIRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<FTIProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<FTIProgramDetailsDto>> GetByStatusAsync(
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
