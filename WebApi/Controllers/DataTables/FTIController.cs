using Application;
using Application.Interface;
using Application.Interface.Services;
using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Domain.Entities.FTI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    public class FTIController : ControllerBase
    {
        private readonly IFtiTrainingProgrammeService _trainingProgrammeService;
        private readonly IFtiOtherActivityService _otherActivityService;

        public FTIController(IFtiTrainingProgrammeService trainingProgrammeService, IFtiOtherActivityService otherActivityService)
        {
            _trainingProgrammeService = trainingProgrammeService;
            _otherActivityService = otherActivityService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            var routes = new Dictionary<string, string>
            {
                {
                    "Training Programme Overview",
                    Url.Action(nameof(GetAllTrainingProgrammeEntries))??string.Empty
                },
                {
                    "Any Other Activities",
                    Url.Action(nameof(GetAllOtherActivitiesEntries))??string.Empty
                }
            };
            return Ok(routes);
        }
        [HttpGet("training-programmes")]
        [ProducesResponseType(typeof(PaginatedResult<FtiTrainingProgram>),200)]
        public async Task<IActionResult> GetAllTrainingProgrammeEntries([FromQuery]PaginationRequest paginationRequest)
        {
            return Ok(await _trainingProgrammeService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [HttpPost("training-programmes")]
        public async Task<ActionResult<FtiTrainingProgram>> AddEntry(CreateFtiTrainingProgrammeEntryDto createDto)
        {
            var entry = await _trainingProgrammeService.AddAsync(createDto);
            return Ok(entry);
        }

        [HttpPatch("training-programmes/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, UpdateTrainingProgrammeEntryDto updateDto)
        {
            await _trainingProgrammeService.UpdateAsync(updateDto);
            return NoContent();
        }
        [HttpDelete("training-programmes/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _trainingProgrammeService.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("other-activities")]
        [ProducesResponseType(typeof(PaginatedResult<FtiOtherActivityDto>), 200)]
        public async Task<IActionResult> GetAllOtherActivitiesEntries([FromQuery] PaginationRequest paginationRequest)
        {
            return Ok(await _otherActivityService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [HttpPost("other-activities")]
        public async Task<IActionResult> AddOAEntry(FtiOtherActivityCreateDto createDto)
        {
            var entry = await _otherActivityService.AddAsync(createDto);
            return Ok(entry);
        }

        [HttpPatch("other-activities/{id}")]
        public async Task<IActionResult> UpdateOAEntry(int id, FtiOtherActivityUpdateDto updateDto)
        {
            var updatedEntry = await _otherActivityService.UpdateAsync(updateDto);
            return Ok(updatedEntry);
        }
        [HttpDelete("other-activities/{id}")]
        public async Task<IActionResult> DeleteOAEntry(int id)
        {
            await _otherActivityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
