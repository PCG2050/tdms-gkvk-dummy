using Application.Interface.Services.DataTables.FIU;
using Application.Models.DataTables.FIU;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.FIU
{
    /// <summary>
    /// Controller for FIU (Farm Information Unit) Program Activities
    /// 
    /// STATUS WORKFLOW:
    /// - Draft: Initial state, can edit
    /// - Saved: Auto-saved state (when trainer clicks "Save Next")
    /// - Pending: Submitted for Unit Head approval
    /// - Approved: Unit Head approved, cannot edit
    /// - Rejected: Unit Head rejected, can edit and resubmit
    /// 
    /// PERMISSIONS:
    /// - Trainers: Create, Edit (Draft/Rejected), Submit, View own
    /// - UnitHeads: Approve/Reject, View all under them
    /// - Admins: View all in organization
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
    public class FIUProgramActivityController : ControllerBase
    {
        private readonly IFIUProgramActivityService _service;

        public FIUProgramActivityController(IFIUProgramActivityService service)
        {
            _service = service;
        }

        // ==========================================
        // CRUD OPERATIONS
        // ==========================================

        /// <summary>
        /// Create new FIU Program Activity (Trainer only)
        /// Initial status: Draft
        /// </summary>
        [HttpPost("Create")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Create([FromBody] FIUProgramActivityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.AddAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "FIU Program Activity created successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Get FIU Program Activity by ID
        /// Returns activity if user has permission to view
        /// </summary>
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                return StatusCode(403, new { message = result.ErrorMessage });
            }

            return Ok(result.Data);
        }

        /// <summary>
        /// Get all FIU Program Activities accessible to current user
        /// Filters based on role and permissions
        /// </summary>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Update FIU Program Activity (Trainer only)
        /// Can only update Draft or Rejected entries
        /// Auto-resets Rejected entries to Draft
        /// </summary>
        [HttpPut("Update/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Update(int id, [FromBody] FIUProgramActivityDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "FIU Program Activity updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete FIU Program Activity (Trainer only)
        /// Can only delete Draft entries
        /// </summary>
        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "FIU Program Activity deleted successfully" });
        }

        // ==========================================
        // FORM STATUS WORKFLOW
        // ==========================================

        /// <summary>
        /// Submit activity for approval (Trainer only)
        /// Changes status: Draft → Pending
        /// Only trainers can submit their own entries
        /// </summary>
        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> SubmitForApproval(int id)
        {
            var result = await _service.SubmitForApprovalAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "FIU Program Activity submitted for approval successfully" });
        }

        /// <summary>
        /// Approve activity (Unit Head/Admin only)
        /// Changes status: Pending → Approved
        /// Optional remarks can be provided
        /// </summary>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApprovalDto? approvalDto = null)
        {
            var result = await _service.ApproveAsync(id, approvalDto?.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "FIU Program Activity approved successfully" });
        }

        /// <summary>
        /// Reject activity with remarks (Unit Head/Admin only)
        /// Changes status: Pending → Rejected
        /// Remarks are REQUIRED for rejection
        /// </summary>
        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Reject(int id, [FromBody] ApprovalDto approvalDto)
        {
            if (string.IsNullOrWhiteSpace(approvalDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _service.RejectAsync(id, approvalDto.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "FIU Program Activity rejected" });
        }

        // ==========================================
        // HISTORY & DASHBOARD
        // ==========================================

        /// <summary>
        /// Get paginated history of FIU Program Activities
        /// Filters based on user role and permissions
        /// Supports filtering by date range, unit location, and search term
        /// </summary>
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int? unitLocationId = null,
            [FromQuery] string? searchTerm = null)
        {
            var result = await _service.GetPaginatedAsync(
                pageNumber,
                pageSize,
                startDate,
                endDate,
                unitLocationId,
                searchTerm);

            return Ok(new
            {
                items = result.Items,
                totalItems = result.TotalItems,
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = (int)Math.Ceiling((double)result.TotalItems / result.PageSize)
            });
        }

        /// <summary>
        /// Get activities by specific status
        /// Status options: "Draft", "Saved", "Pending", "Approved", "Rejected"
        /// Used for filtering history page
        /// </summary>
        [HttpGet("history/status/{status}")]
        public async Task<IActionResult> GetByStatus(
            string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);

            return Ok(new
            {
                status,
                items = result.Items,
                totalItems = result.TotalItems,
                pageNumber = result.PageNumber,
                pageSize = result.PageSize,
                totalPages = (int)Math.Ceiling((double)result.TotalItems / result.PageSize)
            });
        }

        /// <summary>
        /// Get status summary for dashboard
        /// Returns count of activities by status
        /// Example: { "Draft": 5, "Pending": 8, "Approved": 12, "Rejected": 2 }
        /// </summary>
        [HttpGet("dashboard/summary")]
        public async Task<IActionResult> GetStatusSummary()
        {
            var summary = await _service.GetStatusSummaryAsync();

            return Ok(new
            {
                summary,
                total = summary.Values.Sum()
            });
        }
    }

    /// <summary>
    /// DTO for approval/rejection operations
    /// </summary>
    public class ApprovalDto
    {
        /// <summary>
        /// Optional remarks for approval
        /// Required remarks for rejection
        /// </summary>
        public string? Remarks { get; set; }
    }
}