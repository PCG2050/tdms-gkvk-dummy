using Application;
using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserService _currentUser;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
       

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher,ICurrentUserService currentUser,IEmailService emailService, IOptions<EmailSettings> emailOptions)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _currentUser = currentUser;
            _emailService = emailService;
            _emailSettings = emailOptions.Value;       
           
        }

        public async Task<User> CreateUserAsync(UserRegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");


            var user = new User() {
                Email=registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(registerDto.Password),
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

        public async Task<ServiceResult> UpdateUserAsync(UserUpdateDto updateDto)
        {
            var user = await _userRepository.GetByIdAsync(updateDto.Id);
            if (user is null) return ServiceResult.Failure($"User with id {updateDto.Id} not found", ServiceErrorStatus.NOTFOUND);
            var cId = _currentUser.UserId;
            var cRole = _currentUser.Role;
            var cOrg = _currentUser.OrganizationId;
            if (cRole == Role.SUPERADMIN
                || cId == user.Id //Update there own profile
                || user.Role == Role.UNITHEAD && (cId == user.Id || ( cRole == Role.ADMIN && cOrg == user.OrganizationId))
                || user.Role == Role.TRAINER  && (cId == user.Id ||(cRole == Role.ADMIN && cOrg == user.OrganizationId)))
            {
                if (updateDto.Password is not null) user.PasswordHash = _passwordHasher.HashPassword(updateDto.Password);
                if(updateDto.FirstName is not null) user.FirstName = updateDto.FirstName;
                if(updateDto.LastName is not null) user.LastName = updateDto.LastName;
                if (updateDto.Phone is not null)
                {
                    user.Phone = updateDto.Phone;
                    user.IsPhoneConfirmed = false;
                }
                await _userRepository.SaveAsync(user);
                return ServiceResult.Success();
            }
            return ServiceResult.Failure("User is not authorized to perform this action", ServiceErrorStatus.FORBIDDEN);
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

        public async Task<ServiceResult> UpdatedAccountStatus(int id, bool activate = true)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return ServiceResult.Failure("User with {id} not found", ServiceErrorStatus.NOTFOUND);
            //can current user do this action
            var cRole = _currentUser.Role;
            var cOrg = _currentUser.OrganizationId;
            if (user.Role == Role.SUPERADMIN
                || (user.Role == Role.ADMIN && cRole != Role.SUPERADMIN)
                || (user.Role == Role.UNITHEAD 
                    && (cRole != Role.SUPERADMIN
                        || !(cRole == Role.ADMIN && cOrg == user.OrganizationId)))
                       
                || (user.Role == Role.TRAINER 
                    && (cRole != Role.SUPERADMIN 
                        || !(cRole == Role.ADMIN && cOrg == user.OrganizationId))))
                ServiceResult.Failure($"User does not have the permission to deactivate user {id}", ServiceErrorStatus.FORBIDDEN);
            user.IsDeactivated = !activate;
            await _userRepository.SaveAsync(user);
            return ServiceResult.Success();
        }

        // NEW: Forgot Password
        public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return ServiceResult.Failure("Email is required", ServiceErrorStatus.INVALIDOPERATION);

            var user = await _userRepository.GetByEmailAsync(dto.Email);

            // Always return success (avoid user enumeration)
            if (user == null) return ServiceResult.Success();

            var tokenBytes = RandomNumberGenerator.GetBytes(48);
            var token = Convert.ToBase64String(tokenBytes)
                .Replace("+", "-").Replace("/", "_").Replace("=", "");


            var expiresAt = DateTimeOffset.UtcNow.AddHours(1);
            await _userRepository.SetPasswordResetTokenAsync(user.Id, token, expiresAt);

            var resetLink = $"{_emailSettings.ResetPasswordUrlBase}{token}";
            await _emailService.SendPasswordResetEmailAsync(user.Email, resetLink);

            return ServiceResult.Success();
        }

        // NEW: Reset Password
        public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Token) || string.IsNullOrWhiteSpace(dto.NewPassword))
                return ServiceResult.Failure("Token and new password are required", ServiceErrorStatus.INVALIDOPERATION);

            var user = await _userRepository.GetByPasswordResetTokenAsync(dto.Token);
            if (user == null)
                return ServiceResult.Failure("Invalid or expired token", ServiceErrorStatus.INVALIDOPERATION);

            user.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword);
            await _userRepository.SaveAsync(user);
            await _userRepository.ClearPasswordResetTokenAsync(user.Id);

            return ServiceResult.Success();
        }
    }
}
