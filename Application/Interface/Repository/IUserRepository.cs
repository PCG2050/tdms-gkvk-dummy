using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Repository
{
    public interface IUserRepository:IPagination<TrainerDetailsDto>
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetByActivationTokenAsync(string token);
        Task<List<User>> GetAllAsync();
        Task SaveAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetUsersByOrganizationAndRoleAsync(int organizationId, Role role);
        Task SetPasswordResetTokenAsync(int userId, string token, DateTimeOffset expiresAt);
        Task<User?> GetByPasswordResetTokenAsync(string token);
        Task ClearPasswordResetTokenAsync(int userId);
    }
}
