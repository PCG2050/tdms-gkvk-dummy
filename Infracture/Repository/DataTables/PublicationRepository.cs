using Application.Interface.Repository.DataTables;
using Application.Models;
using Domain.Entities.Publications;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
  
        public class PublicationRepository : IPublicationRepository
        {
            private readonly TdmsDbContext _context;

            public PublicationRepository(TdmsDbContext context)
            {
                _context = context;
            }

            public async Task<Publication?> GetByIdAsync(int id)
            {
                return await _context.Publications.FindAsync(id);
            }

            public async Task<Publication?> GetWithDetailsAsync(int id)
            {
                return await _context.Publications
                    .Include(p => p.Category)
                    .Include(p => p.Mode)
                    .Include(p => p.Region)
                    .Include(p => p.Source)
                    .Include(p => p.ExtensionLiterature)
                    .Include(p => p.UnitLocation)
                        .ThenInclude(ul => ul.Unit)
                    .Include(p => p.UnitLocation)
                        .ThenInclude(ul => ul.District)
                            .ThenInclude(d => d.State)
                    .Include(p => p.CreatedBy)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            public async Task<Publication> CreateAsync(Publication publication)
            {
                _context.Publications.Add(publication);
                await _context.SaveChangesAsync();
                return await GetWithDetailsAsync(publication.Id) ?? publication;
            }

            public async Task<Publication> UpdateAsync(Publication publication)
            {
                _context.Publications.Update(publication);
                await _context.SaveChangesAsync();
                return publication;
            }

            public async Task DeleteAsync(int id)
            {
                var publication = await _context.Publications.FindAsync(id);
                if (publication != null)
                {
                    _context.Publications.Remove(publication);
                    await _context.SaveChangesAsync();
                }
            }

            public async Task<PaginatedResult<Publication>> GetPaginatedAsync(
                List<int> unitLocationIds,
                int pageNumber = 1,
                int pageSize = 10,
                DateOnly? startDate = null,
                DateOnly? endDate = null)
            {
                var query = _context.Publications
                    .Include(p => p.Category)
                    .Include(p => p.Mode)
                    .Include(p => p.Region)
                    .Include(p => p.Source)
                    .Include(p => p.ExtensionLiterature)
                    .Include(p => p.UnitLocation)
                        .ThenInclude(ul => ul.Unit)
                    .Include(p => p.UnitLocation)
                        .ThenInclude(ul => ul.District)
                            .ThenInclude(d => d.State)
                    .Where(p => unitLocationIds.Contains(p.UnitLocationId));

                if (startDate.HasValue)
                    query = query.Where(p => p.StartDate >= startDate.Value);

                if (endDate.HasValue)
                    query = query.Where(p => p.EndDate <= endDate.Value);

                var totalCount = await query.CountAsync();

                var items = await query
                                 .OrderByDescending(p => p.PublicationDate.HasValue
                                     ? p.PublicationDate.Value.ToDateTime(TimeOnly.MinValue)
                                     : p.CreatedAt)
                    
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedResult<Publication>
                {
                    Items = items,
                    TotalItems = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize                    
                };
            }

            public async Task<bool> ExistsAsync(int id)
            {
                return await _context.Publications.AnyAsync(p => p.Id == id);
            }

            // Master data methods
            public async Task<List<Category>> GetCategoriesAsync()
            {
                return await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
            }

            public async Task<List<Source>> GetSourcesAsync()
            {
                return await _context.Sources.OrderBy(s => s.SourceName).ToListAsync();
            }

            public async Task<List<Mode>> GetModesAsync()
            {
                return await _context.Modes.OrderBy(m => m.ModeName).ToListAsync();
            }

            public async Task<List<Region>> GetRegionsAsync()
            {
                return await _context.Regions.OrderBy(r => r.RegionName).ToListAsync();
            }

            public async Task<List<ExtensionLiterature>> GetExtensionLiteraturesAsync()
            {
                return await _context.ExtensionLiteratures
                    .OrderByDescending(el => el.ExtensionDate)
                    .ToListAsync();
            }
        }
}

