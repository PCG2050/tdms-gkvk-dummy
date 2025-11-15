// FTITeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTITeachingAidsRepository : IFTITeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public FTITeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTITeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.FTITeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<FTITeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.FTITeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.FTIProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<FTITeachingAidsDeveloped> CreateAsync(FTITeachingAidsDeveloped entity)
        {
            _context.FTITeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTITeachingAidsDeveloped> UpdateAsync(FTITeachingAidsDeveloped entity)
        {
            _context.FTITeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTITeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.FTITeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
