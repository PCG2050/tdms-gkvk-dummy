using Application.Interface.Repository;

namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly TdmsDbContext _context;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;

        public CurrentUserService(
            IHttpContextAccessor httpcontextAccessor,
            TdmsDbContext context,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository)
        {
            _httpcontextAccessor = httpcontextAccessor;
            _context = context;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
        }

        public ClaimsPrincipal? User => _httpcontextAccessor.HttpContext.User;
        public int UserId
        {
            get
            {
                var id = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (id is not null && Int32.TryParse(id, out int userId)) return userId;
                throw new InvalidOperationException("User Id is missing from token");
            }
        }

        public Role Role
        {
            get
            {
                var role = User?.FindFirst(ClaimTypes.Role)?.Value;
                if (role is not null && Enum.TryParse<Role>(role, true, out Role parsedRole)) return parsedRole;
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
                Role role = this.Role;

                // Handle different roles
                if (role == Role.TRAINER)
                {
                    var unitLocations = await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(userId);
                    return unitLocations ?? [];
                }
                else if (role == Role.UNITHEAD)
                {
                    var unitLocations = await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(userId);
                    return unitLocations ?? [];
                }
                else if (role == Role.ADMIN)
                {
                    var unitLocations = await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(this.OrganizationId);
                    return unitLocations ?? [];
                }

                // For any other role or undefined
                return [];
            }
            catch (Exception ex)
            {
                return [];
            }
        }
    }
}
