using Application.Models;
using Domain.Entities;

namespace Application.Interface.Repository
{
    /// <summary>
    /// Generic repository interface for all unit program details (FTI, STU, ATIC, DEU, EEU, NAEP, KVK)
    /// Provides common data access operations for program entities
    /// </summary>
    /// <typeparam name="TProgram">The program entity type (must inherit from ReportEntryBaseEntity)</typeparam>
    public interface IGenericProgramRepository<TProgram> where TProgram : ReportEntryBaseEntity
    {
        /// <summary>
        /// Get queryable for advanced filtering
        /// </summary>
        IQueryable<TProgram> GetQueryable();

        /// <summary>
        /// Get all programs
        /// </summary>
        Task<List<TProgram>> GetAllAsync();

        /// <summary>
        /// Get program by ID (without related entities)
        /// </summary>
        Task<TProgram?> GetByIdAsync(int id);

        /// <summary>
        /// Get program by ID with all child entities included
        /// </summary>
        Task<TProgram?> GetWithDetailsAsync(int id);

        /// <summary>
        /// Create a new program
        /// </summary>
        Task<TProgram> CreateAsync(TProgram entity);

        /// <summary>
        /// Update an existing program
        /// </summary>
        Task<TProgram> UpdateAsync(TProgram entity);

        /// <summary>
        /// Delete a program by ID
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Get paginated programs with filtering
        /// </summary>
        Task<PaginatedResult<TProgram>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null);

        /// <summary>
        /// Get programs by form status (Draft, Pending, Approved, Rejected)
        /// </summary>
        Task<PaginatedResult<TProgram>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10);

        /// <summary>
        /// Get status summary counts for a unit
        /// </summary>
        Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds);
    }
}
