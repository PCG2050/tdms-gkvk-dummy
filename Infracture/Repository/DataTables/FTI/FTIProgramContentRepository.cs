// FTIProgramContentRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIProgramContentRepository : IFTIProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public FTIProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.FTIProgramContentAndResources.FindAsync(id);
        }

        public async Task<FTIProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.FTIProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<FTIProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.FTIProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<FTIProgramContentAndResources> CreateAsync(FTIProgramContentAndResources entity)
        {
            _context.FTIProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIProgramContentAndResources> UpdateAsync(FTIProgramContentAndResources entity)
        {
            _context.FTIProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.FTIProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
