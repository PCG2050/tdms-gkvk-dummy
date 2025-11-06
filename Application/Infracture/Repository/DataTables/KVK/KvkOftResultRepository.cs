using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkOftResultRepository : IKvkOftResultRepository
    {
        private readonly TdmsDbContext _context;

        public KvkOftResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkOftResult> CreateAsync(KvkOftResult entity)
        {
            _context.KvkOFTResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkOftResult?> GetByIdAsync(int id)
        {
            return await _context.KvkOFTResults
                .Include(o => o.DetailsOfDemo)
                .Include(o => o.KvkResult)
                    .ThenInclude(r => r.ProgramDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<KvkOftResult>> GetByResultIdAsync(int resultId)
        {
            return await _context.KvkOFTResults
                .Include(o => o.DetailsOfDemo)
                .Where(o => o.KvkResultId == resultId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkOftResult> UpdateAsync(KvkOftResult entity)
        {
            _context.KvkOFTResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkOFTResults.FindAsync(id);
            if (entity != null)
            {
                _context.KvkOFTResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
