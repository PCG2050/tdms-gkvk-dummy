# Dynamic Reporting System Design

## Overview

This document describes the design for a dynamic reporting system that can generate unified reports across all 11 organizational units in the TDMS GKVK system.

---

## Design Goals

1. **Unified API**: Single endpoint to query data across all units
2. **Flexibility**: Support filtering, aggregation, and grouping
3. **Performance**: Efficient queries with pagination
4. **Type Safety**: Strongly-typed responses
5. **Extensibility**: Easy to add new report types
6. **Role-Based Access**: Respect existing authorization patterns

---

## Architecture

### High-Level Design

```
┌─────────────────────────────────────────────────────────────┐
│                     API Layer                                │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  DynamicReportController                             │   │
│  │  - GET /api/dynamic-report/units/{unitId}           │   │
│  │  - GET /api/dynamic-report/multi-unit               │   │
│  │  - GET /api/dynamic-report/aggregated               │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                   Service Layer                              │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  IDynamicReportService                               │   │
│  │  - GetUnitReport()                                   │   │
│  │  - GetMultiUnitReport()                              │   │
│  │  - GetAggregatedReport()                             │   │
│  │  - GetReportMetadata()                               │   │
│  └──────────────────────────────────────────────────────┘   │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  ReportQueryBuilder                                  │   │
│  │  - BuildQuery()                                      │   │
│  │  - ApplyFilters()                                    │   │
│  │  - ApplyAggregations()                               │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                              ↓
┌─────────────────────────────────────────────────────────────┐
│                Repository Layer                              │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  Unit-Specific Repositories (Existing)               │   │
│  │  - FtiProgramDetailsRepository                       │   │
│  │  - StuProgramDetailsRepository                       │   │
│  │  - ... (for each unit)                               │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

---

## Data Models

### Request Models

#### DynamicReportRequest
```csharp
public class DynamicReportRequest
{
    // Unit selection
    public int? UnitId { get; set; }
    public List<int>? UnitIds { get; set; }

    // Date range
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    // Location filtering
    public List<int>? UnitLocationIds { get; set; }
    public int? OrganizationId { get; set; }

    // Status filtering
    public List<string>? FormStatuses { get; set; }

    // Field selection
    public List<string>? Fields { get; set; }
    public ReportEntityType? EntityType { get; set; }

    // Aggregation
    public List<AggregationConfig>? Aggregations { get; set; }
    public List<string>? GroupBy { get; set; }

    // Pagination
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;

    // Sorting
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}

public enum ReportEntityType
{
    ProgramDetails,
    Report,
    ParticipantDemographics,
    AdvisoryServices,
    TrainingProgrammes,
    Results,
    OtherActivities,
    Sales,           // ATIC only
    VisitorDetails   // ASM only
}

public enum SortDirection
{
    Ascending,
    Descending
}

public class AggregationConfig
{
    public string Field { get; set; }
    public AggregationType Type { get; set; }
    public string? Alias { get; set; }
}

public enum AggregationType
{
    Count,
    Sum,
    Average,
    Min,
    Max,
    CountDistinct
}
```

### Response Models

#### DynamicReportResponse<T>
```csharp
public class DynamicReportResponse<T>
{
    public ReportMetadata Metadata { get; set; }
    public List<T> Data { get; set; }
    public PaginationInfo Pagination { get; set; }
    public Dictionary<string, object>? Aggregations { get; set; }
}

public class ReportMetadata
{
    public List<UnitInfo> Units { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public DateTimeOffset GeneratedAt { get; set; }
    public string GeneratedBy { get; set; }
    public ReportEntityType EntityType { get; set; }
}

public class UnitInfo
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int RecordCount { get; set; }
}

public class PaginationInfo
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalRecords { get; set; }
    public int TotalPages { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
}
```

#### Unified DTO Models

```csharp
public class UnifiedProgramDetailsDto
{
    // Unit identification
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int Id { get; set; }

    // Common fields from ReportEntryBaseEntity
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int UnitLocationId { get; set; }
    public string UnitLocationName { get; set; }
    public int OrganizationId { get; set; }
    public string OrganizationName { get; set; }
    public string FormStatus { get; set; }
    public string? FormStatusRemarks { get; set; }
    public DateTimeOffset? ApprovedAt { get; set; }
    public string? ApprovedByName { get; set; }

