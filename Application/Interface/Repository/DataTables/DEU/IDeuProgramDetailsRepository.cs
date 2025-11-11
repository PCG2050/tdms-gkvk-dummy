// IDeuProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.DEU;
using Domain.Entities.ASM;
using Domain.Entities.DEU;

namespace Application.Interface.Repository.DataTables.DEU
{
    public interface IDeuProgramDetailsRepository
    {
        IQueryable<DeuProgramDetails> GetQueryable();
        Task<List<DeuProgramDetails>> GetAllAsync();
        Task<DeuProgramDetails?> GetByIdAsync(int id);
        Task<DeuProgramDetails?> GetWithDetailsAsync(int id);
        Task<DeuProgramDetails> CreateAsync(DeuProgramDetails entity);
        Task<DeuProgramDetails> UpdateAsync(DeuProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<DeuProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<DeuProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}