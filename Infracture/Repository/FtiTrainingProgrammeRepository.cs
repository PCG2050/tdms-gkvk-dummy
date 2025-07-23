using Application;
using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class FtiTrainingProgrammeRepository : IFtiTrainingProgrammeRepository
    {
        private readonly TdmsDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public FtiTrainingProgrammeRepository(TdmsDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }
        public async Task<PaginatedResult<FtiTrainingProgram>> GetItemsAsync(int pageNumber, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            int userOrgId = _currentUserService.OrganizationId;
            var query = _context.FtiTrainingPrograms.Where(x => x.OrganizationId == userOrgId);
            if (queryFilter != null)
            {
                if (queryFilter.CreatedById is not null) query = query.Where(x => x.CreatedById == queryFilter.CreatedById);
            }
            var paginatedResult = new PaginatedResult<FtiTrainingProgram>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            paginatedResult.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;
            paginatedResult.Items = await query.Skip(offset).Take(pageSize).AsNoTracking().ToListAsync();
            return paginatedResult;
        }
        public async Task AddAsync(FtiTrainingProgram ftiTrainingProgram)
        {
            _context.FtiTrainingPrograms.Add(ftiTrainingProgram);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(FtiTrainingProgram ftiTrainingProgram)
        {
            _context.FtiTrainingPrograms.Update(ftiTrainingProgram);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entry = await _context.FtiTrainingPrograms.FindAsync(id);
            if(entry is not null)_context.FtiTrainingPrograms.Remove(entry);
        }

        public async Task<FtiTrainingProgram?> GetItemAsync(int id)
        {
            return await _context.FtiTrainingPrograms.FindAsync(id);
        }
    }
}
