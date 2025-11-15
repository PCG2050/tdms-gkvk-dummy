// FTIProgramController.cs
using Application.Interface.Services.DataTables.FTI;
using Application.Models.DataTables.FTI;


namespace WebApi.Controllers.DataTables.FTI
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FTIProgramController : ControllerBase
    {
        private readonly IFTIService _service;

        public FTIProgramController(IFTIService service)
        {
            _service = service;
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        /// <summary>
        /// Creates a new FTI program
        /// </summary>
        /// <remarks>
        /// Developer Notes:
        /// Use these endpoints for current KVK module development:
        /// - POST /api/FTIProgram (create program)
        /// - PUT /api/FTIProgram/{id} (update program)
        /// - GET /api/FTIProgram/{id} (get single program)
        /// - GET /api/FTIProgram/{id}/complete (get program with all related data)
        /// - DELETE /api/FTIProgram/{id} (delete program)
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] FtiProgramCreateDto dto)
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
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] FtiProgramUpdateDto dto)
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
        public async Task<IActionResult> AddDemographics(int programId, [FromBody] FtiParticipantDemographicsCreateDto dto)
        {
            var result = await _service.AddDemographicsAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("demographics/{demographicsId}")]
        public async Task<IActionResult> UpdateDemographics(int demographicsId, [FromBody] FtiParticipantDemographicsUpdateDto dto)
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

        /// <summary>
        /// Adds program content (deprecated - use hybrid endpoint instead)
        /// </summary>
        /// <remarks>
        /// Developer Notes:
        /// For current KVK module development, use HYBRID PATTERN endpoints instead:
        /// - POST /api/FTIProgram/{programId}/content-with-children (create content with all children)
        /// - PUT /api/FTIProgram/content/{contentId}/with-children (update content with all children)
        /// - GET /api/FTIProgram/content/{contentId} (get content)
        /// - DELETE /api/FTIProgram/content/{contentId} (delete content)
        ///
        /// Avoid using individual child endpoints (C1, C2, C3) - they are deprecated.
        /// The hybrid endpoints handle all children (Resource Persons, Topics, Teaching Aids) in one transaction.
        /// </remarks>
        [HttpPost("{programId}/content")]
        public async Task<IActionResult> AddProgramContent(int programId, [FromBody] FtiProgramContentCreateDto dto)
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

        // Hybrid pattern endpoints for bulk create/update operations
        /// <summary>
        /// Creates program content with all children in one transaction (RECOMMENDED)
        /// </summary>
        /// <remarks>
        /// This is the RECOMMENDED endpoint for creating program content.
        /// It creates the parent content and all children (Resource Persons, Topics, Teaching Aids) atomically.
        /// Use this instead of individual POST endpoints for content and children.
        /// </remarks>
        [HttpPost("{programId}/content-with-children")]
        public async Task<IActionResult> AddProgramContentWithChildren(int programId, [FromBody] FTIProgramContentWithChildrenCreateDto dto)
        {
            var result = await _service.AddProgramContentWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Updates program content with all children using hybrid pattern (RECOMMENDED)
        /// </summary>
        /// <remarks>
        /// This is the RECOMMENDED endpoint for updating program content.
        /// It uses the hybrid pattern:
        /// - Items WITH Id are UPDATED
        /// - Items WITHOUT Id (null or 0) are CREATED
        /// - Items in DB but NOT in the request are DELETED
        /// All operations are performed in one atomic transaction.
        /// Use this instead of individual PUT/POST/DELETE endpoints for children.
        /// </remarks>
        [HttpPut("content/{contentId}/with-children")]
        public async Task<IActionResult> UpdateProgramContentWithChildren(int contentId, [FromBody] FTIProgramContentWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateProgramContentWithChildrenAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }


        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        [HttpPost("{programId}/advisory-services")]
        public async Task<IActionResult> AddAdvisoryServices(int programId, [FromBody] FtiAdvisoryServicesCreateDto dto)
        {
            var result = await _service.AddAdvisoryServicesAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("advisory-services/{advisoryId}")]
        public async Task<IActionResult> UpdateAdvisoryServices(int advisoryId, [FromBody] FtiAdvisoryServicesUpdateDto dto)
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
        public async Task<IActionResult> AddReport(int programId, [FromBody] FtiReportCreateDto dto)
        {
            var result = await _service.AddReportAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("reports/{reportId}")]
        public async Task<IActionResult> UpdateReport(int reportId, [FromBody] FtiReportUpdateDto dto)
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
        public async Task<IActionResult> AddRecommendation(int programId, [FromBody] FtiRecommendationCreateDto dto)
        {
            var result = await _service.AddRecommendationAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("recommendations/{recommendationId}")]
        public async Task<IActionResult> UpdateRecommendation(int recommendationId, [FromBody] FtiRecommendationUpdateDto dto)
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
