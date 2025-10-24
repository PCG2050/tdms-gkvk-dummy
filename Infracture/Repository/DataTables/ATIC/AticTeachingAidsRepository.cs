// AticTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class AticTeachingAidsRepository : IAticTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public AticTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.AticTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<AticTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.AticTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.AticProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<AticTeachingAidsDeveloped> CreateAsync(AticTeachingAidsDeveloped entity)
        {
            _context.AticTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticTeachingAidsDeveloped> UpdateAsync(AticTeachingAidsDeveloped entity)
        {
            _context.AticTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.AticTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}