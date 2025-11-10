using Application.Interface.Repository.DataTables.FIU;
using Application.Models;
using Domain.Entities.FIU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FIU
{
    public class FIUProgramActivityRepository : IFIUProgramActivityRepository
    {
        private readonly TdmsDbContext _context;

        public FIUProgramActivityRepository(TdmsDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // CORE CRUD OPERATIONS
        // ==========================================

        public async Task<FIUProgramActivity> AddAsync(FIUProgramActivity entity)
        {
            _context.FIUProgramActivities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FIUProgramActivity?> GetByIdAsync(int id)
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<FIUProgramActivity?> GetWithDetailsAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<FIUProgramActivity>> GetAllAsync()
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<FIUProgramActivity> UpdateAsync(FIUProgramActivity entity)
        {
            _context.FIUProgramActivities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FIUProgramActivities.FindAsync(id);
            if (entity == null) return false;

            _context.FIUProgramActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // ==========================================
        // PAGINATION & FILTERS
        // ==========================================

        public async Task<PaginatedResult<FIUProgramActivity>> GetPaginatedAsync(
            List<int> trainerIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            var query = _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => trainerIds.Contains(x.CreatedById ?? 0))
                .AsQueryable();

            // Apply filters
            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            if (unitLocationId.HasValue)
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x =>
                    (x.FIUActivities != null && x.FIUActivities.Name.Contains(searchTerm)) ||
                    (x.UnitLocation != null && x.UnitLocation.District.Name.Contains(searchTerm)));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<FIUProgramActivity>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<FIUProgramActivity>> GetByStatusAsync(
            List<int> trainerIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => trainerIds.Contains(x.CreatedById ?? 0) &&
                           x.FormStatus == status);

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<FIUProgramActivity>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> trainerIds)
        {
            var summary = await _context.FIUProgramActivities
                .Where(x => trainerIds.Contains(x.CreatedById ?? 0))
                .GroupBy(x => x.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            return summary.ToDictionary(x => x.Status, x => x.Count);
        }

        public async Task<List<FIUProgramActivity>> GetByUnitLocationAsync(int unitLocationId)
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => x.UnitLocationId == unitLocationId)
                .ToListAsync();
        }

        public async Task<List<FIUProgramActivity>> GetByCreatedByAsync(int trainerId)
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => x.CreatedById == trainerId)
                .ToListAsync();
        }
    }
}