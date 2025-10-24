
namespace Application.Interface.Repository
{
    public interface IOrganizationRepository
    {
        
        Task<Organization?> GetOrganizationAsync(int id);
        Task<bool> HasOrganizationWithNameAsync(string name);
        Task SaveAsync(Organization organization);
        Task UpdateAsync(Organization organization);
        Task<PaginatedResult<OrganizationDto>> GetPaginatedItemsAsync(int page, int pageSize);

        Task <Organization?> DeleteAsync(int id);
    }
}
