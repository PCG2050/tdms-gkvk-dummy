// NaepParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepParticipantDemographicsRepository : INaepParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public NaepParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.NaepParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<NaepParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.NaepProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<NaepParticipantDemographics> CreateAsync(NaepParticipantDemographics entity)
        {
            _context.NaepParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepParticipantDemographics> UpdateAsync(NaepParticipantDemographics entity)
        {
            _context.NaepParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.NaepParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}