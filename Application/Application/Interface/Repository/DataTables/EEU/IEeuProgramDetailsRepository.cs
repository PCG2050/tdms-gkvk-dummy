// IEeuProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.EEU
{
    public interface IEeuProgramDetailsRepository
    {
        Task<EeuProgramDetails?> GetByIdAsync(int id);
        Task<EeuProgramDetails?> GetWithDetailsAsync(int id);
        Task<EeuProgramDetails> CreateAsync(EeuProgramDetails entity);
        Task<EeuProgramDetails> UpdateAsync(EeuProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<EeuProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<EeuProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}