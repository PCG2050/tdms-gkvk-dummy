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
        public async Task<ActionResult<IEnumerable<OrgUnitLocationIdDetailsDto>>> GetOrgUnits()
        {
            var mappedUnitsWithLocations = await _organizationUnit.GetOrganizationUnitsDetails();
            return Ok(mappedUnitsWithLocations);
        }

        //new one
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrgUnitLocationIdDetailsDto>>> GetOrgUnit(int id)
        {
            var unitLocation = await _organizationUnit.GetOrganizationUnitLocationById(id);
            if (unitLocation == null)
                return NotFound($"Organizaition unit location with ID {id} not found");

            return Ok(unitLocation);
        }

        [HttpPost]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> AddUnitToOrg(OrganizationUnitLocationDto orgUnitAssignment)
        {
            try
            {
                var createdUnitLocation = await _organizationUnit.AddUnitToOrganization(orgUnitAssignment);
                return CreatedAtAction(nameof(GetOrgUnit),
                    new { id = createdUnitLocation.Id },
                    createdUnitLocation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "An error occured while adding Org unit");
            }
           
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RoleString.Admin)]
        [ProducesResponseType(typeof(OrgUnitLocationIdDetailsDto), 200)]
        public async Task<IActionResult> UpdateOrgUnitLocation(int id, [FromBody]OrganizationUnitLocationUpdateDto updateDto)
        {
            try
            {
                updateDto.Id = id;
                var result = await _organizationUnit.UpdateOrganizationUnitLocationAsync(updateDto);

                if (!result.IsSuccess)
                    return BadRequest(new { message = result.ErrorMessage });

                return Ok(result.Data);
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
                return Problem(detail: ex.Message, title: "An error occurred while updating the organization unit location");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> DeleteUnitFromOrg(int id)
        {
            try
            {
                var result = await _organizationUnit.RemoveUnitFromOrganizationById(id);
                if (!result.IsSuccess)
                    return BadRequest(new { message = result.ErrorMessage });

                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, title: "An error occurred while deleting the organization unit location");
            }
        }


        [HttpDelete]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> DeleteUnitFromOrg(OrganizationUnitLocationDto orgUnitAssignment)
        {
            await _organizationUnit.RemoveUnitFromOrganization(orgUnitAssignment);
            return NoContent();
        }

        [HttpPost("trainers")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<ActionResult> MapExistingTrainers(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var result = await _organizationUnit.MapExistingTrainersAsync(trainerAssignment);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return Ok();
        }

        [HttpDelete("trainers")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<ActionResult> UnMapExistingTrainers(ExistingTrainerAssignmentDto trainerAssignment)
        {
            var result = await _organizationUnit.UnMapTrainerFromUnitLocationAsync(trainerAssignment);
            if (!result.IsSuccess) return BadRequest(result.ErrorMessage);
            return Ok();
        }


        [HttpPost("unitHeads/bulk")]
        [Authorize(Roles = RoleString.Admin)]
        public async Task<ActionResult> MapExistingUnitHeadsBulk(BulkUnitHeadAssignmentByLocationDto unitHeadsAssignments)
        {
            var results = await _organizationUnit.SyncUnitHeadAssignmentsByLocationAsync(unitHeadsAssignments);         
            return Ok(results);
        }

        // NEW: Trainer bulk assignment - similar to UnitHead bulk
        [HttpPost("trainers/bulk")]
        [Authorize(Roles = RoleString.UnitHead)]
        public async Task<ActionResult> SyncTrainerAssignmentsBulk(BulkTrainerAssignmentByLocationDto trainerAssignments)
        {
            try
            {
                var result = await _organizationUnit.SyncTrainerAssignmentsByLocationAsync(trainerAssignments);
                if (!result.IsSuccess)
                    return BadRequest(new { message = result.ErrorMessage });

                return Ok(result);
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
                return Problem(detail: ex.Message, title: "An error occurred while syncing trainer assignments");
            }
        }

    }
}
