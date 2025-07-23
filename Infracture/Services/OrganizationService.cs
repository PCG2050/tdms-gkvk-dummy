using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public OrganizationService(ICurrentUserService currentUser, IOrganizationRepository organizationRepository, IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _currentUser = currentUser;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Organization> CreateOrganizationAsync(OrganizationCreateDto organizationCreateDto)
        {
            if (_currentUser.Role != Domain.Entities.Enum.Role.SUPERADMIN)
                throw new UnauthorizedAccessException("");
            var organization  = new Organization
            {
                Name= organizationCreateDto.Name,
                Logo = "",
                CreatedAt = DateTime.UtcNow,
                CreatedById = _currentUser.UserId
            };
            await _organizationRepository.SaveAsync(organization);
            return organization;
        }

        public async Task<Organization?> GetOrganizationAsync(int id)
        {
            return await _organizationRepository.GetOrganizationAsync(id);

        }

        public async Task<User> CreateAdminAsync(UserRegisterDto registerDto, int organizationId)
        {
            if (_currentUser.Role != Domain.Entities.Enum.Role.SUPERADMIN) throw new UnauthorizedAccessException("Not authorized to perform this action");
            var organization = await _organizationRepository.GetOrganizationAsync(organizationId);
            if (organization is null)
                throw new InvalidOperationException("Organization does not exist");
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");
            string passwordHashed = _passwordHasher.HashPassword(registerDto.Password);
            var user = new User
            {
                Email = registerDto.Email,
                PasswordHash = passwordHashed,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                OrganizationId = organizationId,
                CreatedById = _currentUser.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _userRepository.SaveAsync(user);

            return user;
        }

        public Task<List<User>> GetAllOrganizationAdminsAsync(int id)
        {
            return _userRepository.GetAllAsync();
        }
    }
}
