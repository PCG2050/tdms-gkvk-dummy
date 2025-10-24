// EeuTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuTopicsCoveredRepository : IEeuTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public EeuTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.EeuTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<EeuTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.EeuTopicsCoveredInClass
                .Where(t => t.EeuProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<EeuTopicsCoveredInClass> CreateAsync(EeuTopicsCoveredInClass entity)
        {
            _context.EeuTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuTopicsCoveredInClass> UpdateAsync(EeuTopicsCoveredInClass entity)
        {
            _context.EeuTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.EeuTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}