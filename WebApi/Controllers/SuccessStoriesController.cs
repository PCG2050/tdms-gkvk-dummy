using Application.Interface.Services;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All authenticated users can access
    public class SuccessStoriesController : ControllerBase
    {
        private readonly ISuccessStoryService _successStoryService;

        public SuccessStoriesController(ISuccessStoryService successStoryService)
        {
            _successStoryService = successStoryService;
        }

        /// <summary>
        /// Get success stories from all programs (approved forms with success story content)
        /// </summary>
        /// <param name="startDate">Filter by start date</param>
        /// <param name="endDate">Filter by end date</param>
        /// <param name="pageNumber">Page number (default: 1)</param>
        /// <param name="pageSize">Items per page (default: 20)</param>
        /// <returns>Paginated list of success stories</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<SuccessStoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSuccessStories(
            [FromQuery] DateOnly? startDate = null,
            [FromQuery] DateOnly? endDate = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var result = await _successStoryService.GetSuccessStoriesAsync(
                startDate,
                endDate,
                pageNumber,
                pageSize);

            return Ok(result);
        }
    }
}
