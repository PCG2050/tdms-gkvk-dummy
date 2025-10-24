// NaepTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepTeachingAidsRepository : INaepTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public NaepTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.NaepTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<NaepTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.NaepTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.NaepProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<NaepTeachingAidsDeveloped> CreateAsync(NaepTeachingAidsDeveloped entity)
        {
            _context.NaepTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepTeachingAidsDeveloped> UpdateAsync(NaepTeachingAidsDeveloped entity)
        {
            _context.NaepTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.NaepTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}