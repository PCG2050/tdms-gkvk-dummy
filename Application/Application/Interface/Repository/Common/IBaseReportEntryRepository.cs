using Application.Models;
using Domain.Entities;

namespace Application.Interface.Repository.Common
{
    /// <summary>
    /// Generic repository interface for all entities that inherit from ReportEntryBaseEntity.
    /// Provides common CRUD operations, pagination, and status-based queries.
    /// </summary>
    /// <typeparam name="TEntity">Entity type that inherits from ReportEntryBaseEntity</typeparam>
    public interface IBaseReportEntryRepository<TEntity> where TEntity : ReportEntryBaseEntity
    {
        // ============================
        // BASIC CRUD OPERATIONS
        // ============================

        /// <summary>
        /// Gets an entity by its ID (simple query without includes)
        /// </summary>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Gets an entity by its ID with all related entities (eager loading)
        /// </summary>
        Task<TEntity?> GetWithDetailsAsync(int id);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity by its ID
        /// </summary>
        Task DeleteAsync(int id);

        // ============================
        // PAGINATION & FILTERING
        // ============================

        /// <summary>
        /// Gets paginated results with optional filtering
        /// </summary>
        Task<PaginatedResult<TEntity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            string? searchTerm = null);

        /// <summary>
        /// Gets paginated results filtered by form status
        /// </summary>
        Task<PaginatedResult<TEntity>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        // ============================
        // STATUS & SUMMARY
        // ============================

        /// <summary>
        /// Gets a summary count of records grouped by status
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
