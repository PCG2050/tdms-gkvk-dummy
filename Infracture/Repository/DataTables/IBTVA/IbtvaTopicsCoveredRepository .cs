// IbtvaTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaTopicsCoveredRepository : IIbtvaTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.IbtvaTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<IbtvaTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.IbtvaTopicsCoveredInClass
                .Where(t => t.IbtvaProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IbtvaTopicsCoveredInClass> CreateAsync(IbtvaTopicsCoveredInClass entity)
        {
            _context.IbtvaTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaTopicsCoveredInClass> UpdateAsync(IbtvaTopicsCoveredInClass entity)
        {
            _context.IbtvaTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}