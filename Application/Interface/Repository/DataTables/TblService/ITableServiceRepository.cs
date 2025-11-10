using System.Linq;

namespace Application.Interface.Repository.DataTables.TblService
{
    public interface ITableServiceRepository
    {
        // ============================
        // CORE CRUD OPERATIONS
        // ============================

        IQueryable<Domain.Entities.GenericTables.Service.TblService> GetQueryable();
        Task<List<Domain.Entities.GenericTables.Service.TblService>> GetAllAsync();
        Task<Domain.Entities.GenericTables.Service.TblService?> GetByIdAsync(int id);
        Task<Domain.Entities.GenericTables.Service.TblService?> GetWithDetailsAsync(int id);
        Task<Domain.Entities.GenericTables.Service.TblService> CreateAsync(Domain.Entities.GenericTables.Service.TblService entity);
        Task<Domain.Entities.GenericTables.Service.TblService> UpdateAsync(Domain.Entities.GenericTables.Service.TblService entity);
        Task DeleteAsync(int id);

        // ============================
        // PAGINATION & FILTERS
        // ============================

        Task<PaginatedResult<Domain.Entities.GenericTables.Service.TblService>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null);

        Task<PaginatedResult<Domain.Entities.GenericTables.Service.TblService>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
