using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaProgramRepository : IIbtvaProgramRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaProgramRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaProgramDetails> CreateAsync(IbtvaProgramDetails program)
        {
            await _context.IbtvaProgramDetails.AddAsync(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<IbtvaProgramDetails?> GetByIdAsync(int id, int trainerId)
        {
            return await _context.IbtvaProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.Type)
                .Include(p => p.Theme)
                .Include(p => p.ThematicArea)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .FirstOrDefaultAsync(p => p.Id == id && p.CreatedById == trainerId);
        }

        public async Task<IEnumerable<IbtvaProgramDetails>> GetAllAsync(int trainerId)
        {
            return await _context.IbtvaProgramDetails
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.UnitLocation)
                    .ThenInclude(ul => ul.Unit)
                .Where(p => p.CreatedById == trainerId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IbtvaProgramDetails?> UpdateAsync(IbtvaProgramDetails program)
        {
            var existing = await _context.IbtvaProgramDetails
                .FirstOrDefaultAsync(p => p.Id == program.Id && p.CreatedById == program.CreatedById);

            if (existing == null || existing.FormStatus == "Approved")
                return null;

            _context.Entry(existing).CurrentValues.SetValues(program);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id, int trainerId)
        {
            var program = await _context.IbtvaProgramDetails
                .FirstOrDefaultAsync(p => p.Id == id && p.CreatedById == trainerId);

            if (program == null || program.FormStatus == "Pending" || program.FormStatus == "Approved")
                return false;

            _context.IbtvaProgramDetails.Remove(program);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SubmitAsync(int id, int trainerId)
        {
            var program = await _context.IbtvaProgramDetails
                .FirstOrDefaultAsync(p => p.Id == id && p.CreatedById == trainerId);

            if (program == null || program.FormStatus == "Pending" || program.FormStatus == "Approved")
                return false;

            program.FormStatus = "Pending";
            program.UpdatedAt = DateTimeOffset.UtcNow;
            program.UpdatedById = trainerId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}