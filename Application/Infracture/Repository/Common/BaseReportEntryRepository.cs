using Application.Interface.Repository.Common;
using Application.Models;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Common
{
    /// <summary>
    /// Generic base repository implementation for all ReportEntryBaseEntity types.
    /// Provides common CRUD operations, pagination, and status queries.
    /// Derived repositories can override methods to add custom behavior.
    /// </summary>
    /// <typeparam name="TEntity">Entity type that inherits from ReportEntryBaseEntity</typeparam>
    public abstract class BaseReportEntryRepository<TEntity> : IBaseReportEntryRepository<TEntity>
        where TEntity : ReportEntryBaseEntity
    {
        protected readonly TdmsDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseReportEntryRepository(TdmsDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // ============================
        // BASIC CRUD OPERATIONS
        // ============================

        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<TEntity?> GetWithDetailsAsync(int id)
        {
            var query = _dbSet.AsQueryable();

            // Apply common includes (UnitLocation, Organization, Users)
            query = ApplyCommonIncludes(query);

            // Apply entity-specific includes (override in derived classes)
            query = ApplyEntityIncludes(query);

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            // Reload with details
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ============================
        // PAGINATION & FILTERING
        // ============================

        public virtual async Task<PaginatedResult<TEntity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            string? searchTerm = null)
        {
            var query = _dbSet.AsQueryable();

            // Apply common includes for list view
            query = ApplyCommonIncludes(query);

            // Filter by unit locations (multi-tenancy)
            query = query.Where(e => unitLocationIds.Contains(e.UnitLocationId));

            // Apply date range filters
            if (startDate.HasValue)
                query = query.Where(e => e.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.EndDate <= endDate.Value);

            // Apply search term filter (override in derived classes for entity-specific search)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = ApplySearchFilter(query, searchTerm);
            }

            // Apply entity-specific filters (override in derived classes)
            query = ApplyAdditionalFilters(query);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TEntity>(items, totalItems, pageNumber, pageSize);
        }

        public virtual async Task<PaginatedResult<TEntity>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _dbSet
                .Where(e => unitLocationIds.Contains(e.UnitLocationId) && e.FormStatus == status);

            // Apply common includes
            query = ApplyCommonIncludes(query);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TEntity>(items, totalItems, pageNumber, pageSize);
        }

        // ============================
        // STATUS & SUMMARY
        // ============================

        public virtual async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            return await _dbSet
                .Where(e => unitLocationIds.Contains(e.UnitLocationId))
                .GroupBy(e => e.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        // ============================
        // PROTECTED HELPERS (Override in derived classes)
        // ============================

        /// <summary>
        /// Applies common includes for UnitLocation, Organization, and User relationships.
        /// Can be overridden to customize common includes.
        /// </summary>
        protected virtual IQueryable<TEntity> ApplyCommonIncludes(IQueryable<TEntity> query)
        {
            return query
                .Include(e => e.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(e => e.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(e => e.Organization)
                .Include(e => e.CreatedBy)
                .Include(e => e.UpdatedBy)
                .Include(e => e.ApprovedBy);
        }

        /// <summary>
        /// Override this method to add entity-specific includes (navigation properties).
        /// Example: .Include(e => e.ParticipantDemographics)
        /// </summary>
        protected virtual IQueryable<TEntity> ApplyEntityIncludes(IQueryable<TEntity> query)
        {
            // Default: no additional includes
            // Override in derived repositories to add entity-specific navigation properties
            return query;
        }

        /// <summary>
        /// Override this method to implement entity-specific search logic.
        /// Default implementation does no filtering.
        /// </summary>
        protected virtual IQueryable<TEntity> ApplySearchFilter(IQueryable<TEntity> query, string searchTerm)
        {
            // Default: no search filter
            // Override in derived repositories for entity-specific search
            return query;
        }

        /// <summary>
        /// Override this method to add additional entity-specific filters.
        /// Example: Filter by program type, category, etc.
        /// </summary>
        protected virtual IQueryable<TEntity> ApplyAdditionalFilters(IQueryable<TEntity> query)
        {
            // Default: no additional filters
            // Override in derived repositories
            return query;
        }
    }
}
