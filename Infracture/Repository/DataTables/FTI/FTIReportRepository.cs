// FTIReportRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIReportRepository : IFTIReportRepository
    {
        private readonly TdmsDbContext _context;

        public FTIReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIReport?> GetByIdAsync(int id)
        {
            return await _context.FTIReports.FindAsync(id);
        }

        public async Task<FTIReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIReports
                .FirstOrDefaultAsync(r => r.FTIProgramDetailsId == programId);
        }

        public async Task<FTIReport> CreateAsync(FTIReport entity)
        {
            _context.FTIReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIReport> UpdateAsync(FTIReport entity)
        {
            _context.FTIReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIReports.FindAsync(id);
            if (entity != null)
            {
                _context.FTIReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
