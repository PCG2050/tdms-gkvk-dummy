using Application.Interface.Repository.DataTables.TblService;
using Domain.Entities.GenericTables.Service;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class TableHostelRepository : ITableHostelRepository
    {
        private readonly TdmsDbContext _context;
        public TableHostelRepository(TdmsDbContext context)
        {
            _context = context;
        }
        public async Task<TableHostel?> GetTableHostelByIdAsync(int id)
        {
            return await _context.TableHostels.FindAsync(id);
        }

        public async Task<List<TableHostel>> GetTableHostelsByServiceIdAsync(int serviceId)
        {
            return await _context.TableHostels
                .Where(h => h.ServiceId == serviceId)
                .ToListAsync();
        }

        public async Task<TableHostel> CreateTableHostelAsync(TableHostel entity)
        {
            _context.TableHostels.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TableHostel> UpdateTableHostelAsync(TableHostel entity)
        {
            _context.TableHostels.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteTableHostelAsync(int id)
        {
            var entity = await _context.TableHostels.FindAsync(id);
            if (entity != null)
            {
                _context.TableHostels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
