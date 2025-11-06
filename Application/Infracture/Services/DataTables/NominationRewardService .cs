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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.DataTables
{
    public class NominationRewardService : INominationRewardService
    {
        private readonly INominationRewardRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly NominationRewardMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;

        public NominationRewardService(
            INominationRewardRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            NominationRewardMapper mapper,
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

        // -------------------------------------------------------
        // CREATE - FIXED TO HANDLE CHILD ENTITIES
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> CreateAsync(NominationRewardDto createDto)
        {
            // CRITICAL FIX: Map the DTO to entity (this creates parent AND child entities)
            var entity = _mapper.MapToEntity(createDto);

            // Set audit fields
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.FormStatus = "Draft";

            // Save to database
            var saved = await _repository.AddAsync(entity);

            // CRITICAL FIX: Get the saved entity with all child collections
            var savedWithDetails = await _repository.GetWithDetailsAsync(saved.Id);

            // Map to DTO with children
            var dto = _mapper.MapToDtoWithDetails(savedWithDetails!);

            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // UPDATE - FIXED TO USE MAPPER'S UPDATE METHOD
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> UpdateAsync(int id, NominationRewardDto updateDto)
        {
            // CRITICAL FIX: Get entity WITH child collections
            var entity = await _repository.GetWithDetailsAsync(id);

            if (entity == null)
                return ServiceResult<NominationRewardDto>.Failure(
                    "NominationReward not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<NominationRewardDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Check status - can only edit Draft, Saved, or Rejected
            if (entity.FormStatus == "Pending" || entity.FormStatus == "Approved")
                return ServiceResult<NominationRewardDto>.Failure(
                    $"Cannot edit {entity.FormStatus} entries",
                    ServiceErrorStatus.INVALIDOPERATION);

            // CRITICAL FIX: Use the mapper's MapUpdateDtoToEntity which handles child entities
            _mapper.MapUpdateDtoToEntity(updateDto, entity);

            // Update audit fields
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            // Save changes
            await _repository.UpdateAsync(entity);

            // CRITICAL FIX: Get the updated entity with all children
            var updatedWithDetails = await _repository.GetWithDetailsAsync(id);

            // Map to DTO with children
            var dto = _mapper.MapToDtoWithDetails(updatedWithDetails!);

            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // GET BY ID - FIXED TO INCLUDE CHILDREN
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> GetByIdAsync(int id)
        {
            // CRITICAL FIX: Use GetWithDetailsAsync to include child collections
            var entity = await _repository.GetWithDetailsAsync(id);

            if (entity == null)
                return ServiceResult<NominationRewardDto>.Failure(
                    "NominationReward not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<NominationRewardDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Map with child collections
            var dto = _mapper.MapToDtoWithDetails(entity);
            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // GET COMPLETE DETAILS - FIXED TO USE GetWithDetailsAsync
        // -------------------------------------------------------
        public async Task<ServiceResult<CompleteNominationRewardDto>> GetCompleteByIdAsync(int id)
        {
            // CRITICAL FIX: Use GetWithDetailsAsync instead of GetByIdAsync
            var entity = await _repository.GetWithDetailsAsync(id);

            if (entity == null)
                return ServiceResult<CompleteNominationRewardDto>.Failure(
                    "NominationReward not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<CompleteNominationRewardDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(entity);
            return ServiceResult<CompleteNominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // DELETE
        // -------------------------------------------------------
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Failure(
                    "NominationReward not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanDeleteForm(entity))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only Draft items can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _repository.DeleteAsync(id);
            return ServiceResult.Success("NominationReward deleted successfully");
        }

        // -------------------------------------------------------
        // SUBMIT FOR APPROVAL
        // -------------------------------------------------------
        public async Task<ServiceResult> SubmitForApprovalAsync(int nominationRewardId)
        {
            var entity = await _repository.GetByIdAsync(nominationRewardId);
            if (entity == null)
                return ServiceResult.Failure("NominationReward not found");

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied");

            if (entity.FormStatus != "Draft" && entity.FormStatus != "Saved")
                return ServiceResult.Failure("Only Draft or Saved items can be submitted");

            entity.FormStatus = "Pending";
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success("NominationReward submitted for approval");
        }

        // -------------------------------------------------------
        // APPROVE
        // -------------------------------------------------------
        public async Task<ServiceResult> ApproveAsync(int nominationRewardId, string? remarks = null)
        {
            var entity = await _repository.GetByIdAsync(nominationRewardId);
            if (entity == null)
                return ServiceResult.Failure("NominationReward not found");

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads/Admin can approve");

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only Pending items can be approved");

            entity.FormStatus = "Approved";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success("NominationReward approved successfully");
        }

        // -------------------------------------------------------
        // REJECT
        // -------------------------------------------------------
        public async Task<ServiceResult> RejectAsync(int nominationRewardId, string remarks)
        {
            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure("Remarks are required for rejection");

            var entity = await _repository.GetByIdAsync(nominationRewardId);
            if (entity == null)
                return ServiceResult.Failure("NominationReward not found");

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads/Admin can reject");

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only Pending items can be rejected");

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);
            return ServiceResult.Success("NominationReward rejected");
        }

        // -------------------------------------------------------
        // PAGINATION
        // -------------------------------------------------------
        public async Task<PaginatedResult<NominationRewardDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _repository.GetByStatusAsync(
                unitLocationIds,
                status,
                pageNumber,
                pageSize);

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NominationRewardDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        public async Task<PaginatedResult<NominationRewardDto>> GetPaginatedAsync(
            int pageNumber = 1,
            int pageSize = 10,
            DateOnly? startDate = null,
            DateOnly? endDate = null,
            int? unitLocationId = null,
            string? searchTerm = null)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            // Optional: filter by specific location
            if (unitLocationId.HasValue)
            {
                unitLocationIds = unitLocationIds
                    .Where(id => id == unitLocationId.Value)
                    .ToList();
            }

            var result = await _repository.GetPaginatedAsync(
                unitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                null, // typeId
                searchTerm);

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NominationRewardDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }

        // -------------------------------------------------------
        // HISTORY: Get History
        // -------------------------------------------------------
        // ADD THESE METHODS TO NominationRewardService.cs

        public async Task<List<UserHistoryDto>> GetTrainerHistoryAsync()
        {
            var userId = _currentUserService.UserId;
            var unitLocationIds = await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(userId);

            var entities = await _repository.GetAllAsync();

            return entities
                .Where(x => x.CreatedById == userId && unitLocationIds.Contains(x.UnitLocationId))
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new UserHistoryDto
                {
                    Id = x.Id,
                    Title = x.Type?.Name ?? "Untitled",
                    CreatedAt = x.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
                    FormStatus = x.FormStatus
                })
                .ToList();
        }

        public async Task<List<UserHistoryDto>> GetHistoryByUnitLocationAsync(int unitLocationId)
        {
            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return new List<UserHistoryDto>();

            var entities = await _repository.GetAllAsync();

            return entities
                .Where(x => x.UnitLocationId == unitLocationId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new UserHistoryDto
                {
                    Id = x.Id,
                    Title = x.Type?.Name?? "Untitled",
                    CreatedAt = x.CreatedAt.ToString("dd_MM-yyyy HH:mm"),
                    FormStatus = x.FormStatus
                })
                .ToList();
        }

        public async Task<List<UserHistoryDto>> GetMyHistoryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var userId = _currentUserService.UserId;

            var entities = await _repository.GetAllAsync();
            var filtered = entities.Where(x => unitLocationIds.Contains(x.UnitLocationId));

            if (_currentUserService.Role == Role.TRAINER)
                filtered = filtered.Where(x => x.CreatedById == userId);

            return filtered
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new UserHistoryDto
                {
                    Id = x.Id,
                    Title = x.Type?.Name ?? "Untitled",
                    CreatedAt = x.CreatedAt.ToString("dd-MM-yyyy HH:mm"),
                    FormStatus = x.FormStatus
                })
                .ToList();
        }


        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _repository.GetStatusSummaryAsync(unitLocationIds);
           
        }

        // -------------------------------------------------------
        // HELPER: Get Accessible Unit Location IDs
        // -------------------------------------------------------
        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            var role = _currentUserService.Role;
            var userId = _currentUserService.UserId;
            var orgId = _currentUserService.OrganizationId;

            if (role == Role.ADMIN)
            {
                // Admin can access all locations in their organization
                var locations = await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(orgId);
                return locations;
            }
            else if (role == Role.UNITHEAD)
            {
                // UnitHead can access their assigned locations
                return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(userId);
            }
            else if (role == Role.TRAINER)
            {
                // Trainer can access their assigned locations
                var assignments = await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(userId);
                return assignments;
            }
            

            return new List<int>();
        }
    }
}