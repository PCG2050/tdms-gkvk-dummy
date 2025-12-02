using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    /// <summary>
    /// Generic repository implementation for all unit program details
    /// Provides common data access operations using Entity Framework
    /// </summary>
    /// <typeparam name="TProgram">The program entity type (must inherit from ReportEntryBaseEntity)</typeparam>
    public abstract class GenericProgramRepository<TProgram> : IGenericProgramRepository<TProgram>
        where TProgram : ReportEntryBaseEntity
    {
        protected readonly TdmsDbContext Context;

        protected GenericProgramRepository(TdmsDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Override this in derived classes to return the appropriate DbSet
        /// </summary>
        protected abstract DbSet<TProgram> DbSet { get; }

        /// <summary>
        /// Override this to include navigation properties for GetWithDetailsAsync
        /// Default implementation includes common navigations
        /// </summary>
        protected virtual IQueryable<TProgram> IncludeDetails(IQueryable<TProgram> query)
        {
            return query
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District!)
                        .ThenInclude(d => d.State)
                .Include(p => p.Organization)
                .Include(p => p.CreatedBy)
                .Include(p => p.UpdatedBy)
                .Include(p => p.ApprovedBy);
        }

        /// <summary>
        /// Override this to include child collections for GetAllAsync
        /// Default returns basic entity without children
        /// </summary>
        protected virtual IQueryable<TProgram> IncludeChildren(IQueryable<TProgram> query)
        {
            return query;
        }

        public virtual IQueryable<TProgram> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual async Task<List<TProgram>> GetAllAsync()
        {
            return await IncludeChildren(DbSet)
                .AsSplitQuery()
                .ToListAsync();
        }

        public virtual async Task<TProgram?> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TProgram?> GetWithDetailsAsync(int id)
        {
            return await IncludeDetails(DbSet)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<TProgram> CreateAsync(TProgram entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public virtual async Task<TProgram> UpdateAsync(TProgram entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }

        public virtual async Task<PaginatedResult<TProgram>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? programTypeId = null,
            string? searchTerm = null)
        {
            var query = DbSet
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District!)
                        .ThenInclude(d => d.State)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .AsQueryable();

            // Apply date filters
            if (startDate.HasValue)
                query = query.Where(p => p.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(p => p.EndDate <= endDate.Value);

            // Apply search term filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = ApplySearchFilter(query, searchTerm);
            }

            // Apply program type filter (if the entity has ProgramTypeId)
            if (programTypeId.HasValue)
            {
                query = ApplyProgramTypeFilter(query, programTypeId.Value);
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TProgram>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
                //TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public virtual async Task<PaginatedResult<TProgram>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = DbSet
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.District!)
                        .ThenInclude(d => d.State)
                .Include(p => p.CreatedBy)
                .Include(p => p.ApprovedBy)
                .Where(p => unitLocationIds.Contains(p.UnitLocationId) && p.FormStatus == status);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TProgram>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                //TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }


        public virtual async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var summary = await DbSet
                .Where(p => unitLocationIds.Contains(p.UnitLocationId))
                .GroupBy(p => p.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);

            // Ensure all statuses are present
            var statuses = new[] { "Draft", "Pending", "Approved", "Rejected" };
            foreach (var status in statuses)
            {
                if (!summary.ContainsKey(status))
                    summary[status] = 0;
            }

            return summary;
        }

        /// <summary>
        /// Override this to provide custom search filtering logic
        /// Default implementation searches Title and Location fields
        /// </summary>
        protected virtual IQueryable<TProgram> ApplySearchFilter(IQueryable<TProgram> query, string searchTerm)
        {
            // Default: no filtering (override in derived classes to add specific search logic)
            return query;
        }

        /// <summary>
        /// Override this to provide program type filtering
        /// Only relevant for entities that have ProgramTypeId property
        /// </summary>
        protected virtual IQueryable<TProgram> ApplyProgramTypeFilter(IQueryable<TProgram> query, int programTypeId)
        {
            // Default: no filtering (override in derived classes if entity has ProgramTypeId)
            return query;
        }
    }
}
