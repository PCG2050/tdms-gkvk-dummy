// Application/Interface/Services/DataTables/IStuProgramService.cs

using Application.Models.DataTables.DEU;
using Application.Services.Common;

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

        // Hybrid pattern methods for bulk create/update operations
        Task<ServiceResult<StuProgramContentDto>> AddProgramContentWithChildrenAsync(int programId, StuProgramContentWithChildrenCreateDto dto);
        Task<ServiceResult<StuProgramContentDto>> UpdateProgramContentWithChildrenAsync(int contentId, StuProgramContentWithChildrenUpdateDto dto);

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
        // REMOVED: SubmitForApprovalAsync - Form submission now happens automatically via recommendations
        // Task<ServiceResult> SubmitForApprovalAsync(int programId);
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
        /// Get trainer's submission history with pagination
        /// </summary>
        Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get pending approvals for Unit Head and Admin with pagination and optional filters
        /// </summary>
        Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? unitLocationId = null,
            int? trainerId = null);

        /// <summary>
        /// Get trainers assigned to a specific unit location (for filtering purposes)
        /// </summary>
        Task<ServiceResult<List<TrainerBasicInfoDto>>> GetTrainersByUnitLocationAsync(int unitLocationId);
    }

    // DTO for basic trainer information
    public class TrainerBasicInfoDto
    {
        public int TrainerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; } = string.Empty;
    }
}