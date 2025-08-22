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

        Task<List<ServiceResult>> MapExistingUnitHeadsBulkAsync(BulkUnitHeadAssignmentDto unitHeadAssignments);

        Task<List<ServiceResult>> UnMapExistingUnitHeadsBulkAsync(BulkUnitHeadAssignmentDto unitHeadAssignments);
        
    }
}
