// NaepReportRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepReportRepository : INaepReportRepository
    {
        private readonly TdmsDbContext _context;

        public NaepReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepReport?> GetByIdAsync(int id)
        {
            return await _context.NaepReports.FindAsync(id);
        }

        public async Task<NaepReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepReports
                .FirstOrDefaultAsync(r => r.NaepProgramDetailsId == programId);
        }

        public async Task<NaepReport> CreateAsync(NaepReport entity)
        {
            _context.NaepReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepReport> UpdateAsync(NaepReport entity)
        {
            _context.NaepReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepReports.FindAsync(id);
            if (entity != null)
            {
                _context.NaepReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}