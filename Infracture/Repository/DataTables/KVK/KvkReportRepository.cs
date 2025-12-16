// KvkReportRepository.cs
using Application.Interface.Repository.DataTables.KVK;
using Domain.Entities.KVK;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkReportRepository : IKvkReportRepository
    {
        private readonly TdmsDbContext _context;

        public KvkReportRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkReport?> GetByIdAsync(int id)
        {
            return await _context.KvkReports.FindAsync(id);
        }

        public async Task<KvkReport?> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkReports
                .FirstOrDefaultAsync(r => r.KvkProgramDetailsId == programId);
        }

        public async Task<KvkReport> CreateAsync(KvkReport entity)
        {
            _context.KvkReports.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkReport> UpdateAsync(KvkReport entity)
        {
            _context.KvkReports.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkReports.FindAsync(id);
            if (entity != null)
            {
                _context.KvkReports.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}