using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(int userId,string userName,Role role, int organizationId);
        Task<string> GenerateRefreshToken();
    }
}
