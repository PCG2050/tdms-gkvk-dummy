

namespace Application.Interface.Repository.DataTables.ConsultSocialMedia
{
    public interface IConsultingServiceRepository
    {
        /// <summary>
        /// Get queryable for complex filtering (used by history service)
        /// </summary>
        IQueryable<ConsultingAndSocialMediaService> GetQueryable();
        Task<List<ConsultingAndSocialMediaService>> GetAllAsync();
        Task<ConsultingAndSocialMediaService?> GetByIdAsync(int id);
        Task<ConsultingAndSocialMediaService?> GetWithDetailsAsync(int id);
        Task<ConsultingAndSocialMediaService> CreateAsync(ConsultingAndSocialMediaService entity);
        Task<ConsultingAndSocialMediaService> UpdateAsync(ConsultingAndSocialMediaService entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<ConsultingAndSocialMediaService>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null);

        Task<PaginatedResult<ConsultingAndSocialMediaService>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
