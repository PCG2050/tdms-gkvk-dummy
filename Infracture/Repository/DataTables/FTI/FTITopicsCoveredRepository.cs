// FTITopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTITopicsCoveredRepository : IFTITopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public FTITopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTITopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.FTITopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<FTITopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.FTITopicsCoveredInClass
                .Where(t => t.FTIProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

        public async Task<FTITopicsCoveredInClass> CreateAsync(FTITopicsCoveredInClass entity)
        {
            _context.FTITopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTITopicsCoveredInClass> UpdateAsync(FTITopicsCoveredInClass entity)
        {
            _context.FTITopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTITopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.FTITopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
