using Application.Interface.Services.DataTables.FIU;
using Application.Models.DataTables.FIU;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Controllers.DataTables.FIU
{
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
        /// Create new FIU activity (Trainer only)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = nameof(Role.TRAINER))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActivity([FromBody] FIUProgramActivityCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _service.CreateAsync(createDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = result.SuccessMessage,
                activityId = result.Data!.Id,
                data = result.Data
            });
        }

        /// <summary>
        /// Update FIU activity (Trainer only, Draft status)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.TRAINER))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateActivity(int id, [FromBody] FIUProgramActivityUpdateDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest(new { message = "ID mismatch" });

            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _service.UpdateAsync(updateDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = result.SuccessMessage, data = result.Data });
        }

        /// <summary>
        /// Get activity by ID
        /// </summary>
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
        /// Delete activity (Trainer only, Draft status)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.TRAINER))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = result.SuccessMessage });
        }

        /// <summary>
        /// Submit activity for approval (Trainer only)
        /// </summary>
        [HttpPost("{id}/submit")]
        [Authorize(Roles = nameof(Role.TRAINER))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitActivity(int id)
        {
            var result = await _service.SubmitForApprovalAsync(id);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = result.SuccessMessage, data = result.Data });
        }

        /// <summary>
        /// Approve activity (Unit Head only)
        /// </summary>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = nameof(Role.UNITHEAD))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApproveActivity(int id, [FromBody] ApprovalDto approvalDto)
        {
            var result = await _service.ApproveAsync(id, approvalDto.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = result.SuccessMessage, data = result.Data });
        }

        /// <summary>
        /// Reject activity (Unit Head only)
        /// </summary>
        [HttpPost("{id}/reject")]
        [Authorize(Roles = nameof(Role.UNITHEAD))]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectActivity(int id, [FromBody] RejectionDto rejectionDto)
        {
            if (string.IsNullOrWhiteSpace(rejectionDto.Remarks))
                return BadRequest(new { message = "Rejection reason is required" });

            var result = await _service.RejectAsync(id, rejectionDto.Remarks);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = result.SuccessMessage, data = result.Data });
        }

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
        [Authorize(Roles = RoleString.Trainer)]
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