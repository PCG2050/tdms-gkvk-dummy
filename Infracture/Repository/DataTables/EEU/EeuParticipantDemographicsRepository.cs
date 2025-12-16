// EeuParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuParticipantDemographicsRepository : IEeuParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public EeuParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuParticipantDemographics> CreateAsync(EeuParticipantDemographics entity)
        {
            _context.EeuParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.EeuParticipantDemographics
                .Include(d => d.Participant)
                .Include(d => d.ProgramDetails)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<EeuParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuParticipantDemographics
                .Include(d => d.Participant)
                .Where(d => d.EeuProgramDetailsId == programId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<EeuParticipantDemographics> UpdateAsync(EeuParticipantDemographics entity)
        {
            _context.EeuParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.EeuParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}