// Application/Interface/Services/DataTables/IStuProgramService.cs

using Application.Models.DataTables.DEU;

namespace Application.Interface.Services.DataTables.STU
{
    public interface IStuProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<StuProgramDetailsDto>> CreateProgramAsync(StuProgramCreateDto dto);
        Task<ServiceResult<StuProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<StuProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);
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
        Task<ServiceResult<StuProgramContentDto>> AddProgramContentAsync(int programId, StuProgramContentCreateDto dto);
        Task<ServiceResult<StuProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<StuProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // ============================
        // SECTION C1: RESOURCE PERSONS
        // ============================
        Task<ServiceResult<StuResourcePersonDto>> AddResourcePersonAsync(int contentId, StuResourcePersonCreateDto dto);
        Task<ServiceResult<StuResourcePersonDto>> UpdateResourcePersonAsync(int personId, StuResourcePersonUpdateDto dto);
        Task<ServiceResult> DeleteResourcePersonAsync(int personId);
        Task<ServiceResult<List<StuResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId);

        // ============================
        // SECTION C2: TOPICS COVERED
        // ============================
        Task<ServiceResult<StuTopicsCoveredDto>> AddTopicAsync(int contentId, StuTopicsCoveredCreateDto dto);
        Task<ServiceResult<StuTopicsCoveredDto>> UpdateTopicAsync(int topicId, StuTopicsCoveredUpdateDto dto);
        Task<ServiceResult> DeleteTopicAsync(int topicId);
        Task<ServiceResult<List<StuTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId);

        // ============================
        // SECTION C3: TEACHING AIDS
        // ============================
        Task<ServiceResult<StuTeachingAidsDto>> AddTeachingAidAsync(int contentId, StuTeachingAidsCreateDto dto);
        Task<ServiceResult<StuTeachingAidsDto>> UpdateTeachingAidAsync(int aidId, StuTeachingAidsUpdateDto dto);
        Task<ServiceResult> DeleteTeachingAidAsync(int aidId);
        Task<ServiceResult<List<StuTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<StuAdvisoryServicesDto>> AddAdvisoryServicesAsync(int programId, StuAdvisoryServicesCreateDto dto);
        Task<ServiceResult<StuAdvisoryServicesDto>> UpdateAdvisoryServicesAsync(int advisoryId, StuAdvisoryServicesUpdateDto dto);
        Task<ServiceResult> DeleteAdvisoryServicesAsync(int advisoryId);
        Task<ServiceResult<StuAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: REPORTS
        // ============================
        Task<ServiceResult<StuReportDto>> AddReportAsync(int programId, StuReportCreateDto dto);
        Task<ServiceResult<StuReportDto>> UpdateReportAsync(int reportId, StuReportUpdateDto dto);
        Task<ServiceResult> DeleteReportAsync(int reportId);
        Task<ServiceResult<StuReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<StuRecommendationDto>> AddRecommendationAsync(int programId, StuRecommendationCreateDto dto);
        Task<ServiceResult<StuRecommendationDto>> UpdateRecommendationAsync(int recommendationId, StuRecommendationUpdateDto dto);
        Task<ServiceResult> DeleteRecommendationAsync(int recommendationId);
        Task<ServiceResult<StuRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

        // ============================
        // STATUS MANAGEMENT & SUBMISSION
        // ============================
        Task<ServiceResult> SubmitForApprovalAsync(int programId);
        Task<ServiceResult> ApproveAsync(int programId, string? remarks = null);
        Task<ServiceResult> RejectAsync(int programId, string remarks);

        // ============================
        // PAGINATION & FILTERING
        // ============================
        Task<PaginatedResult<StuProgramDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null,
            int? unitLocationId = null);

        Task<PaginatedResult<StuProgramDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync();

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        Task<PaginatedResult<StuProgramDetailsDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10);
    }
}