

namespace WebApi.Controllers.DataTables.IBTVA
{
    using Application.Interface.Services.DataTables;
    using Application.Models.DataTables;
    using Domain.Entities.Enum;
    using Infrastructure.Services.DataTables;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    namespace WebAPI.Controllers.DataTables
    {
        [ApiController]
        [Route("api/[controller]")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
        public class NominationRewardController : ControllerBase
        {
            private readonly INominationRewardService _service;

            public NominationRewardController(INominationRewardService service)
            {
                _service = service;
            }

            // -------------------------
            // CREATE
            // -------------------------
            [HttpPost]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> Create([FromBody] NominationRewardDto createDto)
            {
                var result = await _service.CreateAsync(createDto);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result.Data);
            }

            // -------------------------
            // UPDATE
            // -------------------------
            [HttpPut("{id}")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> Update(int id, [FromBody] NominationRewardDto updateDto)
            {
                var result = await _service.UpdateAsync(id, updateDto);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result.Data);
            }

            // -------------------------
            // GET BY ID
            // -------------------------
            [HttpGet("{id}")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> GetById(int id)
            {
                var result = await _service.GetByIdAsync(id);
                if (!result.IsSuccess)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Data);
            }

            // -------------------------
            // GET COMPLETE DETAILS
            // -------------------------
            [HttpGet("complete/{id}")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> GetCompleteById(int id)
            {
                var result = await _service.GetCompleteByIdAsync(id);
                if (!result.IsSuccess)
                    return NotFound(result.ErrorMessage);

                return Ok(result.Data);
            }

            // -------------------------
            // DELETE
            // -------------------------
            [HttpDelete("{id}")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> Delete(int id)
            {
                var result = await _service.DeleteAsync(id);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result);
            }

            // -------------------------
            // SUBMIT FOR APPROVAL
            // -------------------------
            [HttpPost("{id}/submit")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> SubmitForApproval(int id)
            {
                var result = await _service.SubmitForApprovalAsync(id);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result);
            }

            // -------------------------
            // APPROVE
            // -------------------------
            [HttpPost("{id}/approve")]
            [Authorize(Roles = $"{RoleString.UnitHead}")]
            public async Task<IActionResult> Approve(int id, [FromQuery] string? remarks = null)
            {
                var result = await _service.ApproveAsync(id, remarks);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result);
            }

            // -------------------------
            // REJECT
            // -------------------------
            [HttpPost("{id}/reject")]
            [Authorize(Roles = $"{RoleString.UnitHead}")]
            public async Task<IActionResult> Reject(int id, [FromQuery] string remarks)
            {
                var result = await _service.RejectAsync(id, remarks);
                if (!result.IsSuccess)
                    return BadRequest(result.ErrorMessage);

                return Ok(result);
            }

            // -------------------------
            // GET PAGINATED
            // -------------------------
            [HttpGet("paginated")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> GetPaginated(
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10,
                [FromQuery] DateOnly? startDate = null,
                [FromQuery] DateOnly? endDate = null,
                [FromQuery] int? unitLocationId = null,
                [FromQuery] string? searchTerm = null)
            {
                var result = await _service.GetPaginatedAsync(pageNumber, pageSize, startDate, endDate, unitLocationId, searchTerm);
                return Ok(result);
            }

            // -------------------------
            // GET BY STATUS
            // -------------------------
            [HttpGet("status")]
            public async Task<IActionResult> GetByStatus(
                [FromQuery] string status,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10)
            {
                var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
                return Ok(result);
            }

            // -------------------------
            // STATUS SUMMARY
            // -------------------------
            [HttpGet("status-summary")]
            public async Task<IActionResult> GetStatusSummary()
            {
                var result = await _service.GetStatusSummaryAsync();
                return Ok(result);
            }

            [HttpGet("my-history")]
            [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Trainer}")]
            public async Task<IActionResult> GetMyHistory(
              [FromQuery] int pageNumber = 1,
              [FromQuery] int pageSize = 10)                
            {
                var result = await _service.GetTrainerHistoryAsync(pageNumber, pageSize);
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
                [FromQuery] int pageSize = 10,
                int? createdByIdFilter = null)
            {
                var result = await _service.GetPendingApprovalsAsync(pageNumber, pageSize, createdByIdFilter);
                return Ok(result);
            }

            // -------------------------
            // HYBRID ENDPOINTS
            // -------------------------

            /// <summary>
            /// HYBRID CREATE: Create nomination reward with all child entities in one request
            /// This endpoint allows you to insert parent + all child collections (IFSFarmers, FarmerInnovations, etc.) at once
            /// </summary>
            [HttpPost("hybrid")]
            [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
            public async Task<IActionResult> CreateHybrid([FromBody] NominationRewardHybridCreateDto dto)
            {
                var result = await _service.CreateHybridAsync(dto);
                if (!result.IsSuccess)
                    return StatusCode(GetStatusCode(result.ErrorStatus), result);

                return Ok(result.Data);
            }

            /// <summary>
            /// HYBRID UPDATE: Update nomination reward with all child entities in one request
            /// This endpoint allows you to update parent + manage all children (create/update/delete) at once
            /// Child items with Id = 0 or null will be created
            /// Child items with Id > 0 will be updated
            /// Child items not in the lists will be deleted
            /// </summary>
            [HttpPut("{id}/hybrid")]
            [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
            public async Task<IActionResult> UpdateHybrid(int id, [FromBody] NominationRewardHybridUpdateDto dto)
            {
                var result = await _service.UpdateHybridAsync(id, dto);
                if (!result.IsSuccess)
                    return StatusCode(GetStatusCode(result.ErrorStatus), result);

                return Ok(result.Data);
            }

            // -------------------------
            // HELPER
            // -------------------------
            private int GetStatusCode(ServiceErrorStatus? status)
            {
                return status switch
                {
                    ServiceErrorStatus.NOTFOUND => 404,
                    ServiceErrorStatus.FORBIDDEN => 403,
                    ServiceErrorStatus.BADREQUEST => 400,
                    ServiceErrorStatus.INVALIDOPERATION => 400,
                    _ => 500
                };
            }
        }
    }

}
