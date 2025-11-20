using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Infrastructure.Services.DataTables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    /// <summary>
    /// Controller for TblService (Generic Service table)
    /// 
    /// This manages various service categories including:
    /// - Hostel accommodation
    /// - Training hall rentals
    /// - Revolving fund status
    /// - Visitor details
    /// 
    /// STATUS WORKFLOW:
    /// - Draft: Initial state, can edit
    /// - Saved: Auto-saved state (when trainer clicks "Save Next")
    /// - Pending: Submitted for Unit Head approval
    /// - Approved: Unit Head approved, cannot edit
    /// - Rejected: Unit Head rejected, can edit and resubmit
    /// 
    /// CHILD ENTITIES:
    /// - TableHostel: Hostel accommodation details
    /// - RevolvingFundStatus: Fund tracking (opening, receipt, expenditure, closing)
    /// - VisitorDetail: Visitor information and purpose
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TblServiceController : ControllerBase
    {
        private readonly ITblServiceService _service;

        public TblServiceController(ITblServiceService service)
        {
            _service = service;
        }

        // ==========================================
        // MAIN SERVICE CRUD
        // ==========================================

        /// <summary>
        /// Create new service entry (Trainer only)
        /// Initial status: Draft
        /// Can include child entities inline
        /// </summary>
        [HttpPost]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Create([FromBody] TblServiceCreateDto createDto)
        {
            var result = await _service.CreateAsync(createDto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Service created successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Update service entry (Trainer only)
        /// Can update if status is Draft, Saved, or Rejected
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Update(int id, [FromBody] TblServiceUpdateDto updateDto)
        {
            var result = await _service.UpdateAsync(id, updateDto);
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
                message = "Service updated successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Get service by ID (Trainer can view their own)
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Get complete service with all child entities (for editing)
        /// </summary>
        [HttpGet("complete/{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetCompleteById(int id)
        {
            var result = await _service.GetCompleteTblServiceAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        /// <summary>
        /// Delete service (Trainer - only Draft status)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccess)
            {
                if (result.ErrorStatus == ServiceErrorStatus.NOTFOUND)
                    return NotFound(new { message = result.ErrorMessage });

                if (result.ErrorStatus == ServiceErrorStatus.FORBIDDEN)
                    return Forbid();

                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Service deleted successfully" });
        }

        // ==========================================
        // CHILD ENTITY: TABLE HOSTEL
        // ==========================================

        /// <summary>
        /// Add hostel accommodation entry to a service
        /// </summary>
        [HttpPost("{serviceId}/hostels")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddTableHostel(int serviceId, [FromBody] TableHostelCreateDto dto)
        {
            var result = await _service.AddTableHostelAsync(serviceId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Hostel entry added successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Update hostel accommodation entry
        /// </summary>
        [HttpPut("hostels/{tableHostelId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateTableHostel(int tableHostelId, [FromBody] TableHostelCreateDto dto)
        {
            var result = await _service.UpdateTableHostelAsync(tableHostelId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Hostel entry updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete hostel accommodation entry
        /// </summary>
        [HttpDelete("hostels/{tableHostelId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteTableHostel(int tableHostelId)
        {
            var result = await _service.DeleteTableHostelAsync(tableHostelId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Hostel entry deleted successfully" });
        }

        /// <summary>
        /// Get all hostel entries for a service
        /// </summary>
        [HttpGet("{serviceId}/hostels")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetTableHostels(int serviceId)
        {
            var result = await _service.GetTableHostelsAsync(serviceId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        // ==========================================
        // CHILD ENTITY: REVOLVING FUND STATUS
        // ==========================================

        /// <summary>
        /// Add revolving fund status entry to a service
        /// </summary>
        [HttpPost("{serviceId}/revolving-funds")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddRevolvingFundStatus(int serviceId, [FromBody] RevolvingFundStatusCreateDto dto)
        {
            var result = await _service.AddRevolvingFundStatusAsync(serviceId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Revolving fund status added successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Update revolving fund status entry
        /// </summary>
        [HttpPut("revolving-funds/{fundStatusId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateRevolvingFundStatus(int fundStatusId, [FromBody] RevolvingFundStatusCreateDto dto)
        {
            var result = await _service.UpdateRevolvingFundStatusAsync(fundStatusId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Revolving fund status updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete revolving fund status entry
        /// </summary>
        [HttpDelete("revolving-funds/{fundStatusId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteRevolvingFundStatus(int fundStatusId)
        {
            var result = await _service.DeleteRevolvingFundStatusAsync(fundStatusId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Revolving fund status deleted successfully" });
        }

        /// <summary>
        /// Get all revolving fund statuses for a service
        /// </summary>
        [HttpGet("{serviceId}/revolving-funds")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetRevolvingFundStatuses(int serviceId)
        {
            var result = await _service.GetRevolvingFundStatusesAsync(serviceId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        // ==========================================
        // CHILD ENTITY: VISITOR DETAILS
        // ==========================================

        /// <summary>
        /// Add visitor detail entry to a service
        /// </summary>
        [HttpPost("{serviceId}/visitors")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> AddVisitorDetail(int serviceId, [FromBody] VisitorDetailCreateDto dto)
        {
            var result = await _service.AddVisitorDetailAsync(serviceId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Visitor detail added successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Update visitor detail entry
        /// </summary>
        [HttpPut("visitors/{visitorDetailId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> UpdateVisitorDetail(int visitorDetailId, [FromBody] VisitorDetailCreateDto dto)
        {
            var result = await _service.UpdateVisitorDetailAsync(visitorDetailId, dto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new
            {
                message = "Visitor detail updated successfully",
                data = result.Data
            });
        }

        /// <summary>
        /// Delete visitor detail entry
        /// </summary>
        [HttpDelete("visitors/{visitorDetailId}")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> DeleteVisitorDetail(int visitorDetailId)
        {
            var result = await _service.DeleteVisitorDetailAsync(visitorDetailId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Visitor detail deleted successfully" });
        }

        /// <summary>
        /// Get all visitor details for a service
        /// </summary>
        [HttpGet("{serviceId}/visitors")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetVisitorDetails(int serviceId)
        {
            var result = await _service.GetVisitorDetailsAsync(serviceId);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { data = result.Data });
        }

        // ==========================================
        // COMPOSITE CREATE/UPDATE WITH CHILDREN
        // ==========================================

        /// <summary>
        /// Create TblService with all child entities (TableHostel, RevolvingFundStatus, VisitorDetail) in a single transaction
        /// Solves the parent-child ID dependency - perfect for "Save & Next" button
        /// </summary>
        [HttpPost("with-children")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
        public async Task<IActionResult> CreateWithChildren([FromBody] TblServiceCreateDto dto)
        {
            var result = await _service.CreateWithChildrenAsync(dto);
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
                message = "Service with children created successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        /// <summary>
        /// Update TblService with all child entities using Hybrid Pattern in a single transaction
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// Perfect for "Save & Next" button with inline editing
        /// </summary>
        [HttpPut("{id}/with-children")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead}")]
        public async Task<IActionResult> UpdateWithChildren(int id, [FromBody] TblServiceWithChildrenUpdateDto dto)
        {
            var result = await _service.UpdateWithChildrenAsync(id, dto);
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
                message = "Service with children updated successfully",
                data = result.Data,
                status = result.Data.FormStatus
            });
        }

        // ==========================================
        // SUBMISSION & APPROVAL WORKFLOW
        // ==========================================

        /// <summary>
        /// Submit service for approval (Trainer only)
        /// Changes status: Draft/Saved → Pending
        /// Can only submit Draft or Saved services
        /// </summary>
        [HttpPost("{id}/submit")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> SubmitForApproval(int id)
        {
            var result = await _service.SubmitForApprovalAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Service submitted for approval successfully" });
        }

        /// <summary>
        /// Approve service (Unit Head only)
        /// Changes status: Pending → Approved
        /// </summary>
        [HttpPost("{id}/approve")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Approve(int id, [FromBody] ApprovalDto approvalDto)
        {
            var result = await _service.ApproveTblServiceAsync(id, approvalDto?.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Service approved successfully" });
        }

        /// <summary>
        /// Reject service with remarks (Unit Head only)
        /// Changes status: Pending → Rejected
        /// Remarks are required for rejection
        /// </summary>
        [HttpPost("{id}/reject")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> Reject(int id, [FromBody] ApprovalDto approvalDto)
        {
            if (string.IsNullOrWhiteSpace(approvalDto?.Remarks))
                return BadRequest(new { message = "Remarks are required for rejection" });

            var result = await _service.RejectTblServiceAsync(id, approvalDto.Remarks);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { message = "Service rejected" });
        }

        // ==========================================
        // HISTORY & DASHBOARD
        // ==========================================

        /// <summary>
        /// Get paginated history of trainer's services
        /// Shows all entries with their status (Draft, Saved, Pending, Approved, Rejected)
        /// </summary>
        [HttpGet("history")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetHistory(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? status = null)
        {
            var result = !string.IsNullOrWhiteSpace(status)
                ? await _service.GetByStatusAsync(status, pageNumber, pageSize)
                : await _service.GetPaginatedAsync(pageNumber, pageSize);

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

        /// <summary>
        /// Get status summary for dashboard (counts by status)
        /// Returns: { "Draft": 5, "Saved": 3, "Pending": 8, "Approved": 12, "Rejected": 2 }
        /// </summary>
        [HttpGet("status-summary")]
        [Authorize(Roles = RoleString.Trainer)]
        public async Task<IActionResult> GetStatusSummary()
        {
            var summary = await _service.GetStatusSummaryAsync();
            return Ok(new { data = summary });
        }

        /// <summary>
        /// Get pending services for Unit Head review
        /// </summary>
        [HttpGet("pending-review")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingReview(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _service.GetByStatusAsync("Pending", pageNumber, pageSize);
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

        // ==========================================
        // FILTERS & PAGINATION
        // ==========================================

        /// <summary>
        /// Get paginated services with filters
        /// </summary>
        [HttpGet("paginated")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int? categoryId = null,
            [FromQuery] string? searchTerm = null)
        {
            var result = await _service.GetPaginatedAsync(
                pageNumber,
                pageSize,
                startDate,
                endDate,
                categoryId,
                searchTerm);

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

        /// <summary>
        /// Get services by status with pagination
        /// </summary>
        [HttpGet("by-status")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetByStatus(
            [FromQuery] string status,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetByStatusAsync(status, pageNumber, pageSize);
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

        [HttpGet("my-history")]
        [Authorize(Roles = RoleString.Trainer)]
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
        /// <param name="createdByIdFilter">Optional: Filter by user who created the form (trainer/unit head)</param>
        /// <response code="200">Paginated list of pending approvals with creator names</response>
        [HttpGet("pending-approvals")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetPendingApprovals(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? createdByIdFilter = null)
        {
            var result = await _service.GetPendingApprovalsAsync(pageNumber, pageSize, createdByIdFilter);
            return Ok(result);
        }


    }



}