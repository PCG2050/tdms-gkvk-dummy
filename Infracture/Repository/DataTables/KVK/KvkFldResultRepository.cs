using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkFldResultRepository : IKvkFldResultRepository
    {
        private readonly TdmsDbContext _context;

        public KvkFldResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkFldResult> CreateAsync(KvkFldResult entity)
        {
            _context.KvkFLDResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkFldResult?> GetByIdAsync(int id)
        {
            return await _context.KvkFLDResults
                .Include(f => f.DetailsOfDemo)
                .Include(f => f.KvkResult)
                    .ThenInclude(r => r.ProgramDetails)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<KvkFldResult>> GetByResultIdAsync(int resultId)
        {
            return await _context.KvkFLDResults
                .Include(f => f.DetailsOfDemo)
                .Where(f => f.KvkResultId == resultId)
                .OrderBy(f => f.FldNumber)
                .ToListAsync();
        }

        public async Task<KvkFldResult> UpdateAsync(KvkFldResult entity)
        {
            _context.KvkFLDResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkFLDResults.FindAsync(id);
            if (entity != null)
            {
                _context.KvkFLDResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
