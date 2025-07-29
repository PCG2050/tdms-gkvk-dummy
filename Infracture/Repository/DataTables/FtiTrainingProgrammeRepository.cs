using Application;
using Application.Interface;
using Application.Interface.Repository.DataTables;
using Application.Models;
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
    public class FtiTrainingProgrammeRepository : IFtiTrainingProgrammeRepository
    {
        private readonly TdmsDbContext _context;

        public FtiTrainingProgrammeRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<PaginatedResult<FtiTrainingProgram>> GetPaginatedItemsAsync(int organizationId,int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, QueryFilter? queryFilter = null, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            var query = _context.FtiTrainingPrograms.Where(x => x.OrganizationId == organizationId);
            if (queryFilter?.Filters?.Count > 0)
            {
                var filters = queryFilter.Filters;
                if(filters.ContainsKey("CreatedById") && int.TryParse((string)filters["CreatedById"], out int createdById)) query = query.Where(x => x.CreatedById == createdById);
            }
            var paginatedResult = new PaginatedResult<FtiTrainingProgram>
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize).AsNoTracking().ToListAsync();
            return paginatedResult;
        }
        public async Task<FtiTrainingProgram> AddAsync(FtiTrainingProgram ftiTrainingProgram)
        {
            _context.FtiTrainingPrograms.Add(ftiTrainingProgram);
            await _context.SaveChangesAsync();
            return ftiTrainingProgram;
        }
        public async Task<FtiTrainingProgram> UpdateAsync(FtiTrainingProgram ftiTrainingProgram)
        {
            _context.FtiTrainingPrograms.Update(ftiTrainingProgram);
            await _context.SaveChangesAsync();
            return ftiTrainingProgram;
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.FtiTrainingPrograms.FindAsync(id);
            if(entry is not null)_context.FtiTrainingPrograms.Remove(entry);
            await _context.SaveChangesAsync();
        }

        public async Task<FtiTrainingProgram?> GetItemAsync(int id)
        {
            return await _context.FtiTrainingPrograms.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(FtiTrainingProgram activity)
        {
            _context.FtiTrainingPrograms.Remove(activity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<FtiTrainingProgram?> GetAsync(int id)
        {
            return await _context.FtiTrainingPrograms.FindAsync(id);
        }
    }
}
