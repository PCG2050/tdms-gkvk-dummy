// IFtiProgramDetailsRepository.cs
using Application.Models;
using Application.Models.DataTables.DEU;
using Domain.Entities.DEU;
using Domain.Entities.FTI;

namespace Application.Interface.Repository.DataTables.FTI
{
    public interface IFtiProgramDetailsRepository
    {
        IQueryable<FtiProgramDetails> GetQueryable();
        Task<List<FtiProgramDetails>> GetAllAsync();
        Task<FtiProgramDetails?> GetByIdAsync(int id);
        Task<FtiProgramDetails?> GetWithDetailsAsync(int id);
        Task<FtiProgramDetails> CreateAsync(FtiProgramDetails entity);
        Task<FtiProgramDetails> UpdateAsync(FtiProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<FtiProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<FtiProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
