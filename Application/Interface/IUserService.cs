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
        Task UpdateUserAsync(User user);
        Task<List<User>> GetAllOrganizationUsersAsync();
        Task<List<User>> GetOrganizationUnitTrainers(int unitId);
        Task<User> GetCurrentUserDetailsAsync();
    }
}
