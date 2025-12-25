// NaepResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.NAEP
{
    public class NaepResourcePersonRepository : INaepResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public NaepResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<NaepResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.NaepResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Include(r => r.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<NaepResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.NaepResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Where(rp => rp.NaepProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<NaepResourcePerson> CreateAsync(NaepResourcePerson entity)
        {
            _context.NaepResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<NaepResourcePerson> UpdateAsync(NaepResourcePerson entity)
        {
            _context.NaepResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.NaepResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.NaepResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}