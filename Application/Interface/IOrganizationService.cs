using Application.Models;
using Domain.Entities;

namespace Application.Interface
{
    public interface IOrganizationService
    {
        Task<ServiceResult<Organization>> CreateOrganizationAsync(OrganizationCreateDto organizationCreateDto);
        Task<Organization?> GetOrganizationAsync(int id);
        Task<ServiceResult<Organization>> UpdateOrganizationAsync(OrganizationUpdateDto updateDto);
        Task<User> CreateAdminAsync(UserRegisterDto registerDto, int organizationId);
        Task<List<User>> GetAllOrganizationAdminsAsync(int id);
        Task<PaginatedResult<OrganizationDto>> GetPaginatedItemsAsync(int page, int pageSize);
    }
}
