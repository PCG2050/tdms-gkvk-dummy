// FTIResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FTIResourcePersonRepository : IFTIResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public FTIResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FTIResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.FTIResourcePersons.FindAsync(id);
        }

        public async Task<List<FTIResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.FTIResourcePersons
                .Where(rp => rp.FTIProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<FTIResourcePerson> CreateAsync(FTIResourcePerson entity)
        {
            _context.FTIResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FTIResourcePerson> UpdateAsync(FTIResourcePerson entity)
        {
            _context.FTIResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FTIResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.FTIResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
