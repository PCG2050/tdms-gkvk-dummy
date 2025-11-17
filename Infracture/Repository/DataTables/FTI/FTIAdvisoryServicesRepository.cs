// FtiAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.DEU;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiAdvisoryServicesRepository : IFtiAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public FtiAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.FtiAdvisoryServices.FindAsync(id);
        }

        public async Task<FtiAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.FtiAdvisoryServices
                .FirstOrDefaultAsync(a => a.FtiProgramDetailsId == programId);
        }

        public async Task<FtiAdvisoryServices> CreateAsync(FtiAdvisoryServices entity)
        {
            _context.FtiAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiAdvisoryServices> UpdateAsync(FtiAdvisoryServices entity)
        {
            _context.FtiAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.FtiAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
