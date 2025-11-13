// FTIParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIParticipantDemographicsRepository : IFTIParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public FTIParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.FTIParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<FTIParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.FTIProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<FTIParticipantDemographics> CreateAsync(FTIParticipantDemographics entity)
        {
            _context.FTIParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIParticipantDemographics> UpdateAsync(FTIParticipantDemographics entity)
        {
            _context.FTIParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.FTIParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
