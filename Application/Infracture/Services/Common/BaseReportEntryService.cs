using Application.Interface;
using Application.Interface.Mappers;
using Application.Interface.Repository.Common;
using Application.Interface.Services.Common;
using Application.Models;
using Domain.Entities;
using Infrastructure.Services.Permission;

namespace Infrastructure.Services.Common
{
    /// <summary>
    /// Generic base service implementation for all ReportEntryBaseEntity types.
    /// Provides common CRUD operations with permission checks, workflow management, and pagination.
    /// Derived services can override methods to add custom behavior.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TDto">Read DTO type</typeparam>
    /// <typeparam name="TCreateDto">Create DTO type</typeparam>
    /// <typeparam name="TUpdateDto">Update DTO type</typeparam>
    public abstract class BaseReportEntryService<TEntity, TDto, TCreateDto, TUpdateDto> :
        IBaseReportEntryService<TDto, TCreateDto, TUpdateDto>
        where TEntity : ReportEntryBaseEntity
        where TDto : IBaseDto
        where TCreateDto : ICreateDto
        where TUpdateDto : IUpdateDto
    {
        protected readonly IBaseReportEntryRepository<TEntity> _repository;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IEntityPermissionService _entityPermissionService;
        protected readonly IBaseMapper<TEntity, TDto, TCreateDto, TUpdateDto> _mapper;

        protected BaseReportEntryService(
            IBaseReportEntryRepository<TEntity> repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            IBaseMapper<TEntity, TDto, TCreateDto, TUpdateDto> mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
        }

        // ============================
        // CRUD OPERATIONS
        // ============================

        public virtual async Task<ServiceResult<TDto>> CreateAsync(TCreateDto dto)
        {
            // Map DTO to entity
            var entity = _mapper.MapToEntity(dto);

            // Set audit fields
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.FormStatus = "Draft";

            // Validate unit location access (before creating)
            if (!await CanUserAccessUnitLocationAsync(entity.UnitLocationId))
                return ServiceResult<TDto>.Failure(
                    "Access denied to this unit location",
                    ServiceErrorStatus.FORBIDDEN);

            // Perform additional validation (override in derived classes)
            var validationResult = await ValidateCreate(entity, dto);
            if (!validationResult.Succeeded)
                return ServiceResult<TDto>.Failure(validationResult.ErrorMessage!, validationResult.ErrorStatus);

            // Create entity
            await _repository.CreateAsync(entity);

            // Reload with details and map to DTO
            var result = await _repository.GetWithDetailsAsync(entity.Id);
            var resultDto = _mapper.MapToDtoWithDetails(result!);

            return ServiceResult<TDto>.Success(resultDto);
        }

        public virtual async Task<ServiceResult<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetWithDetailsAsync(id);

            if (entity == null)
                return ServiceResult<TDto>.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<TDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(entity);
            return ServiceResult<TDto>.Success(dto);
        }

