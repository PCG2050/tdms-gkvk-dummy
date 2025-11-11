// INaepProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.DEU;
using Domain.Entities.ASM;
using Domain.Entities.NAEP;

namespace Application.Interface.Repository.DataTables.NAEP
{
    public interface INaepProgramDetailsRepository
    {
        IQueryable<NaepProgramDetails> GetQueryable();
        Task<List<NaepProgramDetails>> GetAllAsync();
        Task<NaepProgramDetails?> GetByIdAsync(int id);
        Task<NaepProgramDetails?> GetWithDetailsAsync(int id);
        Task<NaepProgramDetails> CreateAsync(NaepProgramDetails entity);
        Task<NaepProgramDetails> UpdateAsync(NaepProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<NaepProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<NaepProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}