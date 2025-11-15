using Application.Interface.Services.DataTables;
using Application.Interface.Services.DataTables.ConsultSocialMedia;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        // Composite Create/Update with Children (Hybrid Pattern)
        // ==========================================

        /// <summary>
        /// Create ConsultingService with all child entities (ModeAndOutreach) in a single transaction.
        /// Perfect for "Save & Next" button - solves parent-child ID dependency.
        /// </summary>
        [HttpPost("with-children")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> CreateConsultingServiceWithChildren([FromBody] ConsultingServiceWithChildrenCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.CreateWithChildrenAsync(dto);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new
            {
                message = "Consulting service created successfully with all children. Submit when ready.",
                consultingServiceId = result.Data!.Id,
                data = result.Data
            });
        }

        /// <summary>
        /// Update ConsultingService with all child entities using Hybrid Pattern.
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id (null or 0): CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing.
        /// </summary>
        [HttpPut("{id}/with-children")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateConsultingServiceWithChildren(int id, [FromBody] ConsultingServiceWithChildrenUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _consultingServiceService.UpdateWithChildrenAsync(id, dto);

            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new
            {
                message = "Consulting service and children updated successfully. Submit when ready.",
                data = result.Data
            });
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
       
    }
 
}