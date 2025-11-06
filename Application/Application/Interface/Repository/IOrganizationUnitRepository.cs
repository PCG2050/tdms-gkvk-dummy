using Application.Models;
using Domain.Entities.Junction;

namespace Application.Interface.Repository
{
    public interface IOrganizationUnitRepository
    {
        Task<List<OrganizationUnitLocation>> GetOrganizationUnitsAsync(int organizationId);
        Task<List<int>> GetUnitLocationIdsByOrganizationIdAsync(int organizationId);

        Task<List<int>> GetUnitLocationIdsByOrganizationAndUnitAsync(int organizationId, int unitId);

        Task<bool> IsLocationInOrganizationAsync(int unitLocationId, int organizationId);
        Task<OrganizationUnitLocation?> GetByOrganizationUnitDistrictAsync(int orgId, int unitId, int districtId);
        Task<IEnumerable<OrganizationUnitLocation>> GetByOrganizationIdAsync(int organizationId, int adminId);

        Task<OrganizationUnitLocation?> GetByOrganizationUnitLocationsIdAsync(int id);

        Task<IEnumerable<OrganizationUnitLocation>> GetByUnitIdAsync(int unitId);
        Task<IEnumerable<OrganizationUnitLocation>> GetByDistrictIdAsync(int districtId);
        IQueryable<OrganizationUnitLocation> GetQueryable();
        Task<bool> ExistsAsync(int orgId, int unitId, int districtId);
        Task SaveAsync(OrganizationUnitLocation entity);
        Task DeleteAsync(OrganizationUnitLocation entity);

        // New methods for Admin UnitLocation management
        Task<PaginatedResult<OrgUnitLocationIdDetailsDto>> GetPaginatedUnitLocationsCreatedByAsync(int createdById, int pageNumber = 1, int pageSize = 10);
        Task<List<OrganizationUnitLocation>> GetUnitLocationsCreatedByAsync(int createdById);

        // Additional methods for PATCH operations
        Task<OrganizationUnitLocation?> GetByIdAsync(int id);
    }
}
