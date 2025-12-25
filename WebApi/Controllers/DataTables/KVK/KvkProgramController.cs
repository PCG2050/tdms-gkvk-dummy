// WebApi/Controllers/DataTables/KVK/KvkProgramController.cs
using Application.Models.DataTables.KVK;
using Application.Interface.Services.DataTables.KVK;

namespace WebApi.Controllers.DataTables.KVK
{
    [Authorize]
    [Route("api/kvk/[controller]")]
    [ApiController]
    public class KvkProgramController : ControllerBase
    {
        private readonly IKvkProgramService _service;

        public KvkProgramController(IKvkProgramService service)
        {
            _service = service;
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        /// <summary>
        /// Create a new KVK program entry
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProgram([FromBody] KvkProgramCreateDto dto)
        {
            var result = await _service.CreateProgramAsync(dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get program by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgram(int id)
        {
            var result = await _service.GetProgramByIdAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get complete program with all sections (conditional loading based on CategoryId)
        /// - CategoryId 18 or 24: Loads Results with FLD/OFT data
        /// - Other categories: Loads Reports
        /// </summary>
        [HttpGet("{id}/complete")]
        public async Task<IActionResult> GetCompleteProgram(int id)
        {
            var result = await _service.GetCompleteProgramAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update program details
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] KvkProgramUpdateDto dto)
        {
            var result = await _service.UpdateProgramAsync(id, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete program
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var result = await _service.DeleteProgramAsync(id);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION B: DEMOGRAPHICS
        // ============================

        /// <summary>
        /// Add demographic entry to program
        /// </summary>
        [HttpPost("{programId}/demographics")]
        public async Task<IActionResult> AddDemographics(int programId, [FromBody] KvkParticipantDemographicsCreateDto dto)
        {
            var result = await _service.AddDemographicsAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update demographic entry
        /// </summary>
        [HttpPut("demographics/{demographicsId}")]
        public async Task<IActionResult> UpdateDemographics(int demographicsId, [FromBody] KvkParticipantDemographicsUpdateDto dto)
        {
            var result = await _service.UpdateDemographicsAsync(demographicsId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete demographic entry
        /// </summary>
        [HttpDelete("demographics/{demographicsId}")]
        public async Task<IActionResult> DeleteDemographics(int demographicsId)
        {
            var result = await _service.DeleteDemographicsAsync(demographicsId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get all demographics for a program
        /// </summary>
        [HttpGet("{programId}/demographics")]
        public async Task<IActionResult> GetDemographics(int programId)
        {
            var result = await _service.GetDemographicsByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Add multiple participant demographics entries with hybrid pattern in a single transaction
        /// This endpoint solves the problem of needing parent ID before creating children by handling everything in a single transaction
        /// Perfect for "Save & Next" button - handles all demographics in one call
        /// </summary>
        /// <remarks>
        /// Sample request (creates 3 demographics entries):
        ///
        ///     POST /api/kvk/program/5/demographics-with-children
        ///     {
        ///       "demographics": [
        ///         {
        ///           "participantId": 1,
        ///           "male_SC": 10,
        ///           "male_ST": 5,
        ///           "female_SC": 8,
        ///           "female_ST": 6,
        ///           "total": 29
        ///         },
        ///         {
        ///           "participantId": 2,
        ///           "male_OBC": 15,
        ///           "male_GEN": 20,
        ///           "female_OBC": 12,
        ///           "female_GEN": 18,
        ///           "total": 65
        ///         },
        ///         {
        ///           "participantId": 3,
        ///           "male_SC": 5,
        ///           "female_SC": 5,
        ///           "total": 10
        ///         }
        ///       ]
        ///     }
        ///
        /// All entries are created in a single transaction
        /// </remarks>
        [HttpPost("{programId}/demographics-with-children")]
        public async Task<IActionResult> AddDemographicsWithChildren(int programId, [FromBody] KvkDemographicsWithChildrenCreateDto dto)
        {
            var result = await _service.AddDemographicsWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update all participant demographics for a program using Hybrid Pattern (perfect for "Save & Next" button)
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in request list: DELETE
        /// All changes happen in a single transaction with automatic rollback on failure
        /// </summary>
        /// <remarks>
        /// Sample request (demonstrates CREATE, UPDATE, DELETE):
        ///
        ///     PUT /api/kvk/program/5/demographics-with-children
        ///     {
        ///       "demographics": [
        ///         {
        ///           "id": 10,
        ///           "participantId": 1,
        ///           "male_SC": 12,
        ///           "female_SC": 10,
        ///           "total": 22
        ///         },
        ///         {
        ///           "participantId": 2,
        ///           "male_OBC": 20,
        ///           "female_OBC": 15,
        ///           "total": 35
        ///         }
        ///       ]
        ///     }
        ///
        /// In this example:
        /// - Entry with id=10 will be UPDATED
        /// - Entry without id (participantId=2) will be CREATED
        /// - Any existing entries not in the list will be DELETED
        /// </remarks>
        [HttpPut("{programId}/demographics-with-children")]
        public async Task<IActionResult> UpdateDemographicsWithChildren(int programId, [FromBody] KvkDemographicsWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateDemographicsWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Add program content entry with all child entities (ResourcePersons, Topics, TeachingAids) in one request
        /// This endpoint solves the problem of needing parent ID before creating children by handling everything in a single transaction
        /// </summary>
        [HttpPost("{programId}/content-with-children")]
        public async Task<IActionResult> AddProgramContentWithChildren(int programId, [FromBody] KvkProgramContentWithChildrenCreateDto dto)
        {
            var result = await _service.AddProgramContentWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update program content with all child entities using Hybrid Pattern (perfect for "Save & Next" button)
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in request: DELETE
        /// All changes happen in a single transaction with automatic rollback on failure
        /// </summary>
        [HttpPut("content/{contentId}/with-children")]
        public async Task<IActionResult> UpdateProgramContentWithChildren(int contentId, [FromBody] KvkProgramContentWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateProgramContentWithChildrenAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get program content by ID
        /// </summary>
        [HttpGet("content/{contentId}")]
        public async Task<IActionResult> GetProgramContent(int contentId)
        {
            var result = await _service.GetProgramContentByIdAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete program content
        /// </summary>
        [HttpDelete("content/{contentId}")]
        public async Task<IActionResult> DeleteProgramContent(int contentId)
        {
            var result = await _service.DeleteProgramContentAsync(contentId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get all content entries for a program
        /// </summary>
        [HttpGet("{programId}/content")]
        public async Task<IActionResult> GetProgramContents(int programId)
        {
            var result = await _service.GetProgramContentsByProgramIdAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        /// <summary>
        /// Add or update advisory services
        /// </summary>
        [HttpPost("{programId}/advisory-services")]
        public async Task<IActionResult> AddOrUpdateAdvisoryServices(int programId, [FromBody] KvkAdvisoryServicesCreateDto dto)
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

        /// <summary>
        /// Add advisory services with all child entities (CriticalInputsDistributed) in one request
        /// This endpoint solves the problem of needing parent ID before creating children by handling everything in a single transaction
        /// </summary>
        [HttpPost("{programId}/advisory-services-with-children")]
        public async Task<IActionResult> AddAdvisoryServicesWithChildren(int programId, [FromBody] KvkAdvisoryServicesHybridCreateDto dto)
        {
            var result = await _service.AddAdvisoryServicesWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update advisory services with all child entities using Hybrid Pattern (perfect for "Save & Next" button)
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in request: DELETE
        /// All changes happen in a single transaction with automatic rollback on failure
        /// </summary>
        [HttpPut("advisory-services/{advisoryServicesId}/with-children")]
        public async Task<IActionResult> UpdateAdvisoryServicesWithChildren(int advisoryServicesId, [FromBody] KvkAdvisoryServicesHybridUpdateDto dto)
        {
            var result = await _service.UpdateAdvisoryServicesWithChildrenAsync(advisoryServicesId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24 ONLY)
        // ============================

        /// <summary>
        /// Get result by ID
        /// </summary>
        [HttpGet("results/{resultId}")]
        public async Task<IActionResult> GetResult(int resultId)
        {
            var result = await _service.GetResultByIdAsync(resultId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update Excel URL for results
        /// </summary>
        [HttpPut("results/{resultId}/excel")]
        public async Task<IActionResult> UpdateResultExcel(int resultId, [FromBody] string excelUrl)
        {
            var result = await _service.UpdateResultExcelAsync(resultId, excelUrl);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // E3: COMPOSITE CREATE/UPDATE FOR RESULTS WITH CHILDREN
        // ============================

        /// <summary>
        /// Create KvkResult with all child entities (FldResults and OftResults) in a single transaction
        /// Solves the parent-child ID dependency - perfect for "Save & Next" button
        /// </summary>
        [HttpPost("{programId}/results-with-children")]
        public async Task<IActionResult> CreateResultWithChildren(
            int programId,
            [FromBody] KvkResultWithChildrenCreateDto dto)
        {
            var result = await _service.CreateResultWithChildrenAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update KvkResult with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        [HttpPut("results/{resultId}/with-children")]
        public async Task<IActionResult> UpdateResultWithChildren(
            int resultId,
            [FromBody] KvkResultWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateResultWithChildrenAsync(resultId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================

        /// <summary>
        /// Add or update report (for non-FLD/OFT categories)
        /// </summary>
        [HttpPost("{programId}/reports")]
        public async Task<IActionResult> AddOrUpdateReport(int programId, [FromBody] KvkReportCreateDto dto)
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
        // SECTION G: RECOMMENDATIONS
        // ============================

        /// <summary>
        /// Add or update recommendation
        /// </summary>
        [HttpPost("{programId}/recommendations")]
        public async Task<IActionResult> AddOrUpdateRecommendation(int programId, [FromBody] KvkRecommendationCreateDto dto)
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

        /// <summary>
        /// Submit program for approval (Trainer) - changes status from Draft/Rejected to Pending
        /// </summary>
        // [HttpPost("{programId}/submit")]
        // public async Task<IActionResult> SubmitForApproval(int programId)
        // {
        //     var result = await _service.SubmitForApprovalAsync(programId);
        //     return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        // }

        /// <summary>
        /// Approve program (UnitHead/Admin) - changes status from Pending to Approved
        /// </summary>
        [HttpPost("{programId}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Approve(int programId, [FromBody] ApprovalDto dto)
        {
            var result = await _service.ApproveAsync(programId, dto?.Remarks);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Reject program (UnitHead/Admin) - changes status from Pending to Rejected
        /// </summary>
        [HttpPost("{programId}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
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

        /// <summary>
        /// Search and filter KVK programs with pagination (Admin/UnitHead only)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? searchTerm = null,
            [FromQuery] string? formStatus = null,
            [FromQuery] int? createdById = null,
            [FromQuery] int? unitLocationId = null)
        {
            var result = await _service.GetPaginatedAsync(
                pageNumber, pageSize, startDate, endDate, categoryId,
                searchTerm, formStatus, createdById, unitLocationId);
            return Ok(result);
        }

        /// <summary>
        /// Get programs by status (Admin/UnitHead only)
        /// </summary>
        [HttpGet("status/{status}")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetByStatus(
            string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get summary of programs by status (Admin/UnitHead only)
        /// </summary>
        [HttpGet("status-summary")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetStatusSummary()
        {
            var result = await _service.GetStatusSummaryAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get trainer's own program history with pagination
        /// </summary>
        [HttpGet("my-history")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetMyHistory(
      [FromQuery] int pageNumber = 1,
      [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get pending approvals for Unit Head and Admin with pagination
        /// </summary>
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
                ServiceErrorStatus.INVALIDOPERATION => 400,
                ServiceErrorStatus.UNAUTHORIZED => 401,
                _ => 500
            };
        }
    }

    // ============================
    // DTOs FOR STATUS MANAGEMENT
    // ============================

    public class ApprovalDto
    {
        public string? Remarks { get; set; }
    }

    public class RejectionDto
    {
        public string Remarks { get; set; } = string.Empty;
    }
}