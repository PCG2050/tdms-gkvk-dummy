# Generic Clean Architecture Framework for TDMS-GKVK

## Overview

This document describes the generic clean architecture framework implemented for the TDMS-GKVK project. The framework provides reusable base classes and interfaces that eliminate code duplication across similar modules (STU, FTI, IBTVA, EEU, DEU, ATIC, NAEP).

## Architecture Layers

### 1. Domain Layer
- **Location**: `/Domain/Entities/`
- **Base Entities**:
  - `AuditableBaseEntity` - Provides audit fields (CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
  - `ReportEntryBaseEntity` - Extends AuditableBaseEntity with workflow and multi-tenancy features

### 2. Application Layer
- **Location**: `/Application/Application/`
- **Contains**: Interfaces, DTOs, Mappers
- **Key Components**:
  - DTO marker interfaces (`IBaseDto`, `ICreateDto`, `IUpdateDto`, `ICompleteDto`)
  - Generic repository interfaces
  - Generic service interfaces
  - Generic mapper interface

### 3. Infrastructure Layer
- **Location**: `/Application/Infracture/`
- **Contains**: Repository and Service implementations
- **Key Components**:
  - Generic repository base class
  - Generic service base class

### 4. Presentation Layer
- **Location**: `/WebApi/Controllers/`
- **Contains**: API Controllers

---

## Core Components

### 1. Generic DTO Marker Interfaces

**Location**: `/Application/Application/Interface/IBaseDto.cs`

```csharp
public interface IBaseDto
{
    int Id { get; set; }
}

public interface ICreateDto { }

public interface IUpdateDto
{
    int Id { get; set; }
}

public interface ICompleteDto : IBaseDto { }
```

**Purpose**: Enable generic type constraints for DTOs

**Usage**: Implement these interfaces in your module DTOs:
```csharp
public class StuProgramDetailsDto : IBaseDto { }
public class StuProgramCreateDto : ICreateDto { }
public class StuProgramUpdateDto : IUpdateDto { }
public class StuProgramDetailsCompleteDto : StuProgramDetailsDto, ICompleteDto { }
```

---

### 2. Generic Repository Interface

**Location**: `/Application/Application/Interface/Repository/Common/IBaseReportEntryRepository.cs`

**Methods**:
- `GetByIdAsync(int id)` - Get entity by ID (simple query)
- `GetWithDetailsAsync(int id)` - Get entity with navigation properties
- `CreateAsync(TEntity entity)` - Create new entity
- `UpdateAsync(TEntity entity)` - Update existing entity
- `DeleteAsync(int id)` - Delete entity
- `GetPaginatedAsync(...)` - Get paginated results with filters
- `GetByStatusAsync(...)` - Get records by FormStatus
- `GetStatusSummaryAsync(...)` - Get count by status

**Type Constraint**: `where TEntity : ReportEntryBaseEntity`

---

### 3. Generic Repository Base Class

**Location**: `/Application/Infracture/Repository/Common/BaseReportEntryRepository.cs`

**Features**:
- Implements all common CRUD operations
- Provides pagination with filtering
- Supports eager loading with navigation properties
- Implements status-based queries

**Override Points** (Protected virtual methods):

1. **ApplyCommonIncludes** - Common navigation properties (UnitLocation, Organization, Users)
   ```csharp
   protected virtual IQueryable<TEntity> ApplyCommonIncludes(IQueryable<TEntity> query)
   {
       return query
           .Include(e => e.UnitLocation).ThenInclude(ul => ul.Unit)
           .Include(e => e.Organization)
           .Include(e => e.CreatedBy)
           .Include(e => e.UpdatedBy)
           .Include(e => e.ApprovedBy);
   }
   ```

2. **ApplyEntityIncludes** - Entity-specific navigation properties
   ```csharp
   protected override IQueryable<StuProgramDetails> ApplyEntityIncludes(IQueryable<StuProgramDetails> query)
   {
       return query
           .Include(p => p.ProgramType)
           .Include(p => p.Category)
           .Include(p => p.ParticipantDemographics);
   }
   ```

3. **ApplySearchFilter** - Entity-specific search logic
   ```csharp
   protected override IQueryable<StuProgramDetails> ApplySearchFilter(
       IQueryable<StuProgramDetails> query, string searchTerm)
   {
       return query.Where(p =>
           p.Title.ToLower().Contains(searchTerm.ToLower()) ||
           p.Location.ToLower().Contains(searchTerm.ToLower()));
   }
   ```

4. **ApplyAdditionalFilters** - Custom filters (e.g., by program type)
   ```csharp
   protected override IQueryable<StuProgramDetails> ApplyAdditionalFilters(IQueryable<StuProgramDetails> query)
   {
       // Add custom filters if needed
       return query;
   }
   ```

---

### 4. Generic Mapper Interface

**Location**: `/Application/Application/Interface/Mappers/IBaseMapper.cs`

**Methods**:
- `MapToEntity(TCreateDto dto)` - Maps CreateDto to Entity
- `MapToDtoWithDetails(TEntity entity)` - Maps Entity to DTO (with navigation properties)
- `MapUpdateDtoToEntity(TUpdateDto dto, TEntity entity)` - Partial update mapping

**Type Constraints**:
```csharp
where TEntity : ReportEntryBaseEntity
where TDto : IBaseDto
where TCreateDto : ICreateDto
where TUpdateDto : IUpdateDto
```

**Usage with Mapperly**:
```csharp
[Mapper]
public partial class StuProgramMapper :
    IBaseMapper<StuProgramDetails, StuProgramDetailsDto, StuProgramCreateDto, StuProgramUpdateDto>
{
    public partial StuProgramDetails MapToEntity(StuProgramCreateDto dto);
    public partial StuProgramDetailsDto MapToDtoWithDetails(StuProgramDetails entity);
    public void MapUpdateDtoToEntity(StuProgramUpdateDto dto, StuProgramDetails entity) { /* manual */ }
}
```

---

### 5. Generic Service Interface

**Location**: `/Application/Application/Interface/Services/Common/IBaseReportEntryService.cs`

**Methods**:

**CRUD Operations**:
- `CreateAsync(TCreateDto dto)` - Create new record (Draft status)
- `GetByIdAsync(int id)` - Get by ID with permission check
- `UpdateAsync(int id, TUpdateDto dto)` - Update (Draft only)
- `DeleteAsync(int id)` - Delete (Draft only)

**Workflow Operations**:
- `SubmitForApprovalAsync(int id)` - Draft → Pending
- `ApproveAsync(int id, string? remarks)` - Pending → Approved
- `RejectAsync(int id, string remarks)` - Pending → Rejected

**Queries**:
- `GetPaginatedAsync(...)` - Paginated results with filters
- `GetByStatusAsync(string status, ...)` - Filter by status
- `GetStatusSummaryAsync()` - Count by status

---

### 6. Generic Service Base Class

**Location**: `/Application/Infracture/Services/Common/BaseReportEntryService.cs`

**Features**:
- Implements all CRUD operations with permission checks
- Manages workflow state transitions (Draft → Pending → Approved/Rejected)
- Provides pagination and filtering
- Automatic audit field management (CreatedBy, UpdatedAt, etc.)

**Dependencies** (Injected via constructor):
- `IBaseReportEntryRepository<TEntity>` - Repository
- `ICurrentUserService` - Current user context
- `IEntityPermissionService` - Permission checks
- `IBaseMapper<TEntity, TDto, TCreateDto, TUpdateDto>` - DTO mapping

**Override Points** (Protected virtual methods):

1. **ValidateCreate** - Custom validation before creating
   ```csharp
   protected override async Task<ServiceResult> ValidateCreate(StuProgramDetails entity, StuProgramCreateDto dto)
   {
       if (string.IsNullOrEmpty(entity.Title))
           return ServiceResult.Failure("Title is required", ServiceErrorStatus.BADREQUEST);
       return ServiceResult.Success();
   }
   ```

2. **ValidateUpdate** - Custom validation before updating
   ```csharp
   protected override async Task<ServiceResult> ValidateUpdate(StuProgramDetails entity, StuProgramUpdateDto dto)
   {
       // Add validation logic
       return ServiceResult.Success();
   }
   ```

3. **ValidateSubmission** - Validation before submitting for approval
   ```csharp
   protected override async Task<ServiceResult> ValidateSubmission(StuProgramDetails entity)
   {
       if (!entity.ParticipantDemographics.Any())
           return ServiceResult.Failure("At least one demographic entry required", ServiceErrorStatus.BADREQUEST);
       return ServiceResult.Success();
   }
   ```

4. **GetAccessibleUnitLocationIdsAsync** - Custom access control
   ```csharp
   protected override async Task<List<int>> GetAccessibleUnitLocationIdsAsync(int? specificUnitLocationId)
   {
       // Custom logic for determining accessible unit locations
       return await base.GetAccessibleUnitLocationIdsAsync(specificUnitLocationId);
   }
   ```

5. **CanUserAccessUnitLocationAsync** - Check unit location access
   ```csharp
   protected override async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
   {
       // Custom access logic
       return await base.CanUserAccessUnitLocationAsync(unitLocationId);
   }
   ```

---

## Implementation Guide

### Step 1: Create Entity

Your entity should inherit from `ReportEntryBaseEntity`:

```csharp
namespace Domain.Entities.STU
{
    public class StuProgramDetails : ReportEntryBaseEntity
    {
        public string? Title { get; set; }
        public int? ProgramTypeId { get; set; }

        // Navigation properties
        public ProgramType? ProgramType { get; set; }
        public ICollection<StuParticipantDemographics>? ParticipantDemographics { get; set; }
    }
}
```

### Step 2: Create DTOs

Implement the marker interfaces:

```csharp
namespace Application.Models.DataTables.STU
{
    public class StuProgramDetailsDto : IBaseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        // ... other properties
    }

    public class StuProgramCreateDto : ICreateDto
    {
        public int UnitLocationId { get; set; }
        public string? Title { get; set; }
        // ... other properties
    }

    public class StuProgramUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        // ... other properties (nullable for partial updates)
    }

    public class StuProgramDetailsCompleteDto : StuProgramDetailsDto, ICompleteDto
    {
        public List<StuParticipantDemographicsDto>? Demographics { get; set; }
        // ... other nested collections
    }
}
```

### Step 3: Create Repository Interface

Extend the generic repository interface:

```csharp
namespace Application.Interface.Repository.DataTables.STU
{
    public interface IStuProgramDetailsRepository : IBaseReportEntryRepository<StuProgramDetails>
    {
        // Add module-specific methods here if needed
        // All standard methods are inherited
    }
}
```

### Step 4: Implement Repository

Extend the base repository and override as needed:

```csharp
namespace Infrastructure.Repository.DataTables.STU
{
    public class StuProgramDetailsRepository :
        BaseReportEntryRepository<StuProgramDetails>,
        IStuProgramDetailsRepository
    {
        public StuProgramDetailsRepository(TdmsDbContext context) : base(context)
        {
        }

        // Override to add entity-specific navigation properties
        protected override IQueryable<StuProgramDetails> ApplyEntityIncludes(IQueryable<StuProgramDetails> query)
        {
            return query
                .Include(p => p.ProgramType)
                .Include(p => p.Category)
                .Include(p => p.ParticipantDemographics);
        }

        // Override to add search logic
        protected override IQueryable<StuProgramDetails> ApplySearchFilter(
            IQueryable<StuProgramDetails> query, string searchTerm)
        {
            var lowerSearch = searchTerm.ToLower();
            return query.Where(p =>
                p.Title != null && p.Title.ToLower().Contains(lowerSearch) ||
                p.Location != null && p.Location.ToLower().Contains(lowerSearch));
        }
    }
}
```

### Step 5: Create Mapper

Implement the generic mapper interface (using Mapperly):

```csharp
namespace Application.Mapper.DataTable.STU
{
    [Mapper]
    public partial class StuProgramMapper :
        IBaseMapper<StuProgramDetails, StuProgramDetailsDto, StuProgramCreateDto, StuProgramUpdateDto>
    {
        // Mapperly will generate these
        public partial StuProgramDetails MapToEntity(StuProgramCreateDto dto);
        public partial StuProgramDetailsDto MapToDtoWithDetails(StuProgramDetails entity);

        // Manual implementation for partial updates
        public void MapUpdateDtoToEntity(StuProgramUpdateDto dto, StuProgramDetails entity)
        {
            if (dto.Title != null) entity.Title = dto.Title;
            if (dto.ProgramTypeId.HasValue) entity.ProgramTypeId = dto.ProgramTypeId;
            // ... map other fields only if not null
        }
    }
}
```

### Step 6: Create Service Interface

Extend the generic service interface:

```csharp
namespace Application.Interface.Services.DataTables.STU
{
    public interface IStuProgramService :
        IBaseReportEntryService<StuProgramDetailsDto, StuProgramCreateDto, StuProgramUpdateDto>
    {
        // Add module-specific methods here
        // For example, nested entity operations:
        Task<ServiceResult<StuParticipantDemographicsDto>> AddDemographicsAsync(
            int programId, StuParticipantDemographicsCreateDto dto);
    }
}
```

### Step 7: Implement Service

Extend the base service:

```csharp
namespace Infrastructure.Services.DataTables.STU
{
    public class StuProgramService :
        BaseReportEntryService<StuProgramDetails, StuProgramDetailsDto, StuProgramCreateDto, StuProgramUpdateDto>,
        IStuProgramService
    {
        // Additional dependencies for nested entities
        private readonly IStuParticipantDemographicsRepository _demographicsRepository;

        public StuProgramService(
            IStuProgramDetailsRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            StuProgramMapper mapper,
            IStuParticipantDemographicsRepository demographicsRepository)
            : base(repository, currentUserService, entityPermissionService, mapper)
        {
            _demographicsRepository = demographicsRepository;
        }

        // Override validation methods if needed
        protected override async Task<ServiceResult> ValidateSubmission(StuProgramDetails entity)
        {
            if (string.IsNullOrEmpty(entity.Title))
                return ServiceResult.Failure("Title is required", ServiceErrorStatus.BADREQUEST);

            return ServiceResult.Success();
        }

        // Implement module-specific methods
        public async Task<ServiceResult<StuParticipantDemographicsDto>> AddDemographicsAsync(
            int programId, StuParticipantDemographicsCreateDto dto)
        {
            // Implementation...
        }
    }
}
```

### Step 8: Register in Dependency Injection

Update `Program.cs`:

```csharp
// Repositories
builder.Services.AddScoped<IStuProgramDetailsRepository, StuProgramDetailsRepository>();

// Services
builder.Services.AddScoped<IStuProgramService, StuProgramService>();

// Mappers (Singleton for Mapperly)
builder.Services.AddSingleton<StuProgramMapper>();
```

---

## Benefits

### 1. Code Reduction
- **Before**: ~170 lines per repository
- **After**: ~60 lines per repository (65% reduction)
- **Before**: ~500+ lines per service
- **After**: ~200 lines per service (60% reduction)

### 2. Consistency
- Standardized CRUD operations across all modules
- Consistent permission checking
- Uniform workflow management
- Standardized error handling

### 3. Maintainability
- Bug fixes in one place benefit all modules
- Easy to add new features to all modules
- Clear separation of concerns
- Override points for customization

### 4. Type Safety
- Generic constraints ensure type safety
- Compile-time checking for interface compliance
- IntelliSense support for all methods

---

## Workflow State Machine

```
┌─────────┐
│  Draft  │ (Initial state on create)
└────┬────┘
     │ SubmitForApprovalAsync()
     ▼
┌─────────┐
│ Pending │
└────┬────┘
     │
     ├──► ApproveAsync() ──► ┌──────────┐
     │                        │ Approved │
     │                        └──────────┘
     │
     └──► RejectAsync() ───► ┌──────────┐
                              │ Rejected │
                              └──────────┘

Rules:
- CRUD operations only allowed in Draft state
- Only Draft can transition to Pending
- Only Pending can transition to Approved/Rejected
- Permission checks on all operations
```

---

## Permission Model

The framework integrates with `IEntityPermissionService`:

1. **CanViewForm** - Check if user can view a record
2. **CanModifyForm** - Check if user can edit/delete a record (Draft only)
3. **CanApproveForm** - Check if user has approval permissions
4. **GetAccessibleUnitLocationIdsAsync** - Get unit locations user can access

Multi-tenancy is enforced through:
- `OrganizationId` - Organization the record belongs to
- `UnitLocationId` - Unit location within the organization
- User's accessible unit locations

---

## Testing Strategy

### Unit Tests
Test each override method independently:

```csharp
[Fact]
public async Task ApplySearchFilter_ShouldFilterByTitle()
{
    // Arrange
    var repository = new StuProgramDetailsRepository(_context);
    var query = _context.StuProgramDetails.AsQueryable();

    // Act
    var result = repository.ApplySearchFilter(query, "training");

    // Assert
    var items = await result.ToListAsync();
    Assert.All(items, item => Assert.Contains("training", item.Title.ToLower()));
}
```

### Integration Tests
Test the full service layer:

```csharp
[Fact]
public async Task CreateAsync_ShouldCreateWithDraftStatus()
{
    // Arrange
    var dto = new StuProgramCreateDto { Title = "Test", UnitLocationId = 1 };

    // Act
    var result = await _service.CreateAsync(dto);

    // Assert
    Assert.True(result.Succeeded);
    Assert.Equal("Draft", result.Data.FormStatus);
}
```

---

## Future Enhancements

1. **Generic Controller Base Class** - Reduce controller code duplication
2. **Generic Validation Framework** - Reusable validation rules
3. **Generic Export/Import** - Common export to Excel/PDF functionality
4. **Generic Audit Logging** - Centralized change tracking
5. **Generic Caching Layer** - Performance optimization

---

## Migration Path for Other Modules

To migrate FTI, IBTVA, EEU, DEU, ATIC, NAEP:

1. Update DTOs to implement marker interfaces
2. Update repository interfaces to extend `IBaseReportEntryRepository<T>`
3. Update repository implementations to extend `BaseReportEntryRepository<T>`
4. Update mappers to implement `IBaseMapper<T, TDto, TCreateDto, TUpdateDto>`
5. Update service implementations to extend `BaseReportEntryService<T, TDto, TCreateDto, TUpdateDto>`
6. Remove duplicated code
7. Test thoroughly

---

## Support

For questions or issues with the generic framework, contact the development team or refer to:
- STU module implementation (reference implementation)
- This documentation
- Code comments in base classes

---

**Version**: 1.0
**Last Updated**: 2025-11-22
**Author**: Development Team
