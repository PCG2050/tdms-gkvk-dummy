// DeuTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuTopicsCoveredRepository : IDeuTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public DeuTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.DeuTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<DeuTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.DeuTopicsCoveredInClass
                .Where(t => t.DeuProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<DeuTopicsCoveredInClass> CreateAsync(DeuTopicsCoveredInClass entity)
        {
            _context.DeuTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuTopicsCoveredInClass> UpdateAsync(DeuTopicsCoveredInClass entity)
        {
            _context.DeuTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.DeuTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}