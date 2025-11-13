// Application/Interface/Services/DataTables/IFtiProgramService.cs

using Application.Models.DataTables.DEU;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.FTI
{
    public interface IFtiProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<FtiProgramDetailsDto>> CreateProgramAsync(FtiProgramCreateDto dto);
        Task<ServiceResult<FtiProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<FtiProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<FtiProgramDetailsDto>> UpdateProgramAsync(int id, FtiProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<FtiParticipantDemographicsDto>> AddDemographicsAsync(int programId, FtiParticipantDemographicsCreateDto dto);
        Task<ServiceResult<FtiParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, FtiParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<FtiParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================
        Task<ServiceResult<FtiProgramContentDto>> AddProgramContentAsync(int programId, FtiProgramContentCreateDto dto);
        Task<ServiceResult<FtiProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, FtiProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<FtiProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, FtiProgramContentWithChildrenUpdateDto dto);

        Task<ServiceResult<FtiProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<FtiProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================
        Task<ServiceResult<FtiResourcePersonDto>> AddResourcePersonAsync(int contentId, FtiResourcePersonCreateDto dto);
        Task<ServiceResult<FtiResourcePersonDto>> UpdateResourcePersonAsync(int personId, FtiResourcePersonUpdateDto dto);
        Task<ServiceResult> DeleteResourcePersonAsync(int personId);
        Task<ServiceResult<List<FtiResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId);

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================
        Task<ServiceResult<FtiTopicsCoveredDto>> AddTopicAsync(int contentId, FtiTopicsCoveredCreateDto dto);
        Task<ServiceResult<FtiTopicsCoveredDto>> UpdateTopicAsync(int topicId, FtiTopicsCoveredUpdateDto dto);
        Task<ServiceResult> DeleteTopicAsync(int topicId);
        Task<ServiceResult<List<FtiTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId);

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================
        Task<ServiceResult<FtiTeachingAidsDto>> AddTeachingAidAsync(int contentId, FtiTeachingAidsCreateDto dto);
        Task<ServiceResult<FtiTeachingAidsDto>> UpdateTeachingAidAsync(int aidId, FtiTeachingAidsUpdateDto dto);
        Task<ServiceResult> DeleteTeachingAidAsync(int aidId);
        Task<ServiceResult<List<FtiTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<FtiAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, FtiAdvisoryServicesCreateDto dto);
        Task<ServiceResult<FtiAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, FtiAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<FtiAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<FtiReportDto>> AddReportAsync(int programId, FtiReportCreateDto dto);
        Task<ServiceResult<FtiReportDto>> UpdateReportAsync(int reportId, FtiReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<FtiReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<FtiRecommendationDto>> AddRecommendationAsync(int programId, FtiRecommendationCreateDto dto);
        Task<ServiceResult<FtiRecommendationDto>> UpdateRecommendationAsync(int recommendationId, FtiRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<FtiRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<FtiProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<FtiProgramDetailsDto>> GetByStatusAsync(
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