    // Common program fields
    public string? Title { get; set; }
    public string? ProgramType { get; set; }
    public string? Category { get; set; }
    public string? Theme { get; set; }
    public string? ThematicArea { get; set; }
    public string? Mode { get; set; }
    public string? Duration { get; set; }
    public string? Location { get; set; }
    public string? Region { get; set; }
    public string? Status { get; set; }
    public decimal? TotalOutlayRs { get; set; }

    // Funding details
    public string? SourceOfFund { get; set; }
    public double? FundAmount { get; set; }
    public DateOnly? FundReleaseDate { get; set; }

    // Audit fields
    public DateTimeOffset CreatedAt { get; set; }
    public string? CreatedByName { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    // Unit-specific fields (optional, populated based on unit)
    public Dictionary<string, object>? AdditionalFields { get; set; }

    // Aggregated child data
    public int? TotalParticipants { get; set; }
    public int? MaleParticipants { get; set; }
    public int? FemaleParticipants { get; set; }
}

public class UnifiedParticipantDemographicsDto
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int ProgramId { get; set; }
    public string? ProgramTitle { get; set; }
    public string? ParticipantCategory { get; set; }

    // Demographics
    public int Male_SC { get; set; }
    public int Male_ST { get; set; }
    public int Male_OBC { get; set; }
    public int Male_GEN { get; set; }
    public int Female_SC { get; set; }
    public int Female_ST { get; set; }
    public int Female_OBC { get; set; }
    public int Female_GEN { get; set; }

    // Hostel data (if applicable)
    public int? SC_Male_StayedInHostel { get; set; }
    public int? ST_Male_StayedInHostel { get; set; }
    public int? OBC_Male_StayedInHostel { get; set; }
    public int? GEN_Male_StayedInHostel { get; set; }
    public int? SC_Female_StayedInHostel { get; set; }
    public int? ST_Female_StayedInHostel { get; set; }
    public int? OBC_Female_StayedInHostel { get; set; }
    public int? GEN_Female_StayedInHostel { get; set; }

    // Totals
    public int TotalMale { get; set; }
    public int TotalFemale { get; set; }
    public int Total { get; set; }
}

public class UnifiedAdvisoryServicesDto
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int ProgramId { get; set; }
    public string? ProgramTitle { get; set; }

    public int NoOfFacebookSMS { get; set; }
    public int NoOfSMSSentToRegisteredFarmers { get; set; }
    public int NoOfWhatsappGroups { get; set; }
    public int NoOfWhatsappSMS { get; set; }
    public int NoOfAnsweredWhatsappQueries { get; set; }
    public int NoOfPhoneCalls { get; set; }
    public int NoOfFaceToFaceDiscussions { get; set; }
    public int NoOfGroupDiscussions { get; set; }
    public int NoOfEmailsSent { get; set; }
    public int NoOfNewspaperCoverage { get; set; }
    public int NoOfBeneficiaries { get; set; }

    public int TotalContacts { get; set; }
}

