using Application.Interface.Repository.DataTables.ASM;
using Application.Models;
using Application.Models.DataTables.ASM;
using Domain.Entities.ASM;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ASM
{
    public class ASMVisitorDetailsRepository : IASMVisitorDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public ASMVisitorDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        // ============================
        // CORE CRUD OPERATIONS
        // ============================

        public IQueryable<ASMVisitorDetails> GetQueryable()
        {
            return _context.ASMVisitorDetails.AsQueryable();
        }


        public async Task<ASMVisitorDetails> AddAsync(ASMVisitorDetails entity)
        {
            _context.ASMVisitorDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ASMVisitorDetails?> GetByIdAsync(int id)
        {
            return await _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.ApprovedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ASMVisitorDetails> UpdateAsync(ASMVisitorDetails entity)
        {
            _context.ASMVisitorDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ASMVisitorDetails.FindAsync(id);
            if (entity == null) return false;

            _context.ASMVisitorDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ASMVisitorDetails>> GetAllAsync()
        {
            return await _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.ApprovedBy)
                .AsSplitQuery()
                .ToListAsync();
        }

        // ============================
        // PAGINATION & FILTERS
        // ============================

        public async Task<PaginatedResult<ASMVisitorDetails>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            var query = _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => unitLocationIds.Contains(x.UnitLocationId))
                .AsQueryable();

            // Apply filters
            if (unitLocationId.HasValue)
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);

            if (startDate.HasValue)
                query = query.Where(x => x.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.EndDate <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerSearchTerm = searchTerm.ToLower();
                query = query.Where(x =>
                    x.InstituteName != null && x.InstituteName.ToLower().Contains(lowerSearchTerm));
            }

            // Get total count
            var totalItems = await query.CountAsync();

            // Apply pagination and ordering (most recent first)
            var entities = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ASMVisitorDetails>
            {
                Items = entities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<ASMVisitorDetails>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => unitLocationIds.Contains(x.UnitLocationId) && x.FormStatus == status)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var entities = await query
                .OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ASMVisitorDetails>
            {
                Items = entities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<ASMVisitorDetails>> GetByCreatedByAsync(
            int creatorId,
            int pageNumber = 1,
            int pageSize = 20)
        {
            var query = _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.ApprovedBy)
                .Where(x => x.CreatedById == creatorId)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var entities = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ASMVisitorDetails>
            {
                Items = entities,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<ASMVisitorDetails?> GetWithDetailsAsync(int id)
        {
            return await _context.ASMVisitorDetails
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.ApprovedBy)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ASMVisitorDetails>> GetByUnitLocationAsync(int unitLocationId)
        {
            return await _context.ASMVisitorDetails
                .Where(x => x.UnitLocationId == unitLocationId)
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .ToListAsync();
        }


        // ============================
        // DASHBOARD & STATISTICS
        // ============================

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var statusCounts = await _context.ASMVisitorDetails
                .Where(x => unitLocationIds.Contains(x.UnitLocationId))               
                .GroupBy(x => x.FormStatus)
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
        public async Task<(ASMVisitorSummaryDto summary, int totalEntries)> GetMonthlyVisitorSummaryAsync(
    List<int> unitLocationIds,
    int year,
    int month)
        {
            var startDate = new DateTimeOffset(year, month, 1, 0, 0, 0, TimeSpan.Zero);
            var endDate = startDate.AddMonths(1).AddSeconds(-1);

            var query = _context.ASMVisitorDetails
                .Where(a => unitLocationIds.Contains(a.UnitLocationId) &&
                           a.CreatedAt >= startDate &&
                           a.CreatedAt <= endDate &&
                           (a.FormStatus == "Draft" || a.FormStatus == "Pending" || a.FormStatus == "Approved"));

            // Count total entries
            var totalEntries = await query.CountAsync();

            // Sum all visitor counts
            var summary = new ASMVisitorSummaryDto
            {
                TotalFarmers = await query.SumAsync(v => v.FarmersCount),
                TotalStudents = await query.SumAsync(v => v.StudentsCount),
                TotalPublic = await query.SumAsync(v => v.PublicCount)
            };

            summary.TotalVisitors = summary.TotalFarmers + summary.TotalStudents + summary.TotalPublic;

            return (summary, totalEntries);
        }




    }

}