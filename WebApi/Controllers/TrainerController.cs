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
        private readonly ICurrentUserService _currentUser;
      

        public TrainerController(ITrainerAssignmentService assignmentService, IUserService userService, ICurrentUserService currentUser)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _currentUser = currentUser;
        }

        [HttpGet]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> GetTrainersOverview([FromQuery]PaginationRequest paginationRequest)
        {
            var paginatedResult = await _userService.GetPaginatedOrganizationTrainers(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(paginatedResult);
        }

        [HttpGet("PaginatedDetails/all")]
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

        //new one
        [HttpGet("all")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> GetTrainersDetails()
        {
            var paginatedResult = await _userService.GetPaginatedTrainers();
            return Ok(paginatedResult);
        }

        [HttpDelete("{trainerId}")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<IActionResult> DeleteTrainer(int trainerId)
        {
            var result = await _userService.DeleteTrainerAsync(trainerId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

    }
}
