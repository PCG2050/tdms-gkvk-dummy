
using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UnitHeadController : ControllerBase
    {
        private readonly IUnitHeadAssignmentService _assignmentService;
        private readonly IUserService _userService;

        public UnitHeadController(IUnitHeadAssignmentService assignmentService, IUserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<IActionResult> GetUnitHeadOverview([FromQuery] PaginationRequest paginationRequest)
        {
            var paginatedResult = await _userService.GetPaginatedOrganizationUnitHeads(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(paginatedResult);
        }

        //new one
        [HttpGet("Details/all")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<IActionResult> GetUnitHeadDetails()
        {
            var paginatedResult = await _userService.GetPaginatedOrgUnitHeads();
            return Ok(paginatedResult);
        }



        [HttpGet("{unitHeadId}/units")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetUnitHeadUnits(int unitHeadId)
        {
            var result = await _assignmentService.GetUnitHeadUnits(unitHeadId);
            if (!result.IsSuccess) return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            return Ok(result.Data);
        }

        //Get all trainers created bya a specific Unithead
        [HttpGet("{unitHeadId}/trainers/all")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetAllTrainersCreatedByUnitHead(int unitHeadId)
        {
            try
            {
                var trainers = await _userService.GetAllTrainersCreatedByUnitHead(unitHeadId);
                return Ok(trainers);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: " An error occured while retreiving trainers");
            }
        }
    }
}

