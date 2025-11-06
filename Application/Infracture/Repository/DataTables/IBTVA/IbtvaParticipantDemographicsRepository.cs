// IbtvaParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaParticipantDemographicsRepository : IIbtvaParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.IbtvaParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<IbtvaParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.IbtvaParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.IbtvaProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<IbtvaParticipantDemographics> CreateAsync(IbtvaParticipantDemographics entity)
        {
            _context.IbtvaParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaParticipantDemographics> UpdateAsync(IbtvaParticipantDemographics entity)
        {
            _context.IbtvaParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}