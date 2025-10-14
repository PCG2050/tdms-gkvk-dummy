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
using Infrastructure.Repository.DataTables;
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
        // CREATE
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> CreateAsync(NominationRewardDto createDto)
        {
            var entity = _mapper.MapToEntity(createDto);
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.FormStatus = "Draft";

            var saved = await _repository.AddAsync(entity);
            var dto = _mapper.MapToDto(saved);

            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // UPDATE
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> UpdateAsync(int id, NominationRewardDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<NominationRewardDto>.Failure("NominationReward not found");

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<NominationRewardDto>.Failure("Access denied");

            if (entity.FormStatus != "Draft")
                return ServiceResult<NominationRewardDto>.Failure("Only Draft items can be updated");

            // Update properties manually or through mapper if you add a MapUpdate method later
            entity.AwardName = updateDto.AwardName;
            entity.SpecificContributionTitle = updateDto.SpecificContributionTitle;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _repository.UpdateAsync(entity);

            var dto = _mapper.MapToDto(entity);
            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // GET BY ID
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<NominationRewardDto>.Failure("NominationReward not found");

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<NominationRewardDto>.Failure("Access denied");

            var dto = _mapper.MapToDto(entity);
            return ServiceResult<NominationRewardDto>.Success(dto);
        }

        // -------------------------------------------------------
        // GET COMPLETE DETAILS
        // -------------------------------------------------------
        public async Task<ServiceResult<CompleteNominationRewardDto>> GetCompleteByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<CompleteNominationRewardDto>.Failure("NominationReward not found");

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<CompleteNominationRewardDto>.Failure("Access denied");

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
                return ServiceResult.Failure("NominationReward not found");

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied");

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure("Only Draft items can be deleted");

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

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure("Only Draft items can be submitted");

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

            if (unitLocationId.HasValue && unitLocationIds.Contains(unitLocationId.Value))
            {
                unitLocationIds = new List<int> { unitLocationId.Value };
            }

            var result = await _repository.GetPaginatedAsync(
             unitLocationIds,
             pageNumber ,
             pageSize ,
             startDate ,
             endDate,
             unitLocationId ,
             searchTerm );

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<NominationRewardDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }


        // -------------------------------------------------------
        // STATUS SUMMARY
        // -------------------------------------------------------
        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _repository.GetStatusSummaryAsync(unitLocationIds);
        }

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
