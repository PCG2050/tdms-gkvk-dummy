

namespace Application.Interface.Repository
{
    public interface IUnitRepository
    {
        Task<Unit> GetUnitById(int id);
        Task<IEnumerable<Unit>> GetUnitByIdsAsync(ICollection<int> ids);
        Task<IEnumerable<Unit>> GetAllMainUnitsAsync();
        Task<ICollection<Unit>> GetUnitsByOrganization(int orgId);
        Task SaveAsync(Unit unit);

    }
}
