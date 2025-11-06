// EeuProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuProgramContentRepository : IEeuProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public EeuProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.EeuProgramContentAndResources.FindAsync(id);
        }

        public async Task<EeuProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.EeuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<EeuProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.EeuProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<EeuProgramContentAndResources> CreateAsync(EeuProgramContentAndResources entity)
        {
            _context.EeuProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuProgramContentAndResources> UpdateAsync(EeuProgramContentAndResources entity)
        {
            _context.EeuProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.EeuProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}