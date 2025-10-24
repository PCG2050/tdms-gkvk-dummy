using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIRepository : IFTIRepository
    {
        private readonly TdmsDbContext _context;
        public FTIRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<FTIProgramDetails>> GetAllProgramsAsync()
        {
            return await _context.FTIProgramDetails
                .Include(p => p.ParticipantDemographics)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.ResourcePersons)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.TopicsCovered)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .ToListAsync();
        }

        public async Task<FTIProgramDetails?> GetProgramByIdAsync(int id)
        {
            return await _context.FTIProgramDetails
                .Include(p => p.ParticipantDemographics)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.ResourcePersons)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.TopicsCovered)
                .Include(p => p.ProgramContentAndResources)
                    .ThenInclude(c => c.TeachingAids)
                .Include(p => p.AdvisoryServices)
                .Include(p => p.Reports)
                .Include(p => p.Recommendations)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<FTIProgramDetails> AddProgramAsync(FTIProgramDetails entity)
        {
            _context.FTIProgramDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIProgramDetails> UpdateProgramAsync(FTIProgramDetails entity)
        {
            _context.FTIProgramDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteProgramAsync(int id)
        {
            var entity = await _context.FTIProgramDetails.FindAsync(id);
            if (entity == null) return false;
            _context.FTIProgramDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
