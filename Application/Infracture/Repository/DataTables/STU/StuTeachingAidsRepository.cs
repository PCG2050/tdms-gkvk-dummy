// StuTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuTeachingAidsRepository : IStuTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public StuTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.StuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<StuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.StuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.StuProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<StuTeachingAidsDeveloped> CreateAsync(StuTeachingAidsDeveloped entity)
        {
            _context.StuTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuTeachingAidsDeveloped> UpdateAsync(StuTeachingAidsDeveloped entity)
        {
            _context.StuTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.StuTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}