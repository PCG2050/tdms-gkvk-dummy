// Application/Interface/Services/DataTables/IIbtvaProgramService.cs
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
        Task<ServiceResult<IbtvaProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
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
        Task<ServiceResult<IbtvaProgramContentDto>> AddProgramContentAsync(int programId, IbtvaProgramContentCreateDto dto);
        Task<ServiceResult<IbtvaProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<IbtvaProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // Hybrid pattern methods for bulk create/update operations (RECOMMENDED)
        Task<ServiceResult<IbtvaProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, IbtvaProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<IbtvaProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, IbtvaProgramContentWithChildrenUpdateDto dto);

        // ============================
        // SECTION C1-C3: RESOURCE PERSONS, TOPICS, TEACHING AIDS
        // Individual CRUD methods REMOVED - Use hybrid endpoints instead
        // ============================

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<IbtvaAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, IbtvaAdvisoryServicesCreateDto dto);
        Task<ServiceResult<IbtvaAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, IbtvaAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<IbtvaAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<IbtvaReportDto>> AddReportAsync(int programId, IbtvaReportCreateDto dto);
        Task<ServiceResult<IbtvaReportDto>> UpdateReportAsync(int reportId, IbtvaReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<IbtvaReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<IbtvaRecommendationDto>> AddRecommendationAsync(int programId, IbtvaRecommendationCreateDto dto);
        Task<ServiceResult<IbtvaRecommendationDto>> UpdateRecommendationAsync(int recommendationId, IbtvaRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<IbtvaRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<IbtvaProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<IbtvaProgramDetailsDto>> GetByStatusAsync(
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
        Task<PaginatedResult<IbtvaProgramDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        Task<PaginatedResult<IbtvaProgramDetailsDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}