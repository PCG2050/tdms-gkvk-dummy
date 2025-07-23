using Application.Interface;
using Application.Models;
using Domain.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("/api/organization-units")]
    public class OrganizationUnitController : ControllerBase
    {
        readonly IOrganizationUnitService _organizationUnit;

        public OrganizationUnitController(IOrganizationUnitService organizationUnit)
        {
            _organizationUnit = organizationUnit;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrgUnitLocationDetailsDto>>> GetOrgUnits()
        {
            var mappedUnitsWithLocations = await _organizationUnit.GetOrganizationUnitsDetails();
            return Ok(mappedUnitsWithLocations);
        }

        [HttpPost]
        [Authorize(Roles = RoleString.Admin)]
        public ActionResult AddUnitToOrg(OrganizationUnitLocationDto orgUnitAssignment)
        {
            _organizationUnit.AddUnitToOrganization(orgUnitAssignment);
            return Created();
        }

        [HttpDelete]
        [Authorize(Roles = RoleString.Admin)]
        public ActionResult DeleteUnitFromOrg(OrganizationUnitLocationDto orgUnitAssignment)
        {
            _organizationUnit.RemoveUnitFromOrganization(orgUnitAssignment);
            return NoContent();
        }

        [HttpPost("trainers")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> MapExistingTrainers(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var result = await _organizationUnit.MapExistingTrainersAsync(trainerAssignment);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpDelete("trainers")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> UnMapExistingTrainers(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var result = await _organizationUnit.UnMapTrainerFromUnitLocationAsync(trainerAssignment);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return Ok();
        }
    }
}
