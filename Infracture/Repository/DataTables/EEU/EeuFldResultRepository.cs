using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuFldResultRepository : IEeuFldResultRepository
    {
        private readonly TdmsDbContext _context;

        public EeuFldResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuFldResult> CreateAsync(EeuFldResult entity)
        {
            _context.EeuFLDResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuFldResult?> GetByIdAsync(int id)
        {
            return await _context.EeuFLDResults
                .Include(f => f.DetailsOfDemo)
                .Include(f => f.EeuResult)
                    .ThenInclude(r => r.ProgramDetails)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<EeuFldResult>> GetByResultIdAsync(int resultId)
        {
            return await _context.EeuFLDResults
                .Include(f => f.DetailsOfDemo)
                .Where(f => f.EeuResultId == resultId)
                .OrderBy(f => f.FldNumber)
                .ToListAsync();
        }

        public async Task<EeuFldResult> UpdateAsync(EeuFldResult entity)
        {
            _context.EeuFLDResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuFLDResults.FindAsync(id);
            if (entity != null)
            {
                _context.EeuFLDResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
