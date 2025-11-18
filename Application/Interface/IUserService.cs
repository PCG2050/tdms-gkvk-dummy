using Application.Models;
using Domain.Entities;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegisterDto registerDto);

        Task<ServiceResult> DeleteUnitHeadAsync(int userId, int currentUserId);

        Task<ServiceResult> DeleteTrainerAsync(int trainerId);

        Task<User?> ValidateUserAsync(string email, string password);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User> GetUserByActivationTokenAsync(string token);
        Task<ServiceResult> UpdateUserAsync(UserUpdateDto updateDto);
        Task<List<User>> GetAllOrganizationUsersAsync();
        Task<List<User>> GetOrganizationUnitTrainers(int unitId);
        Task<List<User>> GetOrganizationTrainers();
        Task<User> GetCurrentUserDetailsAsync();
        Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedOrganizationTrainers(int pageNumber = Constants.Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.Constants.PAGINATION_PAGE_SIZE_DEFAULT);

        Task<PaginatedResult<FlatTrainerDetailsDto>> GetPaginatedOrgTrainers(int pageNumber = Constants.Constants.PAGINATION_PAGE_SIZE_DEFAULT, int pageSize = Constants.Constants.PAGINATION_PAGE_SIZE_DEFAULT);

        Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedOrganizationUnitHeads(int pageNumber = Constants.Constants.PAGINATION_PAGE_NUMBER_DEFAULT, int pageSize = Constants.Constants.PAGINATION_PAGE_SIZE_DEFAULT);

        Task<List<FlatUnitHeadDetailsDto>> GetPaginatedOrgUnitHeads();
        Task<List<FlatUnitHeadDetailsDto>> GetPaginatedTrainers();
        Task<ServiceResult> UpdatedAccountStatus(int id, bool activate = true);

        Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto dto);


        Task<ServiceResult<OTPVerificationResult>> VerifyPasswordResetOTPAsync(VerifyOTPDto dto);

        Task<ServiceResult> ResetPasswordWithOTPAsync(ResetPasswordWithOTPDto dto);


        //New method for unitheads to get trainers they created
        Task<List<TrainerDetailsDto>> GetAllTrainersCreatedByUnitHead(int unitHeadId);

        /// <summary>
        /// Get all trainers assigned to a specific unit location
        /// </summary>
        Task<List<TrainerDetailsDto>> GetTrainersByUnitLocationAsync(int unitLocationId);

    }
}
