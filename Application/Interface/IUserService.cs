using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegisterDto registerDto);
        Task<User?> ValidateUserAsync(string email, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetUserByActivationTokenAsync(string token);
        Task<ServiceResult> UpdateUserAsync(UserUpdateDto updateDto);
        Task<List<User>> GetAllOrganizationUsersAsync();
        Task<List<User>> GetOrganizationUnitTrainers(int unitId);
        Task<List<User>> GetOrganizationTrainers();
        Task<User> GetCurrentUserDetailsAsync();
        Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedOrganizationTrainers(int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);

        Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedOrganizationUnitHeads(int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT);

        Task<ServiceResult> UpdatedAccountStatus(int id, bool activate = true);
        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto dto);
        Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto);
    }
}
