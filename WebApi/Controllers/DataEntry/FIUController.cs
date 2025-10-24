

namespace WebApi.Controllers.DataEntry
{
    /// <summary>
    /// Controller for FIU (Farm Information Unit) data entry
    /// FIU only has 2 tables: FIUProgramActivity and FIUOtherActivity
    /// </summary>
    [ApiController]
    [Route("api/units/fiu")]
    [Authorize(Roles = RoleString.Trainer)]
    public class FIUController : ControllerBase
    {
        private readonly TdmsDbContext _context;
        private readonly ICurrentUserService _currentUser;
        private readonly ITrainerAssignmentRepository _trainerAssignment;
        private const int FIU_UNIT_ID = 3; // FIU Unit ID from master data

        public FIUController(
            TdmsDbContext context,
            ICurrentUserService currentUser,
            ITrainerAssignmentRepository trainerAssignment)
        {
            _context = context;
            _currentUser = currentUser;
            _trainerAssignment = trainerAssignment;
        }

        #region Helper Methods

        /// <summary>
        /// Validates that the trainer has access to the specified unit location
        /// </summary>
        private async Task<bool> ValidateTrainerAccess(int unitLocationId)
        {
            var assignments = await _trainerAssignment.GetByTrainerIdAsync(_currentUser.UserId);
            return assignments.Any(a => a.UnitLocationId == unitLocationId);
        }

        /// <summary>
        /// Validates that the unit location belongs to FIU unit
        /// </summary>
        private async Task<bool> ValidateFIUUnit(int unitLocationId)
        {
            var unitLocation = await _context.OrganizationUnitLocations
                .FirstOrDefaultAsync(ul => ul.Id == unitLocationId && ul.UnitId == FIU_UNIT_ID);
            return unitLocation != null;
        }

        #endregion

        #region FIU Program Activities

        /// <summary>
        /// Get all program activities created by the current trainer for their assigned unit location
        /// Trainer can only see their own data
        /// </summary>
        /// <param name="unitLocationId">The unit location ID to filter activities</param>
        /// <returns>List of FIU program activities created by current trainer</returns>
        [HttpGet("program-activities")]
        public async Task<IActionResult> GetProgramActivities([FromQuery] int unitLocationId)
        {
            if (!await ValidateFIUUnit(unitLocationId))
                return BadRequest("Invalid FIU unit location");

            if (!await ValidateTrainerAccess(unitLocationId))
                return Forbid("You don't have access to this unit location");

            // Only get activities created by current trainer
            var activities = await _context.FIUProgramActivities
                .Where(a => a.CreatedById == _currentUser.UserId)
                .Include(a => a.FIUActivities) // Include the master data for activity type
                .Include(a => a.CreatedBy)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return Ok(activities);
        }

