// IbtvaProgramContentRepository.cs
using Application.Interface.Repository.DataTables.IBTVA;
using Domain.Entities.IBTVA;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables.IBTVA
{
    public class IbtvaProgramContentRepository : IIbtvaProgramContentRepository
    {
        private readonly TdmsDbContext _context;

        public IbtvaProgramContentRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IbtvaProgramContentAndResources?> GetByIdAsync(int id)
        {
            return await _context.IbtvaProgramContentAndResources.FindAsync(id);
        }

        public async Task<IbtvaProgramContentAndResources?> GetWithDetailsAsync(int id)
        {
            return await _context.IbtvaProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid!)
                .Include(c => c.ProgramDetails)
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<List<IbtvaProgramContentAndResources>> GetByProgramIdAsync(int programId)
        {
            return await _context.IbtvaProgramContentAndResources
                .Include(c => c.ResourcePersons!)
                .Include(c => c.TopicsCovered!)
                .Include(c => c.TeachingAids!)
                    .ThenInclude(ta => ta.TypeOfAid!)
                .Where(c => c.IbtvaProgramDetailsId == programId)
                .ToListAsync();
        }

        public async Task<IbtvaProgramContentAndResources> CreateAsync(IbtvaProgramContentAndResources entity)
        {
            _context.IbtvaProgramContentAndResources.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IbtvaProgramContentAndResources> UpdateAsync(IbtvaProgramContentAndResources entity)
        {
            _context.IbtvaProgramContentAndResources.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.IbtvaProgramContentAndResources.FindAsync(id);
            if (entity != null)
            {
                _context.IbtvaProgramContentAndResources.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}