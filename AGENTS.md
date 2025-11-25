# TDMS GKVK - Training and Data Management System

ASP.NET Core Web API for managing training programs, organizational units, and user assignments at GKVK (Gandhi Krishi Vigyan Kendra).

## Core Commands

**Note**: `dotnet` CLI is not available in this environment. Use manual migration file creation for database schema changes.

- Build solution: Project builds automatically on save
- Run migrations: Create migration files manually in `Infracture/Migrations/`
- Start API: Handled by WebApi project startup
- Run tests: Test framework not configured (manual testing via API)

## Project Layout

```
├─ Domain/                  → Core entities and enums
│  ├─ Entities/            → Database models (User, UnitHeadAssignment, etc.)
│  └─ Entities/Junction/   → Many-to-many relationship tables
├─ Application/            → Interfaces and DTOs
│  ├─ Interface/          → Service and repository interfaces
│  └─ Models/             → DTOs for API requests/responses
├─ Infracture/            → Data access and services
│  ├─ DbContext/          → Entity Framework DbContext
│  ├─ Repository/         → Repository implementations
│  ├─ Services/           → Business logic services
│  └─ Migrations/         → EF Core migration files
└─ WebApi/                → API controllers and startup
   └─ Controllers/        → REST API endpoints
```

## Development Patterns & Constraints

### Architecture Rules

- **Clean Architecture**: Domain → Application → Infrastructure → WebApi
- **Never reference Infrastructure from Domain**
- **Entity placement**:
  - Core entities in `Domain/Entities/`
  - Junction tables in `Domain/Entities/Junction/`
- **Service placement**:
  - Interfaces in `Application/Interface/`
  - Implementations in `Infracture/Services/`
- **Repository pattern**: All data access through repositories

### Entity Conventions

**All entities inherit from `AuditableBaseEntity`:**
```csharp
public int Id { get; set; }
public int CreatedById { get; set; }
public DateTimeOffset CreatedAt { get; set; }
public int? UpdatedById { get; set; }
public DateTimeOffset? UpdatedAt { get; set; }
```

**Soft delete entities also include:**
```csharp
public bool IsDeactivated { get; set; } = false;
public DateTimeOffset? DeactivatedAt { get; set; }
public int? DeactivatedById { get; set; }
```

**Naming conventions:**
- Foreign keys: `{EntityName}Id` (e.g., `UnitLocationId`, `TrainerId`)
- Navigation properties: Match entity name (e.g., `UnitLocation`, `Trainer`)
- Junction tables: `{Entity1}{Entity2}Assignment` (e.g., `UnitHeadAssignment`)

### Service Layer Patterns

**ServiceResult return types:**
```csharp
// Success
return ServiceResult.Success();
return ServiceResult<T>.Success(data);

// Failure
return ServiceResult.Failure("Error message", ServiceErrorStatus.NOTFOUND);
```

**Common error statuses:**
- `NOTFOUND` - Entity doesn't exist
- `FORBIDDEN` - Permission denied
- `INVALIDOPERATION` - Business rule violation

### Repository Patterns

**Standard repository methods:**
```csharp
Task<T?> GetByIdAsync(int id);
Task<List<T>> GetAllAsync();
Task<T> AddAsync(T entity);
Task<T> SaveAsync(T entity);  // For updates
Task DeleteAsync(T entity);
```

**Soft delete queries MUST filter:**
```csharp
.Where(x => !x.IsDeactivated)  // Always exclude deactivated
```

### DTO and Mapper Patterns

**DTO types:**
- `CreateDto` - For POST requests (no Id)
- `UpdateDto` - For PUT/PATCH requests (includes Id)
- `HybridDto` - Items with Id = update, without = create

**Important**: Mappers only copy DTO fields. Always explicitly set:
- Foreign keys (e.g., `ServiceId`, `UnitLocationId`, `OrganizationId`)
- Audit fields (`CreatedById`, `UpdatedById`, timestamps)

```csharp
// ❌ BAD - Mapper overwrites ServiceId
_mapper.MapUpdateDtoToEntity(dto, existing);

// ✅ GOOD - Explicitly preserve after mapping
_mapper.MapUpdateDtoToEntity(dto, existing);
existing.ServiceId = serviceId;  // Preserve foreign key
```

### Soft Delete Pattern

**When implementing soft delete:**

1. **Add to entity:**
   ```csharp
   public bool IsDeactivated { get; set; } = false;
   public DateTimeOffset? DeactivatedAt { get; set; }
   public int? DeactivatedById { get; set; }
   ```

2. **Update ALL repository query methods** to filter:
   ```csharp
   .Where(x => !x.IsDeactivated)
   ```

3. **Add reactivation support:**
   ```csharp
   Task<T?> GetDeactivatedAssignmentAsync(int id1, int id2);
   ```

