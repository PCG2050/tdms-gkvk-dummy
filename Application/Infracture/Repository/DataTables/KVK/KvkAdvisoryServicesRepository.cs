// KvkAdvisoryServicesRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkAdvisoryServicesRepository : IKvkAdvisoryServicesRepository
    {
        private readonly TdmsDbContext _context;

        public KvkAdvisoryServicesRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkAdvisoryServices?> GetByIdAsync(int id)
        {
            return await _context.KvkAdvisoryServices.FindAsync(id);
        }

        public async Task<KvkAdvisoryServices?> GetByProgramIdAsync(int programId)
        {
            return await _context.KvkAdvisoryServices
                .FirstOrDefaultAsync(a => a.KvkProgramDetailsId == programId);
        }

        public async Task<KvkAdvisoryServices> CreateAsync(KvkAdvisoryServices entity)
        {
            _context.KvkAdvisoryServices.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkAdvisoryServices> UpdateAsync(KvkAdvisoryServices entity)
        {
            _context.KvkAdvisoryServices.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkAdvisoryServices.FindAsync(id);
            if (entity != null)
            {
                _context.KvkAdvisoryServices.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}