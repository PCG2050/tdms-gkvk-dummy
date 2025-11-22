// Application/Interface/Services/DataTables/IStuProgramService.cs

using Application.Interface.Services.Common;
using Application.Models;
using Application.Models.DataTables.STU;

namespace Application.Interface.Services.DataTables.STU
{
    /// <summary>
    /// Service interface for STU Program management using HYBRID PATTERN.
    ///
    /// HYBRID PATTERN:
    /// - Extends IBaseReportEntryService for main entity CRUD operations
    /// - Adds specific methods for nested entities (Demographics, Content, ResourcePersons, etc.)
    ///
    /// INHERITED METHODS FROM BASE (for main entity - StuProgramDetails):
    /// - CreateAsync(StuProgramCreateDto dto) - Create new program
    /// - GetByIdAsync(int id) - Get program by ID
    /// - UpdateAsync(int id, StuProgramUpdateDto dto) - Update program
    /// - DeleteAsync(int id) - Delete program
    /// - SubmitForApprovalAsync(int id) - Submit for approval
    /// - ApproveAsync(int id, string? remarks) - Approve program
    /// - RejectAsync(int id, string remarks) - Reject program
    /// - GetPaginatedAsync(...) - Get paginated programs
    /// - GetByStatusAsync(string status, ...) - Get programs by status
    /// - GetStatusSummaryAsync() - Get status summary
    /// </summary>
    public interface IStuProgramService : IBaseReportEntryService<StuProgramDetailsDto, StuProgramCreateDto, StuProgramUpdateDto>
    {
        // ============================
        // MAIN ENTITY - ADDITIONAL METHODS
        // ============================

        /// <summary>
        /// Gets complete program with all nested entities loaded
        /// </summary>
        Task<ServiceResult<StuProgramDetailsCompleteDto>> GetCompleteProgramAsync(int id);

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
        // STATUS MANAGEMENT & SUBMISSION - Inherited from IBaseReportEntryService
        // ============================
        // Task<ServiceResult> SubmitForApprovalAsync(int id);
        // Task<ServiceResult> ApproveAsync(int id, string? remarks);
        // Task<ServiceResult> RejectAsync(int id, string remarks);

        // ============================
        // PAGINATION & FILTERING - Inherited from IBaseReportEntryService
        // ============================
        // Task<PaginatedResult<StuProgramDetailsDto>> GetPaginatedAsync(...);
        // Task<PaginatedResult<StuProgramDetailsDto>> GetByStatusAsync(...);
        // Task<Dictionary<string, int>> GetStatusSummaryAsync();
    }
}
