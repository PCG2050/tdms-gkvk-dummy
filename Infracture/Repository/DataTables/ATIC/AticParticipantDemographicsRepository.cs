

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticParticipantDemographicsRepository : IAticParticipantDemographicsRepository
    {
        private readonly TdmsDbContext _context;

        public AticParticipantDemographicsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticParticipantDemographics?> GetByIdAsync(int id)
        {
            return await _context.AticParticipantDemographics
                .Include(p => p.Participant)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<AticParticipantDemographics>> GetByProgramIdAsync(int programId)
        {
            return await _context.AticParticipantDemographics
                .Include(p => p.Participant)
                .Where(p => p.AticProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<AticParticipantDemographics> CreateAsync(AticParticipantDemographics entity)
        {
            _context.AticParticipantDemographics.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticParticipantDemographics> UpdateAsync(AticParticipantDemographics entity)
        {
            _context.AticParticipantDemographics.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticParticipantDemographics.FindAsync(id);
            if (entity != null)
            {
                _context.AticParticipantDemographics.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}