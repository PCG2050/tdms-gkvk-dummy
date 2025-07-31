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
    [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead},{RoleString.Trainer}")]
    public class StuTrainingProgrammeController : ControllerBase
    {
        private readonly IStuTrainingProgrammeService _trainingProgrammeService;

        public StuTrainingProgrammeController(IStuTrainingProgrammeService trainingProgrammeService)
        {
            _trainingProgrammeService = trainingProgrammeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] PaginationRequest paginationRequest)
        {
            return Ok(await _trainingProgrammeService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [HttpPost]
        public async Task<IActionResult> AddEntry(StuTrainingProgrammeCreateDto createDto)
        {
            var entry = await _trainingProgrammeService.AddAsync(createDto);
            return Ok(entry);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, StuTrainingProgrammeUpdateDto updateDto)
        {
            var updatedEntry = await _trainingProgrammeService.UpdateAsync(updateDto);
            return Ok(updatedEntry);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _trainingProgrammeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
