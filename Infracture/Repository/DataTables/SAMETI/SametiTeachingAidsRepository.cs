// SametiTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiTeachingAidsRepository : ISametiTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public SametiTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.SametiTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<SametiTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.SametiTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.SametiProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<SametiTeachingAidsDeveloped> CreateAsync(SametiTeachingAidsDeveloped entity)
        {
            _context.SametiTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiTeachingAidsDeveloped> UpdateAsync(SametiTeachingAidsDeveloped entity)
        {
            _context.SametiTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.SametiTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}