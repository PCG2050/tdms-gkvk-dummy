using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Mapper;
using Application.Models;
using Application.Models.DataTables;
using Domain.Entities.Enum;
using Domain.Entities.GenericTables;

namespace Infrastructure.Services.DataTables
{
    public class TableOtherActivityService : ITableOtherActivityService
    {
        private readonly ITableOtherActivityRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly TableOtherActivityMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public TableOtherActivityService(
            ITableOtherActivityRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            TableOtherActivityMapper mapper,
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

        public async Task<ServiceResult<TableOtherActivityDto>> CreateAsync(TableOtherActivityCreateDto createDto)
        {
            // Verify trainer has access to this unit location
            if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var activity = _mapper.MapToEntity(createDto);
            activity.CreatedById = _currentUserService.UserId;
            activity.CreatedAt = DateTimeOffset.UtcNow;
            activity.OrganizationId = _currentUserService.OrganizationId;
            activity.FormStatus = "Draft";

            await _repository.AddAsync(activity);
            await _repository.SaveChangesAsync();

            var dto = _mapper.MapToDto(activity);
            return ServiceResult<TableOtherActivityDto>.Success(dto);
        }

        public async Task<ServiceResult<TableOtherActivityDto>> GetByIdAsync(int id)
        {
            var activity = await _repository.GetByIdWithDetailsAsync(id);

            if (activity == null)
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(activity))
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(activity);
            return ServiceResult<TableOtherActivityDto>.Success(dto);
        }

        public async Task<ServiceResult<TableOtherActivityDto>> UpdateAsync(int id, TableOtherActivityUpdateDto updateDto)
        {
            var activity = await _repository.GetByIdAsync(id);

            if (activity == null)
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Activity not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(activity))
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Access denied or activity cannot be modified in current status",
                    ServiceErrorStatus.FORBIDDEN);

            if (activity.FormStatus != "Draft" && activity.FormStatus != "Rejected")
                return ServiceResult<TableOtherActivityDto>.Failure(
                    "Cannot modify activities in Pending or Approved status",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapToExistingEntity(updateDto, activity);
            activity.UpdatedById = _currentUserService.UserId;
            activity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(activity);
            await _repository.SaveChangesAsync();

            var activityWithDetails = await _repository.GetByIdWithDetailsAsync(id);
            var dto = _mapper.MapToDto(activityWithDetails!);
            return ServiceResult<TableOtherActivityDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var activity = await _repository.GetByIdAsync(id);

            if (activity == null)
                return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanDeleteForm(activity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (activity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only Draft activities can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _repository.DeleteAsync(activity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success();
        }

        // ==========================================
        // SUBMISSION & APPROVAL WORKFLOW
        // ==========================================

        public async Task<ServiceResult> SubmitForApprovalAsync(int id)
        {
            var activity = await _repository.GetByIdAsync(id);

            if (activity == null)
                return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(activity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (activity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only Draft activities can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Validate required fields before submission
            if (string.IsNullOrWhiteSpace(activity.Title))
                return ServiceResult.Failure(
                    "Title is required before submission",
                    ServiceErrorStatus.VALIDATIONERROR);

            activity.FormStatus = "Pending";
            activity.UpdatedById = _currentUserService.UserId;
            activity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(activity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ApproveAsync(int id, string? remarks = null)
        {
            var activity = await _repository.GetByIdAsync(id);

            if (activity == null)
                return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);

            // Check if current user is Unit Head for this unit location
            var currentUserRole = _currentUserService.Role;
            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
                return ServiceResult.Failure("Only Unit Heads can approve", ServiceErrorStatus.FORBIDDEN);

            if (currentUserRole == Role.UNITHEAD)
            {
                var unitHeadLocations = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                if (!unitHeadLocations.Contains(activity.UnitLocationId))
                    return ServiceResult.Failure(
                        "You can only approve activities in your assigned unit locations",
                        ServiceErrorStatus.FORBIDDEN);
            }

            if (activity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only Pending activities can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

            activity.FormStatus = "Approved";
            activity.FormStatusRemarks = remarks;
            activity.ApprovedById = _currentUserService.UserId;
            activity.ApprovedAt = DateTimeOffset.UtcNow;
            activity.UpdatedById = _currentUserService.UserId;
            activity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(activity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectAsync(int id, string remarks)
        {
            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.VALIDATIONERROR);

            var activity = await _repository.GetByIdAsync(id);

            if (activity == null)
                return ServiceResult.Failure("Activity not found", ServiceErrorStatus.NOTFOUND);

            // Check if current user is Unit Head for this unit location
            var currentUserRole = _currentUserService.Role;
            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
                return ServiceResult.Failure("Only Unit Heads can reject", ServiceErrorStatus.FORBIDDEN);

            if (currentUserRole == Role.UNITHEAD)
            {
                var unitHeadLocations = await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

                if (!unitHeadLocations.Contains(activity.UnitLocationId))
                    return ServiceResult.Failure(
                        "You can only reject activities in your assigned unit locations",
                        ServiceErrorStatus.FORBIDDEN);
            }

            if (activity.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only Pending activities can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            activity.FormStatus = "Rejected";
            activity.FormStatusRemarks = remarks;
            activity.ApprovedById = _currentUserService.UserId;
            activity.ApprovedAt = DateTimeOffset.UtcNow;
            activity.UpdatedById = _currentUserService.UserId;
            activity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(activity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success();
        }

        // ==========================================
        // PAGINATION & FILTERING
        // ==========================================

        public async Task<PaginatedResult<TableOtherActivityDto>> GetPaginatedAsync(
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

            return new PaginatedResult<TableOtherActivityDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<TableOtherActivityDto>> GetByStatusAsync(
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

            return new PaginatedResult<TableOtherActivityDto>
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

        public async Task<PaginatedResult<TableOtherActivityDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            var currentUserId = _currentUserService.UserId;

            var result = await _repository.GetByCreatorAsync(
                currentUserId,
                pageNumber,
                pageSize);

            // Map entities to DTOs
            var dtos = result.Items.Select(_mapper.MapToDto).ToList();

            return new PaginatedResult<TableOtherActivityDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<TableOtherActivityDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            var currentUserRole = _currentUserService.Role;

            if (currentUserRole != Role.ADMIN && currentUserRole != Role.UNITHEAD)
            {
                return new PaginatedResult<TableOtherActivityDto>
                {
                    Items = new List<TableOtherActivityDto>(),
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

            return new PaginatedResult<TableOtherActivityDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
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

            // SuperAdmin sees all - this method may not exist, handle gracefully
            if (currentUserRole == Role.SUPERADMIN)
            {
                // Fallback to getting all unit locations for the organization
                return await _organizationUnitRepository
                    .GetUnitLocationIdsByOrganizationIdAsync(currentOrgId);
            }

            // Admin sees all in their organization
            if (currentUserRole == Role.ADMIN)
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