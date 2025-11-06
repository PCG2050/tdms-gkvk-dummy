using Application.Models;

namespace Application.Interface
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(LoginRequestDto request, DeviceInfoDto deviceInfo);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request, DeviceInfoDto deviceInfo);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> RevokeAllUserTokensAsync(int userId);
        Task LogoutAsync(string refreshToken);
        Task<IEnumerable<UserSessionDto>> GetActiveSessionsAsync(int userId);
    }
}
