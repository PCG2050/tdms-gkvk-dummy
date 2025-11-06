using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class TableOtherActivityRepository : ITableOtherActivityRepository
    {
        private readonly TdmsDbContext _context;

        public TableOtherActivityRepository(TdmsDbContext context)
        {
            _context = context;
        }

        // ============================
        // CORE CRUD OPERATIONS
        // ============================

        public async Task<TableOtherActivity?> GetByIdAsync(int id)
        {
            return await _context.OtherActivities.FindAsync(id);
        }

        public async Task<TableOtherActivity?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.OtherActivities
                .Include(a => a.UnitLocation)
                .Include(a => a.Organization)
                .Include(a => a.CreatedBy)
                .Include(a => a.ApprovedBy)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(TableOtherActivity entity)
        {
            await _context.OtherActivities.AddAsync(entity);
        }

        public async Task UpdateAsync(TableOtherActivity entity)
        {
            _context.OtherActivities.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TableOtherActivity entity)
        {
            _context.OtherActivities.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ============================
        // PAGINATION & FILTERS
        // ============================

        public async Task<PaginatedResult<TableOtherActivity>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            var query = _context.OtherActivities
                .Include(a => a.UnitLocation)
                .Include(a => a.Organization)
                .Include(a => a.CreatedBy)
                .Include(a => a.ApprovedBy)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId))
                .AsQueryable();

            // Apply filters
            if (unitLocationId.HasValue)
                query = query.Where(a => a.UnitLocationId == unitLocationId.Value);

            if (startDate.HasValue)
                query = query.Where(a => a.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(a => a.EndDate <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(a =>
                    (a.Title != null && a.Title.ToLower().Contains(lowerSearchTerm)) ||
                    (a.Description != null && a.Description.ToLower().Contains(lowerSearchTerm)));
            }

            // Get total count
            var totalItems = await query.CountAsync();

            // Apply pagination and ordering (most recent first)
            var activities = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TableOtherActivity>
            {
                Items = activities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<TableOtherActivity>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.OtherActivities
                .Include(a => a.UnitLocation)
                .Include(a => a.Organization)
                .Include(a => a.CreatedBy)
                .Include(a => a.ApprovedBy)
                .Where(a => unitLocationIds.Contains(a.UnitLocationId) && a.FormStatus == status)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var activities = await query
                .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TableOtherActivity>
            {
                Items = activities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<TableOtherActivity>> GetByCreatorAsync(
            int creatorId,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var query = _context.OtherActivities
                .Include(a => a.UnitLocation)
                .Include(a => a.Organization)
                .Include(a => a.CreatedBy)
                .Include(a => a.ApprovedBy)
                .Where(a => a.CreatedById == creatorId)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var activities = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TableOtherActivity>
            {
                Items = activities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // ============================
        // DASHBOARD & STATISTICS
        // ============================

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var statusCounts = await _context.OtherActivities
                .Where(a => unitLocationIds.Contains(a.UnitLocationId))
                .GroupBy(a => a.FormStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var summary = new Dictionary<string, int>
            {
                { "Draft", 0 },
                { "Pending", 0 },
                { "Approved", 0 },
                { "Rejected", 0 }
            };

            foreach (var item in statusCounts)
            {
                if (summary.ContainsKey(item.Status))
                    summary[item.Status] = item.Count;
            }

            return summary;
        }
    }
}