using Application.Interface.Repository;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly TdmsDbContext _context;

        public UserSessionRepository(TdmsDbContext context)
        {
            _context = context;
        }

        public async Task<UserSession?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.UserSessions
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.RefreshToken == refreshToken && s.IsActive);
        }

        public async Task<UserSession?> GetByIdAsync(int id)
        {
            return await _context.UserSessions
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<UserSession>> GetActiveSessionsByUserIdAsync(int userId)
        {
            return await _context.UserSessions
                .Where(s => s.UserId == userId && s.IsActive && s.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(s => s.LastUsedAt)
                .ToListAsync();
        }

        public async Task<UserSession> CreateAsync(UserSession session)
        {
            _context.UserSessions.Add(session);
            await _context.SaveChangesAsync();
            return session;
        }

        public async Task UpdateAsync(UserSession session)
        {
            _context.UserSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var session = await _context.UserSessions.FindAsync(id);
            if (session != null)
            {
                _context.UserSessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllUserSessionsAsync(int userId)
        {
            var sessions = await _context.UserSessions
                .Where(s => s.UserId == userId && s.IsActive)
                .ToListAsync();

            foreach (var session in sessions)
            {
                session.IsActive = false;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpiredSessionsAsync()
        {
            var expiredSessions = await _context.UserSessions
                .Where(s => s.ExpiresAt <= DateTime.UtcNow)
                .ToListAsync();

            _context.UserSessions.RemoveRange(expiredSessions);
            await _context.SaveChangesAsync();
        }
    }
}
