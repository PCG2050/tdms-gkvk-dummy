// AticTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticTopicsCoveredRepository : IAticTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public AticTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.AticTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<AticTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.AticTopicsCoveredInClass
                .Where(t => t.AticProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<AticTopicsCoveredInClass> CreateAsync(AticTopicsCoveredInClass entity)
        {
            _context.AticTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticTopicsCoveredInClass> UpdateAsync(AticTopicsCoveredInClass entity)
        {
            _context.AticTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.AticTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}