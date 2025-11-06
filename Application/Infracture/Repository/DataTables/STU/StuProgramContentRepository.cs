// StuProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuProgramContentRepository : IStuProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public StuProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.StuProgramContentAndResources.FindAsync(id);
        }

        public async Task<StuProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.StuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<StuProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.StuProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.StuProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<StuProgramContentAndResources> CreateAsync(StuProgramContentAndResources entity)
        {
            _context.StuProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuProgramContentAndResources> UpdateAsync(StuProgramContentAndResources entity)
        {
            _context.StuProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.StuProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}