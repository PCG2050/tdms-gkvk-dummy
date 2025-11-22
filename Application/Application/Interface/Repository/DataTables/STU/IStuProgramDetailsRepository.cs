// IStuProgramDetailsRepository.cs

using Application.Models;
using Domain.Entities.STU;

namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuProgramDetailsRepository
    {
        Task<StuProgramDetails?> GetByIdAsync(int id);
        Task<StuProgramDetails?> GetWithDetailsAsync(int id);
        Task<StuProgramDetails> CreateAsync(StuProgramDetails entity);
        Task<StuProgramDetails> UpdateAsync(StuProgramDetails entity);
        Task DeleteAsync(int id);

        Task<PaginatedResult<StuProgramDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        Task<PaginatedResult<StuProgramDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}