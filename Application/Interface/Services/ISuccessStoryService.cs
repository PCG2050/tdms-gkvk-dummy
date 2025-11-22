using Application.Models;
using Application.Services.Common;

namespace Application.Interface.Services
{
    public interface ISuccessStoryService
    {
        /// <summary>
        /// Get success stories from all programs (approved forms only)
        /// </summary>
        Task<PaginatedResult<SuccessStoryDto>> GetSuccessStoriesAsync(
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int pageNumber = 1,
            int pageSize = 20);
    }
}