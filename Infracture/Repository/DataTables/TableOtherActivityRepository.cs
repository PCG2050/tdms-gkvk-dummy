using Application.Interface.Repository.DataTables;
using Domain.Entities.GenericTables;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DataTables
{
    public class TableOtherActivityRepository : ITableOtherActivityRepository
    {
        private readonly TdmsDbContext _context;

        public TableOtherActivityRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TableOtherActivity entity)
        {
            await _context.OtherActivities.AddAsync(entity);
        }

        public async Task DeleteAsync(TableOtherActivity entity)
        {
            _context.OtherActivities.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<List<TableOtherActivity>> GetAllAsync()
        {
            return await _context.OtherActivities.ToListAsync();
        }

        public async Task<TableOtherActivity?> GetByIdAsync(int id)
        {
            return await _context.OtherActivities.FindAsync(id);
        }

        public async Task UpdateAsync(TableOtherActivity entity)
        {
            _context.OtherActivities.Update(entity);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
