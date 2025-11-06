using Application.Interface.Repository.DataTables;
using Application.Interface.Repository.DataTables.TblService;
using Domain.Entities.GenericTables.Service;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.DataTables
{
    public class VisitorDetailRepository : IVisitorDetailsRepository
    {
        private readonly TdmsDbContext _context;

        public VisitorDetailRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<VisitorDetail?> GetVisitorDetailByIdAsync(int id)
        {
            return await _context.VisitorDetails.FindAsync(id);
        }

        public async Task<List<VisitorDetail>> GetVisitorDetailsByServiceIdAsync(int serviceId)
        {
            return await _context.VisitorDetails
                .Where(v => v.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<VisitorDetail> CreateVisitorDetailAsync(VisitorDetail entity)
        {
            _context.VisitorDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<VisitorDetail> UpdateVisitorDetailAsync(VisitorDetail entity)
        {
            _context.VisitorDetails.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteVisitorDetailAsync(int id)
        {
            var entity = await _context.VisitorDetails.FindAsync(id);
            if (entity != null)
            {
                _context.VisitorDetails.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
