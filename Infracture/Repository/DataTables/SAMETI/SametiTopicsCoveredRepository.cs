// SametiTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiTopicsCoveredRepository : ISametiTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public SametiTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.SametiTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<SametiTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.SametiTopicsCoveredInClass
                .Where(t => t.SametiProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<SametiTopicsCoveredInClass> CreateAsync(SametiTopicsCoveredInClass entity)
        {
            _context.SametiTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiTopicsCoveredInClass> UpdateAsync(SametiTopicsCoveredInClass entity)
        {
            _context.SametiTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.SametiTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}