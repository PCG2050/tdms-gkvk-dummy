// EeuResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.EEU
{
    public class EeuResourcePersonRepository : IEeuResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public EeuResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<EeuResourcePerson> CreateAsync(EeuResourcePerson entity)
        {
            _context.EeuResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<EeuResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.EeuResourcePersons
                .Include(r => r.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<EeuResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.EeuResourcePersons
                .Where(r => r.EeuProgramContentAndResourcesId == contentId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<EeuResourcePerson> UpdateAsync(EeuResourcePerson entity)
        {
            _context.EeuResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.EeuResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.EeuResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}