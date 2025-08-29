using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class OrganizationController:ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OrganizationDto>> GetOrganization(int id)
        {
            var organization = await _organizationService.GetOrganizationAsync(id);
            if (organization is null)
                return NotFound();
            return Ok(new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name
            });
        }

        [Authorize(Roles =RoleString.SuperAdmin)]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<OrganizationDto>),200)]
        public async Task<IActionResult> GetOrganizations([FromQuery]PaginationRequest request)
        {
            var paginatedResult = await _organizationService.GetPaginatedItemsAsync(request.PageNumber, request.PageSize);
            return Ok(paginatedResult);
        }

        [HttpPost]
        [Authorize(Roles = RoleString.SuperAdmin)]
        [ProducesResponseType(type:typeof(OrganizationDto),200)]
        public async Task<IActionResult> CreateOrganization(OrganizationCreateDto createDto)
        {
            var organizationResult = await _organizationService.CreateOrganizationAsync(createDto);
            if (!organizationResult.IsSuccess) return ServiceResponseToActionResult.Error(organizationResult.ErrorMessage, organizationResult.ErrorStatus);
            var organization = organizationResult.Data;
            return CreatedAtAction(nameof(GetOrganization),new {id=organization.Id}, organization);
        } 

     

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleString.SuperAdmin)]
        [ProducesResponseType(typeof(OrganizationDto), 200)]
        public async Task<IActionResult> UpdateOrganization(int id, OrganizationUpdateDto updateDto)
        {
            updateDto.Id = id; // Ensure ID from route overrides body
            var organizationResult = await _organizationService.UpdateOrganizationAsync(updateDto);

            if (!organizationResult.IsSuccess)
                return ServiceResponseToActionResult.Error(organizationResult.ErrorMessage, organizationResult.ErrorStatus);

            return Ok(organizationResult.Data);
        }

        [HttpPost("{id}/users")]
        [Authorize(Roles = $"{RoleString.SuperAdmin}, {RoleString.Admin},{RoleString.UnitHead}")]
        public async Task<ActionResult<UserDto>> CreateUsers(int id,UserRegisterDto user)
        {
            try
            {
                var admin = await _organizationService.CreateUser(user, id);
                return Ok( new UserDto
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName                
               
                });
            }catch(InvalidOperationException e)
            {
                return BadRequest(new { e.Message });
            }
            catch(UnauthorizedAccessException e)
            {
                return Forbid(e.Message);
            }
            catch (Exception)
            {
                return Problem();
            }
        }

        [HttpGet("{id}/admins")]
        [Authorize(Roles = RoleString.SuperAdmin)]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        public async Task<IActionResult> GetAdmins(int id)
        {
            try
            {
                var admins = await _organizationService.GetAdminsByOrganizationIdAsync(id);
                return Ok(admins);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.SuperAdmin)]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var result = await _organizationService.DeleteOrganizationAsync(id);

            return Ok();
        }

    }
}
