using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrganizationService
    {
        Task<Organization> CreateOrganizationAsync(OrganizationCreateDto organizationCreateDto);
        Task<Organization?> GetOrganizationAsync(int id);
        Task<User> CreateAdminAsync(UserRegisterDto registerDto, int organizationId);
        Task<List<User>> GetAllOrganizationAdminsAsync(int id);
    }
}
