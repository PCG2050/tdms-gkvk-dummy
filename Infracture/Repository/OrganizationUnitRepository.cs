using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Junction;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class OrganizationUnitRepository : IOrganizationUnitRepository
    {
        private readonly TdmsDbContext _dbContext;

        public OrganizationUnitRepository(TdmsDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<int>> GetUnitLocationIdsByOrganizationIdAsync(int organizationId)
        {
            return await _dbContext.OrganizationUnitLocations
                .Where(oul => oul.OrganizationId == organizationId)
                .Select(oul => oul.Id)
                .Distinct()
                .ToListAsync();

        }

        public async Task<List<int>> GetUnitLocationIdsByOrganizationAndUnitAsync(
         int organizationId,
         int unitId)
        {
            return await _dbContext.OrganizationUnitLocations
                .Where(l => l.OrganizationId == organizationId && l.UnitId == unitId)
                .Select(l => l.Id)
                .ToListAsync();
        }

        public async Task<bool> IsLocationInOrganizationAsync(int unitLocationId, int organizationId)
        {
            return await _dbContext.OrganizationUnitLocations
                .AnyAsync(oul => oul.Id == unitLocationId && oul.OrganizationId == organizationId);
        }


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

        public async Task<IEnumerable<OrganizationUnitLocation>> GetByOrganizationIdAsync(int organizationId, int adminId)
        {
            return await _dbContext.OrganizationUnitLocations.Where(x => x.OrganizationId == organizationId && x.CreatedById == adminId)
                .ToListAsync();
        }


        public async Task<OrganizationUnitLocation?> GetByOrganizationUnitLocationsIdAsync(int id)
        {
            return await _dbContext.OrganizationUnitLocations
                .Include(x => x.Unit)
                .Include(x => x.District)
                .FirstOrDefaultAsync(x => x.Id == id);
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
            return await _dbContext.OrganizationUnitLocations.Where(x => x.UnitId == unitId)
                .ToListAsync();
        }

        public IQueryable<OrganizationUnitLocation> GetQueryable()
        {
            return _dbContext.OrganizationUnitLocations;
        }

        public async Task SaveAsync(OrganizationUnitLocation entity)
        {
            if (_dbContext.Entry<OrganizationUnitLocation>(entity).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                await _dbContext.AddAsync(entity);
            else
                _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            //_dbContext.OrganizationUnitLocations.Add(entity);                   

            //await _dbContext.SaveChangesAsync();
        }

        public async Task<OrganizationUnitLocation?> GetByIdAsync(int id)
        {
            return await _dbContext.OrganizationUnitLocations
                .Include(x => x.Unit)
                .Include(x => x.District)
                    .ThenInclude(d => d.State)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // NEW: Get paginated UnitLocations created by a specific user (Admin)
        public async Task<PaginatedResult<OrgUnitLocationIdDetailsDto>> GetPaginatedUnitLocationsCreatedByAsync(int createdById, int pageNumber = 1, int pageSize = 10)
        {
            var query = _dbContext.OrganizationUnitLocations
                .Where(x => x.CreatedById == createdById)
                .Include(x => x.Unit)
                .Include(x => x.District)
                    .ThenInclude(d => d.State);

            var result = new PaginatedResult<OrgUnitLocationIdDetailsDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            result.TotalItems = await query.AsNoTracking().CountAsync();
            int offset = (pageNumber - 1) * pageSize;

            var unitLocationsDtoQuery = await query
                .OrderBy(x => x.Id)
                .Skip(offset)
                .Take(pageSize)
                .Select(x => new OrgUnitLocationIdDetailsDto
                {
                    OrgUnitLocationId = x.Id,
                    UnitId = x.Unit.Id,
                    UnitName = x.Unit.Name,
                    StateId = x.District.State.Id,
                    StateName = x.District.State.Name,
                    DistrictId = x.District.Id,
                    DistrictName = x.District.Name
                })
                .ToListAsync();

            result.Items = unitLocationsDtoQuery;
            return result;
        }

        // NEW: Get all UnitLocations created by a specific user (Admin) - non-paginated
        public async Task<List<OrganizationUnitLocation>> GetUnitLocationsCreatedByAsync(int createdById)
        {
            return await _dbContext.OrganizationUnitLocations
                .Where(x => x.CreatedById == createdById)
                .Include(x => x.Unit)
                .Include(x => x.District)
                    .ThenInclude(d => d.State)
                .OrderBy(x => x.Unit.Name)
                .ThenBy(x => x.District.State.Name)
                .ThenBy(x => x.District.Name)
                .ToListAsync();
        }
    }
}
