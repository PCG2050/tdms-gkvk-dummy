// DeuTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuTeachingAidsRepository : IDeuTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public DeuTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.DeuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<DeuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.DeuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.DeuProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<DeuTeachingAidsDeveloped> CreateAsync(DeuTeachingAidsDeveloped entity)
        {
            _context.DeuTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuTeachingAidsDeveloped> UpdateAsync(DeuTeachingAidsDeveloped entity)
        {
            _context.DeuTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.DeuTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}