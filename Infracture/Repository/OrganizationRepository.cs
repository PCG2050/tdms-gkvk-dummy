using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly TdmsDbContext _context;

        public OrganizationRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<Organization?> GetOrganizationAsync(int id)
        {
            return await _context.Organizations
                .Include(o=> o.District)
                    .ThenInclude(d=> d.State)
                .FirstOrDefaultAsync(o=> o.Id == id);
        }

        public async Task<PaginatedResult<OrganizationDto>> GetPaginatedItemsAsync(int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            var query = _context.Organizations
                .Include(o => o.District)
                .ThenInclude(d => d.State)
                .Select(o=> new OrganizationDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    LogoUrl = o.Logo,
                    DistrictName = o.District.Name,
                    StateName = o.District.State.Name,
                    StorageContainerName = o.StorageContainerName
                }).AsNoTracking();
            var paginatedResult = new PaginatedResult<OrganizationDto>
            {
                PageNumber = page,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.CountAsync();
            int offset = (page - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize).ToListAsync();
            return paginatedResult;
        }

        public async Task<bool> HasOrganizationWithNameAsync(string name)
        {
            return await _context.Organizations.AnyAsync(o => o.Name == name);
        }

        public async Task SaveAsync(Organization organization)
        {
            if (_context.Entry<Organization>(organization).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                await _context.AddAsync(organization);
            else
                _context.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            if (_context.Entry(organization).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                await _context.SaveChangesAsync();
        }
    }
}
