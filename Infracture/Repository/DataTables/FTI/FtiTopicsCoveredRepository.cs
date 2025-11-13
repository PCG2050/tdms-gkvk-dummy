// FtiTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiTopicsCoveredRepository : IFtiTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public FtiTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.FtiTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<FtiTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.FtiTopicsCoveredInClass
                .Where(t => t.FtiProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<FtiTopicsCoveredInClass> CreateAsync(FtiTopicsCoveredInClass entity)
        {
            _context.FtiTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiTopicsCoveredInClass> UpdateAsync(FtiTopicsCoveredInClass entity)
        {
            _context.FtiTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.FtiTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
