
namespace Application.Interface.Repository
{
    public interface IUserSessionRepository
    {
        Task<UserSession?> GetByRefreshTokenAsync(string refreshToken);
        Task<UserSession?> GetByIdAsync(int id);
        Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(int userId);
        Task<UserSession> CreateAsync(UserSession session);
        Task UpdateAsync(UserSession session);
        Task DeleteAsync(int id);
        Task DeleteAllUserSessionsAsync(int userId);
        Task DeleteExpiredSessionsAsync();
    }
}
