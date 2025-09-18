using Application.Interface;
using Domain.Entities.Enum;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly TdmsDbContext _context;

        public CurrentUserService(IHttpContextAccessor httpcontextAccessor, TdmsDbContext context)
        {
            _httpcontextAccessor = httpcontextAccessor;
            _context = context;
        }

        public ClaimsPrincipal? User => _httpcontextAccessor.HttpContext.User;
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

        public bool IsAuthenticated
        {
            get
            {
                return _httpcontextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            }
        }
     

        public async Task<IReadOnlyCollection<int>> MappedUnitLocationIds()
        {
            try
            {
                int userId = this.UserId;
                var unitLocations = await _context.UnitTrainers.Where(x => x.TrainerId == userId).Select(x => x.UnitLocationId).ToArrayAsync();
                return unitLocations??[];
            }
            catch(Exception ex)
            {
                return [];
            }
        }
    }
}
