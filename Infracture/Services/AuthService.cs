using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _sessionRepository;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly int _refreshTokenExpiryDays;

        public AuthService(
            IUserRepository userRepository,
            IUserSessionRepository sessionRepository,
            ITokenService tokenService,
            IConfiguration configuration,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _tokenService = tokenService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
            _refreshTokenExpiryDays = int.Parse(_configuration["AppSettings:RefreshTokenExpiryDays"] ?? "30");
        }

        public async Task<TokenResponseDto> LoginAsync(LoginRequestDto request, DeviceInfoDto deviceInfo)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            if (user.IsDeactivated)
            {
                throw new InvalidOperationException("Account is deactivated");
            }

            // Determine refresh token expiry based on RememberMe
            // RememberMe = false → 1 day (expires with access token)
            // RememberMe = true → configured days (default 30 days)
            var refreshTokenExpiryDays = request.RememberMe ? _refreshTokenExpiryDays : 1;

            // Create new session
            var session = new UserSession
            {
                UserId = user.Id,
                RefreshToken = _tokenService.GenerateRefreshToken(),
                DeviceType = deviceInfo.DeviceType ?? "Unknown Device",
                IpAddress = deviceInfo.IpAddress,
                UserAgent = deviceInfo.UserAgent,
                DeviceId = deviceInfo.DeviceId,
                Location = deviceInfo.Location,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays),
                LastUsedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _sessionRepository.CreateAsync(session);

            var accessToken = _tokenService.GenerateAccessToken(user.Id, user.FirstName,user.Role, user.OrganizationId??-1);
            var accessTokenExpiry = _tokenService.GetTokenExpiration(accessToken);

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = session.RefreshToken,
                AccessTokenExpires = accessTokenExpiry,
                RefreshTokenExpires = session.ExpiresAt
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request, DeviceInfoDto deviceInfo)
        {
            var session = await _sessionRepository.GetByRefreshTokenAsync(request.RefreshToken);

            if (session == null || !session.IsActive || session.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }

            // Check if token is from the same device
            if (!string.IsNullOrEmpty(deviceInfo.DeviceId) &&
                !string.IsNullOrEmpty(session.DeviceId) &&
                session.DeviceId != deviceInfo.DeviceId)
            {
                throw new UnauthorizedAccessException("Device mismatch");
            }

            var user = await _userRepository.GetByIdAsync(session.UserId);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Generate new tokens
            var newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email,user.Role,user.OrganizationId??-1);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            // Update session
            session.RefreshToken = newRefreshToken;
            session.LastUsedAt = DateTime.UtcNow;
            session.IpAddress = deviceInfo.IpAddress;
            session.ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);// extend refresh token expiration time

            await _sessionRepository.UpdateAsync(session);

            var accessTokenExpiry = _tokenService.GetTokenExpiration(newAccessToken);

            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                AccessTokenExpires = accessTokenExpiry,
                RefreshTokenExpires = session.ExpiresAt
            };
        }
       

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var session = await _sessionRepository.GetByRefreshTokenAsync(refreshToken);
            if (session == null) return false;

            session.IsActive = false;
            await _sessionRepository.UpdateAsync(session);
            return true;
        }

        public async Task<bool> RevokeAllUserTokensAsync(int userId)
        {
            await _sessionRepository.DeleteAllUserSessionsAsync(userId);
            return true;
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var session = await _sessionRepository.GetByRefreshTokenAsync(refreshToken);
            if (session != null)
            {
                await _sessionRepository.DeleteAsync(session.Id);
            }
        }

        public async Task<IEnumerable<UserSessionDto>> GetActiveSessionsAsync(int userId)
        {
            var sessions = await _sessionRepository.GetActiveSessionsByUserIdAsync(userId);
            return sessions.Select(s => new UserSessionDto
            {
                Id = s.Id,
                DeviceInfo = s.DeviceType,
                IpAddress = s.IpAddress,
                Location = s.Location ?? "Unknown",
                CreatedAt = s.CreatedAt,
                LastUsedAt = s.LastUsedAt ?? s.CreatedAt,
                IsCurrent = false
            });
        }
    }
}
