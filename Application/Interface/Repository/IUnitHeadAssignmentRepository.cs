using Application.Models;
using Domain.Entities.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUnitHeadAssignmentRepository
    {
       
            Task<UnitHeadAssignment?> GetByUnitHeadLocationAsync(int unitLocationId, int unitHeadId);
            Task<UnitHeadAssignment> AddAsync(UnitHeadAssignment unitLocationUnitHead);
            Task<UnitHeadAssignment> UpdateAsync(UnitHeadAssignment unitLocationUnitHead);
            Task DeleteAsync(int id);
            Task DeleteAsync(UnitHeadAssignment unitLocationUnitHead);
            Task<bool> AssignmentExistsAsync(ExistingUnitHeadAssignmentDto UnitHeadAssignment);
            Task<List<UnitWithLocationsDto>> GetAssignmentsDetailsByUnitHeadAsync(int unitHeadId);
        
    }
}
