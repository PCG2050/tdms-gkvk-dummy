using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;

using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/publications")]
    [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationsController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        // ==========================================
        // PHASE 1: Basic Publication Details
        // ==========================================

        [HttpPost("phase1")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> CreatePublicationPhase1([FromBody] PublicationCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.CreatePhase1Async(createDto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Phase 1 saved. Proceed to Phase 2 (Publisher Details).",
                publicationId = result.Data!.Id,
                data = result.Data
            });
        }

        [HttpPut("phase1/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdatePublicationPhase1(int id, [FromBody] PublicationUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.UpdateAsync(id, updateDto);
            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Phase 1 updated successfully", data = result.Data });
        }

        // ==========================================
        // PHASE 2: Publisher Details
        // ==========================================

        [HttpPost("{publicationId}/phase2/publisher-details")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddPublisherDetailsPhase2(int publicationId, [FromBody] PublisherDetailsCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.AddPublisherDetailsAsync(publicationId, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Phase 2 saved. Proceed to Phase 3 (Extension Literature).",
                data = result.Data
            });
        }

        [HttpPut("{publicationId}/phase2/publisher-details/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdatePublisherDetailsPhase2(int publicationId, int id, [FromBody] PublisherDetailsCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.UpdatePublisherDetailsAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Phase 2 updated successfully", data = result.Data });
        }

        [HttpPost("{publicationId}/phase2/skip")]
        [Authorize(Roles = RoleString.Trainer)]
        
        public async Task<IActionResult> SkipPublisherDetailsPhase2(int publicationId)
        {
            var result = await _publicationService.GetByIdAsync(publicationId);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { message = "Phase 2 skipped. Proceed to Phase 3 (Extension Literature)." });
        }

        // ==========================================
        // PHASE 3: Extension Literature
        // ==========================================

        [HttpPost("{publicationId}/phase3/extension-literature")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddExtensionLiteraturePhase3(int publicationId, [FromBody] ExtensionLiteratureCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.AddExtensionLiteratureAsync(publicationId, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Extension literature added. You can add more or click Submit.",
                data = result.Data
            });
        }

        [HttpPut("{publicationId}/phase3/extension-literature/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateExtensionLiteraturePhase3(int publicationId, int id, [FromBody] ExtensionLiteratureCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data", errors = ModelState });

            var result = await _publicationService.UpdateExtensionLiteratureAsync(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Extension literature updated", data = result.Data });
        }

        [HttpDelete("{publicationId}/phase3/extension-literature/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteExtensionLiteraturePhase3(int publicationId, int id)
        {
            var result = await _publicationService.DeleteExtensionLiteratureAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Extension literature deleted successfully" });
        }

        [HttpGet("{publicationId}/phase3/extension-literatures")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetExtensionLiteraturesPhase3(int publicationId)
        {
            var result = await _publicationService.GetExtensionLiteraturesAsync(publicationId);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        // ==========================================
        // FINAL SUBMIT (Draft → Pending)
        // ==========================================

        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> SubmitPublication(int id)
        {
            var result = await _publicationService.SubmitForApprovalAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Publication submitted successfully! It will be reviewed by Unit Head.",
                status = "Pending"
            });
        }

        // ==========================================
        // HISTORY & DASHBOARD
        // ==========================================

        [HttpGet("history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetPublicationHistory(
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] string? status = null)
        {
            var result = !string.IsNullOrWhiteSpace(status)
                ? await _publicationService.GetByStatusAsync(status, pageNumber, pageSize)
                : await _publicationService.GetPaginatedAsync(pageNumber, pageSize);

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationById(int id)
        {
            var result = await _publicationService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        [HttpGet("{id}/complete")]
        public async Task<IActionResult> GetCompletePublication(int id)
        {
            var result = await _publicationService.GetCompletePublicationAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeletePublication(int id)
        {
            var result = await _publicationService.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Publication deleted successfully" });
        }

        [HttpGet("status-summary")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetPublicationStatusSummary()
        {
            var summary = await _publicationService.GetStatusSummaryAsync();
            return Ok(new { data = summary });
        }

        // ==========================================
        // UNIT HEAD APPROVALS
        // ==========================================

        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> ApprovePublication(int id, [FromBody] ApprovalDto approvalDto)
        {
            var result = await _publicationService.ApprovePublicationAsync(id, approvalDto?.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Publication approved successfully" });
        }

        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> RejectPublication(int id, [FromBody] ApprovalDto approvalDto)
        {
            if (string.IsNullOrWhiteSpace(approvalDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _publicationService.RejectPublicationAsync(id, approvalDto.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Publication rejected" });
        }

        [HttpGet("pending-review")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingPublications([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _publicationService.GetByStatusAsync("pending", pageNumber, pageSize);
            return Ok(new { data = result.Items, pagination = result });
        }

        [HttpGet("by-unit")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPublicationsByUnit([FromQuery] PaginationRequest pagination)
        {
            var result = await _publicationService.GetGroupedByUnitAsync(pagination);
            return Ok(result);
        }
    }

    // ============================
    // Local DTOs
    // ============================

    public class ApprovalDto
    {
        public string? Remarks { get; set; }
    }
}
