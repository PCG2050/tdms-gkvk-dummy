using Application.Interface.Repository;
using Application.Models;
using Domain.Entities.Enum;
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
    public class UnitHeadAssignmentRepository : IUnitHeadAssignmentRepository
    {
        private readonly TdmsDbContext _context;

        public UnitHeadAssignmentRepository(TdmsDbContext context)
        {
            _context = context;
        }


        public async Task<List<UnitHeadAssignment>> GetAllAsync()
        {
            return await _context.UnitHeadAssignments.ToListAsync();
        }

        public async Task<List<int>> GetUnitLocationIdsByUnitHeadIdAsync(int unitHeadId)
        {
            return await _context.UnitHeadAssignments
             .Where(uha => uha.UnitHeadId == unitHeadId)
             .Select(uha => uha.UnitLocationId) 
             .Distinct()
             .ToListAsync();
        }
        public async Task<bool> IsUnitHeadAssignedToLocationAsync(int unitHeadId, int unitLocationId)
        {
            return await _context.UnitHeadAssignments
                .AnyAsync(a => a.UnitHeadId == unitHeadId && a.UnitLocationId == unitLocationId);
        }
        public async Task<List<int>> GetUnitIdsByUnitHeadIdAsync(int unitHeadId)
        {
            return await _context.UnitHeadAssignments
                .Include(a => a.UnitLocation)
                .Where(a => a.UnitHeadId == unitHeadId)
                .Select(a => a.UnitLocation.UnitId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<UnitHeadAssignment> AddAsync(UnitHeadAssignment unitLocationUnitHead)
        {
            await _context.AddAsync(unitLocationUnitHead);
            await _context.SaveChangesAsync();
            return unitLocationUnitHead;
        }

        public async Task<bool> UnitHeadAssignmentExistsAsync(ExistingUnitHeadAssignmentDto unitHeadAssignment)
        {


            return await (from unitLocation in _context.OrganizationUnitLocations
                          join unitHead in _context.UnitHeadAssignments on unitLocation.Id equals unitHead.UnitLocationId
                          where unitLocation.UnitId == unitHeadAssignment.UnitId
                          && unitLocation.DistrictId == unitHeadAssignment.DistrictId
                          && unitHead.UnitHeadId == unitHeadAssignment.UnitHeadId
                          select unitHead).AnyAsync();
        }


        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(UnitHeadAssignment unitLocationUnitHead)
        {
            _context.UnitHeadAssignments.Remove(unitLocationUnitHead);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AssignmentExistsByLocationAsync(int unitLocationId, int unitHeadId)
        {
            return await _context.UnitHeadAssignments
                .AnyAsync(x => x.UnitLocationId == unitLocationId && x.UnitHeadId == unitHeadId);
        }

        public async Task<UnitHeadAssignment?> GetByUnitHeadLocationAsync(int unitLocationId, int unitHeadId)
        {
          
            return await _context.UnitHeadAssignments.FirstOrDefaultAsync(x => x.UnitLocationId == unitLocationId
            && x.UnitHeadId == unitHeadId);
        }

        public async Task<UnitHeadAssignment> UpdateAsync(UnitHeadAssignment unitLocationUnitHead)
        {
            _context.UnitHeadAssignments.Update(unitLocationUnitHead);
            await _context.SaveChangesAsync();
            return unitLocationUnitHead;
        }

        public async Task<List<UnitWithLocationsDto>> GetAssignmentsDetailsByUnitHeadAsync(int unitHeadId)
        {

            var unitHeadAssignments = await _context.UnitHeadAssignments
                 .Include(x => x.UnitLocation)
                     .ThenInclude(l => l.Unit)
                 .Include(x => x.UnitLocation)
                     .ThenInclude(l => l.District)
                     .ThenInclude(d => d.State)
                 .Where(x => x.UnitHeadId == unitHeadId)
                 .GroupBy(ut => new { ut.UnitLocation.Unit.Id, ut.UnitLocation.Unit.Name })
             .Select(g => new UnitWithLocationsDto
             {
                 UnitId = g.Key.Id,
                 UnitName = g.Key.Name,
                 Locations = g.Select(ut => new UnitLocationDto
                 {
                     UnitLocationId = ut.UnitLocation.Id,
                     DistrictId = ut.UnitLocation.District.Id,
                     DistrictName = ut.UnitLocation.District.Name,
                     StateId = ut.UnitLocation.District.State.Id,
                     StateName = ut.UnitLocation.District.State.Name
                 }).ToList()
             }).ToListAsync();
            return unitHeadAssignments;
        }


        public async Task<PaginatedResult<UnitHeadFlatDto>> GetPaginatedUnitHeadsAsync(int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10)
        {
            var query = _context.Users
                .Where(x => x.OrganizationId == organizationId && x.Role == Role.UNITHEAD);

            var result = new PaginatedResult<UnitHeadFlatDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = await query.AsNoTracking().CountAsync()
            };

            int offset = (pageNumber - 1) * pageSize;

            var unitHeadsDtoQuery = await query
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(pageSize)
                .SelectMany(u => u.UnitHeadAssignments.Select(assignment => new UnitHeadFlatDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    

                    UnitId = assignment.UnitLocation.Unit.Id,
                    UnitName = assignment.UnitLocation.Unit.Name,

                    StateId = assignment.UnitLocation.District.State.Id,
                    StateName = assignment.UnitLocation.District.State.Name,

                    DistrictId = assignment.UnitLocation.District.Id,
                    DistrictName = assignment.UnitLocation.District.Name
                }))
                .ToListAsync();

            result.Items = unitHeadsDtoQuery;
            return result;
        }




        public async Task<List<UnitHeadAssignment>> GetByUnitHeadIdAsync(int unitHeadId)
        {
            return await _context.UnitHeadAssignments
             .Include(x => x.UnitLocation)
                 .ThenInclude(l => l.Unit)
             .Include(x => x.UnitLocation)
                 .ThenInclude(l => l.District)
                 .ThenInclude(d => d.State)
             .Where(x => x.UnitHeadId == unitHeadId)           
                .ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<UnitHeadAssignment> assignments)
        {
            await _context.UnitHeadAssignments.AddRangeAsync(assignments);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<UnitHeadAssignment> assignments)
        {
            _context.UnitHeadAssignments.RemoveRange(assignments);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> HasAssignmentsForUnitLocationAsync(int unitLocationId)
        {
            return await _context.UnitHeadAssignments
                .AnyAsync(ta => ta.UnitLocationId == unitLocationId);
        }
    }
}
