using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    /// <summary>
    /// Controller for TableOtherActivity (Generic Activity table for all units)
    /// 
    /// This is a COMMON/GENERIC table used across ALL 10 units for miscellaneous activities
    /// that don't fit into specific unit tables (like training programs, research, etc.)
    /// 
    /// USAGE:
    /// - Any trainer from any unit can create "Other Activity" entries
    /// - Common for events, workshops, field visits, miscellaneous tasks
    /// - Each entry is linked to a specific UnitLocation
    /// 
    /// STATUS WORKFLOW:
    /// - Draft: Initial state when created, can edit freely
    /// - Pending: Submitted for Unit Head approval (via Submit endpoint)
    /// - Approved: Unit Head approved, cannot edit
    /// - Rejected: Unit Head rejected with remarks, trainer can edit and resubmit
    /// 
    /// PERMISSIONS:
    /// - Trainers: Create, edit (Draft/Rejected only), submit, view own
    /// - UnitHeads: Approve/reject, view all in their unit locations
    /// - Admins: Full access across organization
    /// </summary>
    [ApiController]
    [Route("api/data-tables/other-activities")]
    public class TableOtherActivityController : ControllerBase
    {
        private readonly ITableOtherActivityService _service;

        public TableOtherActivityController(ITableOtherActivityService service)
        {
            _service = service;
        }

        // ==========================================
        // MAIN CRUD OPERATIONS
        // ==========================================

        /// <summary>
        /// Create new OtherActivity entry (Trainer only)
        /// Initial status: Draft
        /// Auto-sets CreatedById, CreatedAt, and OrganizationId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/data-tables/other-activities
        ///     {
        ///       "unitLocationId": 1,
        ///       "startDate": "2025-01-15",
        ///       "endDate": "2025-01-16",
        ///       "title": "Field Visit to Organic Farm",
        ///       "description": "Educational visit to demonstrate sustainable farming practices",
        ///       "uploadPath": "/uploads/field-visit-photos.zip"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Activity created successfully</response>
        /// <response code="400">Validation error or access denied</response>
        /// <response code="401">Unauthorized - authentication required</response>
        [HttpPost]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Create([FromBody] TableOtherActivityCreateDto createDto)
        {
            var result = await _service.CreateAsync(createDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Activity created successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Get OtherActivity by ID
        /// Returns full details including creator and approver information
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <response code="200">Activity found</response>
        /// <response code="404">Activity not found</response>
        /// <response code="403">Access denied to this activity</response>
        [HttpGet("{id}")]
        [Authorize]
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
        /// Update OtherActivity (Trainer only, Draft or Rejected status only)
        /// Cannot update Pending or Approved activities
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="updateDto">Updated activity data</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/data-tables/other-activities/5
        ///     {
        ///       "title": "Updated Field Visit Title",
        ///       "description": "Updated description with more details",
        ///       "startDate": "2025-01-20"
        ///     }
        /// 
        /// Note: Only fields you want to update need to be included
        /// </remarks>
        /// <response code="200">Activity updated successfully</response>
        /// <response code="400">Cannot modify (wrong status or access denied)</response>
        /// <response code="404">Activity not found</response>
        [HttpPut("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Update(int id, [FromBody] TableOtherActivityUpdateDto updateDto)
        {
            var result = await _service.UpdateAsync(id, updateDto);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new
            {
                message = "Activity updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete OtherActivity (Trainer only, Draft status only)
        /// Cannot delete submitted or approved activities
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <response code="200">Activity deleted successfully</response>
        /// <response code="400">Cannot delete (wrong status)</response>
        /// <response code="404">Activity not found</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Activity deleted successfully" });
        }

        // ==========================================
        // SUBMISSION & APPROVAL WORKFLOW
        // ==========================================

        /// <summary>
        /// Submit OtherActivity for approval (Trainer only)
        /// Changes status: Draft → Pending
        /// After submission, activity cannot be edited until approved/rejected
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <response code="200">Activity submitted successfully</response>
        /// <response code="400">Cannot submit (validation error or wrong status)</response>
        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Submit(int id)
        {
            var result = await _service.SubmitForApprovalAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity submitted for approval successfully" });
        }

        /// <summary>
        /// Approve OtherActivity (Unit Head/Admin only)
        /// Changes status: Pending → Approved
        /// Records approver and timestamp
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="approvalDto">Optional approval remarks</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/data-tables/other-activities/5/approve
        ///     {
        ///       "remarks": "Good work, approved for documentation"
        ///     }
        /// 
        /// Remarks are optional for approval
        /// </remarks>
        /// <response code="200">Activity approved successfully</response>
        /// <response code="400">Cannot approve (wrong status or access denied)</response>
        /// <response code="403">Not authorized (must be Unit Head of this unit)</response>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApprovalDto? approvalDto = null)
        {
            var result = await _service.ApproveAsync(id, approvalDto?.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity approved successfully" });
        }

        /// <summary>
        /// Reject OtherActivity with remarks (Unit Head/Admin only)
        /// Changes status: Pending → Rejected
        /// Remarks are REQUIRED for rejection
        /// Trainer can then edit and resubmit
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="approvalDto">Rejection remarks (required)</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/data-tables/other-activities/5/reject
        ///     {
        ///       "remarks": "Please add more details about participants and outcomes"
        ///     }
        /// 
        /// Remarks are REQUIRED for rejection
        /// </remarks>
        /// <response code="200">Activity rejected successfully</response>
        /// <response code="400">Remarks required or wrong status</response>
        /// <response code="403">Not authorized</response>
        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Reject(int id, [FromBody] ApprovalDto approvalDto)
        {
            if (string.IsNullOrWhiteSpace(approvalDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _service.RejectAsync(id, approvalDto.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity rejected" });
        }

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        /// <summary>
        /// Get paginated list of activities with filters
        /// Auto-filters based on user role:
        /// - Trainers: Only their own entries
        /// - UnitHeads: All entries in their assigned unit locations
        /// - Admins: All entries in organization
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 10, max: 100)</param>
        /// <param name="startDate">Filter by start date (format: YYYY-MM-DD)</param>
        /// <param name="endDate">Filter by end date (format: YYYY-MM-DD)</param>
        /// <param name="unitLocationId">Filter by specific unit location</param>
        /// <param name="searchTerm">Search in title and description</param>
        /// <response code="200">Paginated list of activities</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int? unitLocationId = null,
            [FromQuery] string? searchTerm = null)
        {
            if (pageSize > 100)
                pageSize = 100;

            var result = await _service.GetPaginatedAsync(
                pageNumber,
                pageSize,
                startDate,
                endDate,
                unitLocationId,
                searchTerm);

            return Ok(result);
        }

        /// <summary>
        /// Get activities by status (for filtering in UI)
        /// Useful for showing "My Drafts", "Pending Approval", etc.
        /// </summary>
        /// <param name="status">Status: Draft, Pending, Approved, or Rejected</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 10)</param>
        /// <response code="200">Paginated list filtered by status</response>
        [HttpGet("by-status/{status}")]
        [Authorize]
        public async Task<IActionResult> GetByStatus(
            string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
            return Ok(result);
        }

        // ==========================================
        // DASHBOARD & HISTORY
        // ==========================================

        /// <summary>
        /// Get status summary (count by status)
        /// Returns: { "Draft": 5, "Pending": 3, "Approved": 12, "Rejected": 1 }
        /// Filtered based on user's accessible unit locations
        /// </summary>
        /// <response code="200">Status counts dictionary</response>
        [HttpGet("status-summary")]
        [Authorize]
        public async Task<IActionResult> GetStatusSummary()
        {
            var result = await _service.GetStatusSummaryAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        /// <summary>
        /// Get trainer's submission history (Trainer only)
        /// Shows all activities created by the logged-in trainer
        /// Sorted by most recent first
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <response code="200">Paginated history of trainer's submissions</response>
        [HttpGet("my-history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetMyHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get pending approvals for Unit Head
        /// Shows all activities in Pending status for unit locations
        /// assigned to the logged-in Unit Head
        /// </summary>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <response code="200">Paginated list of pending approvals</response>
        [HttpGet("pending-approvals")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingApprovals(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetPendingApprovalsAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }


}