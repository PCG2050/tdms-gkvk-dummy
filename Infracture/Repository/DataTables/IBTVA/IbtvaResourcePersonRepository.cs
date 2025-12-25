// IbtvaResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaResourcePersonRepository : IIbtvaResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.IbtvaResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Include(r => r.ProgramContentAndResources)
                    .ThenInclude(pc => pc.ProgramDetails)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<IbtvaResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.IbtvaResourcePersons
                .Include(r => r.ResourceType)
                .Include(r => r.Responsibility)
                .Where(rp => rp.IbtvaProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<IbtvaResourcePerson> CreateAsync(IbtvaResourcePerson entity)
        {
            _context.IbtvaResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaResourcePerson> UpdateAsync(IbtvaResourcePerson entity)
        {
            _context.IbtvaResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}