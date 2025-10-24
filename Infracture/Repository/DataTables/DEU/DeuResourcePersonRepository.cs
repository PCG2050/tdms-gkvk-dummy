// DeuResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.DEU
{
    public class DeuResourcePersonRepository : IDeuResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public DeuResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<DeuResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.DeuResourcePersons.FindAsync(id);
        }

        public async Task<List<DeuResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.DeuResourcePersons
                .Where(rp => rp.DeuProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<DeuResourcePerson> CreateAsync(DeuResourcePerson entity)
        {
            _context.DeuResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<DeuResourcePerson> UpdateAsync(DeuResourcePerson entity)
        {
            _context.DeuResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.DeuResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.DeuResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}