// NaepProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepProgramContentRepository : INaepProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public NaepProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.NaepProgramContentAndResources.FindAsync(id);
        }

        public async Task<NaepProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.NaepProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<NaepProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.NaepProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<NaepProgramContentAndResources> CreateAsync(NaepProgramContentAndResources entity)
        {
            _context.NaepProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepProgramContentAndResources> UpdateAsync(NaepProgramContentAndResources entity)
        {
            _context.NaepProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.NaepProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}