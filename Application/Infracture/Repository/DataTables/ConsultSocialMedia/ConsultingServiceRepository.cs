using Application.Interface.Repository.DataTables.ConsultSocialMedia;
using Application.Models;
using Domain.Entities.GenericTables.ConsultingAndSocialMediaService;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.ConsultSocialMedia
{
    public class ConsultingServiceRepository : IConsultingServiceRepository
    {
        private readonly TdmsDbContext _context;

        public ConsultingServiceRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<ConsultingAndSocialMediaService>> GetAllAsync()
        {
            return await _context.TableConsultingAndSocialMediaServices
                .Include(n => n.Category)  
                .Include( n=> n.Title)
                .Include(n=>n.Date)
                .ToListAsync();
        }

        public async Task<ConsultingAndSocialMediaService?> GetByIdAsync(int id)
        {
            return await _context.TableConsultingAndSocialMediaServices.FindAsync(id);
        }

        public async Task<ConsultingAndSocialMediaService?> GetWithDetailsAsync(int id)
        {
            return await _context.TableConsultingAndSocialMediaServices
                .Include(c => c.Category)
                .Include(c => c.RelatedTo)
                .Include(c => c.ExtensionActivity)
                .Include(c => c.Particulars)
                .Include(c => c.ModeOutreach)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(c => c.Organization)
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.ApprovedBy)
                .Include(c => c.TableModeAndOutreaches)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ConsultingAndSocialMediaService> CreateAsync(ConsultingAndSocialMediaService entity)
        {
            _context.TableConsultingAndSocialMediaServices.Add(entity);
            await _context.SaveChangesAsync();
            return await GetWithDetailsAsync(entity.Id) ?? entity;
        }

        public async Task<ConsultingAndSocialMediaService> UpdateAsync(ConsultingAndSocialMediaService entity)
        {
            _context.TableConsultingAndSocialMediaServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.TableConsultingAndSocialMediaServices.FindAsync(id);
            if (entity != null)
            {
                _context.TableConsultingAndSocialMediaServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<ConsultingAndSocialMediaService>> GetPaginatedAsync(
            List<int> unitLocationIds,
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null)
        {
            var query = _context.TableConsultingAndSocialMediaServices
                .Include(c => c.Category)
                .Include(c => c.RelatedTo)
                .Include(c => c.ExtensionActivity)
                .Include(c => c.Particulars)
                .Include(c => c.ModeOutreach)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.District)
                        .ThenInclude(d => d.State)
                .Include(c => c.Organization)
                .Include(c => c.CreatedBy)
                .Include(c => c.ApprovedBy)
                .Where(c => unitLocationIds.Contains(c.UnitLocationId))
                .AsQueryable();

            // Apply filters
            if (startDate.HasValue)
                query = query.Where(c => c.StartDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(c => c.EndDate <= endDate.Value);

            if (categoryId.HasValue)
                query = query.Where(c => c.CategoryId == categoryId.Value);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c =>
                    (c.Title != null && c.Title.Contains(searchTerm)) ||
                    (c.Location != null && c.Location.Contains(searchTerm)));
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ConsultingAndSocialMediaService>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<PaginatedResult<ConsultingAndSocialMediaService>> GetByStatusAsync(
            List<int> unitLocationIds,
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _context.TableConsultingAndSocialMediaServices
                .Include(c => c.Category)
                .Include(c => c.RelatedTo)
                .Include(c => c.ExtensionActivity)
                .Include(c => c.Particulars)
                .Include(c => c.ModeOutreach)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Include(c => c.UnitLocation)
                    .ThenInclude(ul => ul.District)
                .Include(c => c.Organization)
                .Include(c => c.CreatedBy)
                .Include(c => c.ApprovedBy)
                .Where(c => unitLocationIds.Contains(c.UnitLocationId) &&
                           c.FormStatus.ToLower() == status.ToLower());

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(c => c.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<ConsultingAndSocialMediaService>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync(List<int> unitLocationIds)
        {
            var summary = await _context.TableConsultingAndSocialMediaServices
                .Where(c => unitLocationIds.Contains(c.UnitLocationId))
                .GroupBy(c => c.FormStatus)
                .Select(g => new { FormStatus = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.FormStatus, x => x.Count);

            // Ensure all statuses are present
            var allStatuses = new[] { "Draft", "Pending", "Approved", "Rejected" };
            foreach (var status in allStatuses)
            {
                if (!summary.ContainsKey(status))
                    summary[status] = 0;
            }

            return summary;
        }
    }
}