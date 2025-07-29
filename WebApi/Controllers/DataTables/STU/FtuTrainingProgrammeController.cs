using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.STU
{
    [ApiController]
    [Route("api/STU/training-programme")]
    public class FtuTrainingProgrammeController : ControllerBase
    {
        private readonly IStuTrainingProgrammeService _trainingProgrammeService;

        public FtuTrainingProgrammeController(IStuTrainingProgrammeService trainingProgrammeService)
        {
            _trainingProgrammeService = trainingProgrammeService;
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpGet("other-activities")]
        public async Task<IActionResult> GetAllOtherActivitiesEntries([FromQuery] PaginationRequest paginationRequest)
        {
            return Ok(await _trainingProgrammeService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPost("other-activities")]
        public async Task<IActionResult> AddEntry(StuTrainingProgrammeCreateDto createDto)
        {
            var entry = await _trainingProgrammeService.AddAsync(createDto);
            return Ok(entry);
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPatch("other-activities/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, StuTrainingProgrammeUpdateDto updateDto)
        {
            var updatedEntry = await _trainingProgrammeService.UpdateAsync(updateDto);
            return Ok(updatedEntry);
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpDelete("other-activities/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _trainingProgrammeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
