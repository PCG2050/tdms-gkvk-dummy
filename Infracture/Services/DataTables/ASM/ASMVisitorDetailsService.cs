using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables.ASM;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables.ASM;
using Application.Mapper.DataTable.ASM;
using Application.Models;
using Application.Models.DataTables.ASM;
using Domain.Entities.ASM;
using Domain.Entities.Enum;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DataTables.ASM
{
    public class ASMVisitorDetailsService : IASMVisitorDetailsService
    {
        private readonly IASMVisitorDetailsRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly ASMVisitorDetailsMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public ASMVisitorDetailsService(
            IASMVisitorDetailsRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            ASMVisitorDetailsMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
        }

        // ==========================================
        // MAIN CRUD OPERATIONS
        // ==========================================

        public async Task<ServiceResult<ASMVisitorDetailsDto>> AddAsync(ASMVisitorDetailsCreateDto createDto)
        {
            // Verify trainer has access to this unit location
            if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var entity = new ASMVisitorDetails
            {
                UnitLocationId = createDto.UnitLocationId,
                InstituteName = createDto.InstituteName,
                StartDate = createDto.StartDate,
                EndDate = createDto.EndDate,
                FarmersCount = createDto.FarmersCount,
                StudentsCount = createDto.StudentsCount,
                PublicCount = createDto.PublicCount,
                SubmittedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow,
                OrganizationId = _currentUserService.OrganizationId,
                // Auto-approve forms created by Unit Heads
                FormStatus = _currentUserService.Role == Role.UNITHEAD ? "Approved" : "Pending",
                ApprovedById = _currentUserService.Role == Role.UNITHEAD ? _currentUserService.UserId : null,
                ApprovedAt = _currentUserService.Role == Role.UNITHEAD ? DateTimeOffset.UtcNow : null,
                FormStatusRemarks = _currentUserService.Role == Role.UNITHEAD ? "Auto-approved (Unit Head)" : null
            };

            var savedEntity = await _repository.AddAsync(entity);
            var dto = _mapper.MapToDto(savedEntity);

            return ServiceResult<ASMVisitorDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<ASMVisitorDetailsBatchResultDto>> AddBatchAsync(ASMVisitorDetailsBatchCreateDto batchCreateDto)
        {
            var result = new ASMVisitorDetailsBatchResultDto
            {
                TotalProcessed = batchCreateDto.VisitorDetails.Count
            };

            if (batchCreateDto.VisitorDetails == null || !batchCreateDto.VisitorDetails.Any())
            {
                return ServiceResult<ASMVisitorDetailsBatchResultDto>.Failure(
                    "No visitor details provided for batch creation",
                    ServiceErrorStatus.VALIDATIONERROR);
            }

            for (int i = 0; i < batchCreateDto.VisitorDetails.Count; i++)
            {
                var createDto = batchCreateDto.VisitorDetails[i];

                try
                {
                    // Verify trainer has access to this unit location
                    if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                    {
                        result.FailedEntries.Add(new BatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Access denied to unit location",
                            OriginalData = createDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    var entity = new ASMVisitorDetails
                    {
                        UnitLocationId = createDto.UnitLocationId,
                        InstituteName = createDto.InstituteName,
                        StartDate = createDto.StartDate,
                        EndDate = createDto.EndDate,
                        FarmersCount = createDto.FarmersCount,
                        StudentsCount = createDto.StudentsCount,
                        PublicCount = createDto.PublicCount,
                        SubmittedDate = DateOnly.FromDateTime(DateTime.UtcNow),
                        CreatedById = _currentUserService.UserId,
                        CreatedAt = DateTimeOffset.UtcNow,
                        OrganizationId = _currentUserService.OrganizationId,
                        // Auto-approve forms created by Unit Heads
                        FormStatus = _currentUserService.Role == Role.UNITHEAD ? "Approved" : "Pending",
                        ApprovedById = _currentUserService.Role == Role.UNITHEAD ? _currentUserService.UserId : null,
                        ApprovedAt = _currentUserService.Role == Role.UNITHEAD ? DateTimeOffset.UtcNow : null,
                        FormStatusRemarks = _currentUserService.Role == Role.UNITHEAD ? "Auto-approved (Unit Head)" : null
                    };

                    var savedEntity = await _repository.AddAsync(entity);
                    var dto = _mapper.MapToDto(savedEntity);
                    result.SuccessfulEntries.Add(dto);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.FailedEntries.Add(new BatchErrorDto
                    {
                        Index = i,
                        ErrorMessage = ex.Message,
                        OriginalData = createDto
                    });
                    result.FailureCount++;
                }
            }

            return ServiceResult<ASMVisitorDetailsBatchResultDto>.Success(result);
        }

        public async Task<ServiceResult<ASMVisitorDetailsBatchResultDto>> UpdateBatchAsync(ASMVisitorDetailsBatchUpdateDto batchUpdateDto)
        {
            var result = new ASMVisitorDetailsBatchResultDto
            {
                TotalProcessed = batchUpdateDto.VisitorDetails.Count
            };

            if (batchUpdateDto.VisitorDetails == null || !batchUpdateDto.VisitorDetails.Any())
            {
                return ServiceResult<ASMVisitorDetailsBatchResultDto>.Failure(
                    "No visitor details provided for batch update",
                    ServiceErrorStatus.VALIDATIONERROR);
            }

            for (int i = 0; i < batchUpdateDto.VisitorDetails.Count; i++)
            {
                var updateDto = batchUpdateDto.VisitorDetails[i];

                try
                {
                    var entity = await _repository.GetByIdAsync(updateDto.Id);

                    if (entity == null)
                    {
                        result.FailedEntries.Add(new BatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = $"Visitor detail with ID {updateDto.Id} not found",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    if (!await _entityPermissionService.CanModifyForm(entity))
                    {
                        result.FailedEntries.Add(new BatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Access denied or entry cannot be modified in current status",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    // Unit Heads can edit their own approved forms
                    bool isUnitHeadEditingOwnApprovedForm = _currentUserService.Role == Role.UNITHEAD
                        && entity.FormStatus == "Approved"
                        && entity.CreatedById == _currentUserService.UserId;

                    if (entity.FormStatus == "Approved" && !isUnitHeadEditingOwnApprovedForm)
                    {
                        result.FailedEntries.Add(new BatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Cannot modify approved entries",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    // Update only provided fields
                    if (updateDto.InstituteName != null)
                        entity.InstituteName = updateDto.InstituteName;

                    if (updateDto.StartDate.HasValue)
                        entity.StartDate = updateDto.StartDate;

                    if (updateDto.EndDate.HasValue)
                        entity.EndDate = updateDto.EndDate;

                    if (updateDto.FarmersCount.HasValue)
                        entity.FarmersCount = updateDto.FarmersCount.Value;

                    if (updateDto.StudentsCount.HasValue)
                        entity.StudentsCount = updateDto.StudentsCount.Value;

                    if (updateDto.PublicCount.HasValue)
                        entity.PublicCount = updateDto.PublicCount.Value;

                    entity.SubmittedDate = DateOnly.FromDateTime(DateTime.UtcNow);
                    entity.FormStatus = "Pending";
                    entity.UpdatedById = _currentUserService.UserId;
                    entity.UpdatedAt = DateTimeOffset.UtcNow;

                    var updatedEntity = await _repository.UpdateAsync(entity);
                    var dto = _mapper.MapToDto(updatedEntity);
                    result.SuccessfulEntries.Add(dto);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.FailedEntries.Add(new BatchErrorDto
                    {
                        Index = i,
                        ErrorMessage = ex.Message,
                        OriginalData = updateDto
                    });
                    result.FailureCount++;
                }
            }

            return ServiceResult<ASMVisitorDetailsBatchResultDto>.Success(result);
        }

        public async Task<ServiceResult<ASMVisitorDetailsDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Visitor detail not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(entity);
            return ServiceResult<ASMVisitorDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<ASMVisitorDetailsDto>> UpdateAsync(int id, ASMVisitorDetailsUpdateDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Visitor detail not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Access denied or entry cannot be modified in current status",
                    ServiceErrorStatus.FORBIDDEN);

            // Unit Heads can edit their own approved forms
            bool isUnitHeadEditingOwnApprovedForm = _currentUserService.Role == Role.UNITHEAD
                && entity.FormStatus == "Approved"
                && entity.CreatedById == _currentUserService.UserId;

            if (entity.FormStatus == "Approved" && !isUnitHeadEditingOwnApprovedForm)
                return ServiceResult<ASMVisitorDetailsDto>.Failure(
                    "Cannot modify approved entries",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Update only provided fields
            if (updateDto.InstituteName != null)
                entity.InstituteName = updateDto.InstituteName;

            if (updateDto.StartDate.HasValue)
                entity.StartDate = updateDto.StartDate;

            if (updateDto.EndDate.HasValue)
                entity.EndDate = updateDto.EndDate;

            if (updateDto.FarmersCount.HasValue)
                entity.FarmersCount = updateDto.FarmersCount.Value;

            if (updateDto.StudentsCount.HasValue)
                entity.StudentsCount = updateDto.StudentsCount.Value;

            if (updateDto.PublicCount.HasValue)
                entity.PublicCount = updateDto.PublicCount.Value;

            entity.SubmittedDate = DateOnly.FromDateTime(DateTime.UtcNow);
            entity.FormStatus = "Pending";
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            var updatedEntity = await _repository.UpdateAsync(entity);
            var dto = _mapper.MapToDto(updatedEntity);

            return ServiceResult<ASMVisitorDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure("Visitor detail not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanDeleteForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus == "Approved")
                return ServiceResult.Failure(
                    "Cannot delete approved entries",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _repository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ==========================================
        // SUBMISSION & APPROVAL WORKFLOW
        // ==========================================

        //public async Task<ServiceResult> SubmitForApprovalAsync(int id)
        //{
        //    var entity = await _repository.GetByIdAsync(id);

        //    if (entity == null)
        //        return ServiceResult.Failure("Visitor detail not found", ServiceErrorStatus.NOTFOUND);

        //    if (!await _entityPermissionService.CanModifyForm(entity))
        //        return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

        //    // Since all creates/updates now automatically set to Pending, this endpoint is mostly redundant
        //    // But we'll keep it for backward compatibility and handle Rejected status
        //    if (entity.FormStatus == "Approved")
        //        return ServiceResult.Failure(
        //            "Cannot submit approved entries",
        //            ServiceErrorStatus.INVALIDOPERATION);

        //    // If already Pending, just return success (idempotent)
        //    if (entity.FormStatus == "Pending")
        //        return ServiceResult.Success();

        //    // Validate required fields before submission
        //    if (entity.FarmersCount == 0 && entity.StudentsCount == 0 && entity.PublicCount == 0)
        //        return ServiceResult.Failure(
        //            "At least one visitor type count must be greater than zero",
        //            ServiceErrorStatus.VALIDATIONERROR);

        //    entity.FormStatus = "Pending";
        //    entity.UpdatedById = _currentUserService.UserId;
        //    entity.UpdatedAt = DateTimeOffset.UtcNow;

        //    await _repository.UpdateAsync(entity);
        //    return ServiceResult.Success();
        //}

        public async Task<ServiceResult> ApproveAsync(int id, string? remarks = null)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure("Visitor detail not found", ServiceErrorStatus.NOTFOUND);

            // Check if current user is Unit Head for this unit location
            var currentUserRole = _currentUserService.Role;
            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
                return ServiceResult.Failure("Only Unit Heads can approve", ServiceErrorStatus.FORBIDDEN);

            if (currentUserRole == Role.UNITHEAD)
            {
                var unitHeadLocations = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                if (!unitHeadLocations.Contains(entity.UnitLocationId))
                    return ServiceResult.Failure(
                        "You can only approve entries in your assigned unit locations",
                        ServiceErrorStatus.FORBIDDEN);
            }

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only Pending entries can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Approved";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectAsync(int id, string remarks)
        {
            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.VALIDATIONERROR);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure("Visitor detail not found", ServiceErrorStatus.NOTFOUND);

            // Check if current user is Unit Head for this unit location
            var currentUserRole = _currentUserService.Role;
            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
                return ServiceResult.Failure("Only Unit Heads can reject", ServiceErrorStatus.FORBIDDEN);

            if (currentUserRole == Role.UNITHEAD)
            {
                var unitHeadLocations = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                if (!unitHeadLocations.Contains(entity.UnitLocationId))
                    return ServiceResult.Failure(
                        "You can only reject entries in your assigned unit locations",
                        ServiceErrorStatus.FORBIDDEN);
            }

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only Pending entries can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        public async Task<PaginatedResult<ASMVisitorDetailsDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _repository.GetPaginatedAsync(
                accessibleUnitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                unitLocationId,
                searchTerm);

            // Map entities to DTOs
            var dtos = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<ASMVisitorDetailsDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<ASMVisitorDetailsDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _repository.GetByStatusAsync(
                accessibleUnitLocationIds,
                status,
                pageNumber,
                pageSize);

            // Map entities to DTOs
            var dtos = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<ASMVisitorDetailsDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        // ==========================================
        // DASHBOARD & STATISTICS
        // ==========================================

        public async Task<ServiceResult<Dictionary<string, int>>> GetStatusSummaryAsync()
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var summary = await _repository.GetStatusSummaryAsync(accessibleUnitLocationIds);

            return ServiceResult<Dictionary<string, int>>.Success(summary);
        }

        public async Task<PaginatedResult<ASMVisitorDetailsDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            var currentUserId = _currentUserService.UserId;

            var result = await _repository.GetByCreatedByAsync(
                currentUserId,
                pageNumber,
                pageSize);

            // Map entities to DTOs
            var dtos = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<ASMVisitorDetailsDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<ASMVisitorDetailsDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            var currentUserRole = _currentUserService.Role;

            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
            {
                return new PaginatedResult<ASMVisitorDetailsDto>
                {
                    Items = new List<ASMVisitorDetailsDto>(),
                    TotalItems = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }

            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _repository.GetByStatusAsync(
                accessibleUnitLocationIds,
                "Pending",
                pageNumber,
                pageSize);

            // Map entities to DTOs
            var dtos = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<ASMVisitorDetailsDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<ASMVisitorDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _repository.GetQueryable()
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District)
                .Where(x => x.CreatedById == trainerId)
                .Where(x => unitLocationIds.Contains(x.UnitLocationId));

            if (unitLocationId.HasValue)
            {
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = items.Select(x => _mapper.MapToDto(x)).ToList();

            return new PaginatedResult<ASMVisitorDetailsDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        // ==========================================
        // HELPER METHODS
        // ==========================================

        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var currentUserRole = _currentUserService.Role;
            var currentUserId = _currentUserService.UserId;

            // Admin has access to all unit locations in their organization
            if (currentUserRole == Role.ADMIN || currentUserRole == Role.SUPERADMIN)
                return true;

            // Unit Head can access their assigned unit locations
            if (currentUserRole == Role.UNITHEAD)
            {
                var unitHeadLocations = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(currentUserId);
                return unitHeadLocations.Contains(unitLocationId);
            }

            // Trainer can access their assigned unit locations
            if (currentUserRole == Role.TRAINER)
            {
                var trainerAssignments = await _trainerAssignmentRepository
                    .GetByTrainerIdAsync(currentUserId);
                return trainerAssignments.Any(t => t.UnitLocationId == unitLocationId);
            }

            return false;
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            var currentUserRole = _currentUserService.Role;
            var currentUserId = _currentUserService.UserId;
            var currentOrgId = _currentUserService.OrganizationId;

            // SuperAdmin/Admin sees all in their organization
            if (currentUserRole == Role.SUPERADMIN || currentUserRole == Role.ADMIN)
            {
                return await _organizationUnitRepository
                    .GetUnitLocationIdsByOrganizationIdAsync(currentOrgId);
            }

            // Unit Head sees their assigned unit locations
            if (currentUserRole == Role.UNITHEAD)
            {
                return await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(currentUserId);
            }

            // Trainer sees their assigned unit locations
            if (currentUserRole == Role.TRAINER)
            {
                return await _trainerAssignmentRepository
                    .GetUnitLocationIdsByTrainerIdAsync(currentUserId);
            }

            return new List<int>();
        }
    }
}