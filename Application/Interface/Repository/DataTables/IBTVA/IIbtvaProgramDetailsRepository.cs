// IIbtvaProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.IBTVA;
using Domain.Entities.IBTVA;

namespace Application.Interface.Repository.DataTables.IBTVA
{
    public interface IIbtvaProgramDetailsRepository
    {
        Task<IbtvaProgramDetails?> GetByIdAsync(int id);
        Task<IbtvaProgramDetails?> GetWithDetailsAsync(int id);
        Task<IbtvaProgramDetails> CreateAsync(IbtvaProgramDetails entity);
        Task<IbtvaProgramDetails> UpdateAsync(IbtvaProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<IbtvaProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<IbtvaProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}