using Application.Interface.Repository;
using Application.Models;
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
        public async Task<UnitHeadAssignment> AddAsync(UnitHeadAssignment unitLocationUnitHead)
        {
            await _context.AddAsync(unitLocationUnitHead);
            await _context.SaveChangesAsync();
            return unitLocationUnitHead;
        }

        public async Task<bool> AssignmentExistsAsync(ExistingUnitHeadAssignmentDto unitHeadAssignment)
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

        public async Task<UnitHeadAssignment?> GetByUnitHeadLocationAsync(int unitLocationId, int unitHeadId)
        {
          
            return await _context.UnitHeadAssignments.FirstOrDefaultAsync(x => x.UnitLocationId == unitLocationId
            && x.UnitHeadId == unitHeadId);
        }

        public Task<UnitHeadAssignment> UpdateAsync(UnitHeadAssignment unitLocationUnitHead)
        {
            throw new NotImplementedException();
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
    }
}
