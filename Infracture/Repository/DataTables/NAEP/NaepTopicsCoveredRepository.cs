// NaepTopicsCoveredRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepTopicsCoveredRepository : INaepTopicsCoveredRepository
    {
        private readonly TdmsDbContext _context;

        public NaepTopicsCoveredRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepTopicsCoveredInClass?> GetByIdAsync(int id)
        {
            return await _context.NaepTopicsCoveredInClass.FindAsync(id);
        }

        public async Task<List<NaepTopicsCoveredInClass>> GetByContentIdAsync(int contentId)
        {
            return await _context.NaepTopicsCoveredInClass
                .Where(t => t.NaepProgramContentAndResourcesId == contentId)
                .OrderBy(t => t.Date)
                .ThenBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<NaepTopicsCoveredInClass> CreateAsync(NaepTopicsCoveredInClass entity)
        {
            _context.NaepTopicsCoveredInClass.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepTopicsCoveredInClass> UpdateAsync(NaepTopicsCoveredInClass entity)
        {
            _context.NaepTopicsCoveredInClass.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepTopicsCoveredInClass.FindAsync(id);
            if (entity != null)
            {
                _context.NaepTopicsCoveredInClass.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}