4. **Deactivate instead of delete:**
   ```csharp
   entity.IsDeactivated = true;
   entity.DeactivatedAt = DateTimeOffset.UtcNow;
   entity.DeactivatedById = _currentUser.UserId;
   await repository.SaveAsync(entity);
   ```

### Permission Patterns

**Role hierarchy:**
```
SUPERADMIN > ADMIN > UNITHEAD > TRAINER
```

**Permission checks:**
```csharp
// Current user context
var cId = _currentUser.UserId;
var cRole = _currentUser.Role;
var cOrg = _currentUser.OrganizationId;

// Ownership check
if (entity.CreatedById != cId)
    return ServiceResult.Failure("Unauthorized", ServiceErrorStatus.FORBIDDEN);
```

**Form status permissions:**
- Use `IPermissionService.CanModifyForm()` to check edit permissions
- FormStatus affects who can modify (Pending, Approved, Rejected)

### Navigation Property Patterns

**EF Core includes:**
```csharp
// Single level
.Include(x => x.UnitLocation)

// Multiple levels
.Include(x => x.UnitLocation)
    .ThenInclude(l => l.District)
        .ThenInclude(d => d.State)

// Multiple properties
.Include(x => x.ProgramType)
.Include(x => x.Status)
```

**Common mistake**: Missing `.Include()` causes navigation properties to be null.

### JSON Converter Patterns

**Nullable int handling:**
- Empty strings ("") convert to null
- Uses `NullableIntConverter` on DTO properties
- Apply to all nullable int properties in DTOs

### Database Migration Patterns

**Manual migration creation** (dotnet CLI not available):

1. **Name format**: `YYYYMMDDHHMMSS_DescriptiveName.cs`
   ```
   20251125120000_AddSoftDeleteToAssignments.cs
   ```

2. **Structure**:
   ```csharp
   public partial class DescriptiveName : Migration
   {
       protected override void Up(MigrationBuilder migrationBuilder)
       {
           // Add columns, tables, etc.
       }

       protected override void Down(MigrationBuilder migrationBuilder)
       {
           // Reverse the Up changes
       }
   }
   ```

3. **Common operations**:
   ```csharp
   // Add column
   migrationBuilder.AddColumn<bool>(
       name: "IsDeactivated",
       table: "TableName",
       type: "bit",
       nullable: false,
       defaultValue: false);

   // Drop column
   migrationBuilder.DropColumn(
       name: "ColumnName",
       table: "TableName");
   ```

## Git Workflow

### Branch Naming
- Feature branches: `claude/descriptive-name-{sessionId}`
- Example: `claude/fix-patch-user-response-01V5zWssBFVXqTChJBF14upG`
- **IMPORTANT**: Must start with `claude/` and end with session ID or push will fail (403)

### Commit Message Format

**Structure**:
```
Brief summary line (imperative mood)

1. Context/explanation
   - Detailed point 1
   - File: path/to/file.cs:line

2. Implementation details
   - What changed and why
   - File: path/to/file.cs
```

**Example**:
```
Implement soft delete for user assignments and fix email update

1. Fixed email update in UserService.UpdateUserAsync
   - Added email uniqueness check before update
   - Reset email confirmation flag when email changes
   - File: Infracture/Services/UserService.cs

2. Added soft delete to assignment entities
   - Added IsDeactivated, DeactivatedAt, DeactivatedById fields
   - Files: Domain/Entities/Junction/UnitHeadAssignment.cs
           Domain/Entities/Junction/TrainerAssignment.cs
```

### Push Protocol

**Retry logic for network failures:**
```bash
git push -u origin <branch-name>
# If network error: retry up to 4 times with exponential backoff (2s, 4s, 8s, 16s)
```

**Safety rules:**
- ✅ Can force push feature branches: `git push --force-with-lease`
- ❌ Never force push to main/master
- ❌ Never skip hooks (--no-verify)
- ❌ Never update git config

### Pre-Commit Checklist

Before committing:
1. ✅ Read all modified files first
2. ✅ Verify foreign key preservation after mapper calls
3. ✅ Check navigation properties have `.Include()`
4. ✅ Confirm soft delete filters on all queries
5. ✅ Ensure audit fields are set (CreatedById, UpdatedById)

## Common Gotchas

### Mapper Overwrites Foreign Keys
**Problem**: Automapper overwrites foreign keys during updates
```csharp
_mapper.MapUpdateDtoToEntity(dto, existing);
// existing.ServiceId is now 0!
```
**Solution**: Explicitly preserve after mapping
```csharp
_mapper.MapUpdateDtoToEntity(dto, existing);
existing.ServiceId = serviceId;  // ← Add this
```

### Missing Navigation Properties
**Problem**: Navigation property is null even though FK is set
```csharp
var program = await repository.GetByIdAsync(id);
var typeName = program.ProgramType.Name;  // NullReferenceException!
```
**Solution**: Add `.Include()`
```csharp
return await _context.KvkPrograms
    .Include(p => p.ProgramType)  // ← Add this
    .FirstOrDefaultAsync(p => p.Id == id);
```