public class AggregatedReportDto
{
    public string GroupByValue { get; set; }
    public int RecordCount { get; set; }
    public Dictionary<string, object> Metrics { get; set; }
}
```

---

## API Endpoints

### 1. Get Unit-Specific Report

**Endpoint:** `GET /api/dynamic-report/units/{unitId}`

**Description:** Get a report for a specific unit with filtering and pagination.

**Query Parameters:**
- All fields from `DynamicReportRequest`

**Response:**
```json
{
  "metadata": {
    "units": [
      {
        "unitId": 1,
        "unitName": "Farmers Training Institute",
        "recordCount": 150
      }
    ],
    "startDate": "2024-01-01",
    "endDate": "2024-12-31",
    "generatedAt": "2025-12-16T10:30:00Z",
    "generatedBy": "John Doe",
    "entityType": "ProgramDetails"
  },
  "data": [
    {
      "unitId": 1,
      "unitName": "Farmers Training Institute",
      "id": 123,
      "title": "Organic Farming Training",
      "startDate": "2024-03-15",
      "endDate": "2024-03-18",
      "formStatus": "Approved",
      "totalParticipants": 45,
      "maleParticipants": 30,
      "femaleParticipants": 15
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 50,
    "totalRecords": 150,
    "totalPages": 3,
    "hasPrevious": false,
    "hasNext": true
  },
  "aggregations": {
    "totalPrograms": 150,
    "totalParticipants": 6750,
    "averageParticipantsPerProgram": 45
  }
}
```

### 2. Get Multi-Unit Report

**Endpoint:** `GET /api/dynamic-report/multi-unit`

**Description:** Get a combined report across multiple units.

**Query Parameters:**
- `unitIds` - Comma-separated list of unit IDs
- All other fields from `DynamicReportRequest`

**Example Request:**
```
GET /api/dynamic-report/multi-unit?unitIds=1,2,10&startDate=2024-01-01&endDate=2024-12-31&formStatus=Approved
```

### 3. Get Aggregated Report

**Endpoint:** `GET /api/dynamic-report/aggregated`

**Description:** Get aggregated metrics across units.

**Query Parameters:**
- `groupBy` - Fields to group by (e.g., "unitId", "formStatus", "month")
- `aggregations` - Metrics to calculate
- All filter fields from `DynamicReportRequest`

**Example Request:**
```
GET /api/dynamic-report/aggregated?groupBy=unitId,formStatus&aggregations=count,sum:totalParticipants
```

**Response:**
```json
{
  "metadata": { ... },
  "data": [
    {
      "groupByValue": "FTI - Approved",
      "recordCount": 120,
      "metrics": {
        "totalParticipants": 5400,
        "totalPrograms": 120,
        "averageParticipants": 45
      }
    },
    {
      "groupByValue": "STU - Approved",
      "recordCount": 85,
      "metrics": {
        "totalParticipants": 2550,
        "totalPrograms": 85,
        "averageParticipants": 30
      }
    }
  ]
}
```

### 4. Get Report Metadata

**Endpoint:** `GET /api/dynamic-report/metadata`

**Description:** Get metadata about available report fields and options for each unit.

**Response:**
```json
{
  "units": [
    {
      "unitId": 1,
      "unitName": "Farmers Training Institute",
      "availableEntities": [
        "ProgramDetails",
        "Report",
        "ParticipantDemographics",
        "AdvisoryServices"
      ],
      "commonFields": [
        "title",
        "startDate",
        "endDate",
        "formStatus"
      ],
      "unitSpecificFields": [
        "tpNo",
        "batchNo"
      ]
    }
  ],
  "commonFilters": [
    "startDate",
    "endDate",
    "formStatus",
    "unitLocationId"
  ],
  "formStatuses": ["Draft", "Pending", "Approved", "Rejected"],
  "aggregationTypes": ["Count", "Sum", "Average", "Min", "Max"]
}
```

---

## Unit Configuration

### UnitReportConfiguration

Define configuration for each unit to handle their unique structures:

```csharp
public class UnitReportConfiguration
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public string EntityNamespace { get; set; }
    public Type ProgramDetailsType { get; set; }
    public Type? ReportType { get; set; }
    public Type? ParticipantDemographicsType { get; set; }
    public Type? AdvisoryServicesType { get; set; }
    public List<string> UniqueFields { get; set; }
    public bool HasResults { get; set; }  // For EEU & KVK
    public bool IsSalesUnit { get; set; }  // For ATIC
    public bool IsVisitorUnit { get; set; }  // For ASM
    public bool IsActivityUnit { get; set; }  // For FIU
}

public static class UnitConfigurations
{
    private static readonly Dictionary<int, UnitReportConfiguration> _configs = new()
    {
        {
            UnitConstants.FTI_UNIT_ID,
            new UnitReportConfiguration
            {
                UnitId = 1,
                UnitName = "Farmers Training Institute",
                EntityNamespace = "Domain.Entities.FTI",
                ProgramDetailsType = typeof(FtiProgramDetails),
                ReportType = typeof(FtiReport),
                ParticipantDemographicsType = typeof(FtiParticipantDemographics),
                AdvisoryServicesType = typeof(FtiAdvisoryServices),
                UniqueFields = new List<string> { "TPNo", "BatchNo" },
                HasResults = false,
                IsSalesUnit = false,
                IsVisitorUnit = false,
                IsActivityUnit = false
            }
        },
        // ... configurations for all 11 units
    };

    public static UnitReportConfiguration GetConfiguration(int unitId)
    {
        return _configs.TryGetValue(unitId, out var config)
            ? config
            : throw new ArgumentException($"No configuration for unit {unitId}");
    }
}
```

---

## Query Builder Design

### Dynamic Query Construction

```csharp
public interface IReportQueryBuilder
{
    IQueryable<T> BuildQuery<T>(
        DbContext context,
        DynamicReportRequest request,
        UnitReportConfiguration unitConfig
    ) where T : class;

