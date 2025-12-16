// SametiParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiParticipantDemographicsRepository : ISametiParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public SametiParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.SametiParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<SametiParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.SametiParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.SametiProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<SametiParticipantDemographics> CreateAsync(SametiParticipantDemographics entity)
        {
            _context.SametiParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiParticipantDemographics> UpdateAsync(SametiParticipantDemographics entity)
        {
            _context.SametiParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.SametiParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}