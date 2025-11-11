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

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================
        Task<ServiceResult<IbtvaResourcePersonDto>> AddResourcePersonAsync(int contentId, IbtvaResourcePersonCreateDto dto);
        Task<ServiceResult<IbtvaResourcePersonDto>> UpdateResourcePersonAsync(int personId, IbtvaResourcePersonUpdateDto dto);
        Task<ServiceResult> DeleteResourcePersonAsync(int personId);
        Task<ServiceResult<List<IbtvaResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId);

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================
        Task<ServiceResult<IbtvaTopicsCoveredDto>> AddTopicAsync(int contentId, IbtvaTopicsCoveredCreateDto dto);
        Task<ServiceResult<IbtvaTopicsCoveredDto>> UpdateTopicAsync(int topicId, IbtvaTopicsCoveredUpdateDto dto);
        Task<ServiceResult> DeleteTopicAsync(int topicId);
        Task<ServiceResult<List<IbtvaTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId);

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================
        Task<ServiceResult<IbtvaTeachingAidsDto>> AddTeachingAidAsync(int contentId, IbtvaTeachingAidsCreateDto dto);
        Task<ServiceResult<IbtvaTeachingAidsDto>> UpdateTeachingAidAsync(int aidId, IbtvaTeachingAidsUpdateDto dto);
        Task<ServiceResult> DeleteTeachingAidAsync(int aidId);
        Task<ServiceResult<List<IbtvaTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId);

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
    }
}