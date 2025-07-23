using Domain.Entities.Junction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IOrganizationUnitRepository
    {
        Task<OrganizationUnitLocation?> GetByOrganizationUnitDistrictAsync(int orgId, int unitId, int districtId);
        Task<IEnumerable<OrganizationUnitLocation>> GetByOrganizationIdAsync(int organizationId);
        Task<IEnumerable<OrganizationUnitLocation>> GetByUnitIdAsync(int unitId);
        Task<IEnumerable<OrganizationUnitLocation>> GetByDistrictIdAsync(int districtId);
        IQueryable<OrganizationUnitLocation> GetQueryable();
        Task<bool> ExistsAsync(int orgId, int unitId, int districtId);
        Task SaveAsync(OrganizationUnitLocation entity);
        Task DeleteAsync(OrganizationUnitLocation entity);
    }
}
