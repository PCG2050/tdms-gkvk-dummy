using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.STU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class StuOtherActivitiesRepository : IStuOtherActivitiesRepository
    {
        private readonly TdmsDbContext _context;

        public StuOtherActivitiesRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<StuOtherActivity> AddAsync(StuOtherActivity entity)
        {
            await _context.StuOtherActivities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(StuOtherActivity activity)
        {
            _context.StuOtherActivities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<StuOtherActivity?> GetAsync(int id)
        {
            return await _context.StuOtherActivities.FindAsync(id);
        }

        public async Task<PaginatedResult<StuOtherActivityDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.StuOtherActivities.Where(x => x.OrganizationId == organizationId);
            if (queryFilter?.Filters.Count > 0)
            {
                var filters = queryFilter.Filters;
                if (filters.ContainsKey("TrainerId") && Int32.TryParse(filters["TrainerId"].ToString(), out int trainerId)) query = query.Where(x => x.CreatedById == trainerId);
            }
            var paginatedResult = new PaginatedResult<StuOtherActivityDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize)
                .Select(f=> new StuOtherActivityDto
                {
                    Id = f.Id,
                    ActivityDetails = f.ActivityDetails,
                    CreatedAt = f.CreatedAt,
                    EndDate = f.EndDate,
                    StartDate = f.StartDate,
                    UpdateAt = f.UpdatedAt,
                    Attachements = f.Attachements
                }).ToListAsync();
            return paginatedResult;
        }

        public async Task<StuOtherActivity> UpdateAsync(StuOtherActivity activity)
        {
            if(_context.StuOtherActivities.Entry(activity).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                await _context.SaveChangesAsync();
            return activity;
        }
    }
}
