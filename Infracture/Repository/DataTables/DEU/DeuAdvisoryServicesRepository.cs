// DeuAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuAdvisoryServicesRepository : IDeuAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public DeuAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.DeuAdvisoryServices.FindAsync(id);
        }

        public async Task<DeuAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.DeuAdvisoryServices
                .FirstOrDefaultAsync(a => a.DeuProgramDetailsId == programId);
        }

        public async Task<DeuAdvisoryServices> CreateAsync(DeuAdvisoryServices entity)
        {
            _context.DeuAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuAdvisoryServices> UpdateAsync(DeuAdvisoryServices entity)
        {
            _context.DeuAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.DeuAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}