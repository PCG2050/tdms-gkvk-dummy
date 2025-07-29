using Application.Models;
using Domain.Entities;
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
    }
}
