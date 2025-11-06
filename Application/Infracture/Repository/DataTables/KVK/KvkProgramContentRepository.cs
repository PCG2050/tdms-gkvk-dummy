// KvkProgramContentRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkProgramContentRepository : IKvkProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public KvkProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkProgramContentAndResources> CreateAsync(KvkProgramContentAndResources entity)
        {
            _context.KvkProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<KvkProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ProgramDetails)
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<KvkProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid)
                .Where(c => c.KvkProgramDetailsId == programId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkProgramContentAndResources> UpdateAsync(KvkProgramContentAndResources entity)
        {
            _context.KvkProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.KvkProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}