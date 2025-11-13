
using Application.Models;
using Application.Models.DataTables.KVK;
using Application.Services.Common;

namespace Application.Interface.Services.DataTables.KVK
{
    public interface IKvkProgramService
    {
        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================
        Task<ServiceResult<KvkProgramDetailsDto>> CreateProgramAsync(KvkProgramCreateDto dto);
        Task<ServiceResult<KvkProgramDetailsDto>> GetProgramByIdAsync(int id);
        Task<ServiceResult<KvkProgramCompleteDto>> GetCompleteProgramAsync(int id);
        Task<ServiceResult<KvkProgramDetailsDto>> UpdateProgramAsync(int id, KvkProgramUpdateDto dto);
        Task<ServiceResult> DeleteProgramAsync(int id);

        // ============================
        // SECTION B: PARTICIPANT DEMOGRAPHICS
        // ============================
        Task<ServiceResult<KvkParticipantDemographicsDto>> AddDemographicsAsync(int programId, KvkParticipantDemographicsCreateDto dto);
        Task<ServiceResult<KvkParticipantDemographicsDto>> UpdateDemographicsAsync(int demographicsId, KvkParticipantDemographicsUpdateDto dto);
        Task<ServiceResult> DeleteDemographicsAsync(int demographicsId);
        Task<ServiceResult<List<KvkParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId);

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================
        Task<ServiceResult<KvkProgramContentDto>> AddProgramContentAsync(int programId, KvkProgramContentCreateDto dto);

        /// <summary>
        /// Create KvkProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// </summary>
        Task<ServiceResult<KvkProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, KvkProgramContentWithChildrenCreateDto dto);

        Task<ServiceResult<KvkProgramContentDto>> GetProgramContentByIdAsync(int contentId);
        Task<ServiceResult> DeleteProgramContentAsync(int contentId);
        Task<ServiceResult<List<KvkProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId);

        // C1: Resource Persons
        Task<ServiceResult<KvkResourcePersonDto>> AddResourcePersonAsync(int contentId, KvkResourcePersonCreateDto dto);
        Task<ServiceResult<KvkResourcePersonDto>> UpdateResourcePersonAsync(int personId, KvkResourcePersonUpdateDto dto);
        Task<ServiceResult> DeleteResourcePersonAsync(int personId);
        Task<ServiceResult<List<KvkResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId);

        // C2: Topics Covered
        Task<ServiceResult<KvkTopicsCoveredDto>> AddTopicAsync(int contentId, KvkTopicsCoveredCreateDto dto);
        Task<ServiceResult<KvkTopicsCoveredDto>> UpdateTopicAsync(int topicId, KvkTopicsCoveredUpdateDto dto);
        Task<ServiceResult> DeleteTopicAsync(int topicId);
        Task<ServiceResult<List<KvkTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId);

        // C3: Teaching Aids
        Task<ServiceResult<KvkTeachingAidsDto>> AddTeachingAidAsync(int contentId, KvkTeachingAidsCreateDto dto);
        Task<ServiceResult<KvkTeachingAidsDto>> UpdateTeachingAidAsync(int aidId, KvkTeachingAidsUpdateDto dto);
        Task<ServiceResult> DeleteTeachingAidAsync(int aidId);
        Task<ServiceResult<List<KvkTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId);

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================
        Task<ServiceResult<KvkAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(int programId, KvkAdvisoryServicesCreateDto dto);
        Task<ServiceResult<KvkAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId);

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24)
        // ============================
        Task<ServiceResult<KvkResultDto>> GetOrCreateResultAsync(int programId);
        Task<ServiceResult<KvkResultDto>> GetResultByIdAsync(int resultId);
        Task<ServiceResult> UpdateResultExcelAsync(int resultId, string excelUrl);

        // E1: FLD Results
        Task<ServiceResult<KvkFldResultDto>> AddFldResultAsync(int resultId, KvkFldResultCreateDto dto);
        Task<ServiceResult<KvkFldResultDto>> UpdateFldResultAsync(int fldId, KvkFldResultUpdateDto dto);
        Task<ServiceResult> DeleteFldResultAsync(int fldId);
        Task<ServiceResult<List<KvkFldResultDto>>> GetFldResultsByResultIdAsync(int resultId);

        // E2: OFT Results
        Task<ServiceResult<KvkOftResultDto>> AddOftResultAsync(int resultId, KvkOftResultCreateDto dto);
        Task<ServiceResult<KvkOftResultDto>> UpdateOftResultAsync(int oftId, KvkOftResultUpdateDto dto);
        Task<ServiceResult> DeleteOftResultAsync(int oftId);
        Task<ServiceResult<List<KvkOftResultDto>>> GetOftResultsByResultIdAsync(int resultId);

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================
        Task<ServiceResult<KvkReportDto>> AddOrUpdateReportAsync(int programId, KvkReportCreateDto dto);
        Task<ServiceResult<KvkReportDto>> GetReportByProgramIdAsync(int programId);

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================
        Task<ServiceResult<KvkRecommendationDto>> AddOrUpdateRecommendationAsync(int programId, KvkRecommendationCreateDto dto);
        Task<ServiceResult<KvkRecommendationDto>> GetRecommendationByProgramIdAsync(int programId);

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
