using Application.Models;
using Domain.Entities.GenericTables;

namespace Application.Interface.Repository.DataTables
{
    public interface IPublicationRepository
    {
        Task<Publication?> GetByIdAsync(int id);
        Task<Publication?> GetWithDetailsAsync(int id);
        Task<Publication> CreateAsync(Publication publication);
        Task<Publication> UpdateAsync(Publication publication);
        Task DeleteAsync(int id);

        Task<PaginatedResult<Publication>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null);

        Task<PaginatedResult<Publication>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}