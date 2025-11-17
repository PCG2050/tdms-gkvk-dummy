// FtiResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Application.Interface.Repository.DataTables.FTI;
using Domain.Entities.DEU;
using Domain.Entities.FTI;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.FTI
{
    public class FtiResourcePersonRepository : IFtiResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public FtiResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FtiResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.FtiResourcePersons.FindAsync(id);
        }

        public async Task<List<FtiResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.FtiResourcePersons
                .Where(rp => rp.FtiProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<FtiResourcePerson> CreateAsync(FtiResourcePerson entity)
        {
            _context.FtiResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FtiResourcePerson> UpdateAsync(FtiResourcePerson entity)
        {
            _context.FtiResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FtiResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.FtiResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
