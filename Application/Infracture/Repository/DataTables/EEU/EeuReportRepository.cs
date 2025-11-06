// EeuReportRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuReportRepository : IEeuReportRepository
    {
        private readonly TdmsDbContext _context;

        public EeuReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuReport?> GetByIdAsync(int id)
        {
            return await _context.EeuReports.FindAsync(id);
        }

        public async Task<EeuReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuReports
                .FirstOrDefaultAsync(r => r.EeuProgramDetailsId == programId);
        }

        public async Task<EeuReport> CreateAsync(EeuReport entity)
        {
            _context.EeuReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuReport> UpdateAsync(EeuReport entity)
        {
            _context.EeuReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuReports.FindAsync(id);
            if (entity != null)
            {
                _context.EeuReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}