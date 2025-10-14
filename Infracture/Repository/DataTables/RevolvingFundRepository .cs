using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.TblService;
using Domain.Entities.GenericTables.Service;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class RevolvingFundRepository : IRevolvingFundRepository
    {
        private readonly TdmsDbContext _context;

        public RevolvingFundRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<RevolvingFundStatus?> GetRevolvingFundStatusByIdAsync(int id)
        {
            return await _context.RevolvingFundStatuses.FindAsync(id);
        }

        public async Task<List<RevolvingFundStatus>> GetRevolvingFundStatusesByServiceIdAsync(int serviceId)
        {
            return await _context.RevolvingFundStatuses
                .Where(r => r.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<RevolvingFundStatus> CreateRevolvingFundStatusAsync(RevolvingFundStatus entity)
        {
            _context.RevolvingFundStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RevolvingFundStatus> UpdateRevolvingFundStatusAsync(RevolvingFundStatus entity)
        {
            _context.RevolvingFundStatuses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteRevolvingFundStatusAsync(int id)
        {
            var entity = await _context.RevolvingFundStatuses.FindAsync(id);
            if (entity != null)
            {
                _context.RevolvingFundStatuses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
