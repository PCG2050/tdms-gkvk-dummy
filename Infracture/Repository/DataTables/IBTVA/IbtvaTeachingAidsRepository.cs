// IbtvaTeachingAidsRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaTeachingAidsRepository : IIbtvaTeachingAidsRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaTeachingAidsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaTeachingAidsDeveloped?> GetByIdAsync(int id)
        {
            return await _context.IbtvaTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<IbtvaTeachingAidsDeveloped>> GetByContentIdAsync(int contentId)
        {
            return await _context.IbtvaTeachingAidsDeveloped
                .Include(ta => ta.TypeOfAid)
                .Where(ta => ta.IbtvaProgramContentAndResourcesId == contentId)
                .OrderBy(ta => ta.CreatedAt)
                .ToListAsync();
        }

        public async Task<IbtvaTeachingAidsDeveloped> CreateAsync(IbtvaTeachingAidsDeveloped entity)
        {
            _context.IbtvaTeachingAidsDeveloped.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaTeachingAidsDeveloped> UpdateAsync(IbtvaTeachingAidsDeveloped entity)
        {
            _context.IbtvaTeachingAidsDeveloped.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaTeachingAidsDeveloped.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaTeachingAidsDeveloped.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}