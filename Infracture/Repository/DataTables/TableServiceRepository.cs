using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.TblService;
using Application.Models;
using Domain.Entities.GenericTables.Service;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class TableServiceRepository : ITableServiceRepository
    {
        private readonly TdmsDbContext _context;

        public TableServiceRepository(TdmsDbContext context)
        {
            _context = context;
        }

        // ============================
        // CORE CRUD
        // ============================

        public async Task<List<TblService>> GetAllAsync()
        {
            
        
            return await _context.Services    
                .Include(n => n.Hostels)
                .Include(n => n.RevolvingFundStatuses)
                .Include(n => n.VisitorDetails)            
                .ToListAsync();
        
        }
        public async Task<TblService?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<TblService?> GetWithDetailsAsync(int id)
        {
            return await _context.Services
                .Include(s => s.Category)
                .Include(s => s.Theme)
                .Include(s => s.SourceOfFund)
                .Include(s => s.QuantityUnit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(s => s.Organization)
                .Include(s => s.CreatedBy)
                .Include(s => s.UpdatedBy)
                .Include(s => s.ApprovedBy)
                .Include(s => s.Hostels)
                .Include(s => s.RevolvingFundStatuses)
                .Include(s => s.VisitorDetails)
                .AsSplitQuery()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<TblService> CreateAsync(TblService entity)
        {
            _context.Services.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<TblService> UpdateAsync(TblService entity)
        {
            //_context.Services.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Services.FindAsync(id);
            if (entity != null)
            {
                _context.Services.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ============================
        // PAGINATION
        // ============================
        public async Task<PaginatedResult<TblService>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null)
        {
            var query = _context.Services
                .Include(s => s.Category)
                .Include(s => s.Theme)
                .Include(s => s.SourceOfFund)
                .Include(s => s.QuantityUnit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(s => s.Organization)
                .Include(s => s.CreatedBy)
                .Include(s => s.ApprovedBy)
                .Where(s => unitLocationIds.Contains(s.UnitLocationId))
                .AsQueryable();

            // Apply filters
            if (startDate.HasValue)
                query = query.Where(s => s.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.EndDate <= endDate.Value);

            if (categoryId.HasValue)
                query = query.Where(s => s.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(s =>
                    (s.RentedTo != null && s.RentedTo.Contains(searchTerm)) ||
                    (s.TitleOfActivityConducted != null && s.TitleOfActivityConducted.Contains(searchTerm)) ||
                    (s.CropPlantProductName != null && s.CropPlantProductName.Contains(searchTerm)));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TblService>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<PaginatedResult<TblService>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.Services
                .Include(s => s.Category)
                .Include(s => s.Theme)
                .Include(s => s.SourceOfFund)
                .Include(s => s.QuantityUnit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(s => s.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(s => s.Organization)
                .Include(s => s.CreatedBy)
                .Include(s => s.ApprovedBy)
                .Where(s => unitLocationIds.Contains(s.UnitLocationId) &&
                           s.FormStatus.ToLower() == status.ToLower());

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<TblService>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // ============================
        // STATUS SUMMARY
        // ============================
        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var summary = await _context.Services
                .Where(s => unitLocationIds.Contains(s.UnitLocationId))
                .GroupBy(s => s.FormStatus)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Key, x => x.Count);

            // Ensure all statuses exist
            var statuses = new[] { "Draft", "Pending", "Approved", "Rejected" };
            foreach (var status in statuses)
            {
                if (!summary.ContainsKey(status))
                    summary[status] = 0;
            }

            return summary;
        }


        public async Task<VisitorDetail?> GetVisitorDetailByIdAsync(int id)
        {
            return await _context.VisitorDetails.FindAsync(id);
        }

        public async Task<List<VisitorDetail>> GetVisitorDetailsByServiceIdAsync(int serviceId)
        {
            return await _context.VisitorDetails
                .Where(v => v.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<VisitorDetail> CreateVisitorDetailAsync(VisitorDetail entity)
        {
            _context.VisitorDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<VisitorDetail> UpdateVisitorDetailAsync(VisitorDetail entity)
        {
            _context.VisitorDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteVisitorDetailAsync(int id)
        {
            var entity = await _context.VisitorDetails.FindAsync(id);
            if (entity != null)
            {
                _context.VisitorDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
