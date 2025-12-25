// SametiResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.SAMETI;
using Domain.Entities.SAMETI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.SAMETI
{
    public class SametiResourcePersonRepository : ISametiResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public SametiResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<SametiResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.SametiResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Include(r => r.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<SametiResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.SametiResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Where(rp => rp.SametiProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<SametiResourcePerson> CreateAsync(SametiResourcePerson entity)
        {
            _context.SametiResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<SametiResourcePerson> UpdateAsync(SametiResourcePerson entity)
        {
            _context.SametiResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.SametiResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.SametiResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}