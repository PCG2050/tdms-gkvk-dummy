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
    public class TrainerController:ControllerBase
    {
        private readonly ITrainerAssignmentService _assignmentService;
        private readonly IUserService _userService;

        public TrainerController(ITrainerAssignmentService assignmentService, IUserService userService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetTrainersOverview([FromQuery]PaginationRequest paginationRequest)
        {
            var paginatedResult = await _userService.GetPaginatedOrganizationTrainers(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(paginatedResult);
        }

        [HttpGet("Details/all")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetTrainerDetails([FromQuery] PaginationRequest paginationRequest)
        {
            var paginationResult = await _userService.GetPaginatedOrgTrainers(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(paginationResult);
        }


        [HttpGet("{trainerId}/units")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetTrainerUnits(int trainerId)
        {
            var result = await _assignmentService.GetTrainerUnits(trainerId);
            if(!result.IsSuccess)return ServiceResponseToActionResult.Error(result.ErrorMessage,result.ErrorStatus);
            return Ok(result.Data);
        }
    }
}
