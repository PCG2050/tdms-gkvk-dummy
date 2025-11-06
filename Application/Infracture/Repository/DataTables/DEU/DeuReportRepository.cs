// DeuReportRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuReportRepository : IDeuReportRepository
    {
        private readonly TdmsDbContext _context;

        public DeuReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuReport?> GetByIdAsync(int id)
        {
            return await _context.DeuReports.FindAsync(id);
        }

        public async Task<DeuReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.DeuReports
                .FirstOrDefaultAsync(r => r.DeuProgramDetailsId == programId);
        }

        public async Task<DeuReport> CreateAsync(DeuReport entity)
        {
            _context.DeuReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuReport> UpdateAsync(DeuReport entity)
        {
            _context.DeuReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuReports.FindAsync(id);
            if (entity != null)
            {
                _context.DeuReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}