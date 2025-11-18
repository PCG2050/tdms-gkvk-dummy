


namespace Application.Interface.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetByActivationTokenAsync(string token);
        Task<List<User>> GetAllAsync();
        Task SaveAsync(User user);
        Task DeleteAsync(User user);
        Task<List<User>> GetUsersByOrganizationAndRoleAsync(int organizationId, Role role);
        //Task SetPasswordResetTokenAsync(int userId, string token, DateTimeOffset expiresAt);


        Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedItemsAsync(
            int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedUnitHeadsAsync(
            int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<List<FlatUnitHeadDetailsDto>> GetDetailedPaginatedUnitHeadsAsync(int organizationId, int adminId);

        Task<List<FlatUnitHeadDetailsDto>> GetDetailedPaginatedTrainersAsync(int organizationId, int unitHeadId);

        Task<PaginatedResult<FlatTrainerDetailsDto>> GetPaginatedTrainerDetailsWithLocationAsync(
            int organizaitonId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<List<User>> GetTrainersCreatedByAsync(int createdById);

        Task SetPasswordResetOTPAsync(int userId, string otp, DateTimeOffset expiresAt);
        Task<User?> GetByEmailForPasswordResetAsync(string email);
        Task<bool> ValidatePasswordResetOTPAsync(int userId, string otp);
        Task ClearPasswordResetOTPAsync(int userId);
        Task<User?> GetUserWithValidOTPAsync(int userId, string otp);

        /// <summary>
        /// Get users by a list of user IDs
        /// </summary>
        Task<List<User>> GetUsersByIdsAsync(List<int> userIds);

    }
}
