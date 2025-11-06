// StuTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuTopicsCoveredRepository : IStuTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public StuTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.StuTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<StuTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.StuTopicsCoveredInClass
                .Where(t => t.StuProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<StuTopicsCoveredInClass> CreateAsync(StuTopicsCoveredInClass entity)
        {
            _context.StuTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuTopicsCoveredInClass> UpdateAsync(StuTopicsCoveredInClass entity)
        {
            _context.StuTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.StuTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}