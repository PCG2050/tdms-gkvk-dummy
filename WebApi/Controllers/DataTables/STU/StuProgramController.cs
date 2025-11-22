// StuProgramController.cs
using Application.Interface.Services.DataTables.STU;
using Application.Models.DataTables.STU;


namespace WebApi.Controllers.DataTables.STU
{
    [Authorize]
    [Route("api/stu/[controller]")]
    [ApiController]
    public class StuProgramController : ControllerBase
    {
        private readonly IStuProgramService _service;

        public StuProgramController(IStuProgramService service)
        {
            _service = service;
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] StuProgramCreateDto dto)
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
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] StuProgramUpdateDto dto)
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
        public async Task<IActionResult> AddDemographics(int programId, [FromBody] StuParticipantDemographicsCreateDto dto)
        {
            var result = await _service.AddDemographicsAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("demographics/{demographicsId}")]
        public async Task<IActionResult> UpdateDemographics(int demographicsId, [FromBody] StuParticipantDemographicsUpdateDto dto)
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
        public async Task<IActionResult> AddProgramContent(int programId, [FromBody] StuProgramContentCreateDto dto)
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

        /// <summary>
        /// Creates program content with all children in one transaction (RECOMMENDED)
        /// </summary>
        [HttpPost("{programId}/content-with-children")]
        public async Task<IActionResult> AddProgramContentWithChildren(int programId, [FromBody] StuProgramContentWithChildrenCreateDto dto)
        {
            var result = await _service.AddProgramContentWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Updates program content with all children using hybrid pattern (RECOMMENDED)
        /// </summary>
        [HttpPut("content/{contentId}/with-children")]
        public async Task<IActionResult> UpdateProgramContentWithChildren(int contentId, [FromBody] StuProgramContentWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateProgramContentWithChildrenAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        /// <summary>
        /// Add or update advisory services
        /// </summary>
        [HttpPost("{programId}/advisory-services")]
        public async Task<IActionResult> AddOrUpdateAdvisoryServices(int programId, [FromBody] StuAdvisoryServicesCreateDto dto)
        {
            var result = await _service.AddOrUpdateAdvisoryServicesAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get advisory services for a program
        /// </summary>
        [HttpGet("{programId}/advisory-services")]
        public async Task<IActionResult> GetAdvisoryServices(int programId)
        {
            var result = await _service.GetAdvisoryServicesByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION E: REPORTS
        // ============================

        /// <summary>
        /// Add or update report
        /// </summary>
        [HttpPost("{programId}/reports")]
        public async Task<IActionResult> AddOrUpdateReport(int programId, [FromBody] StuReportCreateDto dto)
        {
            var result = await _service.AddOrUpdateReportAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get report for a program
        /// </summary>
        [HttpGet("{programId}/reports")]
        public async Task<IActionResult> GetReport(int programId)
        {
            var result = await _service.GetReportByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION F: RECOMMENDATIONS
        // ============================

        /// <summary>
        /// Add or update recommendation
        /// </summary>
        [HttpPost("{programId}/recommendations")]
        public async Task<IActionResult> AddOrUpdateRecommendation(int programId, [FromBody] StuRecommendationCreateDto dto)
        {
            var result = await _service.AddOrUpdateRecommendationAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get recommendation for a program
        /// </summary>
        [HttpGet("{programId}/recommendations")]
        public async Task<IActionResult> GetRecommendation(int programId)
        {
            var result = await _service.GetRecommendationByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // STATUS MANAGEMENT
        // ============================

        // [HttpPost("{programId}/submit")]
        // public async Task<IActionResult> SubmitForApproval(int programId)
        // {
        //     var result = await _service.SubmitForApprovalAsync(programId);
        //     return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        // }

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