using Application.Interface.Services;
using Application.Interface.Services.DataTables;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.DataTables
{
    [ApiController]
    [Route("api/publications")]
    [Authorize(Roles = RoleString.Trainer)]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePublication([FromBody] PublicationCreateDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _publicationService.CreateAsync(createDto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return CreatedAtAction(nameof(GetPublication), new { id = result.Data.Id }, result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublication(int id)
        {
            var result = await _publicationService.GetByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublication(int id, [FromBody] PublicationUpdateDto updateDto)
        {
            var result = await _publicationService.UpdateAsync(id, updateDto);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication(int id)
        {
            var result = await _publicationService.DeleteAsync(id);
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetPublications(
            [FromQuery] int? unitLocationId = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null)
        {
            var result = await _publicationService.GetPaginatedAsync(
                unitLocationId,
                pageNumber,
                pageSize,
                startDate,
                endDate);

            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        // Master data endpoints
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _publicationService.GetCategoriesAsync();
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet("sources")]
        public async Task<IActionResult> GetSources()
        {
            var result = await _publicationService.GetSourcesAsync();
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet("modes")]
        public async Task<IActionResult> GetModes()
        {
            var result = await _publicationService.GetModesAsync();
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet("regions")]
        public async Task<IActionResult> GetRegions()
        {
            var result = await _publicationService.GetRegionsAsync();
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }

        [HttpGet("extension-literatures")]
        public async Task<IActionResult> GetExtensionLiteratures()
        {
            var result = await _publicationService.GetExtensionLiteraturesAsync();
            if (!result.IsSuccess)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result.Data);
        }
    }
}