using Application.Interface.Repository.DataTables.ASM;
using Domain.Entities.ASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.ASM
{
    public class ASMVisitorDetailsRepository : IASMVisitorDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public ASMVisitorDetailsRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<ASMVisitorDetails> AddAsync(ASMVisitorDetails entity)
        {
            _context.ASMVisitorDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ASMVisitorDetails>> GetAllAsync()
        {
            return await _context.ASMVisitorDetails
                .Include(x => x.Status)
                .ToListAsync();
        }

        public async Task<ASMVisitorDetails?> GetByIdAsync(int id)
        {
            return await _context.ASMVisitorDetails
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ASMVisitorDetails> UpdateAsync(ASMVisitorDetails entity)
        {
            _context.ASMVisitorDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ASMVisitorDetails.FindAsync(id);
            if (entity == null) return false;

            _context.ASMVisitorDetails.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
