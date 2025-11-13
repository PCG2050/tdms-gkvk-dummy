// FtiTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiTeachingAidsRepository : IFtiTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public FtiTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.FtiTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<FtiTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.FtiTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.FtiProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<FtiTeachingAidsDeveloped> CreateAsync(FtiTeachingAidsDeveloped entity)
        {
            _context.FtiTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiTeachingAidsDeveloped> UpdateAsync(FtiTeachingAidsDeveloped entity)
        {
            _context.FtiTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.FtiTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
