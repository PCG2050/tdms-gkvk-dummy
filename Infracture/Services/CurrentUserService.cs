using Application.Interface;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ClaimsPrincipal? User => _contextAccessor.HttpContext.User;
        public int UserId {
            get
                {
                    var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (id is not null && Int32.TryParse(id,out int userId)) return userId;
                    throw new InvalidOperationException("User Id is missing from token");
                }
            }

        public Role Role {
            get
            {
                var role = User?.FindFirst(ClaimTypes.Role)?.Value;
                if(role is not null && Enum.TryParse<Role>(role, true, out Role parsedRole)) return parsedRole;
                return Role.UNDEFINED;
            }
        }
        public int OrganizationId
        {
            get
            {
                var orgId = User?.FindFirst("organization")?.Value;
                if (orgId is not null && Int32.TryParse(orgId, out int id)) return id;
                throw new InvalidOperationException("Organization Id is missing from token");
            }
        }
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
    }
}
