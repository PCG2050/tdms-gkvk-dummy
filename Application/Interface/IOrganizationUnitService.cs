using Application.Models;
using Domain.Entities;
using Domain.Entities.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrganizationUnitService
    {
        Task<IEnumerable<OrganizationUnitLocation>> GetOrganizationUnits();
        Task<IEnumerable<OrgUnitLocationIdDetailsDto>> GetOrganizationUnitsDetails();
        Task<OrganizationUnitLocation> AddUnitToOrganization(OrganizationUnitLocationDto addUnitLocationDto);
        Task RemoveUnitFromOrganization(OrganizationUnitLocationDto organizationUnitLocationDto);
        Task<ServiceResult> MapExistingTrainersAsync(ExistingTrainerAssignmentDto trainerAssignment);
        Task<ServiceResult> UnMapTrainerFromUnitLocationAsync(ExistingTrainerAssignmentDto trainerAssignment);       

        Task<List<ServiceResult>> MapExistingUnitHeadsByLocationBulkAsync(BulkUnitHeadAssignmentByLocationDto request);

        Task<ServiceResult> SyncUnitHeadAssignmentsByLocationAsync(BulkUnitHeadAssignmentByLocationDto request);


        // New methods for Admin to get UnitLocations they created
        Task<PaginatedResult<OrgUnitLocationIdDetailsDto>> GetUnitLocationsCreatedByAdmin(int adminId, int pageNumber = 1, int pageSize = 10);
        Task<List<OrgUnitLocationIdDetailsDto>> GetAllUnitLocationsCreatedByAdmin(int adminId);

        // New methods for PATCH operations
        Task<OrgUnitLocationIdDetailsDto?> GetOrganizationUnitLocationById(int id);
        Task<ServiceResult<OrgUnitLocationIdDetailsDto>> UpdateOrganizationUnitLocationAsync(OrganizationUnitLocationUpdateDto updateDto);
        //Task<List<ServiceResult<OrgUnitLocationIdDetailsDto>>> BulkUpdateOrganizationUnitLocationsAsync(BulkOrganizationUnitLocationUpdateDto bulkUpdateDto);
        Task<ServiceResult> RemoveUnitFromOrganizationById(int id);




    }
}
