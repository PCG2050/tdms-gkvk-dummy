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
//    /// IBTVA Demographics Controller - Phase 2: Participant Demographics
//    /// </summary>
//    [ApiController]
//    [ApiVersion("1.0")]
//    [Route("api/v{version:apiVersion}/ibtva/programs/{programId:int}/demographics")]
//    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
//    public class IbtvaDemographicsController : ControllerBase
//    {
//        private readonly IIbtvaDemographicsService _demographicsService;
//        private readonly ICurrentUserService _currentUser;

//        public IbtvaDemographicsController(
//            IIbtvaDemographicsService demographicsService,
//            ICurrentUserService currentUser)
//        {
//            _demographicsService = demographicsService;
//            _currentUser = currentUser;
//        }

//        /// <summary>
//        /// Create demographic entry (Phase 2)
//        /// </summary>
//        /// <response code="201">Demographic entry created successfully</response>
//        /// <response code="400">Invalid request data</response>
//        [HttpPost]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.Created)]
//        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
//        public async Task<IActionResult> Create(int programId, [FromBody] DemographicsCreateDto createDto)
//        {
//            try
//            {
//                var result = await _demographicsService.CreateAsync(programId, createDto, _currentUser.UserId);

//                return StatusCode((int)HttpStatusCode.Created, new
//                {
//                    success = true,
//                    message = "Phase 2 demographic entry saved successfully",
//                    data = result,
//                    nextPhase = 3
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode((int)HttpStatusCode.InternalServerError, new
//                {
//                    success = false,
//                    error = "An error occurred while creating demographic entry",
//                    details = ex.Message
//                });
//            }
//        }

//        /// <summary>
//        /// Get demographic by ID
//        /// </summary>
//        /// <response code="200">Demographic retrieved successfully</response>
//        /// <response code="404">Demographic not found</response>
//        [HttpGet("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> GetById(int programId, int id)
//        {
//            var demographic = await _demographicsService.GetByIdAsync(id, programId);

//            if (demographic == null)
//                return StatusCode((int)HttpStatusCode.NotFound, new
//                {
//                    success = false,
//                    error = "Demographic entry not found"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                data = demographic
//            });
//        }

//        /// <summary>
//        /// Get all demographics for program
//        /// </summary>
//        /// <response code="200">Demographics retrieved successfully</response>
//        [HttpGet]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        public async Task<IActionResult> GetAll(int programId)
//        {
//            var demographics = await _demographicsService.GetAllByProgramAsync(programId);

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                data = demographics,
//                count = demographics.Count()
//            });
//        }

//        /// <summary>
//        /// Update demographic entry
//        /// </summary>
//        /// <response code="200">Demographic updated successfully</response>
//        /// <response code="404">Demographic not found</response>
//        [HttpPut("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> Update(int programId, int id, [FromBody] DemographicsCreateDto updateDto)
//        {
//            var result = await _demographicsService.UpdateAsync(id, programId, updateDto, _currentUser.UserId);

//            if (result == null)
//                return StatusCode((int)HttpStatusCode.NotFound, new
//                {
//                    success = false,
//                    error = "Demographic entry not found"
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                message = "Demographic entry updated successfully",
//                data = result
//            });
//        }

//        /// <summary>
//        /// Delete demographic entry
//        /// </summary>
//        /// <response code="200">Demographic deleted successfully</response>
//        /// <response code="404">Demographic not found</response>
//        [HttpDelete("{id:int}")]
//        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
//        [ProducesResponseType((int)HttpStatusCode.NotFound)]
//        public async Task<IActionResult> Delete(int programId, int id)
//        {
//            var success = await _demographicsService.DeleteAsync(id, programId);

//            if (!success)
//                return StatusCode((int)HttpStatusCode.NotFound, new
//                {
//                    success = false,
//                    error = "Demographic entry not found
//                });

//            return StatusCode((int)HttpStatusCode.OK, new
//            {
//                success = true,
//                message = "Demographic entry deleted successfully"
//            });
//        }
//    }
//}