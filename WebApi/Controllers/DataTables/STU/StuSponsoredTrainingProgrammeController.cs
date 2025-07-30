using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.STU
{
    [ApiController]
    [Route("api/STU/sponsored-training-programme")]
    public class StuSponsoredTrainingProgrammeController : ControllerBase
    {
        private readonly IStuTrainingProgrammeService _trainingProgrammeService;

        public StuSponsoredTrainingProgrammeController(IStuTrainingProgrammeService trainingProgrammeService)
        {
            _trainingProgrammeService = trainingProgrammeService;
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpGet]
        public async Task<IActionResult> GetAllOtherActivitiesEntries([FromQuery] PaginationRequest paginationRequest)
        {
            return Ok(await _trainingProgrammeService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPost]
        public async Task<IActionResult> AddEntry(StuTrainingProgrammeCreateDto createDto)
        {
            var entry = await _trainingProgrammeService.AddAsync(createDto);
            return Ok(entry);
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, StuTrainingProgrammeUpdateDto updateDto)
        {
            var updatedEntry = await _trainingProgrammeService.UpdateAsync(updateDto);
            return Ok(updatedEntry);
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            await _trainingProgrammeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
