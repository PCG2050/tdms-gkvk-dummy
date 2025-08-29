using Application.Models;
using Domain.Entities;
using Domain.Entities.Enum;

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
        Task SetPasswordResetTokenAsync(int userId, string token, DateTimeOffset expiresAt);
        Task<User?> GetByPasswordResetTokenAsync(string token);
        Task ClearPasswordResetTokenAsync(int userId);

        Task<PaginatedResult<TrainerDetailsDto>> GetPaginatedItemsAsync(
            int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<PaginatedResult<UnitHeadDetailsDto>> GetPaginatedUnitHeadsAsync(
            int organizationId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<List<FlatUnitHeadDetailsDto>> GetDetailedPaginatedUnitHeadsAsync(int organizationId);

        Task<PaginatedResult<FlatTrainerDetailsDto>> GetDetailedPaginatedTrainersAsync(
            int organizaitonId, int pageNumber = 1, QueryFilter? queryFilter = null, int pageSize = 10);

        Task<List<User>> GetTrainersCreatedByAsync(int createdById);
    }
}
