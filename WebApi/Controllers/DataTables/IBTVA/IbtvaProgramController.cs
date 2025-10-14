//using Application.Interface;
//using Application.Interface.Services.DataTables;
//using Application.Interface.Services.DataTables.IBTVA;
//using Application.Models.DataTables;
//using Application.Models.DataTables.IBTVA;
//using Domain.Entities.Enum;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Net;

//namespace WebApi.Controllers.DataTables.IBTVA
//{
//    /// <summary>
//    /// IBTVA Program Controller - Phase 1: Program Details
//    /// </summary>
//    [ApiController]
//    [ApiVersion("1.0")]
//    [Route("api/v{version:apiVersion}/ibtva/programs")]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    public class IbtvaProgramController : ControllerBase
//    {
//        private readonly IIbtvaProgramService _programService;
//        private readonly ICurrentUserService _currentUser;

//        public IbtvaProgramController(
//            IIbtvaProgramService programService,
//            ICurrentUserService currentUser)
//        {
//            _programService = programService;
//            _currentUser = currentUser;
//        }

//        /// <summary>
//        /// Create new IBTVA program (Phase 1)
//        /// </summary>
//        /// <response code="201">Program created successfully</response>
//        /// <response code="400">Invalid request data</response>
//        /// <response code="401">Unauthorized</response>
//        [HttpPost]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.Created)]
//        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> Create([FromBody] IbtvaProgramCreateDto createDto)
//        {
//            try
//            {
//                var result = await _programService.CreateAsync(
//                    createDto,
//                    _currentUser.UserId,
//                    _currentUser.OrganizationId);

//                return StatusCode((int)HttpStatusCode.Created, new
//                {
//                    success = true,
//                    message = "Phase 1 saved successfully. You can now proceed to Phase 2.",
//                    data = result,
//                    nextPhase = 2
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode((int)HttpStatusCode.InternalServerError, new
//                {
//                    success = false,
//                    error = "An error occurred while creating the program",
//                    details = ex.Message
//                });
//            }
//        }

//        /// <summary>
//        /// Get IBTVA program by ID
//        /// </summary>
//        /// <response code="200">Program retrieved successfully</response>
//        /// <response code="404">Program not found</response>
//        [HttpGet("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var program = await _programService.GetByIdAsync(id, _currentUser.UserId);

//            if (program == null)
//                return StatusCode((int)HttpStatusCode.NotFound, new
//                {
//                    success = false,
//                    error = "Program not found or you don't have access to it"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                data = program
//            });
//        }

//        /// <summary>
//        /// Get all IBTVA programs for current trainer
//        /// </summary>
//        /// <response code="200">Programs retrieved successfully</response>
//        [HttpGet]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        public async Task<IActionResult> GetAll()
//        {
//            var programs = await _programService.GetAllAsync(_currentUser.UserId);

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                data = programs,
//                count = programs.Count()
//            });
//        }

//        /// <summary>
//        /// Update IBTVA program (Phase 1)
//        /// </summary>
//        /// <response code="200">Program updated successfully</response>
//        /// <response code="404">Program not found</response>
//        /// <response code="400">Cannot update approved program</response>
//        [HttpPut("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> Update(int id, [FromBody] IbtvaProgramUpdateDto updateDto)
//        {
//            var result = await _programService.UpdateAsync(id, updateDto, _currentUser.UserId);

//            if (result == null)
//                return StatusCode((int)HttpStatusCode.NotFound, new
//                {
//                    success = false,
//                    error = "Program not found, already approved, or you don't have access to it"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                message = "Phase 1 updated successfully",
//                data = result
//            });
//        }

//        /// <summary>
//        /// Delete IBTVA program
//        /// </summary>
//        /// <response code="200">Program deleted successfully</response>
//        /// <response code="400">Cannot delete submitted/approved program</response>
//        /// <response code="404">Program not found</response>
//        [HttpDelete("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var success = await _programService.DeleteAsync(id, _currentUser.UserId);

//            if (!success)
//                return StatusCode((int)HttpStatusCode.BadRequest, new
//                {
//                    success = false,
//                    error = "Cannot delete program. It may be submitted/approved or you don't have access"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                message = "Program deleted successfully"
//            });
//        }

//        /// <summary>
//        /// Submit program for approval
//        /// </summary>
//        /// <response code="200">Program submitted successfully</response>
//        /// <response code="400">Program already submitted or approved</response>
//        [HttpPost("{id:int}/submit")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> Submit(int id)
//        {
//            var success = await _programService.SubmitAsync(id, _currentUser.UserId);

//            if (!success)
//                return StatusCode((int)HttpStatusCode.BadRequest, new
//                {
//                    success = false,
//                    error = "Cannot submit program. It may already be submitted/approved or not found"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                message = "Program submitted for approval successfully",
//                formStatus = "Pending"
//            });
//        }
//    }
//}
