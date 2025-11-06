// AticReportRepository.cs
using Application.Interface.Repository.DataTables.ATIC;
using Domain.Entities.ATIC;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticReportRepository : IAticReportRepository
    {
        private readonly TdmsDbContext _context;

        public AticReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticReport?> GetByIdAsync(int id)
        {
            return await _context.AticReports.FindAsync(id);
        }

        public async Task<AticReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.AticReports
                .FirstOrDefaultAsync(r => r.AticProgramDetailsId == programId);
        }

        public async Task<AticReport> CreateAsync(AticReport entity)
        {
            _context.AticReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticReport> UpdateAsync(AticReport entity)
        {
            _context.AticReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticReports.FindAsync(id);
            if (entity != null)
            {
                _context.AticReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}