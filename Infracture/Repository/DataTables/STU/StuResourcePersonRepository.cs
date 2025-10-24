// StuResourcePersonRepository.cs
using Application.Interface.Repository.DataTables.DEU;
using Domain.Entities.DEU;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.STU
{
    public class StuResourcePersonRepository : IStuResourcePersonRepository
    {
        private readonly TdmsDbContext _context;

        public StuResourcePersonRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<StuResourcePerson?> GetByIdAsync(int id)
        {
            return await _context.StuResourcePersons.FindAsync(id);
        }

        public async Task<List<StuResourcePerson>> GetByContentIdAsync(int contentId)
        {
            return await _context.StuResourcePersons
                .Where(rp => rp.StuProgramContentAndResourcesId == contentId)
                .OrderBy(rp => rp.CreatedAt)
                .ToListAsync();
        }

        public async Task<StuResourcePerson> CreateAsync(StuResourcePerson entity)
        {
            _context.StuResourcePersons.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StuResourcePerson> UpdateAsync(StuResourcePerson entity)
        {
            _context.StuResourcePersons.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.StuResourcePersons.FindAsync(id);
            if (entity != null)
            {
                _context.StuResourcePersons.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}