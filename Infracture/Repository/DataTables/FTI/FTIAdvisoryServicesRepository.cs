// FTIAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIAdvisoryServicesRepository : IFTIAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public FTIAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.FTIAdvisoryServices.FindAsync(id);
        }

        public async Task<FTIAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.FTIAdvisoryServices
                .FirstOrDefaultAsync(a => a.FTIProgramDetailsId == programId);
        }

        public async Task<FTIAdvisoryServices> CreateAsync(FTIAdvisoryServices entity)
        {
            _context.FTIAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIAdvisoryServices> UpdateAsync(FTIAdvisoryServices entity)
        {
            _context.FTIAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.FTIAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
