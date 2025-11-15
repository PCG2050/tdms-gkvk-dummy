// IFTIProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFTIProgramDetailsRepository
    {
        IQueryable<FTIProgramDetails> GetQueryable();
        Task<List<FTIProgramDetails>> GetAllAsync();
        Task<FTIProgramDetails?> GetByIdAsync(int id);
        Task<FTIProgramDetails?> GetWithDetailsAsync(int id);
        Task<FTIProgramDetails> CreateAsync(FTIProgramDetails entity);
        Task<FTIProgramDetails> UpdateAsync(FTIProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<FTIProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<FTIProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
