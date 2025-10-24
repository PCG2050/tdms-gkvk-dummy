// StuParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuParticipantDemographicsRepository : IStuParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public StuParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.StuParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<StuParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.StuParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.StuProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<StuParticipantDemographics> CreateAsync(StuParticipantDemographics entity)
        {
            _context.StuParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuParticipantDemographics> UpdateAsync(StuParticipantDemographics entity)
        {
            _context.StuParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.StuParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}