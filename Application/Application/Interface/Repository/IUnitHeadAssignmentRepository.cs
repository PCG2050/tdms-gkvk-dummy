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
        Task<List<UnitHeadAssignment>> GetAllAsync();
        Task<List<int>> GetUnitLocationIdsByUnitHeadIdAsync(int unitHeadId);
        Task<bool> IsUnitHeadAssignedToLocationAsync(int unitHeadId, int unitLocationId);       
        Task<List<int>> GetUnitIdsByUnitHeadIdAsync(int unitHeadId);

        Task<UnitHeadAssignment?> GetByUnitHeadLocationAsync(int unitLocationId, int unitHeadId);
        Task<UnitHeadAssignment> AddAsync(UnitHeadAssignment unitLocationUnitHead);
        Task<UnitHeadAssignment> UpdateAsync(UnitHeadAssignment unitLocationUnitHead);
        Task DeleteAsync(int id);
        Task DeleteAsync(UnitHeadAssignment unitLocationUnitHead);
        Task<bool> UnitHeadAssignmentExistsAsync(ExistingUnitHeadAssignmentDto UnitHeadAssignment);

        Task<bool> AssignmentExistsByLocationAsync(int unitLocationId, int unitHeadId);
        Task<List<UnitWithLocationsDto>> GetAssignmentsDetailsByUnitHeadAsync(int unitHeadId);

        Task<List<UnitHeadAssignment>> GetByUnitHeadIdAsync(int unitHeadId);
        Task AddRangeAsync(IEnumerable<UnitHeadAssignment> assignments);
        Task DeleteRangeAsync(IEnumerable<UnitHeadAssignment> assignments);


        // New method for checking assignments before deletion
        Task<bool> HasAssignmentsForUnitLocationAsync(int unitLocationId);

        

    }
}
