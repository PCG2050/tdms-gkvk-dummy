// EeuAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuAdvisoryServicesRepository : IEeuAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public EeuAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.EeuAdvisoryServices.FindAsync(id);
        }

        public async Task<EeuAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.EeuAdvisoryServices
                .FirstOrDefaultAsync(a => a.EeuProgramDetailsId == programId);
        }

        public async Task<EeuAdvisoryServices> CreateAsync(EeuAdvisoryServices entity)
        {
            _context.EeuAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuAdvisoryServices> UpdateAsync(EeuAdvisoryServices entity)
        {
            _context.EeuAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.EeuAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}