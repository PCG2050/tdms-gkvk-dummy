using Application.Interface.Repository.DataTables.FIU;
using Domain.Entities.FIU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables.FIU
{
    public class FIUActivityRepository : IFIUActivityRepository
    {
        private readonly TdmsDbContext _context;

        public FIUActivityRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<FIUActivity>> GetAllActiveAsync()
        {
            return await _context.FIUActivities
                .Where(a => a.IsActive)
                .OrderBy(a => a.DisplayOrder)
                .ToListAsync();
        }

        public async Task<FIUActivity?> GetByIdAsync(int id)
        {
            return await _context.FIUActivities.FindAsync(id);
        }

        public async Task<List<FIUActivity>> GetByCategoryAsync(string category)
        {
            return await _context.FIUActivities
                .Where(a => a.IsActive && a.ActivityCategory == category)
                .OrderBy(a => a.DisplayOrder)
                .ToListAsync();
        }
    }
}
