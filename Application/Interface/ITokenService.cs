using Domain.Entities;
using Domain.Entities.Enum;

namespace Application.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(int userId,string userName,Role role, int organizationId);
        string GenerateRefreshToken();
        DateTime GetTokenExpiration(string token);
    }
}
