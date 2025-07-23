using Application.Interface;
using Domain.Entities;
using Domain.Entities.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Services
{
    public class AuthTokenService(IConfiguration configuration) : ITokenService
    {
        public Task<string> GenerateAccessToken(int userId, string userName, Role role, int organizationId)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, userId.ToString()),
                new("user_name",userName),
                new (ClaimTypes.Role,role.ToString()),
                new ("organization",organizationId.ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:TokenKey")!)
                );
            var signingCred = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                issuer: configuration["AppSettings:Issuer"],
                audience: configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCred
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Task<string> GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }
}
