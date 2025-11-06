using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly TdmsDbContext _context;

        public UnitRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllMainUnitsAsync()
        {
            return await _context.Units.Where(x => x.ParentUnitId == null).ToListAsync();
        }

        public Task<Unit> GetUnitById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Unit>> GetUnitByIdsAsync(ICollection<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Unit>> GetUnitsByOrganization(int orgId)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
