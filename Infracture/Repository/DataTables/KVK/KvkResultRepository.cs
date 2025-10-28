using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkResultRepository : IKvkResultRepository
    {
        private readonly TdmsDbContext _context;

        public KvkResultRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkResult?> GetByIdAsync(int id)
        {
            return await _context.KvkResults
                .Include(r => r.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<KvkResult?> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkResults
                .FirstOrDefaultAsync(r => r.KvkProgramDetailsId == programId);
        }

        public async Task<KvkResult?> GetWithDetailsAsync(int id)
        {
            return await _context.KvkResults
                .Include(r => r.ProgramDetails)
                .Include(r => r.FldResults!)
                    .ThenInclude(f => f.DetailsOfDemo)
                .Include(r => r.OftResults!)
                    .ThenInclude(o => o.DetailsOfDemo)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<KvkResult> CreateAsync(KvkResult entity)
        {
            _context.KvkResults.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkResult> UpdateAsync(KvkResult entity)
        {
            _context.KvkResults.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkResults.FindAsync(id);
            if (entity != null)
            {
                _context.KvkResults.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
