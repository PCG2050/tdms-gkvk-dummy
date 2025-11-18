
using Application.Services.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DataTables.ConsultSocialMedia
{
    public class ConsultingServiceService : IConsultingServiceService
    {
        private readonly IConsultingServiceRepository _consultingServiceRepository;
        private readonly IModeAndOutreachRepository _modeAndOutreachRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly ConsultingServiceMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        // Generic history service
        private readonly GenericTrainerHistoryService<ConsultingAndSocialMediaService> _historyService;


        public ConsultingServiceService(
            IConsultingServiceRepository consultingServiceRepository,
            IModeAndOutreachRepository modeAndOutreachRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            ConsultingServiceMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _consultingServiceRepository = consultingServiceRepository;
            _modeAndOutreachRepository = modeAndOutreachRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<ConsultingAndSocialMediaService>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // ==========================================
        // Main Consulting Service CRUD
        // ==========================================

        public async Task<ServiceResult<ConsultingServiceDto>> CreateAsync(ConsultingServiceCreateDto createDto)
        {
            if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var consultingService = _mapper.MapToEntity(createDto);
            consultingService.CreatedById = _currentUserService.UserId;
            consultingService.CreatedAt = DateTimeOffset.UtcNow;
            consultingService.OrganizationId = _currentUserService.OrganizationId;

            // Auto-approve forms created by Unit Heads
            if (_currentUserService.Role == Role.UNITHEAD)
            {
                consultingService.FormStatus = "Approved";
                consultingService.ApprovedById = _currentUserService.UserId;
                consultingService.ApprovedAt = DateTimeOffset.UtcNow;
                consultingService.FormStatusRemarks = "Auto-approved (Unit Head)";
            }
            else
            {
                consultingService.FormStatus = "Draft";
            }

            var savedService = await _consultingServiceRepository.CreateAsync(consultingService);
            var serviceWithDetails = await _consultingServiceRepository.GetWithDetailsAsync(savedService.Id);
            var dto = _mapper.MapToDtoWithDetails(serviceWithDetails!);

            return ServiceResult<ConsultingServiceDto>.Success(dto);
        }

        public async Task<ServiceResult<CompleteConsultingServiceDto>> GetCompleteConsultingServiceAsync(int id)
        {
            var consultingService = await _consultingServiceRepository.GetWithDetailsAsync(id);

            if (consultingService == null)
                return ServiceResult<CompleteConsultingServiceDto>.Failure(
                    "Consulting service not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(consultingService))
                return ServiceResult<CompleteConsultingServiceDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(consultingService);
            return ServiceResult<CompleteConsultingServiceDto>.Success(dto);
        }

        public async Task<ServiceResult<ConsultingServiceDto>> GetByIdAsync(int id)
        {
            var consultingService = await _consultingServiceRepository.GetWithDetailsAsync(id);

            if (consultingService == null)
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Consulting service not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(consultingService))
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(consultingService);
            return ServiceResult<ConsultingServiceDto>.Success(dto);
        }

        public async Task<ServiceResult<ConsultingServiceDto>> UpdateAsync(int id, ConsultingServiceUpdateDto updateDto)
        {
            var consultingService = await _consultingServiceRepository.GetWithDetailsAsync(id);

            if (consultingService == null)
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Consulting service not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(consultingService))
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Allow edit for Draft, Rejected, or Approved (if unit head is the creator)
            if (consultingService.FormStatus == "Draft" || consultingService.FormStatus == "Rejected")
            {
                // Trainers can edit Draft and Rejected forms
            }
            else if (consultingService.FormStatus == "Approved" &&
                     _currentUserService.Role == Role.UNITHEAD &&
                     consultingService.CreatedById == _currentUserService.UserId)
            {
                // Unit heads can edit their own approved forms
            }
            else if (consultingService.FormStatus == "Pending" &&
                     _currentUserService.Role == Role.UNITHEAD)
            {
                // Unit heads can edit pending forms from trainers in their unit locations
                var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
                if (!unitLocationIds.Contains(consultingService.UnitLocationId))
                {
                    return ServiceResult<ConsultingServiceDto>.Failure(
                        "Access denied to this unit location",
                        ServiceErrorStatus.FORBIDDEN);
                }
                // Allow edit
            }
            else
            {
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Cannot edit in current status",
                    ServiceErrorStatus.INVALIDOPERATION);
            }

            _mapper.MapUpdateDtoToEntity(updateDto, consultingService);
            consultingService.UpdatedById = _currentUserService.UserId;
            consultingService.UpdatedAt = DateTimeOffset.UtcNow;

            await _consultingServiceRepository.UpdateAsync(consultingService);

            var updatedService = await _consultingServiceRepository.GetWithDetailsAsync(id);
            var dto = _mapper.MapToDtoWithDetails(updatedService!);

            return ServiceResult<ConsultingServiceDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var consultingService = await _consultingServiceRepository.GetByIdAsync(id);

            if (consultingService == null)
                return ServiceResult.Failure("Consulting service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(consultingService))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (consultingService.FormStatus != "Draft" && consultingService.FormStatus != "Rejected" && consultingService.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only draft consulting services can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _consultingServiceRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ==========================================
        // Composite Create/Update with Children
        // ==========================================

        public async Task<ServiceResult<ConsultingServiceDto>> CreateWithChildrenAsync(ConsultingServiceWithChildrenCreateDto dto)
        {
            // Step 1: Validate unit location access
            if (!await CanUserAccessUnitLocationAsync(dto.UnitLocationId))
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            try
            {
                // Step 2: Create parent ConsultingService
                var parentEntity = _mapper.MapToEntity(dto);
                parentEntity.CreatedById = _currentUserService.UserId;
                parentEntity.CreatedAt = DateTimeOffset.UtcNow;
                parentEntity.OrganizationId = _currentUserService.OrganizationId;
                parentEntity.FormStatus = "Draft";

                var createdService = await _consultingServiceRepository.CreateAsync(parentEntity);
                var serviceId = createdService.Id;

                // Step 3: Create ModeAndOutreach children if provided
                if (dto.ModeAndOutreaches != null && dto.ModeAndOutreaches.Any())
                {
                    foreach (var modeDto in dto.ModeAndOutreaches)
                    {
                        var modeEntity = _mapper.MapToEntity(modeDto);
                        modeEntity.ConsultingServiceId = serviceId;
                        modeEntity.CreatedById = _currentUserService.UserId;
                        modeEntity.CreatedAt = DateTimeOffset.UtcNow;
                        await _modeAndOutreachRepository.CreateAsync(modeEntity);
                    }

                    // AUTO-SUBMIT: Since ModeAndOutreach is saved, automatically change status to Pending
                    if (createdService.FormStatus == "Draft" || createdService.FormStatus == "Rejected")
                    {
                        createdService.FormStatus = "Pending";
                        createdService.UpdatedById = _currentUserService.UserId;
                        createdService.UpdatedAt = DateTimeOffset.UtcNow;
                        await _consultingServiceRepository.UpdateAsync(createdService);
                    }
                }

                // Step 4: Get complete entity with all children and return
                var completeEntity = await _consultingServiceRepository.GetWithDetailsAsync(serviceId);
                return ServiceResult<ConsultingServiceDto>.Success(_mapper.MapToDto(completeEntity));
            }
            catch (Exception ex)
            {
                return ServiceResult<ConsultingServiceDto>.Failure(
                    $"Failed to create consulting service with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        public async Task<ServiceResult<ConsultingServiceDto>> UpdateWithChildrenAsync(int consultingServiceId, ConsultingServiceWithChildrenUpdateDto dto)
        {
            // Step 1: Get existing service
            var existingService = await _consultingServiceRepository.GetByIdAsync(consultingServiceId);
            if (existingService == null)
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Consulting service not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Check permissions
            if (!await _entityPermissionService.CanModifyForm(existingService))
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            if (existingService.FormStatus != "Draft" && existingService.FormStatus != "Rejected" && existingService.FormStatus != "Pending")
                return ServiceResult<ConsultingServiceDto>.Failure(
                    "Cannot modify approved consulting services",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 4: Update parent ConsultingService
                _mapper.MapUpdateDtoToEntity(dto, existingService);
                existingService.UpdatedById = _currentUserService.UserId;
                existingService.UpdatedAt = DateTimeOffset.UtcNow;
                await _consultingServiceRepository.UpdateAsync(existingService);

                // Step 5: Handle ModeAndOutreach children - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingModeAndOutreaches = await _modeAndOutreachRepository.GetByConsultingServiceIdAsync(consultingServiceId);

                if (dto.ModeAndOutreaches != null)
                {
                    var incomingIds = dto.ModeAndOutreaches
                        .Where(m => m.Id.HasValue && m.Id.Value > 0)
                        .Select(m => m.Id!.Value)
                        .ToList();

                    // DELETE: Items in DB but NOT in incoming array
                    var itemsToDelete = existingModeAndOutreaches.Where(m => !incomingIds.Contains(m.Id)).ToList();
                    foreach (var item in itemsToDelete)
                    {
                        await _modeAndOutreachRepository.DeleteAsync(item.Id);
                    }

                    // CREATE or UPDATE
                    foreach (var modeDto in dto.ModeAndOutreaches)
                    {
                        if (modeDto.Id.HasValue && modeDto.Id.Value > 0)
                        {
                            // UPDATE existing
                            var existing = existingModeAndOutreaches.FirstOrDefault(m => m.Id == modeDto.Id.Value);
                            if (existing != null)
                            {
                                _mapper.MapUpdateDtoToEntity(modeDto, existing);
                                existing.UpdatedById = _currentUserService.UserId;
                                existing.UpdatedAt = DateTimeOffset.UtcNow;
                                await _modeAndOutreachRepository.UpdateAsync(existing);
                            }
                        }
                        else
                        {
                            // CREATE new
                            var newMode = _mapper.MapToEntity(modeDto);
                            newMode.ConsultingServiceId = consultingServiceId;
                            newMode.CreatedById = _currentUserService.UserId;
                            newMode.CreatedAt = DateTimeOffset.UtcNow;
                            await _modeAndOutreachRepository.CreateAsync(newMode);
                        }
                    }
                }
                else
                {
                    // If dto.ModeAndOutreaches is null, delete all existing
                    foreach (var item in existingModeAndOutreaches)
                    {
                        await _modeAndOutreachRepository.DeleteAsync(item.Id);
                    }
                }

                // AUTO-SUBMIT: If ModeAndOutreach children exist, automatically change status to Pending
                var updatedModeAndOutreaches = await _modeAndOutreachRepository.GetByConsultingServiceIdAsync(consultingServiceId);
                if (updatedModeAndOutreaches.Any() &&
                    (existingService.FormStatus == "Draft" || existingService.FormStatus == "Rejected"))
                {
                    existingService.FormStatus = "Pending";
                    existingService.UpdatedById = _currentUserService.UserId;
                    existingService.UpdatedAt = DateTimeOffset.UtcNow;
                    await _consultingServiceRepository.UpdateAsync(existingService);
                }

                // Step 6: Get complete entity with all children and return
                var completeEntity = await _consultingServiceRepository.GetWithDetailsAsync(consultingServiceId);
                return ServiceResult<ConsultingServiceDto>.Success(_mapper.MapToDto(completeEntity));
            }
            catch (Exception ex)
            {
                return ServiceResult<ConsultingServiceDto>.Failure(
                    $"Failed to update consulting service with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        // ==========================================
        // Submission & Approval
        // ==========================================

        public async Task<ServiceResult> SubmitForApprovalAsync(int consultingServiceId)
        {
            var consultingService = await _consultingServiceRepository.GetByIdAsync(consultingServiceId);

            if (consultingService == null)
                return ServiceResult.Failure("Consulting service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(consultingService))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (consultingService.FormStatus != "Draft" && consultingService.FormStatus != "Rejected" && consultingService.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only draft consulting services can be submitted",
                    ServiceErrorStatus.INVALIDOPERATION);

            consultingService.FormStatus = "Pending";
            consultingService.UpdatedById = _currentUserService.UserId;
            consultingService.UpdatedAt = DateTimeOffset.UtcNow;

            await _consultingServiceRepository.UpdateAsync(consultingService);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ApproveConsultingServiceAsync(int consultingServiceId, string? remarks = null)
        {
            var consultingService = await _consultingServiceRepository.GetByIdAsync(consultingServiceId);

            if (consultingService == null)
                return ServiceResult.Failure("Consulting service not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads can approve consulting services",
                    ServiceErrorStatus.FORBIDDEN);

            if (consultingService.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending consulting services can be approved",
                    ServiceErrorStatus.INVALIDOPERATION);

            consultingService.FormStatus = "Approved";
            consultingService.FormStatusRemarks = remarks;
            consultingService.ApprovedAt = DateTimeOffset.UtcNow;
            consultingService.ApprovedById = _currentUserService.UserId;
            consultingService.UpdatedById = _currentUserService.UserId;
            consultingService.UpdatedAt = DateTimeOffset.UtcNow;

            await _consultingServiceRepository.UpdateAsync(consultingService);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectConsultingServiceAsync(int consultingServiceId, string remarks)
        {
            var consultingService = await _consultingServiceRepository.GetByIdAsync(consultingServiceId);

            if (consultingService == null)
                return ServiceResult.Failure("Consulting service not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure(
                    "Only Unit Heads can reject consulting services",
                    ServiceErrorStatus.FORBIDDEN);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure(
                    "Remarks are required for rejection",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (consultingService.FormStatus != "Pending")
                return ServiceResult.Failure(
                    "Only pending consulting services can be rejected",
                    ServiceErrorStatus.INVALIDOPERATION);

            consultingService.FormStatus = "Rejected";
            consultingService.FormStatusRemarks = remarks;
            consultingService.UpdatedById = _currentUserService.UserId;
            consultingService.UpdatedAt = DateTimeOffset.UtcNow;

            await _consultingServiceRepository.UpdateAsync(consultingService);
            return ServiceResult.Success();
        }

        // ==========================================
        // Pagination & Filtering
        // ==========================================

        public async Task<PaginatedResult<ConsultingServiceDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? categoryId = null,
            string? searchTerm = null,
            int? unitLocationId = null)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            if (unitLocationId.HasValue && unitLocationIds.Contains(unitLocationId.Value))
            {
                unitLocationIds = new List<int> { unitLocationId.Value };
            }

            var result = await _consultingServiceRepository.GetPaginatedAsync(
                unitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                categoryId,
                searchTerm);

            var dtos = result.Items.Select(c => _mapper.MapToDtoWithDetails(c)).ToList();

            return new PaginatedResult<ConsultingServiceDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<ConsultingServiceDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _consultingServiceRepository.GetByStatusAsync(
                unitLocationIds,
                status,
                pageNumber,
                pageSize);

            var dtos = result.Items.Select(c => _mapper.MapToDtoWithDetails(c)).ToList();

            return new PaginatedResult<ConsultingServiceDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _consultingServiceRepository.GetStatusSummaryAsync(unitLocationIds);
        }

        // -------------------------------------------------------
        // HISTORY: Using Generic Service
        // -------------------------------------------------------

        /// <summary>
        /// Get trainer's submission history with pagination
        /// </summary>
        public async Task<PaginatedResult<TrainerHistoryItemDto>> GetTrainerHistoryAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            // Get base query with necessary includes
            var query = _consultingServiceRepository.GetQueryable()
                .Include(x => x.Category);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.Category?.Name,
                getFormStatus: x => x.FormStatus,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get pending approvals for Unit Head with pagination
        /// </summary>
        public async Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var query = _consultingServiceRepository.GetQueryable()
                .Include(x => x.Category);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.Category?.Name,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize);
        }

        public async Task<PaginatedResult<ConsultingServiceDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _consultingServiceRepository.GetQueryable()
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District)
                .Include(x => x.Category)
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

            var dtos = items.Select(x => _mapper.MapToDtoWithDetails(x)).ToList();

            return new PaginatedResult<ConsultingServiceDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        public async Task<PaginatedResult<ConsultingServiceDto>> GetUnifiedHistoryAsync(
            int? trainerId = null,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var currentUserId = _currentUserService.UserId;
            var currentRole = _currentUserService.Role;

            // Get accessible unit locations
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            // Build query
            var query = _consultingServiceRepository.GetQueryable()
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District)
                .Include(x => x.Category);

            // Filter by creator
            if (trainerId.HasValue && trainerId.Value > 0)
            {
                // Viewing specific trainer's history (unit heads only)
                if (currentRole != Role.UNITHEAD && currentRole != Role.ADMIN)
                {
                    return new PaginatedResult<ConsultingServiceDto>(
                        new List<ConsultingServiceDto>(),
                        0,
                        pageNumber,
                        pageSize);
                }
                query = query.Where(x => x.CreatedById == trainerId.Value);
            }
            else
            {
                // Viewing own history
                query = query.Where(x => x.CreatedById == currentUserId);
            }

            // Filter by unit location
            query = query.Where(x => unitLocationIds.Contains(x.UnitLocationId));

            if (unitLocationId.HasValue && unitLocationId.Value > 0)
            {
                query = query.Where(x => x.UnitLocationId == unitLocationId.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderByDescending(x => x.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtos = items.Select(x => _mapper.MapToDtoWithDetails(x)).ToList();

            return new PaginatedResult<ConsultingServiceDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        // ==========================================
        // Private Helper Methods
        // ==========================================

        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return accessibleUnitLocationIds.Contains(unitLocationId);
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            if (_currentUserService.Role == Role.TRAINER)
            {
                return await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);
            }
            else if (_currentUserService.Role == Role.UNITHEAD)
            {
                return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);
            }
            else if (_currentUserService.Role == Role.ADMIN)
            {
                return await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);
            }

            return new List<int>();
        }
    }
}