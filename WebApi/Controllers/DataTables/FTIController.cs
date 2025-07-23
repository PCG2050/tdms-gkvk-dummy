using Application;
using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/[controller]")]
    public class FTIController : ControllerBase
    {
        private readonly IFtiTrainingProgrammeService _trainingProgrammeService;

        public FTIController(IFtiTrainingProgrammeService trainingProgrammeService)
        {
            _trainingProgrammeService = trainingProgrammeService;
        }
        [Authorize(Roles =$"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpGet("training-programmes")]
        public async Task<IActionResult> GetAllTrainingProgrammeEntries(int pageNumber = Constants.PAGINATION_PAGEN_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            return Ok(await _trainingProgrammeService.GetEtries(pageNumber, pageSize));
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPost("training-programmes")]
        public async Task<IActionResult> AddEntry(CreateFtiTrainingProgrammeEntryDto createDto)
        {
            var entry = await _trainingProgrammeService.AddEntry(createDto);
            return Ok(entry);
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPatch("training-programmes/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, UpdateTrainingProgrammeEntryDto updateDto)
        {
            await _trainingProgrammeService.UpdateEntry(updateDto);
            return NoContent();
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpDelete("training-programmes/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _trainingProgrammeService.DeleteEntry(id);
            return NoContent();
        }
    }
}
