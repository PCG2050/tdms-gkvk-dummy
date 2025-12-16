// KvkResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.KVK
{
    public class KvkResourcePersonRepository : IKvkResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public KvkResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<KvkResourcePerson> CreateAsync(KvkResourcePerson entity)
        {
            _context.KvkResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<KvkResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.KvkResourcePersons
                .Include(r => r.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<KvkResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.KvkResourcePersons
                .Where(r => r.KvkProgramContentAndResourcesId == contentId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<KvkResourcePerson> UpdateAsync(KvkResourcePerson entity)
        {
            _context.KvkResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.KvkResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.KvkResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}