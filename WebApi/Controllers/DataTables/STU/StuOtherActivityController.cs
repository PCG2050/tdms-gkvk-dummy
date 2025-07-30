using Application.Interface.Services.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables.STU
{
    [ApiController]
    [Route("api/STU/other-activity")]
    public class StuOtherActivityController : ControllerBase
    {
        private readonly IStuOtherActivityService _otherActivityService;

        public StuOtherActivityController(IStuOtherActivityService otherActivityService)
        {
            _otherActivityService = otherActivityService;
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpGet]
        public async Task<IActionResult> GetAllOtherActivitiesEntries([FromQuery] PaginationRequest paginationRequest)
        {
            return Ok(await _otherActivityService.GetPaginatedItemsAsync(paginationRequest.PageNumber, paginationRequest.PageSize));
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPost]
        public async Task<IActionResult> AddOAEntry(StuOtherActivityCreateDto createDto)
        {
            var entry = await _otherActivityService.AddAsync(createDto);
            return Ok(entry);
        }

        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateOAEntry(int id, StuOtherActivityUpdateDto updateDto)
        {
            var updatedEntry = await _otherActivityService.UpdateAsync(updateDto);
            return Ok(updatedEntry);
        }
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.Trainer}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOAEntry(int id)
        {
            await _otherActivityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
