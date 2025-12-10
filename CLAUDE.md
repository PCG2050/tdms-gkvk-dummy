# CLAUDE - Development Notes

This document contains important development notes and bug fixes for the TDMS GKVK project.

## Bug Fixes

### Financial Status Endpoints - Forbidden Error (2025-12-10)

**Issue:**
Users with UNITHEAD and ADMIN roles were receiving "Forbidden" errors when accessing financial status endpoints in the published website, even though they had proper authorization.

**Root Cause:**
The financial endpoints were experiencing Forbidden errors due to TWO issues:

1. **FinancialBudgetService.cs** was using `_currentUserService.MappedUnitLocationIds()` instead of its own `GetAccessibleUnitLocationIdsAsync()` method (lines 135 and 152)
2. **CurrentUserService.cs** had a broken `MappedUnitLocationIds()` method that only queried the `UnitTrainers` table, which only worked for TRAINER role

This combination caused empty unit location arrays for all roles, leading to permission check failures.

**Affected Endpoints:**
- `GET /api/financial-budget` - Get paginated financial budgets
- `GET /api/financial-budget/status/{status}` - Get financial budgets by status
- `GET /api/financial-budget/{id}` - Get financial budget by ID
- `POST /api/financial-budget` - Create financial budget
- `PUT /api/financial-budget/{id}` - Update financial budget
- All other financial budget endpoints that use `MappedUnitLocationIds()`

**Solution:**
Applied two fixes to resolve the authorization issues:

**Fix 1: FinancialBudgetService.cs**
- Changed `GetPaginatedAsync()` to use `GetAccessibleUnitLocationIdsAsync()` instead of `_currentUserService.MappedUnitLocationIds()`
- Changed `GetByStatusAsync()` to use `GetAccessibleUnitLocationIdsAsync()` instead of `_currentUserService.MappedUnitLocationIds()`
- This follows the same pattern as other working services (TblService, PublicationService, KvkProgramService)

**Fix 2: CurrentUserService.cs** (for future-proofing)
- Updated `MappedUnitLocationIds()` method to handle all three user roles:
  1. **TRAINER role:** Queries `TrainerAssignment` table via `ITrainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync()`
  2. **UNITHEAD role:** Queries `UnitHeadAssignment` table via `IUnitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync()`
  3. **ADMIN role:** Queries all unit locations in the organization via `IOrganizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync()`
- Added repository dependencies: `ITrainerAssignmentRepository`, `IUnitHeadAssignmentRepository`, `IOrganizationUnitRepository`

**Modified Files:**
1. `Infracture/Services/DataTables/FinancialBudgetService.cs`
   - Line 135: Changed to use `GetAccessibleUnitLocationIdsAsync()`
   - Line 152: Changed to use `GetAccessibleUnitLocationIdsAsync()`

2. `Infracture/Services/CurrentUserService.cs`
   - Added repository dependencies in constructor
   - Refactored `MappedUnitLocationIds()` method to handle all roles

**Code Changes:**

**Change 1: FinancialBudgetService.cs**

```csharp
// BEFORE (Lines 135 and 152 - Using CurrentUserService)
var unitLocationIds = (await _currentUserService.MappedUnitLocationIds()).ToList();

// AFTER (Using service's own helper method - same as TblService, Publication, KVK)
var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
```

The `GetAccessibleUnitLocationIdsAsync()` method already existed in FinancialBudgetService (lines 709-721) and follows the correct pattern:

```csharp
private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
{
    if (_currentUserService.Role == Role.TRAINER)
        return await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);

    if (_currentUserService.Role == Role.UNITHEAD)
        return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

    if (_currentUserService.Role == Role.ADMIN)
        return await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);

    return new List<int>();
}
```

**Change 2: CurrentUserService.cs** (for future-proofing)

```csharp
// BEFORE (Only worked for TRAINER role)
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

// AFTER (Works for all roles: TRAINER, UNITHEAD, ADMIN)
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
This fix ensures that all user roles (TRAINER, UNITHEAD, ADMIN) can properly access financial endpoints based on their assigned unit locations or organization scope.

**Key Insight:**
The correct pattern used throughout the application (TblService, PublicationService, KvkProgramService) is for each service to:
1. Inject repositories directly (`ITrainerAssignmentRepository`, `IUnitHeadAssignmentRepository`, `IOrganizationUnitRepository`)
2. Implement its own private `GetAccessibleUnitLocationIdsAsync()` helper method
3. **NOT** rely on `_currentUserService.MappedUnitLocationIds()`

This ensures reliable role-based access control without dependencies on CurrentUserService.

**Additional Fix - History Endpoint (2025-12-10):**

The `/api/financial-budget/my-history` endpoint was failing because it included unnecessary navigation properties in the query:
```csharp
// BEFORE (Caused errors)
var query = _repository.GetQueryable()
    .Include(x => x.UnitLocation)
        .ThenInclude(ul => ul.Unit);

// AFTER (Fixed - follows same pattern as TblService, PublicationService)
var query = _repository.GetQueryable();
```

The `GenericTrainerHistoryService` only needs access to scalar properties (UnitLocationId, FormStatus, etc.), not loaded navigation properties. Following the pattern used by other working services resolved the issue.

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
