// StuAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuAdvisoryServicesRepository : IStuAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public StuAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.StuAdvisoryServices.FindAsync(id);
        }

        public async Task<StuAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.StuAdvisoryServices
                .FirstOrDefaultAsync(a => a.StuProgramDetailsId == programId);
        }

        public async Task<StuAdvisoryServices> CreateAsync(StuAdvisoryServices entity)
        {
            _context.StuAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuAdvisoryServices> UpdateAsync(StuAdvisoryServices entity)
        {
            _context.StuAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.StuAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}