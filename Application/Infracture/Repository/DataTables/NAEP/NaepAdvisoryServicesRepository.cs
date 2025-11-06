// NaepAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepAdvisoryServicesRepository : INaepAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public NaepAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.NaepAdvisoryServices.FindAsync(id);
        }

        public async Task<NaepAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.NaepAdvisoryServices
                .FirstOrDefaultAsync(a => a.NaepProgramDetailsId == programId);
        }

        public async Task<NaepAdvisoryServices> CreateAsync(NaepAdvisoryServices entity)
        {
            _context.NaepAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepAdvisoryServices> UpdateAsync(NaepAdvisoryServices entity)
        {
            _context.NaepAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.NaepAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}