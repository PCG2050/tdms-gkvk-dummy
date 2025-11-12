using Application.Models;
using Application.Models.DataTables.FIU;
using Domain.Entities.FIU;

namespace Application.Interface.Repository.DataTables.FIU
{
    public interface IFIUProgramActivityRepository
    {
        IQueryable<FIUProgramActivity> GetQueryable();
        // CRUD
        Task<FIUProgramActivity?> GetByIdAsync(int id);
        Task<FIUProgramActivity> CreateAsync(FIUProgramActivity activity);
        Task<FIUProgramActivity> UpdateAsync(FIUProgramActivity activity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);

        // Queries
        Task<PaginatedResult<FIUProgramActivity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            int? activityId = null,
            string? status = null);

        Task<List<FIUProgramActivity>> GetByUnitLocationIdAsync(int unitLocationId);
        Task<List<FIUProgramActivity>> GetByCreatedByIdAsync(int userId);
        Task<List<FIUProgramActivity>> GetByStatusAsync(List<int> unitLocationIds, string status);

        // Statistics
        Task<Dictionary<string, int>> GetStatsByActivityTypeAsync(List<int> unitLocationIds);
        Task<Dictionary<string, int>> GetStatsByStatusAsync(List<int> unitLocationIds);

        // Monthly Report
        Task<List<FIUActivitySummaryDto>> GetMonthlyActivitySummaryAsync(
            List<int> unitLocationIds,
            int year,
            int month);
    }
}