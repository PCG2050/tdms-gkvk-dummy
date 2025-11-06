// AticAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.ATIC;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticAdvisoryServicesRepository : IAticAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public AticAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.AticAdvisoryServices.FindAsync(id);
        }

        public async Task<AticAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.AticAdvisoryServices
                .FirstOrDefaultAsync(a => a.AticProgramDetailsId == programId);
        }

        public async Task<AticAdvisoryServices> CreateAsync(AticAdvisoryServices entity)
        {
            _context.AticAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticAdvisoryServices> UpdateAsync(AticAdvisoryServices entity)
        {
            _context.AticAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.AticAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}