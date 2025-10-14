using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaDemographicsRepository : IIbtvaDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaParticipantDemographics> CreateAsync(IbtvaParticipantDemographics demographic)
        {
            await _context.IbtvaParticipantDemographics.AddAsync(demographic);
            await _context.SaveChangesAsync();
            return demographic;
        }

        public async Task<IbtvaParticipantDemographics?> GetByIdAsync(int id, int programId)
        {
            return await _context.IbtvaParticipantDemographics
                .Include(d => d.Participant)
                .FirstOrDefaultAsync(d => d.Id == id && d.IbtvaProgramDetailsId == programId);
        }

        public async Task<IEnumerable<IbtvaParticipantDemographics>> GetAllByProgramAsync(int programId)
        {
            return await _context.IbtvaParticipantDemographics
                .Include(d => d.Participant)
                .Where(d => d.IbtvaProgramDetailsId == programId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<IbtvaParticipantDemographics?> UpdateAsync(IbtvaParticipantDemographics demographic)
        {
            if (demographic.IbtvaProgramDetailsId == null)
                return null;

            var existing = await GetByIdAsync(demographic.Id, demographic.IbtvaProgramDetailsId.Value);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(demographic);
            existing.UpdatedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, int programId)
        {
            var demographic = await GetByIdAsync(id, programId);
            if (demographic == null) return false;

            _context.IbtvaParticipantDemographics.Remove(demographic);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
