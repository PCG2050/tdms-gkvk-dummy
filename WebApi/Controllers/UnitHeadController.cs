

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class UnitHeadController : ControllerBase
    {
        private readonly IUnitHeadAssignmentService _assignmentService;
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOrganizationUnitService _organizationUnitService;
        private readonly IOrganizationService _organizationService;

        public UnitHeadController(IUnitHeadAssignmentService assignmentService, IUserService userService, ICurrentUserService currentUserService, IOrganizationUnitService organizationUnitService, IOrganizationService organizationService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _currentUserService = currentUserService;
            _organizationUnitService = organizationUnitService;
            _organizationService = organizationService;
        }

        [HttpGet]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<IActionResult> GetUnitHeadOverview([FromQuery] PaginationRequest paginationRequest)
        {
            var paginatedResult = await _userService.GetPaginatedOrganizationUnitHeads(paginationRequest.PageNumber, paginationRequest.PageSize);
            return Ok(paginatedResult);
        }

        //new one
        [HttpGet("Details/all")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<IActionResult> GetUnitHeadDetails()
        {
            var paginatedResult = await _userService.GetPaginatedOrgUnitHeads();
            return Ok(paginatedResult);
        }



        [HttpGet("{unitHeadId}/units")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetUnitHeadUnits(int unitHeadId)
        {
            var result = await _assignmentService.GetUnitHeadUnits(unitHeadId);
            if (!result.IsSuccess) return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);
            return Ok(result.Data);
        }

        //Get all trainers created bya a specific UnitHead
        [HttpGet("{unitHeadId}/trainers/all")]
        [Authorize(Roles = $"{RoleString.UnitHead},{RoleString.Admin}")]
        public async Task<IActionResult> GetAllTrainersCreatedByUnitHead(int unitHeadId)
        {
            try
            {
                var trainers = await _userService.GetAllTrainersCreatedByUnitHead(unitHeadId);
                return Ok(trainers);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: " An error occured while retreiving trainers");
            }
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> RegisterUnitHead(UserRegisterDto registerDto)
        {
            try
            {
                int orgId = _currentUserService.OrganizationId;

                // Step 1: Create user
                var user = await _organizationService.CreateUser(registerDto, orgId);

                // Step 2: If UnitHead, sync the assignments
                if (user.Role == Role.UNITHEAD && registerDto.OrganizationUnitLocationIds?.Any() == true)
                {
                    var request = new BulkUnitHeadAssignmentByLocationDto
                    {
                        UnitHeadId = user.Id,
                        OrganizationUnitLocationIds = registerDto.OrganizationUnitLocationIds
                    };

                    await _organizationUnitService.SyncUnitHeadAssignmentsByLocationAsync(request);
                }
                //Return Clean DTO instead of Entity
                var unitHeadResponseDto = new UserWithAssignmentsResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    Role = user.Role,
                    OrganizationId = user.OrganizationId,
                    IsDeactivated = user.IsDeactivated,

                    OrganizationUnitLocationIds = registerDto.OrganizationUnitLocationIds ?? new List<int>()
                };

                return Ok(unitHeadResponseDto);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("UnitHead/{id}")]
        [Authorize(Roles = $"{RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<IActionResult> UpdateUnitHead(int id, UserUpdateDto updateDto)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user is null) return NotFound($"User {id} not found");

                // Update user basic details
                await _userService.UpdateUserAsync(updateDto);

                // If UnitHead role, sync assignments
                if (updateDto.Role == Role.UNITHEAD && updateDto.OrganizationUnitLocationIds != null)
                {
                    var request = new BulkUnitHeadAssignmentByLocationDto
                    {
                        UnitHeadId = user.Id,
                        OrganizationUnitLocationIds = updateDto.OrganizationUnitLocationIds
                    };

                    await _organizationUnitService.SyncUnitHeadAssignmentsByLocationAsync(request);
                }

                return Ok(ServiceResult.Success("User updated successfully"));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/UnitHead")]
        [Authorize(Roles = $"{RoleString.Admin}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUnitHeadAsync(id, _currentUserService.UserId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}

