


namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/consulting-services")]
    [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
    public class ConsultingServicesController : ControllerBase
    {
        private readonly IConsultingServiceService _consultingServiceService;

        public ConsultingServicesController(IConsultingServiceService consultingServiceService)
        {
            _consultingServiceService = consultingServiceService;
        }

        // ==========================================
        // Main Consulting Service CRUD
        // ==========================================

        [HttpPost]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> CreateConsultingService([FromBody] ConsultingServiceCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.CreateAsync(createDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Consulting service created successfully. Add mode and outreach details or submit.",
                consultingServiceId = result.Data!.Id,
                data = result.Data
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateConsultingService(int id, [FromBody] ConsultingServiceUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.UpdateAsync(id, updateDto);
            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Consulting service updated successfully", data = result.Data });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultingServiceById(int id)
        {
            var result = await _consultingServiceService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        [HttpGet("{id}/complete")]
        public async Task<IActionResult> GetCompleteConsultingService(int id)
        {
            var result = await _consultingServiceService.GetCompleteConsultingServiceAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteConsultingService(int id)
        {
            var result = await _consultingServiceService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Consulting service deleted successfully" });
        }

        // ==========================================
        // ModeAndOutreach Management
        // ==========================================

        [HttpPost("{consultingServiceId}/mode-and-outreach")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddModeAndOutreach(int consultingServiceId, [FromBody] ModeAndOutreachCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.AddModeAndOutreachAsync(consultingServiceId, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Mode and outreach added successfully. Add more or submit the form.",
                data = result.Data
            });
        }

        [HttpPut("{consultingServiceId}/mode-and-outreach/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateModeAndOutreach(int consultingServiceId, int id, [FromBody] ModeAndOutreachCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.UpdateModeAndOutreachAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });            
            
            return Ok(new { message = "Mode and outreach updated successfully", data = result.Data });
        }

        [HttpDelete("{consultingServiceId}/mode-and-outreach/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteModeAndOutreach(int consultingServiceId, int id)
        {
            var result = await _consultingServiceService.DeleteModeAndOutreachAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Mode and outreach deleted successfully" });
        }

        [HttpGet("{consultingServiceId}/mode-and-outreach")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetModeAndOutreaches(int consultingServiceId)
        {
            var result = await _consultingServiceService.GetModeAndOutreachesAsync(consultingServiceId);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        // ==========================================
        // Submit for Approval
        // ==========================================

        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> SubmitConsultingService(int id)
        {
            var result = await _consultingServiceService.SubmitForApprovalAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Consulting service submitted successfully! It will be reviewed by Unit Head.",
                status = "Pending"
            });
        }

        // ==========================================
        // History & Dashboard
        // ==========================================

        [HttpGet("history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetConsultingServiceHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? status = null)
        {
            var result = !string.IsNullOrWhiteSpace(status)
                ? await _consultingServiceService.GetByStatusAsync(status, pageNumber, pageSize)
                : await _consultingServiceService.GetPaginatedAsync(pageNumber, pageSize);

            return Ok(new
            {
                data = result.Items,
                pagination = new
                {
                    pageNumber = result.PageNumber,
                    pageSize = result.PageSize,
                    totalItems = result.TotalItems
                }
            });
        }

        [HttpGet("status-summary")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetConsultingServiceStatusSummary()
        {
            var summary = await _consultingServiceService.GetStatusSummaryAsync();
            return Ok(new { data = summary });
        }

        // ==========================================
        // Unit Head Approvals
        // ==========================================

        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> ApproveConsultingService(int id, [FromBody] ApprovalDto approvalDto)
        {
            var result = await _consultingServiceService.ApproveConsultingServiceAsync(id, approvalDto?.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Consulting service approved successfully" });
        }

        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> RejectConsultingService(int id, [FromBody] ApprovalDto approvalDto)
        {
            if (string.IsNullOrWhiteSpace(approvalDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _consultingServiceService.RejectConsultingServiceAsync(id, approvalDto.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Consulting service rejected" });
        }


        [HttpGet("pending-review")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingConsultingServices([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _consultingServiceService.GetByStatusAsync("pending", pageNumber, pageSize);
            return Ok(new { data = result.Items, pagination = result });
        }

        [HttpGet("my-history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetMyHistory(
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10)
        {
            var result = await _consultingServiceService.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get pending approvals for Unit Head
        /// Shows all entries in Pending status for unit locations
        /// assigned to the logged-in Unit Head
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <response code="200">Paginated list of pending approvals</response>
        [HttpGet("pending-approvals")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingApprovals(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _consultingServiceService.GetPendingApprovalsAsync(pageNumber, pageSize);
            return Ok(result);
        }

    }
 
}