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

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        /// <summary>
        /// Add program content entry
        /// </summary>
        [HttpPost("{programId}/content")]
        public async Task<IActionResult> AddProgramContent(int programId, [FromBody] KvkProgramContentCreateDto dto)
        {
            var result = await _service.AddProgramContentAsync(programId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

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
        // C1: RESOURCE PERSONS
        // ============================

        [HttpPost("content/{contentId}/resource-persons")]
        public async Task<IActionResult> AddResourcePerson(int contentId, [FromBody] KvkResourcePersonCreateDto dto)
        {
            var result = await _service.AddResourcePersonAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("resource-persons/{personId}")]
        public async Task<IActionResult> UpdateResourcePerson(int personId, [FromBody] KvkResourcePersonUpdateDto dto)
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

        // ============================
        // C2: TOPICS COVERED
        // ============================

        [HttpPost("content/{contentId}/topics")]
        public async Task<IActionResult> AddTopic(int contentId, [FromBody] KvkTopicsCoveredCreateDto dto)
        {
            var result = await _service.AddTopicAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("topics/{topicId}")]
        public async Task<IActionResult> UpdateTopic(int topicId, [FromBody] KvkTopicsCoveredUpdateDto dto)
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

        // ============================
        // C3: TEACHING AIDS
        // ============================

        [HttpPost("content/{contentId}/teaching-aids")]
        public async Task<IActionResult> AddTeachingAid(int contentId, [FromBody] KvkTeachingAidsCreateDto dto)
        {
            var result = await _service.AddTeachingAidAsync(contentId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        [HttpPut("teaching-aids/{aidId}")]
        public async Task<IActionResult> UpdateTeachingAid(int aidId, [FromBody] KvkTeachingAidsUpdateDto dto)
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

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24 ONLY)
        // ============================

        /// <summary>
        /// Get or create Result record (only for CategoryId 18 or 24)
        /// </summary>
        [HttpGet("{programId}/results")]
        public async Task<IActionResult> GetOrCreateResult(int programId)
        {
            var result = await _service.GetOrCreateResultAsync(programId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

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
        // E1: FLD RESULTS
        // ============================

        /// <summary>
        /// Add FLD result entry
        /// </summary>
        [HttpPost("results/{resultId}/fld")]
        public async Task<IActionResult> AddFldResult(int resultId, [FromBody] KvkFldResultCreateDto dto)
        {
            var result = await _service.AddFldResultAsync(resultId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update FLD result
        /// </summary>
        [HttpPut("fld-results/{fldId}")]
        public async Task<IActionResult> UpdateFldResult(int fldId, [FromBody] KvkFldResultUpdateDto dto)
        {
            var result = await _service.UpdateFldResultAsync(fldId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete FLD result
        /// </summary>
        [HttpDelete("fld-results/{fldId}")]
        public async Task<IActionResult> DeleteFldResult(int fldId)
        {
            var result = await _service.DeleteFldResultAsync(fldId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get all FLD results for a result record
        /// </summary>
        [HttpGet("results/{resultId}/fld")]
        public async Task<IActionResult> GetFldResults(int resultId)
        {
            var result = await _service.GetFldResultsByResultIdAsync(resultId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        // ============================
        // E2: OFT RESULTS
        // ============================

        /// <summary>
        /// Add OFT result entry
        /// </summary>
        [HttpPost("results/{resultId}/oft")]
        public async Task<IActionResult> AddOftResult(int resultId, [FromBody] KvkOftResultCreateDto dto)
        {
            var result = await _service.AddOftResultAsync(resultId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Update OFT result
        /// </summary>
        [HttpPut("oft-results/{oftId}")]
        public async Task<IActionResult> UpdateOftResult(int oftId, [FromBody] KvkOftResultUpdateDto dto)
        {
            var result = await _service.UpdateOftResultAsync(oftId, dto);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Delete OFT result
        /// </summary>
        [HttpDelete("oft-results/{oftId}")]
        public async Task<IActionResult> DeleteOftResult(int oftId)
        {
            var result = await _service.DeleteOftResultAsync(oftId);
            return result.IsSuccess ? Ok(result) : StatusCode(GetStatusCode(result.ErrorStatus), result);
        }

        /// <summary>
        /// Get all OFT results for a result record
        /// </summary>
        [HttpGet("results/{resultId}/oft")]
        public async Task<IActionResult> GetOftResults(int resultId)
        {
            var result = await _service.GetOftResultsByResultIdAsync(resultId);
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
        /// Get trainer's/unit head's own program history with pagination
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
        /// Get pending approvals with pagination
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

        /// <summary>
        /// Get programs by trainer ID and optionally filter by unit location
        /// </summary>
        [HttpGet("trainer/{trainerId}")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetByTrainer(
            int trainerId,
            [FromQuery] int? unitLocationId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetByTrainerAsync(trainerId, unitLocationId, pageNumber, pageSize);
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
}