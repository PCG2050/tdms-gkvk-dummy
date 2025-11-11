// IbtvaProgramController.cs
using Application.Models.DataTables.IBTVA;


namespace WebApi.Controllers.DataTables.IBTVA
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IbtvaProgramController : ControllerBase
    {
        private readonly IIbtvaProgramService _service;

        public IbtvaProgramController(IIbtvaProgramService service)
        {
            _service = service;
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] IbtvaProgramCreateDto dto)
        {
            var result = await _service.CreateProgramAsync(dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgram(int id)
        {
            var result = await _service.GetProgramByIdAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{id}/complete")]
        public async Task<IActionResult> GetCompleteProgram(int id)
        {
            var result = await _service.GetCompleteProgramAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] IbtvaProgramUpdateDto dto)
        {
            var result = await _service.UpdateProgramAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var result = await _service.DeleteProgramAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION B: DEMOGRAPHICS
        // ============================

        [HttpPost("{programId}/demographics")]
        public async Task<IActionResult> AddDemographics(int programId, [FromBody] IbtvaParticipantDemographicsCreateDto dto)
        {
            var result = await _service.AddDemographicsAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("demographics/{demographicsId}")]
        public async Task<IActionResult> UpdateDemographics(int demographicsId, [FromBody] IbtvaParticipantDemographicsUpdateDto dto)
        {
            var result = await _service.UpdateDemographicsAsync(demographicsId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("demographics/{demographicsId}")]
        public async Task<IActionResult> DeleteDemographics(int demographicsId)
        {
            var result = await _service.DeleteDemographicsAsync(demographicsId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{programId}/demographics")]
        public async Task<IActionResult> GetDemographics(int programId)
        {
            var result = await _service.GetDemographicsByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT
        // ============================

        [HttpPost("{programId}/content")]
        public async Task<IActionResult> AddProgramContent(int programId, [FromBody] IbtvaProgramContentCreateDto dto)
        {
            var result = await _service.AddProgramContentAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("content/{contentId}")]
        public async Task<IActionResult> GetProgramContent(int contentId)
        {
            var result = await _service.GetProgramContentByIdAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("content/{contentId}")]
        public async Task<IActionResult> DeleteProgramContent(int contentId)
        {
            var result = await _service.DeleteProgramContentAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // C1: Resource Persons
        [HttpPost("content/{contentId}/resource-persons")]
        public async Task<IActionResult> AddResourcePerson(int contentId, [FromBody] IbtvaResourcePersonCreateDto dto)
        {
            var result = await _service.AddResourcePersonAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("resource-persons/{personId}")]
        public async Task<IActionResult> UpdateResourcePerson(int personId, [FromBody] IbtvaResourcePersonUpdateDto dto)
        {
            var result = await _service.UpdateResourcePersonAsync(personId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("resource-persons/{personId}")]
        public async Task<IActionResult> DeleteResourcePerson(int personId)
        {
            var result = await _service.DeleteResourcePersonAsync(personId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("content/{contentId}/resource-persons")]
        public async Task<IActionResult> GetResourcePersons(int contentId)
        {
            var result = await _service.GetResourcePersonsByContentIdAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // C2: Topics Covered
        [HttpPost("content/{contentId}/topics")]
        public async Task<IActionResult> AddTopic(int contentId, [FromBody] IbtvaTopicsCoveredCreateDto dto)
        {
            var result = await _service.AddTopicAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("topics/{topicId}")]
        public async Task<IActionResult> UpdateTopic(int topicId, [FromBody] IbtvaTopicsCoveredUpdateDto dto)
        {
            var result = await _service.UpdateTopicAsync(topicId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("topics/{topicId}")]
        public async Task<IActionResult> DeleteTopic(int topicId)
        {
            var result = await _service.DeleteTopicAsync(topicId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("content/{contentId}/topics")]
        public async Task<IActionResult> GetTopics(int contentId)
        {
            var result = await _service.GetTopicsByContentIdAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // C3: Teaching Aids
        [HttpPost("content/{contentId}/teaching-aids")]
        public async Task<IActionResult> AddTeachingAid(int contentId, [FromBody] IbtvaTeachingAidsCreateDto dto)
        {
            var result = await _service.AddTeachingAidAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("teaching-aids/{aidId}")]
        public async Task<IActionResult> UpdateTeachingAid(int aidId, [FromBody] IbtvaTeachingAidsUpdateDto dto)
        {
            var result = await _service.UpdateTeachingAidAsync(aidId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("teaching-aids/{aidId}")]
        public async Task<IActionResult> DeleteTeachingAid(int aidId)
        {
            var result = await _service.DeleteTeachingAidAsync(aidId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("content/{contentId}/teaching-aids")]
        public async Task<IActionResult> GetTeachingAids(int contentId)
        {
            var result = await _service.GetTeachingAidsByContentIdAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        [HttpPost("{programId}/advisory-services")]
        public async Task<IActionResult> AddAdvisoryServices(int programId, [FromBody] IbtvaAdvisoryServicesCreateDto dto)
        {
            var result = await _service.AddAdvisoryServicesAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("advisory-services/{advisoryId}")]
        public async Task<IActionResult> UpdateAdvisoryServices(int advisoryId, [FromBody] IbtvaAdvisoryServicesUpdateDto dto)
        {
            var result = await _service.UpdateAdvisoryServicesAsync(advisoryId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("advisory-services/{advisoryId}")]
        public async Task<IActionResult> DeleteAdvisoryServices(int advisoryId)
        {
            var result = await _service.DeleteAdvisoryServicesAsync(advisoryId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{programId}/advisory-services")]
        public async Task<IActionResult> GetAdvisoryServices(int programId)
        {
            var result = await _service.GetAdvisoryServicesByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION E: REPORTS
        // ============================

        [HttpPost("{programId}/reports")]
        public async Task<IActionResult> AddReport(int programId, [FromBody] IbtvaReportCreateDto dto)
        {
            var result = await _service.AddReportAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("reports/{reportId}")]
        public async Task<IActionResult> UpdateReport(int reportId, [FromBody] IbtvaReportUpdateDto dto)
        {
            var result = await _service.UpdateReportAsync(reportId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("reports/{reportId}")]
        public async Task<IActionResult> DeleteReport(int reportId)
        {
            var result = await _service.DeleteReportAsync(reportId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{programId}/reports")]
        public async Task<IActionResult> GetReport(int programId)
        {
            var result = await _service.GetReportByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================

        [HttpPost("{programId}/recommendations")]
        public async Task<IActionResult> AddRecommendation(int programId, [FromBody] IbtvaRecommendationCreateDto dto)
        {
            var result = await _service.AddRecommendationAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("recommendations/{recommendationId}")]
        public async Task<IActionResult> UpdateRecommendation(int recommendationId, [FromBody] IbtvaRecommendationUpdateDto dto)
        {
            var result = await _service.UpdateRecommendationAsync(recommendationId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpDelete("recommendations/{recommendationId}")]
        public async Task<IActionResult> DeleteRecommendation(int recommendationId)
        {
            var result = await _service.DeleteRecommendationAsync(recommendationId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpGet("{programId}/recommendations")]
        public async Task<IActionResult> GetRecommendation(int programId)
        {
            var result = await _service.GetRecommendationByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // STATUS MANAGEMENT
        // ============================

        [HttpPost("{programId}/submit")]
        public async Task<IActionResult> SubmitForApproval(int programId)
        {
            var result = await _service.SubmitForApprovalAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPost("{programId}/approve")]
        public async Task<IActionResult> Approve(int programId, [FromBody] ApprovalDto dto)
        {
            var result = await _service.ApproveAsync(programId, dto?.Remarks);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPost("{programId}/reject")]
        public async Task<IActionResult> Reject(int programId, [FromBody] RejectionDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _service.RejectAsync(programId, dto.Remarks);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // LISTING & FILTERING
        // ============================

        [HttpGet]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int? programTypeId = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? unitLocationId = null)
        {
            var result = await _service.GetPaginatedAsync(
                pageNumber, pageSize, startDate, endDate, programTypeId, searchTerm, unitLocationId);
            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(
            string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("status-summary")]
        public async Task<IActionResult> GetStatusSummary()
        {
            var result = await _service.GetStatusSummaryAsync();
            return Ok(result);
        }

        [HttpGet("my-history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetMyHistory(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("pending-approvals")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingApprovals(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetPendingApprovalsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // ============================
        // HELPER METHOD
        // ============================

        private int GetStatusCode(ServiceErrorStatus? errorStatus)
        {
            return errorStatus switch
            {
                ServiceErrorStatus.NOTFOUND => 404,
                ServiceErrorStatus.FORBIDDEN => 403,
                ServiceErrorStatus.BADREQUEST => 400,
                ServiceErrorStatus.INVALIDOPERATION => 400,
                ServiceErrorStatus.CONFLICT => 409,
                _ => StatusCodes.Status500InternalServerError    
            };
        }
    }

    // DTOs for approval/rejection
    public class ApprovalDto
    {
        public string? Remarks { get; set; }
    }

    public class RejectionDto
    {
        public string Remarks { get; set; } = string.Empty;
    }
}