    IQueryable<T> ApplyFilters<T>(
        IQueryable<T> query,
        DynamicReportRequest request
    ) where T : class;

    IQueryable<T> ApplySorting<T>(
        IQueryable<T> query,
        string sortBy,
        SortDirection direction
    ) where T : class;

    IQueryable<T> ApplyPagination<T>(
        IQueryable<T> query,
        int pageNumber,
        int pageSize
    ) where T : class;
}

public class ReportQueryBuilder : IReportQueryBuilder
{
    public IQueryable<T> BuildQuery<T>(
        DbContext context,
        DynamicReportRequest request,
        UnitReportConfiguration unitConfig
    ) where T : class
    {
        var query = context.Set<T>().AsQueryable();

        // Apply filters
        query = ApplyFilters(query, request);

        // Apply sorting
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            query = ApplySorting(query, request.SortBy, request.SortDirection);
        }

        return query;
    }

    public IQueryable<T> ApplyFilters<T>(
        IQueryable<T> query,
        DynamicReportRequest request
    ) where T : class
    {
        // Date range filtering (if entity has StartDate/EndDate)
        if (request.StartDate.HasValue)
        {
            query = query.Where(e =>
                EF.Property<DateOnly>(e, "StartDate") >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(e =>
                EF.Property<DateOnly>(e, "EndDate") <= request.EndDate.Value);
        }

        // Unit location filtering
        if (request.UnitLocationIds?.Any() == true)
        {
            query = query.Where(e =>
                request.UnitLocationIds.Contains(EF.Property<int>(e, "UnitLocationId")));
        }

        // Organization filtering
        if (request.OrganizationId.HasValue)
        {
            query = query.Where(e =>
                EF.Property<int>(e, "OrganizationId") == request.OrganizationId.Value);
        }

        // Status filtering
        if (request.FormStatuses?.Any() == true)
        {
            query = query.Where(e =>
                request.FormStatuses.Contains(EF.Property<string>(e, "FormStatus")));
        }

        return query;
    }

    // Implementation of ApplySorting and ApplyPagination...
}
```

---

## Service Layer Implementation

### IDynamicReportService

```csharp
public interface IDynamicReportService
{
    Task<DynamicReportResponse<UnifiedProgramDetailsDto>> GetUnitReportAsync(
        int unitId,
        DynamicReportRequest request
    );

    Task<DynamicReportResponse<UnifiedProgramDetailsDto>> GetMultiUnitReportAsync(
        DynamicReportRequest request
    );

    Task<DynamicReportResponse<AggregatedReportDto>> GetAggregatedReportAsync(
        DynamicReportRequest request
    );

    Task<DynamicReportResponse<UnifiedParticipantDemographicsDto>> GetParticipantReportAsync(
        DynamicReportRequest request
    );

    Task<DynamicReportResponse<UnifiedAdvisoryServicesDto>> GetAdvisoryServicesReportAsync(
        DynamicReportRequest request
    );

    Task<ReportMetadataResponse> GetReportMetadataAsync();
}
```

---

## Security & Authorization

### Role-Based Access Control

Follow existing pattern from CLAUDE.md:

```csharp
[Authorize(Roles = $"{RoleString.Trainer},{RoleString.UnitHead},{RoleString.Admin}")]
public class DynamicReportController : ControllerBase
{
    private readonly IDynamicReportService _reportService;
    private readonly ICurrentUserService _currentUserService;

    private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
    {
        if (_currentUserService.Role == Role.TRAINER)
            return await _trainerAssignmentRepository
                .GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);

        if (_currentUserService.Role == Role.UNITHEAD)
            return await _unitHeadAssignmentRepository
                .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

        if (_currentUserService.Role == Role.ADMIN)
            return await _organizationUnitRepository
                .GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);

        return new List<int>();
    }
}
```

---

## Performance Considerations

### 1. Query Optimization
- Use `.AsNoTracking()` for read-only queries
- Project to DTOs early to reduce data transfer
- Use appropriate indexes on filter fields

### 2. Caching Strategy
```csharp
public class CachedDynamicReportService : IDynamicReportService
{
    private readonly IMemoryCache _cache;
    private readonly IDynamicReportService _innerService;

