
using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        
        [HttpGet("{unitHeadId}/units")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetUnitHeadUnits(int unitHeadId)
        {
            var result = await _assignmentService.GetUnitHeadUnits(unitHeadId);
            if (!result.IsSuccess) return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            return Ok(result.Data);
        }
    }
}

