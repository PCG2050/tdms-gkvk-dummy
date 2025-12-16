// SametiReportRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiReportRepository : ISametiReportRepository
    {
        private readonly TdmsDbContext _context;

        public SametiReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiReport?> GetByIdAsync(int id)
        {
            return await _context.SametiReports.FindAsync(id);
        }

        public async Task<SametiReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.SametiReports
                .FirstOrDefaultAsync(r => r.SametiProgramDetailsId == programId);
        }

        public async Task<SametiReport> CreateAsync(SametiReport entity)
        {
            _context.SametiReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiReport> UpdateAsync(SametiReport entity)
        {
            _context.SametiReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiReports.FindAsync(id);
            if (entity != null)
            {
                _context.SametiReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}