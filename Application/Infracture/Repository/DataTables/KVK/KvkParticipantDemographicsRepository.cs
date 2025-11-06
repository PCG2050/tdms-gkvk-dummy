// KvkParticipantDemographicsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkParticipantDemographicsRepository : IKvkParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public KvkParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkParticipantDemographics> CreateAsync(KvkParticipantDemographics entity)
        {
            _context.KvkParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.KvkParticipantDemographics
                .Include(d => d.Participant)
                .Include(d => d.ProgramDetails)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<KvkParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkParticipantDemographics
                .Include(d => d.Participant)
                .Where(d => d.KvkProgramDetailsId == programId)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkParticipantDemographics> UpdateAsync(KvkParticipantDemographics entity)
        {
            _context.KvkParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.KvkParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}