        /// <summary>
        /// Get a specific program activity by ID
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <returns>FIU program activity</returns>
        [HttpGet("program-activities/{id}")]
        public async Task<IActionResult> GetProgramActivity(int id)
        {
            var activity = await _context.FIUProgramActivities
                .Include(a => a.FIUActivities)
                .Include(a => a.CreatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                return NotFound("Program activity not found");

            if (activity.CreatedById != _currentUser.UserId)
                return Forbid("You can only access your own entries");

            return Ok(activity);
        }

        /// <summary>
        /// Create a new FIU program activity
        /// </summary>
        /// <param name="activityDto">Activity data</param>
        /// <returns>Created activity</returns>
        [HttpPost("program-activities")]
        public async Task<IActionResult> CreateProgramActivity([FromBody] FIUProgramActivityDto activityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await ValidateFIUUnit(activityDto.UnitLocationId))
                return BadRequest("Invalid FIU unit location");

            if (!await ValidateTrainerAccess(activityDto.UnitLocationId))
                return Forbid("You don't have access to this unit location");

            // Validate FIU Activity exists (if provided)
            // Skip validation if FIUActivitiesId is null or 0
            if (activityDto.FIUActivitiesId.HasValue && activityDto.FIUActivitiesId.Value > 0)
            {
                // Check in the correct master data table
                var activityExists = await _context.Set<Domain.Entities.MasterData.FIUActivity>()
                    .AnyAsync(f => f.Id == activityDto.FIUActivitiesId.Value);

                if (!activityExists)
                    return BadRequest($"Invalid FIU Activity ID: {activityDto.FIUActivitiesId.Value}. Please check the available activity types at /api/units/fiu/activity-types");
            }

            var activity = new FIUProgramActivity
            {
                UnitLocationId = activityDto.UnitLocationId,  // ADD THIS
                OrganizationId = _currentUser.OrganizationId,  // ADD THIS
                FIUActivitiesId = activityDto.FIUActivitiesId,
                Number = activityDto.Number,
                UploadMediaUrl = activityDto.UploadMediaUrl,
                CreatedById = _currentUser.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.FIUProgramActivities.Add(activity);
            await _context.SaveChangesAsync();

            // Return a clean response without circular references
            var response = new
            {
                id = activity.Id,
                unitLocationId = activity.UnitLocationId,
                organizationId = activity.OrganizationId,
                fiuActivitiesId = activity.FIUActivitiesId,
                number = activity.Number,
                uploadMediaUrl = activity.UploadMediaUrl,
                createdById = activity.CreatedById,
                createdAt = activity.CreatedAt
            };

            return CreatedAtAction(nameof(GetProgramActivity), new { id = activity.Id }, response);
        }

        /// <summary>
        /// Update an existing FIU program activity
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="activityDto">Updated activity data</param>
        /// <returns>Updated activity</returns>
        [HttpPut("program-activities/{id}")]
        public async Task<IActionResult> UpdateProgramActivity(int id, [FromBody] FIUProgramActivityDto activityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.FIUProgramActivities.FindAsync(id);
            if (existing == null)
                return NotFound("Program activity not found");

            if (existing.CreatedById != _currentUser.UserId)
                return Forbid("You can only update your own entries");

            // Validate FIU Activity exists if provided
            if (activityDto.FIUActivitiesId.HasValue && activityDto.FIUActivitiesId.Value > 0)
            {
                var activityExists = await _context.Set<Domain.Entities.MasterData.FIUActivity>()
                    .AnyAsync(f => f.Id == activityDto.FIUActivitiesId.Value);

                if (!activityExists)
                    return BadRequest($"Invalid FIU Activity ID: {activityDto.FIUActivitiesId.Value}. Please check the available activity types at /api/units/fiu/activity-types");
            }

            // Update fields
            existing.FIUActivitiesId = activityDto.FIUActivitiesId;
            existing.Number = activityDto.Number;
            existing.UploadMediaUrl = activityDto.UploadMediaUrl;
            existing.UpdatedById = _currentUser.UserId;
            existing.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            // Reload with includes
            var updated = await _context.FIUProgramActivities
                .Include(a => a.FIUActivities)
                .Include(a => a.UpdatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(updated);
        }

        /// <summary>
        /// Delete a FIU program activity
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <returns>No content</returns>
        [HttpDelete("program-activities/{id}")]
        public async Task<IActionResult> DeleteProgramActivity(int id)
        {
            var activity = await _context.FIUProgramActivities.FindAsync(id);
            if (activity == null)
                return NotFound("Program activity not found");

            if (activity.CreatedById != _currentUser.UserId)
                return Forbid("You can only delete your own entries");

            _context.FIUProgramActivities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region FIU Other Activities

        /// <summary>
        /// Get all other activities created by the current trainer for their assigned unit location
        /// Trainer can only see their own data
        /// </summary>
        /// <param name="unitLocationId">The unit location ID to filter activities</param>
        /// <returns>List of FIU other activities created by current trainer</returns>
        [HttpGet("other-activities")]
        public async Task<IActionResult> GetOtherActivities([FromQuery] int unitLocationId)
        {
            if (!await ValidateFIUUnit(unitLocationId))
                return BadRequest("Invalid FIU unit location");

            if (!await ValidateTrainerAccess(unitLocationId))
                return Forbid("You don't have access to this unit location");

            // Only get activities created by current trainer
            var activities = await _context.FIUOtherActivities
                .Where(a => a.CreatedById == _currentUser.UserId)
                .Include(a => a.CreatedBy)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return Ok(activities);
        }
     
        /// <summary>
        /// Get a specific other activity by ID
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <returns>FIU other activity</returns>
        [HttpGet("other-activities/{id}")]
        public async Task<IActionResult> GetOtherActivity(int id)
        {
            var activity = await _context.FIUOtherActivities
                .Include(a => a.CreatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null)
                return NotFound("Other activity not found");

            if (activity.CreatedById != _currentUser.UserId)
                return Forbid("You can only access your own entries");

            return Ok(activity);
        }

        /// <summary>
        /// Create a new FIU other activity
        /// </summary>
        /// <param name="activityDto">Activity data</param>
        /// <returns>Created activity</returns>
        [HttpPost("other-activities")]
        public async Task<IActionResult> CreateOtherActivity([FromBody] FIUOtherActivityDto activityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await ValidateFIUUnit(activityDto.UnitLocationId))
                return BadRequest("Invalid FIU unit location");

            if (!await ValidateTrainerAccess(activityDto.UnitLocationId))
                return Forbid("You don't have access to this unit location");

            var activity = new FIUOtherActivity
            {
                UnitLocationId = activityDto.UnitLocationId,  // ADD THIS
                OrganizationId = _currentUser.OrganizationId,  // ADD THIS
                Title = activityDto.Title,
                Description = activityDto.Description,
                UploadMediaUrl = activityDto.UploadMediaUrl,
                CreatedById = _currentUser.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            _context.FIUOtherActivities.Add(activity);
            await _context.SaveChangesAsync();

            // Return a clean response without circular references
            var response = new
            {
                id = activity.Id,
                unitLocationId = activity.UnitLocationId,
                organizationId = activity.OrganizationId,
                title = activity.Title,
                description = activity.Description,
                uploadMediaUrl = activity.UploadMediaUrl,
                createdById = activity.CreatedById,
                createdAt = activity.CreatedAt
            };

            return CreatedAtAction(nameof(GetOtherActivity), new { id = activity.Id }, response);
        }

        /// <summary>
        /// Update an existing FIU other activity
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <param name="activityDto">Updated activity data</param>
        /// <returns>Updated activity</returns>
        [HttpPut("other-activities/{id}")]
        public async Task<IActionResult> UpdateOtherActivity(int id, [FromBody] FIUOtherActivityDto activityDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _context.FIUOtherActivities.FindAsync(id);
            if (existing == null)
                return NotFound("Other activity not found");

            if (existing.CreatedById != _currentUser.UserId)
                return Forbid("You can only update your own entries");

            // Update fields
            existing.Title = activityDto.Title;
            existing.Description = activityDto.Description;
            existing.UploadMediaUrl = activityDto.UploadMediaUrl;
            existing.UpdatedById = _currentUser.UserId;
            existing.UpdatedAt = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();

            // Reload with includes
            var updated = await _context.FIUOtherActivities
                .Include(a => a.UpdatedBy)
                .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(updated);
        }

        /// <summary>
        /// Delete a FIU other activity
        /// </summary>
        /// <param name="id">Activity ID</param>
        /// <returns>No content</returns>
        [HttpDelete("other-activities/{id}")]
        public async Task<IActionResult> DeleteOtherActivity(int id)
        {
            var activity = await _context.FIUOtherActivities.FindAsync(id);
            if (activity == null)
                return NotFound("Other activity not found");

            if (activity.CreatedById != _currentUser.UserId)
                return Forbid("You can only delete your own entries");

            _context.FIUOtherActivities.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        #endregion

        #region Master Data Endpoints

        /// <summary>
        /// Get all FIU activity types (master data for dropdown)
        /// </summary>
        /// <returns>List of FIU activity types</returns>
        [HttpGet("activity-types")]
        public async Task<IActionResult> GetActivityTypes()
        {
            try
            {
                // Use FIUActivities master data table
                var activityTypes = await _context.Set<Domain.Entities.MasterData.FIUActivity>()
                    .OrderBy(f => f.Name)
                    .ToListAsync();

                return Ok(activityTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving activity types", error = ex.Message });
            }
        }

        #endregion
    }

    #region DTOs

    /// <summary>
    /// DTO for creating/updating FIU Program Activities
    /// </summary>
    public class FIUProgramActivityDto
{
    /// <summary>
    /// Required: The unit location ID where this activity is being recorded
    /// </summary>
    public int UnitLocationId { get; set; }

    /// <summary>
    /// Optional: Reference to FIU Activity master data (for dropdown selection)
    /// </summary>
    public int? FIUActivitiesId { get; set; }

    /// <summary>
    /// Optional: Number/count associated with this activity
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Optional: URL to uploaded media file (photo, video, document)
    /// </summary>
    public string? UploadMediaUrl { get; set; }
}

/// <summary>
/// DTO for creating/updating FIU Other Activities
/// </summary>
public class FIUOtherActivityDto
{
    /// <summary>
    /// Required: The unit location ID where this activity is being recorded
    /// </summary>
    public int UnitLocationId { get; set; }

    /// <summary>
    /// Optional: Title of the activity
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Optional: Detailed description of the activity
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Optional: URL to uploaded media file (photo, video, document)
    /// </summary>
    public string? UploadMediaUrl { get; set; }
}

    #endregion
}