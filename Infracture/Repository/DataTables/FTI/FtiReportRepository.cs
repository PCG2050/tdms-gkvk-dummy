// FtiReportRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiReportRepository : IFtiReportRepository
    {
        private readonly TdmsDbContext _context;

        public FtiReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiReport?> GetByIdAsync(int id)
        {
            return await _context.FtiReports.FindAsync(id);
        }

        public async Task<FtiReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.FtiReports
                .FirstOrDefaultAsync(r => r.FtiProgramDetailsId == programId);
        }

        public async Task<FtiReport> CreateAsync(FtiReport entity)
        {
            _context.FtiReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiReport> UpdateAsync(FtiReport entity)
        {
            _context.FtiReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiReports.FindAsync(id);
            if (entity != null)
            {
                _context.FtiReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
