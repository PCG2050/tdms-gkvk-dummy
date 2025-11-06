// AticProgramContentRepository.cs
using Application.Interface.Repository.DataTables.ATIC;
using Domain.Entities.ATIC;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticProgramContentRepository : IAticProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public AticProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.AticProgramContentAndResources.FindAsync(id);
        }

        public async Task<AticProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.AticProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<AticProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.AticProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.AticProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<AticProgramContentAndResources> CreateAsync(AticProgramContentAndResources entity)
        {
            _context.AticProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticProgramContentAndResources> UpdateAsync(AticProgramContentAndResources entity)
        {
            _context.AticProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.AticProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}