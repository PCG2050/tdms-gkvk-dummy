// Application/Interface/Services/DataTables/IDeuProgramService.cs

using Application.Models.DataTables.DEU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.DEU
{
    public interface IDeuProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<DeuProgramDetailsDto>> CreateProgramAsync(DeuProgramCreateDto dto);
        Task<ServiceResult<DeuProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<DeuProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<DeuProgramDetailsDto>> UpdateProgramAsync(int id, DeuProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<DeuParticipantDemographicsDto>> AddDemographicsAsync(int programId, DeuParticipantDemographicsCreateDto dto);
        Task<ServiceResult<DeuParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, DeuParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<DeuParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================
        Task<ServiceResult<DeuProgramContentDto>> AddProgramContentAsync(int programId, DeuProgramContentCreateDto dto);
        Task<ServiceResult<DeuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<DeuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================
        Task<ServiceResult<DeuResourcePersonDto>> AddResourcePersonAsync(int contentId, DeuResourcePersonCreateDto dto);
        Task<ServiceResult<DeuResourcePersonDto>> UpdateResourcePersonAsync(int personId, DeuResourcePersonUpdateDto dto);
        Task<ServiceResult> DeleteResourcePersonAsync(int personId);
        Task<ServiceResult<List<DeuResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId);

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================
        Task<ServiceResult<DeuTopicsCoveredDto>> AddTopicAsync(int contentId, DeuTopicsCoveredCreateDto dto);
        Task<ServiceResult<DeuTopicsCoveredDto>> UpdateTopicAsync(int topicId, DeuTopicsCoveredUpdateDto dto);
        Task<ServiceResult> DeleteTopicAsync(int topicId);
        Task<ServiceResult<List<DeuTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId);

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================
        Task<ServiceResult<DeuTeachingAidsDto>> AddTeachingAidAsync(int contentId, DeuTeachingAidsCreateDto dto);
        Task<ServiceResult<DeuTeachingAidsDto>> UpdateTeachingAidAsync(int aidId, DeuTeachingAidsUpdateDto dto);
        Task<ServiceResult> DeleteTeachingAidAsync(int aidId);
        Task<ServiceResult<List<DeuTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<DeuAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, DeuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<DeuAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, DeuAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<DeuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<DeuReportDto>> AddReportAsync(int programId, DeuReportCreateDto dto);
        Task<ServiceResult<DeuReportDto>> UpdateReportAsync(int reportId, DeuReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<DeuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<DeuRecommendationDto>> AddRecommendationAsync(int programId, DeuRecommendationCreateDto dto);
        Task<ServiceResult<DeuRecommendationDto>> UpdateRecommendationAsync(int recommendationId, DeuRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<DeuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<DeuProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<DeuProgramDetailsDto>> GetByStatusAsync(
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