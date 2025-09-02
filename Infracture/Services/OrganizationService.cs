using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;

namespace Application.Interface
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ICurrentUserService _currentUser;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAzureStorageService _azureStorageService;

        public OrganizationService(ICurrentUserService currentUser, IOrganizationRepository organizationRepository, IUserRepository userRepository, IPasswordHasher passwordHasher, IAzureStorageService azureStorageService)
        {
            _currentUser = currentUser;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _azureStorageService = azureStorageService;
        }
        public async Task<ServiceResult<Organization>> CreateOrganizationAsync(OrganizationCreateDto organizationCreateDto)
        {
            if (_currentUser.Role != Domain.Entities.Enum.Role.SUPERADMIN)
                throw new UnauthorizedAccessException("");
            var result = await GenerateContainersAsync(organizationCreateDto.StorageContainerName);
            if (!result.IsSuccess) return ServiceResult<Organization>.Failure(result.ErrorMessage, result.ErrorStatus);
            var organization = new Organization
            {
                Name = organizationCreateDto.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedById = _currentUser.UserId,
                DistrictId = organizationCreateDto.DistrictId,
                PinCode = organizationCreateDto.Pincode,
                StorageContainerName = organizationCreateDto.StorageContainerName
            };
            await _organizationRepository.SaveAsync(organization);
            return ServiceResult<Organization>.Success(organization);
        }

        public async Task<Organization?> GetOrganizationAsync(int id)
        {
            return await _organizationRepository.GetOrganizationAsync(id);

        }

        public async Task<User> CreateUser(UserRegisterDto registerDto, int organizationId)
        {
            if (registerDto.Role == Domain.Entities.Enum.Role.ADMIN
                && _currentUser.Role != Domain.Entities.Enum.Role.SUPERADMIN) throw new UnauthorizedAccessException("Not authorized to creat a user with this role");
            else if (registerDto.Role == Domain.Entities.Enum.Role.UNITHEAD
                && _currentUser.Role != Domain.Entities.Enum.Role.ADMIN) throw new UnauthorizedAccessException("Not authorized to creat a user with this role");
            else if (registerDto.Role == Domain.Entities.Enum.Role.TRAINER
                && (_currentUser.Role != Domain.Entities.Enum.Role.UNITHEAD)) throw new UnauthorizedAccessException("Not authorized to creat a user with this role");
            else if (registerDto.Role == Domain.Entities.Enum.Role.UNDEFINED) throw new ArgumentException($"Role {registerDto.Role} is not valid");
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
                CreatedAt = DateTimeOffset.UtcNow,
                Phone = registerDto.Phone,
                Role = registerDto.Role
            };

            await _userRepository.SaveAsync(user);

            return user;
        }

        public Task<List<User>> GetAllOrganizationAdminsAsync(int id)
        {
            return _userRepository.GetAllAsync();
        }

        public async Task<ServiceResult<Organization>> UpdateOrganizationAsync(OrganizationUpdateDto updateDto)
        {
            var organization = await _organizationRepository.GetOrganizationAsync(updateDto.Id);
            if (organization is null) return ServiceResult<Organization>.Failure("Organization not found", ServiceErrorStatus.NOTFOUND);
            if (updateDto.StorageContainerName is not null && updateDto.StorageContainerName != organization.StorageContainerName)
            {
                var result = await GenerateContainersAsync(updateDto.StorageContainerName);
                if (!result.IsSuccess) return ServiceResult<Organization>.Failure(result.ErrorMessage, result.ErrorStatus);
                organization.StorageContainerName = updateDto.StorageContainerName;
            }

            if (!string.IsNullOrWhiteSpace(updateDto.Name) && !string.Equals(organization.Name, updateDto.Name, StringComparison.OrdinalIgnoreCase))
            {
                var nameExists = await _organizationRepository.HasOrganizationWithNameAsync(updateDto.Name);
                if (nameExists)
                    return ServiceResult<Organization>.Failure("An organization with this name already exists", ServiceErrorStatus.INVALIDOPERATION);

                organization.Name = updateDto.Name;
            }

            if (updateDto.Pincode is not null) organization.PinCode = updateDto.Pincode;
            if (updateDto.DistrictId.HasValue && organization.DistrictId != updateDto.DistrictId.Value) organization.DistrictId = updateDto.DistrictId.Value;
            await _organizationRepository.UpdateAsync(organization);
            return ServiceResult<Organization>.Success(organization);
        }

        private async Task<ServiceResult> GenerateContainersAsync(string storageContainerName)
        {
            var privateContainerResult = await _azureStorageService.CreateStorageContainer(storageContainerName, Enums.ContainerType.Private);
            if (!privateContainerResult.IsSuccess) return ServiceResult.Failure(privateContainerResult.ErrorMessage, privateContainerResult.ErrorStatus);
            var publicContainerResult = await _azureStorageService.CreateStorageContainer($"{storageContainerName}-public", Enums.ContainerType.Public);
            if (!publicContainerResult.IsSuccess) ServiceResult.Failure(publicContainerResult.ErrorMessage, publicContainerResult.ErrorStatus);
            return ServiceResult.Success();
        }

        public async Task<PaginatedResult<OrganizationDto>> GetPaginatedItemsAsync(int page, int pageSize)
        {
            if (_currentUser.Role != Domain.Entities.Enum.Role.SUPERADMIN) throw new UnauthorizedAccessException();
            return await _organizationRepository.GetPaginatedItemsAsync(page, pageSize);
        }


        public async Task<List<UserDto>> GetAdminsByOrganizationIdAsync(int organizationId)
        {
            if (_currentUser.Role != Role.SUPERADMIN)
                throw new UnauthorizedAccessException("Only SuperAdmins can access this data.");

            var admins = await _userRepository.GetUsersByOrganizationAndRoleAsync(organizationId, Role.ADMIN);

            return admins.Select(admin => new UserDto
            {
                Id = admin.Id,
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Role = admin.Role,
                Phone = admin.Phone,
                OrganizationId = admin.OrganizationId
            }).ToList();
        }
        public async Task<ServiceResult> DeleteOrganizationAsync(int id)
        {
            var organization = await _organizationRepository.GetOrganizationAsync(id);
            if (organization == null)
                return ServiceResult.Failure("Organization not found", ServiceErrorStatus.NOTFOUND);

            await _organizationRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

    }
}
