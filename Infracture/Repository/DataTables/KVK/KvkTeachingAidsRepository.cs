// KvkTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class KvkTeachingAidsRepository : IKvkTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public KvkTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkTeachingAidsDeveloped> CreateAsync(KvkTeachingAidsDeveloped entity)
        {
            _context.KvkTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.KvkTeachingAidsDeveloped
                .Include(t => t.TypeOfAid)
                .Include(t => t.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<KvkTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.KvkTeachingAidsDeveloped
                .Include(t => t.TypeOfAid)
                .Where(t => t.KvkProgramContentAndResourcesId == contentId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkTeachingAidsDeveloped> UpdateAsync(KvkTeachingAidsDeveloped entity)
        {
            _context.KvkTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.KvkTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}