    // Cache metadata for 1 hour
    public async Task<ReportMetadataResponse> GetReportMetadataAsync()
    {
        return await _cache.GetOrCreateAsync(
            "report_metadata",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await _innerService.GetReportMetadataAsync();
            }
        );
    }
}
```

### 3. Pagination
- Always enforce maximum page size (e.g., 1000 records)
- Use cursor-based pagination for large datasets
- Return total count separately to avoid COUNT(*) on every request

---

## Export Capabilities

### Additional Endpoints for Export

```csharp
// Export to Excel
[HttpPost("export/excel")]
public async Task<IActionResult> ExportToExcel(DynamicReportRequest request)
{
    var data = await _reportService.GetMultiUnitReportAsync(request);
    var excelBytes = _excelExportService.GenerateExcel(data);
    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        $"report_{DateTime.Now:yyyyMMdd}.xlsx");
}

// Export to PDF
[HttpPost("export/pdf")]
public async Task<IActionResult> ExportToPdf(DynamicReportRequest request)
{
    var data = await _reportService.GetMultiUnitReportAsync(request);
    var pdfBytes = _pdfExportService.GeneratePdf(data);
    return File(pdfBytes, "application/pdf", $"report_{DateTime.Now:yyyyMMdd}.pdf");
}

// Export to CSV
[HttpPost("export/csv")]
public async Task<IActionResult> ExportToCsv(DynamicReportRequest request)
{
    var data = await _reportService.GetMultiUnitReportAsync(request);
    var csvContent = _csvExportService.GenerateCsv(data);
    return File(Encoding.UTF8.GetBytes(csvContent), "text/csv",
        $"report_{DateTime.Now:yyyyMMdd}.csv");
}
```

---

## Implementation Phases

### Phase 1: Foundation (Week 1-2)
- [ ] Create base DTOs and request/response models
- [ ] Implement unit configuration system
- [ ] Create query builder foundation
- [ ] Set up dynamic report service interface

### Phase 2: Core Reporting (Week 3-4)
- [ ] Implement single-unit reporting
- [ ] Implement multi-unit reporting
- [ ] Add filtering and sorting
- [ ] Add pagination
- [ ] Implement authorization checks

### Phase 3: Aggregation (Week 5)
- [ ] Implement aggregation engine
- [ ] Add group-by functionality
- [ ] Create aggregated report DTOs
- [ ] Add metadata endpoint

### Phase 4: Advanced Features (Week 6)
- [ ] Add export capabilities (Excel, PDF, CSV)
- [ ] Implement caching
- [ ] Add query performance monitoring
- [ ] Create comprehensive API documentation

### Phase 5: Testing & Optimization (Week 7-8)
- [ ] Unit tests for query builder
- [ ] Integration tests for all endpoints
- [ ] Performance testing with large datasets
- [ ] Query optimization based on profiling
- [ ] Documentation and examples

---

## Example Usage Scenarios

### Scenario 1: Get All Approved Programs for FTI in 2024

**Request:**
```http
GET /api/dynamic-report/units/1?startDate=2024-01-01&endDate=2024-12-31&formStatus=Approved&pageSize=100
```

### Scenario 2: Compare Participant Counts Across All Training Units

**Request:**
```http
GET /api/dynamic-report/aggregated?unitIds=1,2,4,6&entityType=ParticipantDemographics&groupBy=unitId&aggregations=sum:total
```

### Scenario 3: Get Monthly Report for All Units

**Request:**
```http
GET /api/dynamic-report/aggregated?groupBy=month,unitId&aggregations=count,sum:totalParticipants&startDate=2024-01-01&endDate=2024-12-31
```

### Scenario 4: Get Advisory Services Impact Report

**Request:**
```http
GET /api/dynamic-report/multi-unit?entityType=AdvisoryServices&unitIds=1,2,4,5,6,8,9,10,11&aggregations=sum:noOfBeneficiaries
```

---

## Next Steps

1. Review and approve this design
2. Create the necessary DTO files
3. Implement the query builder
4. Create the service layer
5. Build the controller endpoints
6. Add comprehensive testing
7. Deploy and monitor

---

## References

- Unit Models Documentation: `UNIT_MODELS_DOCUMENTATION.md`
- Authorization Pattern: `CLAUDE.md` (Financial Status Endpoints section)
- Unit Constants: `Application/Constants/UnitConstants.cs`
