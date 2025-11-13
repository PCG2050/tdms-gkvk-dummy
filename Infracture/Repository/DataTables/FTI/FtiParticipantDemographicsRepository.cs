// FtiParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiParticipantDemographicsRepository : IFtiParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public FtiParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.FtiParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<FtiParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.FtiParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.FtiProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<FtiParticipantDemographics> CreateAsync(FtiParticipantDemographics entity)
        {
            _context.FtiParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiParticipantDemographics> UpdateAsync(FtiParticipantDemographics entity)
        {
            _context.FtiParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.FtiParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
