// KvkTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class KvkTopicsCoveredRepository : IKvkTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public KvkTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkTopicsCoveredInClass> CreateAsync(KvkTopicsCoveredInClass entity)
        {
            _context.KvkTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.KvkTopicsCoveredInClass
                .Include(t => t.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<KvkTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.KvkTopicsCoveredInClass
                .Where(t => t.KvkProgramContentAndResourcesId == contentId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkTopicsCoveredInClass> UpdateAsync(KvkTopicsCoveredInClass entity)
        {
            _context.KvkTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.KvkTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}