using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Services.DataTables.FIU;
using Application.Mapper.Datatable.FIU;
using Application.Models.DataTables.FIU;
using Application.Models.DataTables.FIU.Application.Models.FIU;
using Application.Services.Common;
using Domain.Entities.FIU;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DataTables.FIU
{
    public class FIUProgramActivityService : IFIUProgramActivityService
    {
        private readonly IFIUProgramActivityRepository _activityRepository;
        private readonly IFIUActivityRepository _fiuActivityRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly FIUProgramActivityMapper _mapper;
        private readonly GenericTrainerHistoryService<FIUProgramActivity> _historyService;

        public FIUProgramActivityService(
            IFIUProgramActivityRepository activityRepository,
            IFIUActivityRepository fiuActivityRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            ICurrentUserService currentUserService,
            FIUProgramActivityMapper mapper)
        {
            _activityRepository = activityRepository;
            _fiuActivityRepository = fiuActivityRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<FIUProgramActivity>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // ==========================================
        // BATCH CRUD OPERATIONS
        // ==========================================

        public async Task<ServiceResult<FIUProgramActivityBatchResultDto>> CreateBatchAsync(FIUProgramActivityBatchCreateDto batchCreateDto)
        {
            if (_currentUserService.Role != Role.TRAINER)
                return ServiceResult<FIUProgramActivityBatchResultDto>.Failure("Only trainers can create activities");

            var result = new FIUProgramActivityBatchResultDto
            {
                TotalProcessed = batchCreateDto.Activities.Count
            };

            if (batchCreateDto.Activities == null || !batchCreateDto.Activities.Any())
            {
                return ServiceResult<FIUProgramActivityBatchResultDto>.Failure("No activities provided for batch creation");
            }

            for (int i = 0; i < batchCreateDto.Activities.Count; i++)
            {
                var createDto = batchCreateDto.Activities[i];

                try
                {
                    var hasAccess = await _trainerAssignmentRepository
                        .IsTrainerAssignedToLocationAsync(_currentUserService.UserId, createDto.UnitLocationId);

                    if (!hasAccess)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "You don't have access to this unit location",
                            OriginalData = createDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    var fiuActivity = await _fiuActivityRepository.GetByIdAsync(createDto.FIUActivitiesId);
                    if (fiuActivity == null || !fiuActivity.IsActive)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Invalid activity type",
                            OriginalData = createDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    var activity = _mapper.MapCreateDtoToEntity(createDto);
                    activity.OrganizationId = _currentUserService.OrganizationId;
                    activity.CreatedById = _currentUserService.UserId;
                    activity.SubmittedAt = DateTimeOffset.UtcNow;

                    // Auto-approve forms created by Unit Heads
                    if (_currentUserService.Role == Role.UNITHEAD)
                    {
                        activity.FormStatus = "Approved";
                        activity.ApprovedById = _currentUserService.UserId;
                        activity.ApprovedAt = DateTimeOffset.UtcNow;
                        activity.FormStatusRemarks = "Auto-approved (Unit Head)";
                    }
                    else
                    {
                        activity.FormStatus = "Pending";
                    }

                    var created = await _activityRepository.CreateAsync(activity);
                    var response = _mapper.MapEntityToResponseDto(created);
                    result.SuccessfulEntries.Add(response);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.FailedEntries.Add(new FIUBatchErrorDto
                    {
                        Index = i,
                        ErrorMessage = ex.Message,
                        OriginalData = createDto
                    });
                    result.FailureCount++;
                }
            }

            return ServiceResult<FIUProgramActivityBatchResultDto>.Success(result, "Batch creation completed");
        }

        public async Task<ServiceResult<FIUProgramActivityBatchResultDto>> UpdateBatchAsync(FIUProgramActivityBatchUpdateDto batchUpdateDto)
        {
            var result = new FIUProgramActivityBatchResultDto
            {
                TotalProcessed = batchUpdateDto.Activities.Count
            };

            if (batchUpdateDto.Activities == null || !batchUpdateDto.Activities.Any())
            {
                return ServiceResult<FIUProgramActivityBatchResultDto>.Failure("No activities provided for batch update");
            }

            for (int i = 0; i < batchUpdateDto.Activities.Count; i++)
            {
                var updateDto = batchUpdateDto.Activities[i];

                try
                {
                    var existing = await _activityRepository.GetByIdAsync(updateDto.Id);
                    if (existing == null)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = $"Activity with ID {updateDto.Id} not found",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    if (existing.CreatedById != _currentUserService.UserId)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "You can only update your own activities",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    // Unit Heads can edit their own approved forms
                    bool isUnitHeadEditingOwnApprovedForm = _currentUserService.Role == Role.UNITHEAD
                        && existing.FormStatus == "Approved"
                        && existing.CreatedById == _currentUserService.UserId;

                    // Unit heads can edit pending forms from trainers in their unit locations
                    bool isUnitHeadEditingPendingForm = false;
                    if (existing.FormStatus == "Pending" && _currentUserService.Role == Role.UNITHEAD)
                    {
                        var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
                        isUnitHeadEditingPendingForm = unitLocationIds.Contains(existing.UnitLocationId);
                    }

                    if (existing.FormStatus == "Approved" && !isUnitHeadEditingOwnApprovedForm)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Cannot modify approved activities",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    if (existing.FormStatus == "Pending" && !isUnitHeadEditingPendingForm && existing.CreatedById != _currentUserService.UserId)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Access denied to this unit location",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    var fiuActivity = await _fiuActivityRepository.GetByIdAsync(updateDto.FIUActivitiesId);
                    if (fiuActivity == null || !fiuActivity.IsActive)
                    {
                        result.FailedEntries.Add(new FIUBatchErrorDto
                        {
                            Index = i,
                            ErrorMessage = "Invalid activity type",
                            OriginalData = updateDto
                        });
                        result.FailureCount++;
                        continue;
                    }

                    _mapper.MapUpdateDtoToEntity(updateDto, existing);
                    existing.FormStatus = "Pending";
                    existing.SubmittedAt = DateTimeOffset.UtcNow;

                    var updated = await _activityRepository.UpdateAsync(existing);
                    var response = _mapper.MapEntityToResponseDto(updated);
                    result.SuccessfulEntries.Add(response);
                    result.SuccessCount++;
                }
                catch (Exception ex)
                {
                    result.FailedEntries.Add(new FIUBatchErrorDto
                    {
                        Index = i,
                        ErrorMessage = ex.Message,
                        OriginalData = updateDto
                    });
                    result.FailureCount++;
                }
            }

            return ServiceResult<FIUProgramActivityBatchResultDto>.Success(result, "Batch update completed");
        }

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> GetByIdAsync(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Activity not found");

            var hasAccess = await HasAccessToActivityAsync(activity);
            if (!hasAccess)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You don't have access to this activity");

            var response = _mapper.MapEntityToResponseDto(activity);
            return ServiceResult<FIUProgramActivityResponseDto>.Success(response);
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
                return ServiceResult<bool>.Failure("Activity not found");

            if (activity.CreatedById != _currentUserService.UserId)
                return ServiceResult<bool>.Failure("You can only delete your own activities");

            if (activity.FormStatus == "Approved")
                return ServiceResult<bool>.Failure("Cannot delete approved activities");

            var deleted = await _activityRepository.DeleteAsync(id);
            return ServiceResult<bool>.Success(deleted, "Activity deleted successfully");
        }

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> ApproveAsync(int id, string? remarks)
        {
            if (_currentUserService.Role != Role.UNITHEAD)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only unit heads can approve activities");

            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Activity not found");

            if (activity.FormStatus != "Pending")
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only pending activities can be approved");

            var hasAccess = await _unitHeadAssignmentRepository
                .IsUnitHeadAssignedToLocationAsync(_currentUserService.UserId, activity.UnitLocationId);

            if (!hasAccess)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You don't have access to approve this activity");

            activity.FormStatus = "Approved";
            activity.ApprovedAt = DateTimeOffset.UtcNow;
            activity.ApprovedById = _currentUserService.UserId;
            activity.FormStatusRemarks = remarks;

            var updated = await _activityRepository.UpdateAsync(activity);
            var response = _mapper.MapEntityToResponseDto(updated);

            return ServiceResult<FIUProgramActivityResponseDto>.Success(response, "Activity approved successfully");
        }

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> RejectAsync(int id, string remarks)
        {
            if (_currentUserService.Role != Role.UNITHEAD)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only unit heads can reject activities");

            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Activity not found");

            if (activity.FormStatus != "Pending")
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only pending activities can be rejected");

            var hasAccess = await _unitHeadAssignmentRepository
                .IsUnitHeadAssignedToLocationAsync(_currentUserService.UserId, activity.UnitLocationId);

            if (!hasAccess)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You don't have access to reject this activity");

            activity.FormStatus = "Rejected";
            activity.ApprovedAt = DateTimeOffset.UtcNow;
            activity.ApprovedById = _currentUserService.UserId;
            activity.FormStatusRemarks = remarks;

            var updated = await _activityRepository.UpdateAsync(activity);
            var response = _mapper.MapEntityToResponseDto(updated);

            return ServiceResult<FIUProgramActivityResponseDto>.Success(response, "Activity rejected");
        }

        public async Task<ServiceResult<PaginatedResult<FIUProgramActivityResponseDto>>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? activityId = null,
            string? status = null)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            if (!accessibleUnitLocationIds.Any())
                return ServiceResult<PaginatedResult<FIUProgramActivityResponseDto>>.Failure("No accessible unit locations");

            var result = await _activityRepository.GetPaginatedAsync(
                accessibleUnitLocationIds,
                pageNumber,
                pageSize,
                activityId,
                status);

            var dtos = result.Items.Select(_mapper.MapEntityToResponseDto).ToList();
            var paginatedResult = new PaginatedResult<FIUProgramActivityResponseDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);

            return ServiceResult<PaginatedResult<FIUProgramActivityResponseDto>>.Success(paginatedResult);
        }

        public async Task<ServiceResult<List<FIUProgramActivityResponseDto>>> GetMyActivitiesAsync()
        {
            if (_currentUserService.Role != Role.TRAINER)
                return ServiceResult<List<FIUProgramActivityResponseDto>>.Failure("Only trainers can view their activities");

            var activities = await _activityRepository.GetByCreatedByIdAsync(_currentUserService.UserId);
            var dtos = activities.Select(_mapper.MapEntityToResponseDto).ToList();

            return ServiceResult<List<FIUProgramActivityResponseDto>>.Success(dtos);
        }

        public async Task<ServiceResult<List<FIUProgramActivityResponseDto>>> GetPendingApprovalsAsync()
        {
            if (_currentUserService.Role != Role.UNITHEAD)
                return ServiceResult<List<FIUProgramActivityResponseDto>>.Failure("Only unit heads can view pending approvals");

            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var activities = await _activityRepository.GetByStatusAsync(accessibleUnitLocationIds, "Pending");
            var dtos = activities.Select(_mapper.MapEntityToResponseDto).ToList();

            return ServiceResult<List<FIUProgramActivityResponseDto>>.Success(dtos);
        }

        public async Task<ServiceResult<List<FIUActivityDto>>> GetAvailableActivitiesAsync()
        {
            var activities = await _fiuActivityRepository.GetAllActiveAsync();
            var dtos = activities.Select(_mapper.MapActivityEntityToDto).ToList();
            return ServiceResult<List<FIUActivityDto>>.Success(dtos);
        }

        public async Task<ServiceResult<Dictionary<string, int>>> GetStatsByActivityTypeAsync()
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var stats = await _activityRepository.GetStatsByActivityTypeAsync(accessibleUnitLocationIds);
            return ServiceResult<Dictionary<string, int>>.Success(stats);
        }

        public async Task<ServiceResult<Dictionary<string, int>>> GetStatsByStatusAsync()
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var stats = await _activityRepository.GetStatsByStatusAsync(accessibleUnitLocationIds);
            return ServiceResult<Dictionary<string, int>>.Success(stats);
        }

        public async Task<ServiceResult<FIUMonthlyReportDto>> GetMonthlyReportAsync(FIUReportRequestDto requestDto)
        {
            if (requestDto.Year < 2020 || requestDto.Year > 2100)
                return ServiceResult<FIUMonthlyReportDto>.Failure("Invalid year");

            if (requestDto.Month < 1 || requestDto.Month > 12)
                return ServiceResult<FIUMonthlyReportDto>.Failure("Invalid month");

            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            if (requestDto.UnitLocationId.HasValue)
            {
                if (!unitLocationIds.Contains(requestDto.UnitLocationId.Value))
                    return ServiceResult<FIUMonthlyReportDto>.Failure("You don't have access to this unit location");

                unitLocationIds = new List<int> { requestDto.UnitLocationId.Value };
            }

            // Deconstruct tuple returned by repository into the list and the totalEntries value
            var (activities, totalEntries) = await _activityRepository.GetMonthlyActivitySummaryAsync(
                unitLocationIds,
                requestDto.Year,
                requestDto.Month);

            var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(requestDto.Month);

            var report = new FIUMonthlyReportDto
            {
                Year = requestDto.Year,
                Month = requestDto.Month,
                MonthName = monthName,
                Activities = activities,
                TotalActivities = activities.Count,
                TotalCount = activities.Sum(a => a.Count)
            };

            return ServiceResult<FIUMonthlyReportDto>.Success(report);
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
            var query = _activityRepository.GetQueryable()
                .Include(x => x.FIUActivity);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.FIUActivity.ActivityName,
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
            var query = _activityRepository.GetQueryable()
                .Include(x => x.FIUActivity);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.FIUActivity.ActivityName,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize);
        }

        public async Task<PaginatedResult<FIUProgramActivityResponseDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _activityRepository.GetQueryable()
                .Include(x => x.FIUActivity)
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

            var dtos = items.Select(x => _mapper.MapEntityToResponseDto(x)).ToList();

            return new PaginatedResult<FIUProgramActivityResponseDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        public async Task<PaginatedResult<FIUProgramActivityResponseDto>> GetUnifiedHistoryAsync(
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
            var query = _activityRepository.GetQueryable()
                .Include(x => x.FIUActivity)
                .Include(x => x.CreatedBy)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.Unit)
                .Include(x => x.UnitLocation)
                .ThenInclude(ul => ul.District);

            // Filter by creator
            if (trainerId.HasValue && trainerId.Value > 0)
            {
                // Viewing specific trainer's history (unit heads only)
                if (currentRole != Role.UNITHEAD && currentRole != Role.ADMIN)
                {
                    return new PaginatedResult<FIUProgramActivityResponseDto>(
                        new List<FIUProgramActivityResponseDto>(),
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

            var dtos = items.Select(x => _mapper.MapEntityToResponseDto(x)).ToList();

            return new PaginatedResult<FIUProgramActivityResponseDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            return _currentUserService.Role switch
            {
                Role.TRAINER => await _trainerAssignmentRepository
                    .GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId),
                Role.UNITHEAD => await _unitHeadAssignmentRepository
                    .GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId),
                Role.ADMIN => await _organizationUnitRepository
                    .GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId),
                _ => new List<int>()
            };
        }

        private async Task<bool> HasAccessToActivityAsync(FIUProgramActivity activity)
        {
            return _currentUserService.Role switch
            {
                Role.TRAINER => activity.CreatedById == _currentUserService.UserId,
                Role.UNITHEAD => await _unitHeadAssignmentRepository
                    .IsUnitHeadAssignedToLocationAsync(_currentUserService.UserId, activity.UnitLocationId),
                Role.ADMIN => activity.OrganizationId == _currentUserService.OrganizationId,
                _ => false
            };
        }
    }
}