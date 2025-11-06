using Application.Models;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public abstract class GenericRepository<TEntity,TDto> : IGenericRepository<TEntity,TDto>
    where TEntity : ReportEntryBaseEntity
    where TDto : class
    {
        protected readonly TdmsDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(TdmsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task<PaginatedResult<TDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber, QueryFilter? queryFilter = null, int pageSize = 0)
        {
            var query = _dbSet.Where(x => x.OrganizationId == organizationId);

            if (queryFilter?.Filters.Count > 0)
            {
                var filters = queryFilter.Filters;
                if (filters.ContainsKey("CreatedById") && Int32.TryParse(filters["CreatedById"].ToString(), out int trainerId)) query = query.Where(x => x.CreatedById == trainerId);
            }

            var paginatedResult = new PaginatedResult<TDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;

            // Use the abstract method to project to DTO
            paginatedResult.Items = await ProjectToDto(query.Skip(offset).Take(pageSize)).ToListAsync();

            return paginatedResult;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Modified)
                await _context.SaveChangesAsync();
            return entity;
        }

        protected abstract IQueryable<TDto> ProjectToDto(IQueryable<TEntity> query);
    }
}
