// StuReportRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuReportRepository : IStuReportRepository
    {
        private readonly TdmsDbContext _context;

        public StuReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuReport?> GetByIdAsync(int id)
        {
            return await _context.StuReports.FindAsync(id);
        }

        public async Task<StuReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.StuReports
                .FirstOrDefaultAsync(r => r.StuProgramDetailsId == programId);
        }

        public async Task<StuReport> CreateAsync(StuReport entity)
        {
            _context.StuReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuReport> UpdateAsync(StuReport entity)
        {
            _context.StuReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuReports.FindAsync(id);
            if (entity != null)
            {
                _context.StuReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}