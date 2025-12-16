using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuOftResultRepository : IEeuOftResultRepository
    {
        private readonly TdmsDbContext _context;

        public EeuOftResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuOftResult> CreateAsync(EeuOftResult entity)
        {
            _context.EeuOFTResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuOftResult?> GetByIdAsync(int id)
        {
            return await _context.EeuOFTResults
                .Include(o => o.DetailsOfDemo)
                .Include(o => o.EeuResult)
                    .ThenInclude(r => r.ProgramDetails)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<EeuOftResult>> GetByResultIdAsync(int resultId)
        {
            return await _context.EeuOFTResults
                .Include(o => o.DetailsOfDemo)
                .Where(o => o.EeuResultId == resultId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<EeuOftResult> UpdateAsync(EeuOftResult entity)
        {
            _context.EeuOFTResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuOFTResults.FindAsync(id);
            if (entity != null)
            {
                _context.EeuOFTResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
