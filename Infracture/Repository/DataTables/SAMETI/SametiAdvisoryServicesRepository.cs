// SametiAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiAdvisoryServicesRepository : ISametiAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public SametiAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.SametiAdvisoryServices.FindAsync(id);
        }

        public async Task<SametiAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.SametiAdvisoryServices
                .FirstOrDefaultAsync(a => a.SametiProgramDetailsId == programId);
        }

        public async Task<SametiAdvisoryServices> CreateAsync(SametiAdvisoryServices entity)
        {
            _context.SametiAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiAdvisoryServices> UpdateAsync(SametiAdvisoryServices entity)
        {
            _context.SametiAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.SametiAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}