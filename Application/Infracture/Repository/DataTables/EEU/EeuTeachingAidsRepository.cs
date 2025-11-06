// EeuTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuTeachingAidsRepository : IEeuTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public EeuTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.EeuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<EeuTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.EeuTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.EeuProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<EeuTeachingAidsDeveloped> CreateAsync(EeuTeachingAidsDeveloped entity)
        {
            _context.EeuTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuTeachingAidsDeveloped> UpdateAsync(EeuTeachingAidsDeveloped entity)
        {
            _context.EeuTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.EeuTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}