        public virtual async Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto dto)
        {
            var entity = await _repository.GetWithDetailsAsync(id);

            if (entity == null)
                return ServiceResult<TDto>.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<TDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult<TDto>.Failure(
                    $"Cannot edit {typeof(TEntity).Name.ToLower()} that have been submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Perform additional validation (override in derived classes)
            var validationResult = await ValidateUpdate(entity, dto);
            if (!validationResult.Succeeded)
                return ServiceResult<TDto>.Failure(validationResult.ErrorMessage!, validationResult.ErrorStatus);

            // Map update DTO to entity
            _mapper.MapUpdateDtoToEntity(dto, entity);
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);

            var updatedEntity = await _repository.GetWithDetailsAsync(id);
            var resultDto = _mapper.MapToDtoWithDetails(updatedEntity!);

            return ServiceResult<TDto>.Success(resultDto);
        }

        public virtual async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    $"Only draft {typeof(TEntity).Name.ToLower()} can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _repository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ============================
        // WORKFLOW OPERATIONS
        // ============================

        public virtual async Task<ServiceResult> SubmitForApprovalAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only draft records can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Validate before submission (override in derived classes)
            var validationResult = await ValidateSubmission(entity);
            if (!validationResult.Succeeded)
                return ServiceResult.Failure(validationResult.ErrorMessage!, validationResult.ErrorStatus);

            entity.FormStatus = "Pending";
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> ApproveAsync(int id, string? remarks = null)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanApproveForm(entity))
                return ServiceResult.Failure(
                    "Access denied - insufficient permissions to approve",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending records can be approved",
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

        public virtual async Task<ServiceResult> RejectAsync(int id, string remarks)
        {
            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Rejection remarks are required",
                    ServiceErrorStatus.BADREQUEST);

            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return ServiceResult.Failure(
                    $"{typeof(TEntity).Name} not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanApproveForm(entity))
                return ServiceResult.Failure(
                    "Access denied - insufficient permissions to reject",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending records can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        // ============================
        // PAGINATION & FILTERING
        // ============================

        public virtual async Task<PaginatedResult<TDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            string? searchTerm = null,
            int? unitLocationId = null)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync(unitLocationId);

            var paginatedEntities = await _repository.GetPaginatedAsync(
                unitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                searchTerm);

            var dtos = paginatedEntities.Items
                .Select(e => _mapper.MapToDtoWithDetails(e))
                .ToList();

            return new PaginatedResult<TDto>(
                dtos,
                paginatedEntities.TotalItems,
                pageNumber,
                pageSize);
        }

        public virtual async Task<PaginatedResult<TDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync(null);

            var paginatedEntities = await _repository.GetByStatusAsync(
                unitLocationIds,
                status,
                pageNumber,
                pageSize);

            var dtos = paginatedEntities.Items
                .Select(e => _mapper.MapToDtoWithDetails(e))
                .ToList();

            return new PaginatedResult<TDto>(
                dtos,
                paginatedEntities.TotalItems,
                pageNumber,
                pageSize);
        }

        public virtual async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync(null);
            return await _repository.GetStatusSummaryAsync(unitLocationIds);
        }

        // ============================
        // PROTECTED HELPER METHODS
        // ============================

        /// <summary>
        /// Gets the list of unit location IDs the current user can access.
        /// Override to customize access control logic.
        /// </summary>
        protected virtual async Task<List<int>> GetAccessibleUnitLocationIdsAsync(int? specificUnitLocationId)
        {
            if (specificUnitLocationId.HasValue)
            {
                if (await CanUserAccessUnitLocationAsync(specificUnitLocationId.Value))
                    return new List<int> { specificUnitLocationId.Value };
                return new List<int>();
            }

            // Default: return all unit locations user has access to
            return await _entityPermissionService.GetAccessibleUnitLocationIdsAsync();
        }

        /// <summary>
        /// Checks if the current user can access a specific unit location.
        /// Override to customize access logic.
        /// </summary>
        protected virtual async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleIds = await _entityPermissionService.GetAccessibleUnitLocationIdsAsync();
            return accessibleIds.Contains(unitLocationId);
        }

        /// <summary>
        /// Override to add custom validation before creating an entity.
        /// Default implementation returns success.
        /// </summary>
        protected virtual Task<ServiceResult> ValidateCreate(TEntity entity, TCreateDto dto)
        {
            return Task.FromResult(ServiceResult.Success());
        }

        /// <summary>
        /// Override to add custom validation before updating an entity.
        /// Default implementation returns success.
        /// </summary>
        protected virtual Task<ServiceResult> ValidateUpdate(TEntity entity, TUpdateDto dto)
        {
            return Task.FromResult(ServiceResult.Success());
        }

        /// <summary>
        /// Override to add custom validation before submitting for approval.
        /// Default implementation returns success.
        /// </summary>
        protected virtual Task<ServiceResult> ValidateSubmission(TEntity entity)
        {
            return Task.FromResult(ServiceResult.Success());
        }
    }
}
