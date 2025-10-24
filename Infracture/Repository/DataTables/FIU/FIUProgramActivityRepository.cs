using Application.Interface.Repository.DataTables.FIU;
using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.FIU
{
    public class FIUProgramActivityRepository : IFIUProgramActivityRepository
    {
        private readonly TdmsDbContext _context;

        public FIUProgramActivityRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<FIUProgramActivity> AddAsync(FIUProgramActivity entity)
        {
            _context.FIUProgramActivities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<FIUProgramActivity>> GetAllAsync()
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .ToListAsync();
        }

        public async Task<FIUProgramActivity?> GetByIdAsync(int id)
        {
            return await _context.FIUProgramActivities
                .Include(x => x.UnitLocation)
                .Include(x => x.Organization)
                .Include(x => x.FIUActivities)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