### Soft Delete Filter Missing
**Problem**: Deactivated items show up in lists/queries
**Solution**: Add filter to ALL query methods
```csharp
public async Task<List<T>> GetAllAsync()
{
    return await _context.Items
        .Where(x => !x.IsDeactivated)  // ← Add this
        .ToListAsync();
}
```

### Hard Delete Loses Audit Trail
**Problem**: Using `DeleteAsync()` permanently removes records
**Solution**: Use soft delete pattern instead
```csharp
// ❌ BAD
await repository.DeleteAsync(entity);

// ✅ GOOD
entity.IsDeactivated = true;
entity.DeactivatedAt = DateTimeOffset.UtcNow;
entity.DeactivatedById = _currentUser.UserId;
await repository.SaveAsync(entity);
```

### Form Status Workflow
**Problem**: Users can't edit forms they should have access to
**Solution**: Check both ownership AND form status
```csharp
var canModify = await _permissionService.CanModifyForm(
    formId,
    formStatus,
    createdById,
    _currentUser.UserId,
    _currentUser.Role
);
```

## Key Domain Concepts

### User Roles & Hierarchy
- **SUPERADMIN**: System-wide access, manages admins
- **ADMIN**: Organization-wide access, manages unit heads/locations
- **UNITHEAD**: Unit-level access, manages trainers and programs
- **TRAINER**: Program-level access, data entry

### Multi-tenancy
- Each user belongs to one `OrganizationId`
- Unit heads and trainers are scoped to organizations
- Cross-organization queries blocked by permission checks

### Assignment Pattern
- **UnitHeadAssignment**: Links unit heads to unit locations
- **TrainerAssignment**: Links trainers to unit locations
- Soft delete preserves historical assignments
- Reactivation supported (check deactivated before creating new)

### Form Lifecycle
1. **Pending**: Editable by creator
2. **Approved**: Read-only, locked
3. **Rejected**: Editable by creator for fixes
4. Status controlled by `CanModifyForm()` permission check

### KVK Programs
- Categories: FLD, OFT (use Results endpoint), others (use Reports endpoint)
- Demographics attached to programs (UnitLocationId/OrganizationId inheritance)
- Advisory services follow same pattern
- Nullable int fields use custom converter

## Security & Authentication

### JWT Configuration
- Token includes: UserId, Role, OrganizationId
- Authorization via `[Authorize(Roles = "...")]`
- Current user accessed via `ICurrentUserService`

### Sensitive Data
- User passwords hashed via `IPasswordHasher`
- Email/phone confirmation flags
- Password reset uses OTP (not tokens)
- Session invalidation on password change

### Rate Limiting
- OTP attempts limited (default: 5 attempts)
- Session validation on each request

## Testing Approach

- **No automated test suite**: Manual API testing required
- **Postman/Swagger**: Use for endpoint verification
- **Test scenarios**:
  1. Happy path (success cases)
  2. Permission checks (403 Forbidden)
  3. Not found cases (404)
  4. Validation errors (400)

## External Dependencies

- **Entity Framework Core**: ORM for SQL Server
- **AutoMapper**: DTO ↔ Entity mapping
- **JWT Authentication**: Token-based auth
- **Email Service**: OTP delivery (password reset)

## Evidence Required for Changes

Every change should include:
1. **Read modified files first** - Never edit without reading
2. **Explicit foreign key preservation** - After mapper calls
3. **Navigation property includes** - For all related data
4. **Soft delete filters** - On all query methods
5. **Permission checks** - For protected operations
6. **Commit with context** - Explain what and why

## Quick Reference

### Adding a New Entity
1. Create in `Domain/Entities/` (inherit `AuditableBaseEntity`)
2. Add DbSet to `TdmsDbContext`
3. Create repository interface in `Application/Interface/Repository/`
4. Implement repository in `Infracture/Repository/`
5. Create service interface in `Application/Interface/`
6. Implement service in `Infracture/Services/`
7. Create DTOs in `Application/Models/`
8. Add controller in `WebApi/Controllers/`
9. Create migration in `Infracture/Migrations/`

### Adding Soft Delete
1. Add fields to entity (IsDeactivated, DeactivatedAt, DeactivatedById)
2. Add `.Where(x => !x.IsDeactivated)` to ALL repository queries
3. Add `GetDeactivatedAssignmentAsync()` for reactivation
4. Add `SaveAsync()` method if missing
5. Update service to deactivate instead of delete
6. Update interface with new methods
7. Create migration for new columns

### Fixing Navigation Property Null
1. Find repository method returning the entity
2. Add `.Include(x => x.NavigationProperty)`
3. For nested: `.ThenInclude(x => x.NestedProperty)`
4. Test the query returns populated data

### Debugging Foreign Key Issues
1. Check if mapper is called before explicit assignment
2. Verify assignment happens AFTER mapper
3. Check entity has property set before SaveAsync
4. Verify database column allows value
