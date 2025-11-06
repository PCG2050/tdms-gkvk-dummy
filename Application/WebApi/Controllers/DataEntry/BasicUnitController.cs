using Application.Interface;
using Application.Interface.Repository;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.DataEntry
{
    /// <summary>
    /// Base controller providing common functionality for all unit data entry controllers
    /// Handles trainer access validation and unit location verification
    /// </summary>
    public abstract class BaseUnitController : ControllerBase
    {
        protected readonly TdmsDbContext _context;
        protected readonly ICurrentUserService _currentUser;
        protected readonly ITrainerAssignmentRepository _trainerAssignment;
        protected readonly int _unitId;

        protected BaseUnitController(
            TdmsDbContext context,
            ICurrentUserService currentUser,
            ITrainerAssignmentRepository trainerAssignment,
            int unitId)
        {
            _context = context;
            _currentUser = currentUser;
            _trainerAssignment = trainerAssignment;
            _unitId = unitId;
        }

        /// <summary>
        /// Validates that the trainer has access to the specified unit location
        /// </summary>
        protected async Task<bool> ValidateTrainerAccess(int unitLocationId)
        {
            var assignments = await _trainerAssignment.GetByTrainerIdAsync(_currentUser.UserId);
            return assignments.Any(a => a.UnitLocationId == unitLocationId);
        }

        /// <summary>
        /// Validates that the unit location belongs to this controller's unit
        /// </summary>
        protected async Task<bool> ValidateUnitLocation(int unitLocationId)
        {
            var unitLocation = await _context.OrganizationUnitLocations
                .FirstOrDefaultAsync(ul => ul.Id == unitLocationId && ul.UnitId == _unitId);
            return unitLocation != null;
        }

        /// <summary>
        /// Combined validation: checks both unit location and trainer access
        /// </summary>
        protected async Task<IActionResult?> ValidateAccess(int unitLocationId)
        {
            if (!await ValidateUnitLocation(unitLocationId))
                return BadRequest($"Invalid unit location for {GetUnitName()}");

            if (!await ValidateTrainerAccess(unitLocationId))
                return Forbid("You don't have access to this unit location");
           
            return null; // null means validation passed
        }

        /// <summary>
        /// Gets the name of the unit for error messages
        /// </summary>
        protected abstract string GetUnitName();

        /// <summary>
        /// Validates that an entity was created by the current user
        /// </summary>
        protected bool ValidateOwnership<T>(T entity) where T : ReportEntryBaseEntity
        {
            return entity.CreatedById == _currentUser.UserId;
        }


        /// <summary>
        /// Standard response for ownership validation failure
        /// </summary>
        protected IActionResult OwnershipError()
        {
            return Forbid("You can only modify your own entries");
        }
    }
}