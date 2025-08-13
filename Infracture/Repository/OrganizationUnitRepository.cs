using Application.Interface.Repository;
using Domain.Entities.Junction;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrganizationUnitRepository(TdmsDbContext _dbContext) : IOrganizationUnitRepository
    {
        public async Task DeleteAsync(OrganizationUnitLocation entity)
        {
            _dbContext.OrganizationUnitLocations.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int orgId, int unitId, int districtId)
        {
            return await _dbContext.OrganizationUnitLocations
                .AnyAsync(x => x.OrganizationId == orgId && x.UnitId == unitId && x.DistrictId == districtId);
        }
        public Task<IEnumerable<OrganizationUnitLocation>> GetByDistrictIdAsync(int districtId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrganizationUnitLocation>> GetByOrganizationIdAsync(int organizationId)
        {
            return await _dbContext.OrganizationUnitLocations.Where(x => x.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<OrganizationUnitLocation?> GetByOrganizationUnitDistrictAsync(int orgId, int unitId, int districtId)
        {
            return await _dbContext.OrganizationUnitLocations
                .FirstOrDefaultAsync(x => x.OrganizationId == orgId
                    && x.UnitId == unitId
                    && x.DistrictId == districtId);
        }

        public async Task<IEnumerable<OrganizationUnitLocation>> GetByUnitIdAsync(int unitId)
        {
            return await _dbContext.OrganizationUnitLocations.Where(x=> x.UnitId ==unitId)
                .ToListAsync();
        }

        public IQueryable<OrganizationUnitLocation> GetQueryable()
        {
            return _dbContext.OrganizationUnitLocations;
        }

        public async Task SaveAsync(OrganizationUnitLocation entity)
        {
           
            _dbContext.OrganizationUnitLocations.Add(entity);                   
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
