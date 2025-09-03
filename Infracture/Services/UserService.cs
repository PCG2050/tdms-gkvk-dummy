using Application;
using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Entities.Junction;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICurrentUserService _currentUser;
        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignment;
        private readonly ITrainerAssignmentRepository _trainerAssignment;
        private readonly IUserSessionRepository _sessionRepository;   



        public UserService(IUserRepository userRepository, 
            IPasswordHasher passwordHasher, 
            ICurrentUserService currentUser, 
            IEmailService emailService, 
            IOptions<EmailSettings> emailOptions, 
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository, 
            ITrainerAssignmentRepository trainerAssignment,
            IUserSessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _currentUser = currentUser;
            _emailService = emailService;
            _emailSettings = emailOptions.Value;
            _unitHeadAssignment = unitHeadAssignmentRepository;
            _trainerAssignment = trainerAssignment;
            _sessionRepository = sessionRepository;
        }

        public async Task<User> CreateUserAsync(UserRegisterDto registerDto)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");


            var user = new User()
            {
                Email = registerDto.Email,
                PasswordHash = _passwordHasher.HashPassword(registerDto.Password),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
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
                || user.Role == Role.UNITHEAD && (cId == user.Id || (cRole == Role.ADMIN && cOrg == user.OrganizationId))
                || user.Role == Role.TRAINER && (cId == user.Id || (cRole == Role.UNITHEAD && cOrg == user.OrganizationId)))
            {
                if (updateDto.Password is not null) user.PasswordHash = _passwordHasher.HashPassword(updateDto.Password);
                if (updateDto.FirstName is not null) user.FirstName = updateDto.FirstName;
                if (updateDto.LastName is not null) user.LastName = updateDto.LastName;
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

        public async Task<ServiceResult> DeleteUnitHeadAsync(int userId, int currentUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                return ServiceResult.Failure($"User {userId} not found", ServiceErrorStatus.NOTFOUND);

            // 🔐 Ownership check
            if (user.CreatedById != currentUserId)
                return ServiceResult.Failure("You are not allowed to delete this user", ServiceErrorStatus.FORBIDDEN);

            // If UnitHead → remove assignments
            if (user.Role == Role.UNITHEAD)
            {
                var assignments = await _unitHeadAssignment.GetByUnitHeadIdAsync(user.Id);
                if (assignments.Any())
                    await _unitHeadAssignment.DeleteRangeAsync(assignments);
            }

            await _userRepository.DeleteAsync(user);
            return ServiceResult.Success("User deleted successfully");
        }

   


        public Task<List<User>> GetOrganizationUnitTrainers(int unitId)
        {
            throw new NotImplementedException();

        }

        public async Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedOrganizationTrainers(int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            var usersResult = await _userRepository.GetPaginatedItemsAsync(_currentUser.OrganizationId, pageNumber, null, pageSize);
            return usersResult;
        }


        public async Task<PaginatedResult<FlatTrainerDetailsDto>> GetPaginatedOrgTrainers(int pageNumber = Constants.PAGINATION_PAGE_SIZE_DEFAULT, int PageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            var userResult = await _userRepository.GetPaginatedTrainerDetailsWithLocationAsync(_currentUser.OrganizationId, pageNumber, null, PageSize);
            return userResult;
        }

        public async Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedOrganizationUnitHeads(int pageNumber = Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.PAGINATION_PAGE_SIZE_DEFAULT)
        {
            var usersResult = await _userRepository.GetPaginatedUnitHeadsAsync(_currentUser.OrganizationId, pageNumber, null, pageSize);
            return usersResult;
        }

        //new one with flatdto
        public async Task<List<FlatUnitHeadDetailsDto>> GetPaginatedOrgUnitHeads()
        {
            var userResult = await _userRepository.GetDetailedPaginatedUnitHeadsAsync(_currentUser.OrganizationId, _currentUser.UserId);
            return userResult;
        }


        public async Task<List<FlatUnitHeadDetailsDto>> GetPaginatedTrainers()
        {
            var userResult = await _userRepository.GetDetailedPaginatedTrainersAsync(_currentUser.OrganizationId, _currentUser.UserId);
            return userResult;
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


        public async Task<List<TrainerDetailsDto>> GetAllTrainersCreatedByUnitHead(int unitHeadId)
        {
            if (_currentUser.UserId != unitHeadId && _currentUser.Role != Role.ADMIN)
                throw new UnauthorizedAccessException("You dont have permission to access this data");

            //verify the user is actually an unithead
            var unitHead = await _userRepository.GetByIdAsync(unitHeadId);
            if (unitHead == null || unitHead.Role != Role.UNITHEAD)
                throw new InvalidOperationException("User is not a UnitHead");

            if (_currentUser.Role == Role.ADMIN && _currentUser.OrganizationId != unitHead.OrganizationId)
                throw new UnauthorizedAccessException("You dont have permission to access this data");

            var trainers = await _userRepository.GetTrainersCreatedByAsync(unitHeadId);
            //Convert to TrainerDetailsDto
            return trainers.Select(trainer => new TrainerDetailsDto
            {
                UserId = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                Units = trainer.TrainerAssignments
                .GroupBy(ta => ta.UnitLocation.Unit)
                .Select(unitGroup => new TrainerUnitDto
                {
                    UnitId = unitGroup.Key.Id,
                    Name = unitGroup.Key.Name,
                    Locations = unitGroup
                   .GroupBy(ta => ta.UnitLocation.District.State)
                   .Select(stateGroup => new TrainerLocationDto
                   {
                       StateId = stateGroup.Key.Id,
                       StateName = stateGroup.Key.Name,
                       Districts = stateGroup.Select(ta => new TrainerDistrictDto
                       {
                           DistrictId = ta.UnitLocation.District.Id,
                           DistrictName = ta.UnitLocation.District.Name
                       }).ToList()
                   }).ToList()
                }).ToList()
            }).ToList();
        }

        //Trainers methods
        public async Task<List<TrainerWithAssignmentsDto>> GetTrainersWithAssignmentsCreatedByCurrentUserAsync()
        {
            var currentUserId = _currentUser.UserId;
            var trainers = await _userRepository.GetTrainersCreatedByAsync(currentUserId);

            return trainers.Select(trainer => new TrainerWithAssignmentsDto
            {
                TrainerId = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Gender = trainer.Gender,
                EmployementType = trainer.EmployementType,
                DateOfBirth = trainer.DateOfBirth,
                DateOfJoining = trainer.DateOfJoining,
                IsDeactivated = trainer.IsDeactivated,
                AssignedLocationIds = trainer.TrainerAssignments.Select(ta => ta.UnitLocationId).ToList(),
                UnitLocationDetails = trainer.TrainerAssignments
                    .Select(ta => new UnitLocationDetailsDto
                    {
                        UnitLocationId = ta.UnitLocationId,
                        UnitId = ta.UnitLocation.UnitId,
                        UnitName = ta.UnitLocation.Unit.Name,
                        StateId = ta.UnitLocation.District.State.Id,
                        StateName = ta.UnitLocation.District.State.Name,
                        DistrictId = ta.UnitLocation.District.Id,
                        DistrictName = ta.UnitLocation.District.Name
                    })
                    .ToList()
            }).ToList();
        }

        public async Task<ServiceResult> DeleteTrainerAsync(int trainerId)
        {
            var trainer = await _userRepository.GetByIdAsync(trainerId);
            if (trainer is null)
                return ServiceResult.Failure($"Trainer {trainerId} not found", ServiceErrorStatus.NOTFOUND);

            if (trainer.Role != Role.TRAINER)
                return ServiceResult.Failure("This user is not a trainer", ServiceErrorStatus.FORBIDDEN);

            // 🔐 Check ownership - only the unit head who created the trainer can delete
            if (trainer.CreatedById != _currentUser.UserId)
                return ServiceResult.Failure("You can only delete trainers you created", ServiceErrorStatus.FORBIDDEN);

            // 🔎 Check if trainer is mapped to any units
            var assignments = await _trainerAssignment.GetByTrainerIdAsync(trainerId);

            
            if (assignments.Any())
                return ServiceResult.Failure(
                    "Trainer cannot be deleted because they are still mapped to unit(s). Please remove assignments first.",
                    ServiceErrorStatus.INVALIDOPERATION
                );

            // ✅ Delete trainer
            await _userRepository.DeleteAsync(trainer);
            return ServiceResult.Success("Trainer deleted successfully");
        }

        public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                return ServiceResult.Failure("Email is required", ServiceErrorStatus.INVALIDOPERATION);

            var user = await _userRepository.GetByEmailForPasswordResetAsync(dto.Email);

            // Always return success to prevent user enumeration
            if (user == null)
            {
                // Add a small delay to mimic processing time
                await Task.Delay(500);
                return ServiceResult.Success("If an account with this email exists, you will receive an OTP shortly.");
            }

            if (user.IsDeactivated)
            {
                // Don't reveal account status
                return ServiceResult.Success("If an account with this email exists, you will receive an OTP shortly.");
            }

            // Generate 6-digit OTP
            var otp = GenerateSecureOTP();
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_emailSettings.OTPExpiryMinutes); 

            await _userRepository.SetPasswordResetOTPAsync(user.Id, otp, expiresAt);

            try
            {
                await _emailService.SendPasswordResetOTPAsync(user.Email, otp, user.FirstName);
            }
            catch (Exception ex)
            {
                
                //logging Considered : _logger.LogError(ex, "Failed to send OTP email to {Email}", user.Email);

                // Clear the OTP since email failed
                await _userRepository.ClearPasswordResetOTPAsync(user.Id);

                return ServiceResult.Failure("Failed to send OTP. Please try again later.", ServiceErrorStatus.INVALIDOPERATION);
            }

            return ServiceResult.Success("If an account with this email exists, you will receive an OTP shortly.");
        }

        public async Task<ServiceResult<OTPVerificationResult>> VerifyPasswordResetOTPAsync(VerifyOTPDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.OTP))
                return ServiceResult<OTPVerificationResult>.Failure("Email and OTP are required", ServiceErrorStatus.INVALIDOPERATION);

            var user = await _userRepository.GetByEmailForPasswordResetAsync(dto.Email);
            if (user == null)
            {
                await Task.Delay(500); // Prevent timing attacks
                return ServiceResult<OTPVerificationResult>.Success(
                    OTPVerificationResult.Failure("Invalid email or OTP"));
            }

            // Check if OTP exists
            if (string.IsNullOrEmpty(user.PasswordResetOTP))
            {
                return ServiceResult<OTPVerificationResult>.Success(
                    OTPVerificationResult.Failure("No OTP found. Please request a new one."));
            }

            // Check if too many attempts
            if (user.PasswordResetOTPAttempts >= _emailSettings.MaxOTPAttempts)
            {
                return ServiceResult<OTPVerificationResult>.Success(
                    OTPVerificationResult.Failure("Too many attempts. Please request a new OTP.", 0, true));
            }

            // Check if OTP is expired
            if (!user.PasswordResetOTPExpiresAt.HasValue || DateTimeOffset.UtcNow > user.PasswordResetOTPExpiresAt.Value)
            {
                return ServiceResult<OTPVerificationResult>.Success(
                    OTPVerificationResult.Failure("OTP has expired. Please request a new one."));
            }

            // Validate OTP
            var isValid = await _userRepository.ValidatePasswordResetOTPAsync(user.Id, dto.OTP);

            if (!isValid)
            {
                var remainingAttempts = _emailSettings.MaxOTPAttempts - user.PasswordResetOTPAttempts - 1;
                var message = remainingAttempts > 0
                    ? $"Invalid OTP. {remainingAttempts} attempts remaining."
                    : "Too many failed attempts. Please request a new OTP.";

                return ServiceResult<OTPVerificationResult>.Success(
                    OTPVerificationResult.Failure(message, remainingAttempts, remainingAttempts <= 0));
            }

            return ServiceResult<OTPVerificationResult>.Success(OTPVerificationResult.Success());
        }

        public async Task<ServiceResult> ResetPasswordWithOTPAsync(ResetPasswordWithOTPDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) ||
                string.IsNullOrWhiteSpace(dto.OTP) ||
                string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                return ServiceResult.Failure("All fields are required", ServiceErrorStatus.INVALIDOPERATION);
            }

            var user = await _userRepository.GetByEmailForPasswordResetAsync(dto.Email);
            if (user == null)
            {
                await Task.Delay(500);
                return ServiceResult.Failure("Invalid request", ServiceErrorStatus.INVALIDOPERATION);
            }

            // Verify OTP one more time
            var verifyResult = await VerifyPasswordResetOTPAsync(new VerifyOTPDto
            {
                Email = dto.Email,
                OTP = dto.OTP
            });

            if (!verifyResult.IsSuccess || !verifyResult.Data.IsValid)
            {
                return ServiceResult.Failure(verifyResult.Data?.ErrorMessage ?? "Invalid OTP", ServiceErrorStatus.INVALIDOPERATION);
            }

            // Reset password
            user.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword);
            await _userRepository.SaveAsync(user);

            // Clear OTP data
            await _userRepository.ClearPasswordResetOTPAsync(user.Id);

            //Invalidate all existing sessions for security
            await _sessionRepository.DeleteAllUserSessionsAsync(user.Id);

            return ServiceResult.Success("Password reset successfully");
        }

        private string GenerateSecureOTP()
        {
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);

            var number = Math.Abs(BitConverter.ToInt32(bytes, 0));
            var otp = (number % 900000) + 100000; // Ensures 6-digit number (100000-999999)

            return otp.ToString();
        }



    }
}
