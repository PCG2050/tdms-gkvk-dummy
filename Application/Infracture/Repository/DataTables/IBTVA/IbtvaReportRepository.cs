// IbtvaReportRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaReportRepository : IIbtvaReportRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaReport?> GetByIdAsync(int id)
        {
            return await _context.IbtvaReports.FindAsync(id);
        }

        public async Task<IbtvaReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.IbtvaReports
                .FirstOrDefaultAsync(r => r.IbtvaProgramDetailsId == programId);
        }

        public async Task<IbtvaReport> CreateAsync(IbtvaReport entity)
        {
            _context.IbtvaReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaReport> UpdateAsync(IbtvaReport entity)
        {
            _context.IbtvaReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaReports.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}