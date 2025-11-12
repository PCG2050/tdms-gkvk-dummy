using Application.Interface.Repository.DataTables.FIU;
using Application.Models;
using Application.Models.DataTables.FIU;
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

        public IQueryable<FIUProgramActivity> GetQueryable()
        {
            return _context.FIUProgramActivities.AsQueryable();
        }

        public async Task<FIUProgramActivity?> GetByIdAsync(int id)
        {
            return await _context.FIUProgramActivities
                .Include(a => a.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(a => a.Organization)
                .Include(a => a.FIUActivity)
                .Include(a => a.CreatedBy)
                .Include(a => a.ApprovedBy)
                .Include(a => a.UpdatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<FIUProgramActivity> CreateAsync(FIUProgramActivity activity)
        {
            await _context.FIUProgramActivities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(activity.Id) ?? activity;
        }

        public async Task<FIUProgramActivity> UpdateAsync(FIUProgramActivity activity)
        {
            _context.FIUProgramActivities.Update(activity);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(activity.Id) ?? activity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var activity = await _context.FIUProgramActivities.FindAsync(id);
            if (activity == null) return false;

            _context.FIUProgramActivities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.FIUProgramActivities.AnyAsync(a => a.Id == id);
        }

        public async Task<PaginatedResult<FIUProgramActivity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            int? activityId = null,
            string? status = null)
        {
            var query = _context.FIUProgramActivities
                .Include(a => a.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(a => a.FIUActivity)
                .Include(a => a.CreatedBy)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId))
                .AsQueryable();

            if (activityId.HasValue)
                query = query.Where(a => a.FIUActivitiesId == activityId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(a => a.FormStatus.ToLower() == status.ToLower());

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<FIUProgramActivity>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<List<FIUProgramActivity>> GetByUnitLocationIdAsync(int unitLocationId)
        {
            return await _context.FIUProgramActivities
                .Include(a => a.FIUActivity)
                .Include(a => a.CreatedBy)
                .Where(a => a.UnitLocationId == unitLocationId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<FIUProgramActivity>> GetByCreatedByIdAsync(int userId)
        {
            return await _context.FIUProgramActivities
                .Include(a => a.FIUActivity)
                .Include(a => a.UnitLocation)
                .Where(a => a.CreatedById == userId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<FIUProgramActivity>> GetByStatusAsync(List<int> unitLocationIds, string status)
        {
            return await _context.FIUProgramActivities
                .Include(a => a.FIUActivity)
                .Include(a => a.UnitLocation)
                .Include(a => a.CreatedBy)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId) &&
                           a.FormStatus.ToLower() == status.ToLower())
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Dictionary<string, int>> GetStatsByActivityTypeAsync(List<int> unitLocationIds)
        {
            return await _context.FIUProgramActivities
                .Include(a => a.FIUActivity)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId))
                .GroupBy(a => a.FIUActivity.ActivityName)
                .Select(g => new { ActivityName = g.Key, Count = g.Sum(a => a.Number) })
                .ToDictionaryAsync(x => x.ActivityName, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetStatsByStatusAsync(List<int> unitLocationIds)
        {
            return await _context.FIUProgramActivities
                .Where(a => unitLocationIds.Contains(a.UnitLocationId))
                .GroupBy(a => a.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }

        public async Task<List<FIUActivitySummaryDto>> GetMonthlyActivitySummaryAsync(
            List<int> unitLocationIds,
            int year,
            int month)
        {
            var startDate = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = startDate.AddMonths(1).AddSeconds(-1);

            var summary = await _context.FIUProgramActivities
                .Include(a => a.FIUActivity)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId) &&
                           a.CreatedAt >= startDate &&
                           a.CreatedAt <= endDate &&
                           a.FormStatus == "Approved")
                .GroupBy(a => new
                {
                    a.FIUActivitiesId,
                    ActivityName = a.FIUActivity.ActivityName,
                    DisplayOrder = a.FIUActivity.DisplayOrder
                })
                .Select(g => new FIUActivitySummaryDto
                {
                    SlNo = g.Key.DisplayOrder,
                    ActivityName = g.Key.ActivityName,
                    Count = g.Sum(a => a.Number)
                })
                .OrderBy(s => s.SlNo)
                .ToListAsync();

            return summary;
        }
    }
}