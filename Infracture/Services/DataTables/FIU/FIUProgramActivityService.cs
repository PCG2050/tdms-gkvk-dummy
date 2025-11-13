using Application.Interface.Repository.DataTables.FIU;
using Application.Interface.Services.DataTables.FIU;
using Application.Mapper.Datatable.FIU;
using Application.Models.DataTables.FIU;
using Application.Models.DataTables.FIU.Application.Models.FIU;
using Application.Services.Common;
using Domain.Entities.FIU;
using System.Globalization;

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

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> CreateAsync(FIUProgramActivityCreateDto createDto)
        {
            if (_currentUserService.Role != Role.TRAINER)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only trainers can create activities");

            var hasAccess = await _trainerAssignmentRepository
                .IsTrainerAssignedToLocationAsync(_currentUserService.UserId, createDto.UnitLocationId);

            if (!hasAccess)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You don't have access to this unit location");

            var fiuActivity = await _fiuActivityRepository.GetByIdAsync(createDto.FIUActivitiesId);
            if (fiuActivity == null || !fiuActivity.IsActive)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Invalid activity type");

            var activity = _mapper.MapCreateDtoToEntity(createDto);
            activity.OrganizationId = _currentUserService.OrganizationId;
            activity.CreatedById = _currentUserService.UserId;
            activity.FormStatus = "Draft";

            var created = await _activityRepository.CreateAsync(activity);
            var response = _mapper.MapEntityToResponseDto(created);

            return ServiceResult<FIUProgramActivityResponseDto>.Success(response, "Activity created successfully");
        }

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> UpdateAsync(FIUProgramActivityUpdateDto updateDto)
        {
            var existing = await _activityRepository.GetByIdAsync(updateDto.Id);
            if (existing == null)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Activity not found");

            if (existing.CreatedById != _currentUserService.UserId)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You can only update your own activities");

            if (existing.FormStatus != "Draft")
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only draft activities can be updated");

            var fiuActivity = await _fiuActivityRepository.GetByIdAsync(updateDto.FIUActivitiesId);
            if (fiuActivity == null || !fiuActivity.IsActive)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Invalid activity type");

            _mapper.MapUpdateDtoToEntity(updateDto, existing);
            var updated = await _activityRepository.UpdateAsync(existing);
            var response = _mapper.MapEntityToResponseDto(updated);

            return ServiceResult<FIUProgramActivityResponseDto>.Success(response, "Activity updated successfully");
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

            if (activity.FormStatus != "Draft")
                return ServiceResult<bool>.Failure("Only draft activities can be deleted");

            var deleted = await _activityRepository.DeleteAsync(id);
            return ServiceResult<bool>.Success(deleted, "Activity deleted successfully");
        }

        public async Task<ServiceResult<FIUProgramActivityResponseDto>> SubmitForApprovalAsync(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Activity not found");

            if (activity.CreatedById != _currentUserService.UserId)
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("You can only submit your own activities");

            if (activity.FormStatus != "Draft")
                return ServiceResult<FIUProgramActivityResponseDto>.Failure("Only draft activities can be submitted");

            activity.FormStatus = "Pending";
            activity.SubmittedAt = DateTimeOffset.UtcNow;

            var updated = await _activityRepository.UpdateAsync(activity);
            var response = _mapper.MapEntityToResponseDto(updated);

            return ServiceResult<FIUProgramActivityResponseDto>.Success(response, "Activity submitted for approval");
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