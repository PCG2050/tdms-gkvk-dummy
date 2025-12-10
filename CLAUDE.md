# CLAUDE - Development Notes

This document contains important development notes and bug fixes for the TDMS GKVK project.

## Bug Fixes

### Financial Status Endpoints - Forbidden Error (2025-12-10)

**Issue:**
Users with UNITHEAD and ADMIN roles were receiving "Forbidden" errors when accessing financial status endpoints in the published website, even though they had proper authorization.

**Root Cause:**
The `MappedUnitLocationIds()` method in `Infracture/Services/CurrentUserService.cs` was only querying the `UnitTrainers` table, which meant it only worked for users with the TRAINER role. For UNITHEAD and ADMIN roles, this method returned an empty array, causing permission checks to fail.

**Affected Endpoints:**
- `GET /api/financial-budget` - Get paginated financial budgets
- `GET /api/financial-budget/status/{status}` - Get financial budgets by status
- `GET /api/financial-budget/{id}` - Get financial budget by ID
- `POST /api/financial-budget` - Create financial budget
- `PUT /api/financial-budget/{id}` - Update financial budget
- All other financial budget endpoints that use `MappedUnitLocationIds()`

**Solution:**
Updated the `MappedUnitLocationIds()` method in `CurrentUserService.cs` to handle all three user roles:

1. **TRAINER role:** Queries `TrainerAssignment` table via `ITrainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync()`
2. **UNITHEAD role:** Queries `UnitHeadAssignment` table via `IUnitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync()`
3. **ADMIN role:** Queries all unit locations in the organization via `IOrganizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync()`

**Modified Files:**
- `Infracture/Services/CurrentUserService.cs`
  - Added repository dependencies: `ITrainerAssignmentRepository`, `IUnitHeadAssignmentRepository`, `IOrganizationUnitRepository`
  - Updated constructor to inject the new dependencies
  - Refactored `MappedUnitLocationIds()` method to handle all roles

**Code Changes:**

```csharp
// Before (Only worked for TRAINER role)
public async Task<IReadOnlyCollection<int>> MappedUnitLocationIds()
{
    try
    {
        int userId = this.UserId;
        var unitLocations = await _context.UnitTrainers
            .Where(x => x.TrainerId == userId)
            .Select(x => x.UnitLocationId)
            .ToArrayAsync();
        return unitLocations ?? [];
    }
    catch (Exception ex)
    {
        return [];
    }
}

// After (Works for all roles: TRAINER, UNITHEAD, ADMIN)
public async Task<IReadOnlyCollection<int>> MappedUnitLocationIds()
{
    try
    {
        int userId = this.UserId;
        Role role = this.Role;

        if (role == Role.TRAINER)
        {
            var unitLocations = await _trainerAssignmentRepository
                .GetUnitLocationIdsByTrainerIdAsync(userId);
            return unitLocations ?? [];
        }
        else if (role == Role.UNITHEAD)
        {
            var unitLocations = await _unitHeadAssignmentRepository
                .GetUnitLocationIdsByUnitHeadIdAsync(userId);
            return unitLocations ?? [];
        }
        else if (role == Role.ADMIN)
        {
            var unitLocations = await _organizationUnitRepository
                .GetUnitLocationIdsByOrganizationIdAsync(this.OrganizationId);
            return unitLocations ?? [];
        }

        return [];
    }
    catch (Exception ex)
    {
        return [];
    }
}
```

**Testing:**
After deploying this fix, test the following scenarios:

1. **As TRAINER:**
   - Should be able to access financial budgets for assigned unit locations
   - Query: `SELECT * FROM [dbo].[UnitTrainers] WHERE TrainerId = [YourTrainerId]`

2. **As UNITHEAD:**
   - Should be able to access financial budgets for assigned unit locations
   - Query: `SELECT * FROM [dbo].[UnitHeadAssignments] WHERE UnitHeadId = [YourUnitHeadId]`

3. **As ADMIN:**
   - Should be able to access all financial budgets in their organization
   - Query: `SELECT * FROM [dbo].[OrganizationUnitLocations] WHERE OrganizationId = [YourOrgId]`

**Impact:**
This fix ensures that all user roles (TRAINER, UNITHEAD, ADMIN) can properly access financial endpoints based on their assigned unit locations or organization scope. The same pattern is used throughout the application in other services (e.g., `FinancialBudgetService.GetAccessibleUnitLocationIdsAsync()`), so this fix brings `CurrentUserService` in line with that pattern.

---

## Notes for Future Development

### Authorization Pattern
When implementing new features that require unit location-based access control:

1. Use `ICurrentUserService.MappedUnitLocationIds()` to get the list of accessible unit locations
2. Filter data by these unit location IDs
3. For permission checks on individual entities, use `IEntityPermissionService`
4. Remember that the permission model differs by role:
   - TRAINER: Limited to assigned unit locations
   - UNITHEAD: Limited to assigned unit locations (typically broader than TRAINER)
   - ADMIN: Access to all unit locations in their organization

### Common Service Pattern
Most data table services follow this pattern for role-based filtering:

```csharp
var unitLocationIds = (await _currentUserService.MappedUnitLocationIds()).ToList();
var result = await _repository.GetPaginatedAsync(unitLocationIds, pageNumber, pageSize);
```

This ensures consistent authorization across all endpoints.
