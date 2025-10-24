// DeuProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuProgramContentRepository : IDeuProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public DeuProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.DeuProgramContentAndResources.FindAsync(id);
        }

        public async Task<DeuProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.DeuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<DeuProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.DeuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.DeuProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<DeuProgramContentAndResources> CreateAsync(DeuProgramContentAndResources entity)
        {
            _context.DeuProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuProgramContentAndResources> UpdateAsync(DeuProgramContentAndResources entity)
        {
            _context.DeuProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.DeuProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}