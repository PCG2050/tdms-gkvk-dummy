using Application.Interface.Repository.DataTables;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class FtiOtherActivitiesRepository : IFtiOtherActivitiesRepository
    {
        private readonly TdmsDbContext _context;

        public FtiOtherActivitiesRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<FtiOtherActivity> AddAsync(FtiOtherActivity activity)
        {
            await _context.FtiOtherActivities.AddAsync(activity);
            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<bool> DeleteAsync(FtiOtherActivity activity)
        {
            _context.FtiOtherActivities.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FtiOtherActivity?> GetAsync(int id)
        {
            return await _context.FtiOtherActivities.FindAsync(id);
        }

        public async Task<PaginatedResult<FtiOtherActivityDto>> GetPaginatedItemsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.FtiOtherActivities.Where(x => x.OrganizationId == organizationId);
            if (queryFilter?.Filters.Count > 0)
            {
                var filters = queryFilter.Filters;
                if (filters.ContainsKey("TrainerId") && Int32.TryParse(filters["TrainerId"].ToString(), out int trainerId)) query = query.Where(x => x.CreatedById == trainerId);
            }
            var paginatedResult = new PaginatedResult<FtiOtherActivityDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize)
                .Select(f=> new FtiOtherActivityDto
                {
                    Id = f.Id,
                    ActivityDetails = f.ActivityDetails,
                    CreatedAt = f.CreatedAt,
                    EndDate = f.EndDate,
                    StartDate = f.StartDate,
                    UpdateAt = f.UpdatedAt
                }).ToListAsync();
            return paginatedResult;
        }

        public async Task<FtiOtherActivity> UpdateAsync(FtiOtherActivity activity)
        {
            if(_context.FtiOtherActivities.Entry(activity).State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                await _context.SaveChangesAsync();
            return activity;
        }
    }
}
