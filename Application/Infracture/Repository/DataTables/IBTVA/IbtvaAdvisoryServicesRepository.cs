// IbtvaAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaAdvisoryServicesRepository : IIbtvaAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.IbtvaAdvisoryServices.FindAsync(id);
        }

        public async Task<IbtvaAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.IbtvaAdvisoryServices
                .FirstOrDefaultAsync(a => a.IbtvaProgramDetailsId == programId);
        }

        public async Task<IbtvaAdvisoryServices> CreateAsync(IbtvaAdvisoryServices entity)
        {
            _context.IbtvaAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaAdvisoryServices> UpdateAsync(IbtvaAdvisoryServices entity)
        {
            _context.IbtvaAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}