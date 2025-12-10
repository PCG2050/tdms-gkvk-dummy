using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Mapper;
using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;
using Domain.Entities.Enum;
using Domain.Entities.GenericTables;
using Infrastructure.Repository.DataTables.Publication_Repo;
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
        private readonly IUserRepository _userRepository;
        // Generic history service
        private readonly GenericTrainerHistoryService<NominationReward> _historyService;

        public NominationRewardService(
            INominationRewardRepository repository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            NominationRewardMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            IUserRepository userRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _userRepository = userRepository;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<NominationReward>(currentUserService, trainerAssignmentRepository, organizationUnitRepository, unitHeadAssignmentRepository, userRepository);

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
            //if (entity.FormStatus == "Approved")
            //    return ServiceResult<NominationRewardDto>.Failure(
            //        $"Cannot edit {entity.FormStatus} entries",
            //        ServiceErrorStatus.INVALIDOPERATION);

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

            //if (entity.FormStatus != "Draft")
            //    return ServiceResult.Failure(
            //        "Only Draft items can be deleted",
            //        ServiceErrorStatus.INVALIDOPERATION);

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
            var query = _repository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Type?.Name,
                getFormStatus: x => x.FormStatus,
                getRemarks: x => x.FormStatusRemarks ?? "-",
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get pending approvals for Unit Head with pagination
        /// </summary>
        public async Task<PaginatedResult<PendingApprovalItemDto>> GetPendingApprovalsAsync(
            int pageNumber = 1,
            int pageSize = 10,
            int? createdByIdFilter = null)
           
        {
            var query = _repository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Type?.Name,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize,
                createdByIdFilter);
        }


        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _repository.GetStatusSummaryAsync(unitLocationIds);

        }

        // -------------------------------------------------------
        // HYBRID CREATE - INSERT ALL DATA AT ONCE
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> CreateHybridAsync(NominationRewardHybridCreateDto dto)
        {
            // Check permission for unit location
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            if (!unitLocationIds.Contains(dto.UnitLocationId))
                return ServiceResult<NominationRewardDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            // Create parent entity
            var parent = new NominationReward
            {
                UnitLocationId = dto.UnitLocationId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TypeId = dto.TypeId,
                RegionId = dto.RegionId,
                ContributionId = dto.ContributionId,
                ModeId = dto.ModeId,
                NominationCategoryId = dto.NominationCategoryId,
                OtherRegion = dto.OtherRegion,
                AwardName = dto.AwardName,
                OtherContribution = dto.OtherContribution,
                AwardingAgency = dto.AwardingAgency,
                SpecificContributionTitle = dto.SpecificContributionTitle,
                OrganizerInstitutionName = dto.OrganizerInstitutionName,
                OrganizerInstituteAddress = dto.OrganizerInstituteAddress,
                AwardApplicationDate = dto.AwardApplicationDate,
                AwardFilePath = dto.AwardFilePath,
                AwardEventTitle = dto.AwardEventTitle,
                AwardEventDate = dto.AwardEventDate,
                SanctionLetterDate = dto.SanctionLetterDate,
                SanctionLetterFilePath = dto.SanctionLetterFilePath,
                PaperDate = dto.PaperDate,
                PaperFilePath = dto.PaperFilePath,
                AwardReceivingPhoto = dto.AwardReceivingPhoto,
                AwardReceivingCertificate = dto.AwardReceivingCertificate,
                InstitutionBoardName = dto.InstitutionBoardName,
                InstitutionName = dto.InstitutionName,
                InstitutionDesignation = dto.InstitutionDesignation,
                InstitutionAddress = dto.InstitutionAddress,
                PositionId = dto.PositionId,
                PositionFrom = dto.PositionFrom,
                PositionTo = dto.PositionTo,
                DurationDays = dto.DurationDays,
                NominationDate = dto.NominationDate,
                NominationLetterPath = dto.NominationLetterPath,
                OrganizationId = _currentUserService.OrganizationId,
                FormStatus = "Pending",  // Set to Pending on hybrid create
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            // Add child IFSFarmers
            if (dto.IFSFarmers != null)
            {
                foreach (var childDto in dto.IFSFarmers)
                {
                    parent.NominationRewardIFSFarmers.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Add child FarmerInnovations
            if (dto.FarmerInnovations != null)
            {
                foreach (var childDto in dto.FarmerInnovations)
                {
                    parent.NominationRewardFarmerInnovations.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Add child OrganicFarmers
            if (dto.OrganicFarmers != null)
            {
                foreach (var childDto in dto.OrganicFarmers)
                {
                    parent.NominationRewardOrganicFarmers.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Add child IFSEntrepreneurs
            if (dto.IFSEntrepreneurs != null)
            {
                foreach (var childDto in dto.IFSEntrepreneurs)
                {
                    parent.NominationRewardIFSEntrepreneurs.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Add child EntrepreneurInnovations
            if (dto.EntrepreneurInnovations != null)
            {
                foreach (var childDto in dto.EntrepreneurInnovations)
                {
                    parent.NominationRewardEntrepreneurInnovations.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Add child OrganicEntrepreneurs
            if (dto.OrganicEntrepreneurs != null)
            {
                foreach (var childDto in dto.OrganicEntrepreneurs)
                {
                    parent.NominationRewardOrganicEntrepreneurs.Add(_mapper.MapToEntity(childDto));
                }
            }

            // Save everything in one transaction
            var created = await _repository.AddAsync(parent);
            var result = await _repository.GetWithDetailsAsync(created.Id);

            var resultDto = _mapper.MapToDtoWithDetails(result!);
            return ServiceResult<NominationRewardDto>.Success(resultDto);
        }

        // -------------------------------------------------------
        // HYBRID UPDATE - UPDATE ALL DATA AT ONCE
        // -------------------------------------------------------
        public async Task<ServiceResult<NominationRewardDto>> UpdateHybridAsync(int id, NominationRewardHybridUpdateDto dto)
        {
            // Validate entity exists and check permissions
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<NominationRewardDto>.Failure(
                    "NominationReward not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<NominationRewardDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new NominationReward
                {
                    Id = id,
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    TypeId = dto.TypeId,
                    RegionId = dto.RegionId,
                    ContributionId = dto.ContributionId,
                    ModeId = dto.ModeId,
                    NominationCategoryId = dto.NominationCategoryId,
                    OtherRegion = dto.OtherRegion,
                    AwardName = dto.AwardName,
                    OtherContribution = dto.OtherContribution,
                    AwardingAgency = dto.AwardingAgency,
                    SpecificContributionTitle = dto.SpecificContributionTitle,
                    OrganizerInstitutionName = dto.OrganizerInstitutionName,
                    OrganizerInstituteAddress = dto.OrganizerInstituteAddress,
                    AwardApplicationDate = dto.AwardApplicationDate,
                    AwardFilePath = dto.AwardFilePath,
                    AwardEventTitle = dto.AwardEventTitle,
                    AwardEventDate = dto.AwardEventDate,
                    SanctionLetterDate = dto.SanctionLetterDate,
                    SanctionLetterFilePath = dto.SanctionLetterFilePath,
                    PaperDate = dto.PaperDate,
                    PaperFilePath = dto.PaperFilePath,
                    AwardReceivingPhoto = dto.AwardReceivingPhoto,
                    AwardReceivingCertificate = dto.AwardReceivingCertificate,
                    InstitutionBoardName = dto.InstitutionBoardName,
                    InstitutionName = dto.InstitutionName,
                    InstitutionDesignation = dto.InstitutionDesignation,
                    InstitutionAddress = dto.InstitutionAddress,
                    PositionId = dto.PositionId,
                    PositionFrom = dto.PositionFrom,
                    PositionTo = dto.PositionTo,
                    DurationDays = dto.DurationDays,
                    NominationDate = dto.NominationDate,
                    NominationLetterPath = dto.NominationLetterPath,
                    FormStatus = "Pending",  // Set to Pending on update
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child collections
                var ifsFarmers = dto.IFSFarmers?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                var farmerInnovations = dto.FarmerInnovations?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                var organicFarmers = dto.OrganicFarmers?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                var ifsEntrepreneurs = dto.IFSEntrepreneurs?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                var entrepreneurInnovations = dto.EntrepreneurInnovations?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                var organicEntrepreneurs = dto.OrganicEntrepreneurs?.Select(c =>
                {
                    var mapped = _mapper.MapToEntity(c);
                    mapped.CreatedById = _currentUserService.UserId;
                    mapped.CreatedAt = DateTimeOffset.UtcNow;
                    mapped.UpdatedById = _currentUserService.UserId;
                    mapped.UpdatedAt = DateTimeOffset.UtcNow;
                    return mapped;
                }).ToList();

                // Call repository hybrid update
                var updated = await _repository.UpdateWithChildrenAsync(
                    parentEntity,
                    ifsFarmers,
                    farmerInnovations,
                    organicFarmers,
                    ifsEntrepreneurs,
                    entrepreneurInnovations,
                    organicEntrepreneurs);

                var resultDto = _mapper.MapToDtoWithDetails(updated);
                return ServiceResult<NominationRewardDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<NominationRewardDto>.Failure(
                    $"Failed to update: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
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