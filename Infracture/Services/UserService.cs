using Application;
using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserService _currentUser;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher,ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _currentUser = currentUser;
        }

        public async Task<User> CreateUserAsync(UserRegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");


            var user = new User() {
                Email=registerDto.Email,
                PasswordHash = registerDto.Password,
                FirstName = registerDto.FirstName,
                LastName= registerDto.LastName,
                OrganizationId = registerDto.OrganizationId
            };
            user.CreatedById = _currentUser.UserId;
            user.CreatedAt = DateTimeOffset.UtcNow;
            await _userRepository.SaveAsync(user);
            return user;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                return null;

            var isValidPassword = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            return isValidPassword ? user : null;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetUserByActivationTokenAsync(string token)
        {
            return await _userRepository.GetByActivationTokenAsync(token);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.SaveAsync(user);
        }

        public  Task<List<User>> GetOrganizationUnitTrainers(int unitId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedOrganizationTrainers(int pageNumber=Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            var usersResult = await _userRepository.GetPaginatedItemsAsync(_currentUser.OrganizationId, pageNumber, null, pageSize);
            return usersResult;
        }

        public Task<List<User>> GetAllOrganizationUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetCurrentUserDetailsAsync()
        {
            int userId = _currentUser.UserId;
            var user = await _userRepository.GetByIdAsync(userId);
            return user;
        }

        Task<List<User>> IUserService.GetOrganizationTrainers()
        {
            throw new NotImplementedException();
        }
    }
}
