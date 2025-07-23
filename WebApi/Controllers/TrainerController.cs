using Application.Interface;
using Domain.Entities.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class TrainerController:ControllerBase
    {
        private readonly ITrainerAssignmentService _assignmentService;

        public TrainerController(ITrainerAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("{trainerId}/units")]
        [Authorize(Roles = $"{RoleString.Trainer},{RoleString.Admin}")]
        public async Task<IActionResult> GetTrainerUnits(int trainerId)
        {
            var result = await _assignmentService.GetTrainerUnits(trainerId);
            if(!result.IsSuccess)return ServiceResponseToActionResult.Error(result.ErrorMessage,result.ErrorStatus);
            return Ok(result.Data);
        }
    }
}
