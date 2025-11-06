// DeuParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuParticipantDemographicsRepository : IDeuParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public DeuParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.DeuParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<DeuParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.DeuParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.DeuProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<DeuParticipantDemographics> CreateAsync(DeuParticipantDemographics entity)
        {
            _context.DeuParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuParticipantDemographics> UpdateAsync(DeuParticipantDemographics entity)
        {
            _context.DeuParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.DeuParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}