using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
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
        [HttpPost]
        [Authorize(Roles = RoleString.SuperAdmin)]
        public async Task<ActionResult<OrganizationDto>> CreateOrganization(OrganizationCreateDto createDto)
        {
            var organization = await _organizationService.CreateOrganizationAsync(createDto);
            return CreatedAtAction(nameof(GetOrganization),new {id=organization.Id}, new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name
            });
        }
        [HttpPost("{id}/admins")]
        [Authorize(Roles = RoleString.SuperAdmin)]
        public async Task<ActionResult<UserDto>> CreateOrganizationAdminUser(int id,UserRegisterDto user)
        {
            try
            {
                var admin = await _organizationService.CreateAdminAsync(user, id);
                return CreatedAtAction("", new { id = admin.Id }, new UserDto
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

    }
}
