// AticResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.ATIC
{
    public class AticResourcePersonRepository : IAticResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public AticResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<AticResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.AticResourcePersons.FindAsync(id);
        }

        public async Task<List<AticResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.AticResourcePersons
                .Where(rp => rp.AticProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<AticResourcePerson> CreateAsync(AticResourcePerson entity)
        {
            _context.AticResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<AticResourcePerson> UpdateAsync(AticResourcePerson entity)
        {
            _context.AticResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.AticResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.AticResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}