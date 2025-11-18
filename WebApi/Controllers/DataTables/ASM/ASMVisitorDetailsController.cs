using Application.Interface.Services.DataTables.ASM;
using Application.Models.DataTables.ASM;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.ASM
{
    /// <summary>
    /// Controller for ASMVisitorDetails (Agricultural Science Museum Visitor Tracking)
    /// 
    /// This controller manages visitor detail entries for the Agricultural Science Museum,
    /// tracking different types of visitors: Farmers, Students, and General Public.
    /// 
    /// USAGE:
    /// - Trainers create visitor detail entries for museum visits
    /// - Each entry tracks institute name, date range, and visitor counts by category
    /// - Used for reporting and statistical analysis of museum visitors
    /// 
    /// STATUS WORKFLOW:
    /// - Pending: Automatically set when created or updated, awaiting Unit Head approval
    /// - Approved: Unit Head approved, cannot edit or delete
    /// - Rejected: Unit Head rejected with remarks, trainer can edit (status returns to Pending)
    ///
    /// PERMISSIONS:
    /// - Trainers: Create (auto-pending), edit (Pending/Rejected only), delete (Pending/Rejected only), view own
    /// - UnitHeads: Approve/reject, view all in their unit locations
    /// - Admins: Full access across organization
    /// </summary>
    [ApiController]
    [Route("api/units/asm/visitor-details")]
    public class ASMVisitorDetailsController : ControllerBase
    {
        private readonly IASMVisitorDetailsService _service;

        public ASMVisitorDetailsController(IASMVisitorDetailsService service)
        {
            _service = service;
        }

        // ==========================================
        // MAIN CRUD OPERATIONS
        // ==========================================

        /// <summary>
        /// Create new visitor detail entry (Trainer only)
        /// Initial status: Pending (automatically submitted for approval)
        /// Auto-sets CreatedById, CreatedAt, and OrganizationId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/units/asm/visitor-details
        ///     {
        ///       "unitLocationId": 1,
        ///       "instituteName": "Government High School",
        ///       "startDate": "2025-01-15",
        ///       "endDate": "2025-01-15",
        ///       "farmersCount": 0,
        ///       "studentsCount": 45,
        ///       "publicCount": 5
        ///     }
        ///
        /// Note: Entry is automatically created with status "Pending"
        /// </remarks>
        /// <response code="200">Visitor detail created successfully</response>
        /// <response code="400">Validation error or access denied</response>
        /// <response code="401">Unauthorized - authentication required</response>
        [HttpPost]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddAsync([FromBody] ASMVisitorDetailsCreateDto createDto)
        {
            var result = await _service.AddAsync(createDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Visitor detail created successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Batch create multiple visitor detail entries (Trainer only)
        /// All entries are automatically submitted for approval with status "Pending"
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/units/asm/visitor-details/batch
        ///     {
        ///       "visitorDetails": [
        ///         {
        ///           "unitLocationId": 1,
        ///           "instituteName": "School A",
        ///           "startDate": "2025-01-15",
        ///           "endDate": "2025-01-15",
        ///           "farmersCount": 0,
        ///           "studentsCount": 50,
        ///           "publicCount": 5
        ///         },
        ///         {
        ///           "unitLocationId": 1,
        ///           "instituteName": "School B",
        ///           "startDate": "2025-01-16",
        ///           "endDate": "2025-01-16",
        ///           "farmersCount": 0,
        ///           "studentsCount": 30,
        ///           "publicCount": 2
        ///         }
        ///       ]
        ///     }
        ///
        /// All entries are automatically created with status "Pending"
        /// </remarks>
        /// <response code="200">Batch creation completed (includes success and failure details)</response>
        /// <response code="400">Validation error</response>
        [HttpPost("batch")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddBatchAsync([FromBody] ASMVisitorDetailsBatchCreateDto batchCreateDto)
        {
            var result = await _service.AddBatchAsync(batchCreateDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            var batchResult = result.Data;

            return Ok(new
            {
                message = $"Batch creation completed: {batchResult.SuccessCount} succeeded, {batchResult.FailureCount} failed",
                totalProcessed = batchResult.TotalProcessed,
                successCount = batchResult.SuccessCount,
                failureCount = batchResult.FailureCount,
                successfulEntries = batchResult.SuccessfulEntries,
                failedEntries = batchResult.FailedEntries,
                status = "Pending"
            });
        }

        /// <summary>
        /// Batch update multiple visitor detail entries (Trainer only)
        /// All entries are automatically set to status "Pending" after update
        /// Only non-approved entries can be updated (Pending and Rejected are editable)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/units/asm/visitor-details/batch
        ///     {
        ///       "visitorDetails": [
        ///         {
        ///           "id": 5,
        ///           "studentsCount": 55,
        ///           "publicCount": 7
        ///         },
        ///         {
        ///           "id": 6,
        ///           "instituteName": "Updated School Name",
        ///           "studentsCount": 35
        ///         }
        ///       ]
        ///     }
        ///
        /// All successfully updated entries will have status set to "Pending"
        /// </remarks>
        /// <response code="200">Batch update completed (includes success and failure details)</response>
        /// <response code="400">Validation error</response>
        [HttpPut("batch")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateBatchAsync([FromBody] ASMVisitorDetailsBatchUpdateDto batchUpdateDto)
        {
            var result = await _service.UpdateBatchAsync(batchUpdateDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            var batchResult = result.Data;

            return Ok(new
            {
                message = $"Batch update completed: {batchResult.SuccessCount} succeeded, {batchResult.FailureCount} failed",
                totalProcessed = batchResult.TotalProcessed,
                successCount = batchResult.SuccessCount,
                failureCount = batchResult.FailureCount,
                successfulEntries = batchResult.SuccessfulEntries,
                failedEntries = batchResult.FailedEntries,
                status = "Pending"
            });
        }

        /// <summary>
        /// Get visitor detail by ID
        /// Returns full details including creator and approver information
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <response code="200">Visitor detail found</response>
        /// <response code="404">Visitor detail not found</response>
        /// <response code="403">Access denied to this entry</response>
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
        /// Update visitor detail (Trainer only)
        /// Can update Pending and Rejected entries, but not Approved entries
        /// Status is automatically set to "Pending" after update
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <param name="updateDto">Updated visitor detail data</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/units/asm/visitor-details/5
        ///     {
        ///       "instituteName": "Updated School Name",
        ///       "studentsCount": 50,
        ///       "publicCount": 10
        ///     }
        ///
        /// Note: Only fields you want to update need to be included
        /// Entry status is automatically set to "Pending" after update
        /// </remarks>
        /// <response code="200">Visitor detail updated successfully</response>
        /// <response code="400">Cannot modify (wrong status or access denied)</response>
        /// <response code="404">Visitor detail not found</response>
        [HttpPut("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Update(int id, [FromBody] ASMVisitorDetailsUpdateDto updateDto)
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
                message = "Visitor detail updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete visitor detail (Trainer only)
        /// Can delete Pending and Rejected entries, but not Approved entries
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <response code="200">Visitor detail deleted successfully</response>
        /// <response code="400">Cannot delete (wrong status)</response>
        /// <response code="404">Visitor detail not found</response>
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

            return Ok(new { message = "Visitor detail deleted successfully" });
        }

        // ==========================================
        // SUBMISSION & APPROVAL WORKFLOW
        // ==========================================

        /// <summary>
        /// Submit visitor detail for approval (Trainer only)
        /// NOTE: This endpoint is now redundant since all creates/updates automatically set status to "Pending"
        /// Kept for backward compatibility - changes status to "Pending" if not already
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <response code="200">Visitor detail submitted successfully</response>
        /// <response code="400">Cannot submit (validation error or wrong status)</response>
        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Submit(int id)
        {
            var result = await _service.SubmitForApprovalAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Visitor detail submitted for approval successfully" });
        }

        /// <summary>
        /// Approve visitor detail (Unit Head/Admin only)
        /// Changes status: Pending → Approved
        /// Records approver and timestamp
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <param name="approvalDto">Optional approval remarks</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/units/asm/visitor-details/5/approve
        ///     {
        ///       "remarks": "Verified and approved"
        ///     }
        /// 
        /// Remarks are optional for approval
        /// </remarks>
        /// <response code="200">Visitor detail approved successfully</response>
        /// <response code="400">Cannot approve (wrong status or access denied)</response>
        /// <response code="403">Not authorized (must be Unit Head of this unit)</response>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApprovalDto? approvalDto = null)
        {
            var result = await _service.ApproveAsync(id, approvalDto?.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Visitor detail approved successfully" });
        }

        /// <summary>
        /// Reject visitor detail with remarks (Unit Head/Admin only)
        /// Changes status: Pending → Rejected
        /// Remarks are REQUIRED for rejection
        /// Trainer can then edit and resubmit
        /// </summary>
        /// <param name="id">Visitor detail ID</param>
        /// <param name="approvalDto">Rejection remarks (required)</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/units/asm/visitor-details/5/reject
        ///     {
        ///       "remarks": "Please verify visitor counts and correct the institute name"
        ///     }
        /// 
        /// Remarks are REQUIRED for rejection
        /// </remarks>
        /// <response code="200">Visitor detail rejected successfully</response>
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

            return Ok(new { message = "Visitor detail rejected" });
        }

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        /// <summary>
        /// Get paginated list of visitor details with filters
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
        /// <param name="searchTerm">Search in institute name</param>
        /// <response code="200">Paginated list of visitor details</response>
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
        /// Get visitor details by status (for filtering in UI)
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
        // DASHBOARD & STATISTICS
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
        /// Shows all visitor details created by the logged-in trainer
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
        /// Shows all visitor details in Pending status for unit locations
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

    /// <summary>
    /// DTO for approval/rejection operations
    /// </summary>
    public class ApprovalDto
    {
        /// <summary>
        /// Optional remarks for approval, required for rejection
        /// </summary>
        public string? Remarks { get; set; }
    }
}