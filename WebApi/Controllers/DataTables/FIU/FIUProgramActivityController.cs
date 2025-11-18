using Application.Interface.Services.DataTables.FIU;
using Application.Models.DataTables.FIU;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.DataTables.FIU
{
    /// <summary>
    /// Controller for FIU Program Activities (Farmers Innovation Unit Activities)
    ///
    /// This controller manages activity entries for FIU program activities,
    /// tracking various activity types and their counts.
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
    [Route("api/fiu-activities")]
    [ApiController]
    [Authorize]
    public class FIUProgramActivitiesController : ControllerBase
    {
        private readonly IFIUProgramActivityService _service;

        public FIUProgramActivitiesController(IFIUProgramActivityService service)
        {
            _service = service;
        }

        // ==========================================
        // MAIN CRUD OPERATIONS
        // ==========================================

        /// <summary>
        /// Create new FIU activity (Trainer only)
        /// Automatically set to status "Pending" for approval
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/fiu-activities
        ///     {
        ///       "unitLocationId": 1,
        ///       "fiuActivitiesId": 5,
        ///       "number": 25,
        ///       "uploadMediaUrl": "https://example.com/media.jpg",
        ///       "remarks": "Activity completed successfully"
        ///     }
        ///
        /// Entry is automatically created with status "Pending"
        /// </remarks>
        /// <response code="200">Activity created successfully</response>
        /// <response code="400">Validation error</response>
        [HttpPost]
        [Authorize(Roles = RoleString.Trainer)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActivity([FromBody] FIUProgramActivityCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var batchCreateDto = new FIUProgramActivityBatchCreateDto
            {
                Activities = new List<FIUProgramActivityCreateDto> { createDto }
            };

            var result = await _service.CreateBatchAsync(batchCreateDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            var batchResult = result.Data;

            if (batchResult.FailureCount > 0)
                return BadRequest(new { message = batchResult.FailedEntries[0].ErrorMessage });

            var createdActivity = batchResult.SuccessfulEntries[0];

            return Ok(new
            {
                message = "Activity created successfully",
                activityId = createdActivity.Id,
                data = createdActivity,
                status = "Pending"
            });
        }

        /// <summary>
        /// Update FIU activity (Trainer only)
        /// Automatically set to status "Pending" after update
        /// Only non-approved entries can be updated (Pending and Rejected are editable)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/fiu-activities/5
        ///     {
        ///       "id": 5,
        ///       "fiuActivitiesId": 5,
        ///       "number": 30,
        ///       "uploadMediaUrl": "https://example.com/updated.jpg",
        ///       "remarks": "Updated data"
        ///     }
        ///
        /// Entry is automatically set to status "Pending" after update
        /// </remarks>
        /// <response code="200">Activity updated successfully</response>
        /// <response code="400">Validation error or cannot update (approved status)</response>
        [HttpPut("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] FIUProgramActivityUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest(new { message = "ID mismatch" });

            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var batchUpdateDto = new FIUProgramActivityBatchUpdateDto
            {
                Activities = new List<FIUProgramActivityUpdateDto> { updateDto }
            };

            var result = await _service.UpdateBatchAsync(batchUpdateDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            var batchResult = result.Data;

            if (batchResult.FailureCount > 0)
                return BadRequest(new { message = batchResult.FailedEntries[0].ErrorMessage });

            var updatedActivity = batchResult.SuccessfulEntries[0];

            return Ok(new
            {
                message = "Activity updated successfully",
                data = updatedActivity,
                status = "Pending"
            });
        }

        /// <summary>
        /// Batch create multiple FIU activities (Trainer only)
        /// All entries are automatically submitted for approval with status "Pending"
        /// Can be used for single or multiple entries
        /// </summary>
        /// <remarks>
        /// Sample request for single entry:
        ///
        ///     POST /api/fiu-activities/batch
        ///     {
        ///       "activities": [
        ///         {
        ///           "unitLocationId": 1,
        ///           "fiuActivitiesId": 5,
        ///           "number": 25,
        ///           "uploadMediaUrl": "https://example.com/media.jpg",
        ///           "remarks": "Activity completed successfully"
        ///         }
        ///       ]
        ///     }
        ///
        /// Sample request for multiple entries:
        ///
        ///     POST /api/fiu-activities/batch
        ///     {
        ///       "activities": [
        ///         {
        ///           "unitLocationId": 1,
        ///           "fiuActivitiesId": 5,
        ///           "number": 25,
        ///           "uploadMediaUrl": "https://example.com/media1.jpg"
        ///         },
        ///         {
        ///           "unitLocationId": 1,
        ///           "fiuActivitiesId": 6,
        ///           "number": 30,
        ///           "remarks": "Second activity"
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
        public async Task<IActionResult> CreateBatchAsync([FromBody] FIUProgramActivityBatchCreateDto batchCreateDto)
        {
            var result = await _service.CreateBatchAsync(batchCreateDto);

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
        /// Batch update multiple FIU activities (Trainer only)
        /// All entries are automatically set to status "Pending" after update
        /// Only non-approved entries can be updated (Pending and Rejected are editable)
        /// Can be used for single or multiple entries
        /// </summary>
        /// <remarks>
        /// Sample request for single entry:
        ///
        ///     PUT /api/fiu-activities/batch
        ///     {
        ///       "activities": [
        ///         {
        ///           "id": 5,
        ///           "fiuActivitiesId": 5,
        ///           "number": 30,
        ///           "uploadMediaUrl": "https://example.com/updated.jpg",
        ///           "remarks": "Updated data"
        ///         }
        ///       ]
        ///     }
        ///
        /// Sample request for multiple entries:
        ///
        ///     PUT /api/fiu-activities/batch
        ///     {
        ///       "activities": [
        ///         {
        ///           "id": 5,
        ///           "fiuActivitiesId": 5,
        ///           "number": 30
        ///         },
        ///         {
        ///           "id": 6,
        ///           "fiuActivitiesId": 6,
        ///           "number": 35,
        ///           "remarks": "Updated activity"
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
        public async Task<IActionResult> UpdateBatchAsync([FromBody] FIUProgramActivityBatchUpdateDto batchUpdateDto)
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
        /// Get available activity types (master data dropdown)
        /// </summary>
        [HttpGet("activity-types")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivityTypes()
        {
            var result = await _service.GetAvailableActivitiesAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get activity by ID
        /// Returns full details including creator and approver information
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <response code="200">Activity found</response>
        /// <response code="404">Activity not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivityById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Delete activity (Trainer only)
        /// Can delete Pending and Rejected entries, but not Approved entries
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <response code="200">Activity deleted successfully</response>
        /// <response code="400">Cannot delete (wrong status)</response>
        /// <response code="404">Activity not found</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity deleted successfully" });
        }

        // ==========================================
        // APPROVAL WORKFLOW
        // ==========================================

        /// <summary>
        /// Approve activity (Unit Head/Admin only)
        /// Changes status: Pending → Approved
        /// Records approver and timestamp
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="approvalDto">Optional approval remarks</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/fiu-activities/5/approve
        ///     {
        ///       "remarks": "Verified and approved"
        ///     }
        ///
        /// Remarks are optional for approval
        /// </remarks>
        /// <response code="200">Activity approved successfully</response>
        /// <response code="400">Cannot approve (wrong status or access denied)</response>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApproveActivity(int id, [FromBody] ApprovalDto? approvalDto = null)
        {
            var result = await _service.ApproveAsync(id, approvalDto?.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity approved successfully", data = result.Data });
        }

        /// <summary>
        /// Reject activity with remarks (Unit Head/Admin only)
        /// Changes status: Pending → Rejected
        /// Remarks are REQUIRED for rejection
        /// Trainer can then edit and resubmit
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="rejectionDto">Rejection remarks (required)</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/fiu-activities/5/reject
        ///     {
        ///       "remarks": "Please verify the activity count and provide media upload"
        ///     }
        ///
        /// Remarks are REQUIRED for rejection
        /// </remarks>
        /// <response code="200">Activity rejected successfully</response>
        /// <response code="400">Remarks required or wrong status</response>
        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectActivity(int id, [FromBody] RejectionDto rejectionDto)
        {
            if (string.IsNullOrWhiteSpace(rejectionDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _service.RejectAsync(id, rejectionDto.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Activity rejected", data = result.Data });
        }

        // ==========================================
        // PAGINATION & QUERIES
        // ==========================================

        /// <summary>
        /// Get paginated activities
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? activityId = null,
            [FromQuery] string? status = null)
        {
            var result = await _service.GetPaginatedAsync(pageNumber, pageSize, activityId, status);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                data = result.Data!.Items,
                pagination = new
                {
                    result.Data.Items,
                    result.Data.TotalItems,
                    result.Data.PageNumber,
                    result.Data.PageSize
                }
            });
        }



        /// <summary>
        /// Get trainer's own activities
        /// </summary>
        [HttpGet("my-activities")]
        [Authorize(Roles = nameof(Role.TRAINER))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyActivities()
        {
            var result = await _service.GetMyActivitiesAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get activities pending approval (Unit Head only)
        /// </summary>
        [HttpGet("pending-approvals")]
        [Authorize(Roles = nameof(Role.UNITHEAD))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var result = await _service.GetPendingApprovalsAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get statistics by activity type
        /// </summary>
        [HttpGet("stats/by-activity-type")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatsByActivityType()
        {
            var result = await _service.GetStatsByActivityTypeAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get statistics by status
        /// </summary>
        [HttpGet("stats/by-status")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStatsByStatus()
        {
            var result = await _service.GetStatsByStatusAsync();

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get monthly report (Admin/UnitHead)
        /// </summary>
        [HttpPost("reports/monthly")]
        [Authorize(Roles = $"{nameof(Role.UNITHEAD)},{nameof(Role.ADMIN)}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMonthlyReport([FromBody] FIUReportRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _service.GetMonthlyReportAsync(requestDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        [HttpGet("my-history")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetMyHistory(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("pending-approvals-paginated")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingApprovals(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetPendingApprovalsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get FIU activities by trainer ID (Unit Head and Admin only)
        /// Shows all activities created by the specified trainer
        /// Filtered by accessible unit locations
        /// </summary>
        /// <param name="trainerId">Trainer ID</param>
        /// <param name="unitLocationId">Optional unit location filter</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <response code="200">Paginated list of trainer's activities</response>
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

        /// <summary>
        /// Get unified history - own forms or trainer forms (for unit heads)
        /// </summary>
        /// <param name="trainerId">Optional: Trainer ID to view (unit heads only). If null, shows own history</param>
        /// <param name="unitLocationId">Optional: Filter by unit location</param>
        [HttpGet("unified-history")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetUnifiedHistory(
            [FromQuery] int? trainerId = null,
            [FromQuery] int? unitLocationId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetUnifiedHistoryAsync(trainerId, unitLocationId, pageNumber, pageSize);
            return Ok(result);
        }

    }

    public class ApprovalDto
    {
        public string? Remarks { get; set; }
    }

    public class RejectionDto
    {
        [Required]
        public string Remarks { get; set; } = string.Empty;
    }
}