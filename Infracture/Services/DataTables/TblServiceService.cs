
using Application.Interface.Repository.DataTables.TblService;
using Application.Services.Common;


namespace Infrastructure.Services.DataTables
{
    public class TblServiceService : ITblServiceService
    {
        private readonly ITableServiceRepository _tableServiceRepository;
        private readonly ITableHostelRepository _tableHostelRepository;
        private readonly IRevolvingFundRepository _revolvingFundRepository;
        private readonly IVisitorDetailsRepository _visitorDetailsRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly TblServiceMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUserRepository _userRepository;
        // Generic history service
        private readonly GenericTrainerHistoryService<TblService> _historyService;

        public TblServiceService(
            ITableServiceRepository tableServiceRepository,
            ITableHostelRepository tableHostelRepository,
            IRevolvingFundRepository revolvingFundRepository,
            IVisitorDetailsRepository visitorDetailsRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            TblServiceMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IUserRepository userRepository)
        {
            _tableServiceRepository = tableServiceRepository;
            _tableHostelRepository = tableHostelRepository;
            _revolvingFundRepository = revolvingFundRepository;
            _visitorDetailsRepository = visitorDetailsRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _userRepository = userRepository;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<TblService>(
                currentUserService,
                trainerAssignmentRepository,
                organizationUnitRepository,
                unitHeadAssignmentRepository,
                userRepository);
        }

        // ============================
        // MAIN TblService CRUD
        // ============================

        public async Task<ServiceResult<TblServicesDto>> CreateAsync(TblServiceCreateDto createDto)
        {
            if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                return ServiceResult<TblServicesDto>.Failure("Access denied to unit location", ServiceErrorStatus.FORBIDDEN);

            var entity = _mapper.MapToEntity(createDto);
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.FormStatus = "Draft";

            var created = await _tableServiceRepository.CreateAsync(entity);
            var detailed = await _tableServiceRepository.GetWithDetailsAsync(created.Id);
            var dto = _mapper.MapToDtoWithDetails(detailed!);

            return ServiceResult<TblServicesDto>.Success(dto);
        }


        public async Task<ServiceResult<CompleteTblServicesDto>> GetCompleteTblServiceAsync(int id)
        {
            var entity = await _tableServiceRepository.GetWithDetailsAsync(id);
            if (entity == null)
                return ServiceResult<CompleteTblServicesDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<CompleteTblServicesDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(entity);
            return ServiceResult<CompleteTblServicesDto>.Success(dto);
        }

        public async Task<ServiceResult<TblServicesDto>> GetByIdAsync(int id)
        {
            var entity = await _tableServiceRepository.GetWithDetailsAsync(id);
            if (entity == null)
                return ServiceResult<TblServicesDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(entity))
                return ServiceResult<TblServicesDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(entity);
            return ServiceResult<TblServicesDto>.Success(dto);
        }

        public async Task<ServiceResult<TblServicesDto>> UpdateAsync(int id, TblServiceUpdateDto updateDto)
        {
            var entity = await _tableServiceRepository.GetWithDetailsAsync(id);
            if (entity == null)
                return ServiceResult<TblServicesDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult<TblServicesDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult<TblServicesDto>.Failure("Cannot edit submitted services", ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(updateDto, entity);
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _tableServiceRepository.UpdateAsync(entity);
            var updated = await _tableServiceRepository.GetWithDetailsAsync(id);

            return ServiceResult<TblServicesDto>.Success(_mapper.MapToDtoWithDetails(updated!));
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await _tableServiceRepository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanDeleteForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure("Only draft services can be deleted", ServiceErrorStatus.INVALIDOPERATION);

            await _tableServiceRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ============================
        // CHILD ENTITY: TABLE HOSTEL
        // ============================

        public async Task<ServiceResult<TableHostelDto>> AddTableHostelAsync(int serviceId, TableHostelCreateDto dto)
        {
            var parent = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (parent == null)
                return ServiceResult<TableHostelDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<TableHostelDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (parent.FormStatus != "Draft")
                return ServiceResult<TableHostelDto>.Failure("Cannot add to submitted services", ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.ServiceId = serviceId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            await _tableHostelRepository.CreateTableHostelAsync(entity);
            return ServiceResult<TableHostelDto>.Success(_mapper.MapToDto(entity));
        }

        public async Task<ServiceResult<TableHostelDto>> UpdateTableHostelAsync(int tableHostelId, TableHostelCreateDto dto)
        {
            var tableHostel = await _tableHostelRepository.GetTableHostelByIdAsync(tableHostelId);

            if (tableHostel == null)
                return ServiceResult<TableHostelDto>.Failure(
                    "Table Hostel not found",
                    ServiceErrorStatus.NOTFOUND);

            var tableHostelService = await _tableServiceRepository.GetByIdAsync(tableHostel.ServiceId);
            if (tableHostelService == null || !await _entityPermissionService.CanModifyForm(tableHostelService))
                return ServiceResult<TableHostelDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (tableHostelService.FormStatus != "Draft")
                return ServiceResult<TableHostelDto>.Failure(
                    "Cannot edit mode and outreach for submitted consulting services",
                    ServiceErrorStatus.INVALIDOPERATION);

            tableHostel.Male_GEN = dto.Male_GEN;
            tableHostel.Male_OBC = dto.Male_OBC;
            tableHostel.Male_SC = dto.Male_SC;
            tableHostel.Male_ST = dto.Male_ST;
            tableHostel.Female_GEN = dto.Female_GEN;
            tableHostel.Female_OBC = dto.Female_OBC;
            tableHostel.Female_SC = dto.Female_SC;
            tableHostel.Female_ST = dto.Female_ST;
            tableHostel.NumberOfDaysStayed = dto.NumberOfDaysStayed;
            tableHostel.VillageOrTaluk = dto.VillageOrTaluk;
            tableHostel.Purpose = dto.Purpose;
            tableHostel.SubmittedDate = (DateOnly)dto.Date;
            tableHostel.NumberOfDaysStayed = dto.NumberOfDaysStayed;
            tableHostel.AmountGenerated = dto.AmountGenerated;
            tableHostel.UpdatedById = _currentUserService.UserId;
            tableHostel.UpdatedAt = DateTimeOffset.UtcNow;

            await _tableHostelRepository.UpdateTableHostelAsync(tableHostel);

            var resultDto = _mapper.MapToDto(tableHostel);
            return ServiceResult<TableHostelDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTableHostelAsync(int tableHostelId)
        {
            var tableHostel = await _tableHostelRepository.GetTableHostelByIdAsync(tableHostelId);

            if (tableHostel == null)
                return ServiceResult.Failure("Table Hostel not found", ServiceErrorStatus.NOTFOUND);

            var hostelService = await _tableServiceRepository.GetByIdAsync(tableHostel.ServiceId);
            if (hostelService == null || !await _entityPermissionService.CanModifyForm(hostelService))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (hostelService.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete mode and outreach for submitted consulting services",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _tableHostelRepository.DeleteTableHostelAsync(tableHostelId);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<TableHostelDto>>> GetTableHostelsAsync(int serviceId)
        {
            var items = await _tableHostelRepository.GetTableHostelsByServiceIdAsync(serviceId);
            var dtos = items.Select(_mapper.MapToDto).ToList();
            return ServiceResult<List<TableHostelDto>>.Success(dtos);
        }



        // ============================
        // REVOLVING FUND STATUS
        // ============================

        public async Task<ServiceResult<RevolvingFundStatusDto>> AddRevolvingFundStatusAsync(int serviceId, RevolvingFundStatusCreateDto dto)
        {
            var parent = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (parent == null)
                return ServiceResult<RevolvingFundStatusDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<RevolvingFundStatusDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (parent.FormStatus != "Draft")
                return ServiceResult<RevolvingFundStatusDto>.Failure("Cannot add to submitted services", ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.ServiceId = serviceId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            await _revolvingFundRepository.CreateRevolvingFundStatusAsync(entity);
            return ServiceResult<RevolvingFundStatusDto>.Success(_mapper.MapToDto(entity));
        }

        public async Task<ServiceResult<RevolvingFundStatusDto>> UpdateRevolvingFundStatusAsync(int fundStatusId, RevolvingFundStatusCreateDto dto)
        {
            var entity = await _revolvingFundRepository.GetRevolvingFundStatusByIdAsync(fundStatusId);
            if (entity == null)
                return ServiceResult<RevolvingFundStatusDto>.Failure("Revolving Fund not found", ServiceErrorStatus.NOTFOUND);

            var parent = await _tableServiceRepository.GetByIdAsync(entity.ServiceId);
            if (parent == null || !await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<RevolvingFundStatusDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (parent.FormStatus != "Draft")
                return ServiceResult<RevolvingFundStatusDto>.Failure("Cannot modify submitted services", ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, entity);
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _revolvingFundRepository.UpdateRevolvingFundStatusAsync(entity);
            return ServiceResult<RevolvingFundStatusDto>.Success(_mapper.MapToDto(entity));
        }
        public async Task<ServiceResult> DeleteRevolvingFundStatusAsync(int fundStatusId)
        {
            var entity = await _revolvingFundRepository.GetRevolvingFundStatusByIdAsync(fundStatusId);
            if (entity == null)
                return ServiceResult.Failure("Revolving Fund not found", ServiceErrorStatus.NOTFOUND);

            await _revolvingFundRepository.DeleteRevolvingFundStatusAsync(fundStatusId);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<RevolvingFundStatusDto>>> GetRevolvingFundStatusesAsync(int serviceId)
        {
            var items = await _revolvingFundRepository.GetRevolvingFundStatusesByServiceIdAsync(serviceId);
            var dtos = items.Select(_mapper.MapToDto).ToList();
            return ServiceResult<List<RevolvingFundStatusDto>>.Success(dtos);
        }
        // ============================
        // VISITOR DETAILS
        // ============================

        public async Task<ServiceResult<VisitorDetailDto>> AddVisitorDetailAsync(int serviceId, VisitorDetailCreateDto dto)
        {
            var parent = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (parent == null)
                return ServiceResult<VisitorDetailDto>.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<VisitorDetailDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (parent.FormStatus != "Draft")
                return ServiceResult<VisitorDetailDto>.Failure("Cannot add to submitted services", ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.ServiceId = serviceId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            await _visitorDetailsRepository.CreateVisitorDetailAsync(entity);
            return ServiceResult<VisitorDetailDto>.Success(_mapper.MapToDto(entity));
        }

        public async Task<ServiceResult<VisitorDetailDto>> UpdateVisitorDetailAsync(int visitorDetailId, VisitorDetailCreateDto dto)
        {
            var entity = await _visitorDetailsRepository.GetVisitorDetailByIdAsync(visitorDetailId);
            if (entity == null)
                return ServiceResult<VisitorDetailDto>.Failure("Visitor not found", ServiceErrorStatus.NOTFOUND);

            var parent = await _tableServiceRepository.GetByIdAsync(entity.ServiceId);
            if (parent == null || !await _entityPermissionService.CanModifyForm(parent))
                return ServiceResult<VisitorDetailDto>.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (parent.FormStatus != "Draft")
                return ServiceResult<VisitorDetailDto>.Failure("Cannot modify submitted services", ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, entity);
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _visitorDetailsRepository.UpdateVisitorDetailAsync(entity);
            return ServiceResult<VisitorDetailDto>.Success(_mapper.MapToDto(entity));
        }
        public async Task<ServiceResult> DeleteVisitorDetailAsync(int visitorDetailId)
        {
            var entity = await _visitorDetailsRepository.GetVisitorDetailByIdAsync(visitorDetailId);
            if (entity == null)
                return ServiceResult.Failure("Visitor not found", ServiceErrorStatus.NOTFOUND);

            await _visitorDetailsRepository.DeleteVisitorDetailAsync(visitorDetailId);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<VisitorDetailDto>>> GetVisitorDetailsAsync(int serviceId)
        {
            var items = await _visitorDetailsRepository.GetVisitorDetailsByServiceIdAsync(serviceId);
            var dtos = items.Select(_mapper.MapToDto).ToList();
            return ServiceResult<List<VisitorDetailDto>>.Success(dtos);
        }

        // ============================
        // COMPOSITE CREATE/UPDATE WITH CHILDREN
        // ============================

        public async Task<ServiceResult<TblServicesDto>> CreateWithChildrenAsync(TblServiceCreateDto dto)
        {
            // Step 1: Validate unit location access
            var unitLocation = await _organizationUnitRepository.GetByIdAsync(dto.UnitLocationId);
            if (unitLocation == null)
                return ServiceResult<TblServicesDto>.Failure(
                    "Invalid unit location",
                    ServiceErrorStatus.NOTFOUND);

            try
            {
                // Step 2: Create parent TblService
                var parentEntity = _mapper.MapToEntity(dto);
                parentEntity.OrganizationId = unitLocation.OrganizationId;
                parentEntity.CreatedById = _currentUserService.UserId;
                parentEntity.CreatedAt = DateTimeOffset.UtcNow;
                parentEntity.FormStatus = "Draft";

                var createdService = await _tableServiceRepository.CreateAsync(parentEntity);
                var serviceId = createdService.Id;

                // Step 3: Create TableHostel children if provided
                if (dto.TableHostels != null && dto.TableHostels.Any())
                {
                    foreach (var hostelDto in dto.TableHostels)
                    {
                        var hostelEntity = _mapper.MapToEntity(hostelDto);
                        hostelEntity.ServiceId = serviceId;
                        hostelEntity.CreatedById = _currentUserService.UserId;
                        hostelEntity.CreatedAt = DateTimeOffset.UtcNow;
                        await _tableHostelRepository.CreateTableHostelAsync(hostelEntity);
                    }
                }

                // Step 4: Create RevolvingFundStatus children if provided
                if (dto.RevolvingFundStatuses != null && dto.RevolvingFundStatuses.Any())
                {
                    foreach (var fundDto in dto.RevolvingFundStatuses)
                    {
                        var fundEntity = _mapper.MapToEntity(fundDto);
                        fundEntity.ServiceId = serviceId;
                        fundEntity.CreatedById = _currentUserService.UserId;
                        fundEntity.CreatedAt = DateTimeOffset.UtcNow;
                        await _revolvingFundRepository.CreateRevolvingFundStatusAsync(fundEntity);
                    }
                }

                // Step 5: Create VisitorDetail children if provided
                if (dto.VisitorDetails != null && dto.VisitorDetails.Any())
                {
                    foreach (var visitorDto in dto.VisitorDetails)
                    {
                        var visitorEntity = _mapper.MapToEntity(visitorDto);
                        visitorEntity.ServiceId = serviceId;
                        visitorEntity.CreatedById = _currentUserService.UserId;
                        visitorEntity.CreatedAt = DateTimeOffset.UtcNow;
                        await _visitorDetailsRepository.CreateVisitorDetailAsync(visitorEntity);
                    }
                }

                // Step 6: Get complete entity with all children and return
                var completeEntity = await _tableServiceRepository.GetWithDetailsAsync(serviceId);
                return ServiceResult<TblServicesDto>.Success(_mapper.MapToDto(completeEntity));
            }
            catch (Exception ex)
            {
                return ServiceResult<TblServicesDto>.Failure(
                    $"Failed to create service with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        public async Task<ServiceResult<TblServicesDto>> UpdateWithChildrenAsync(int serviceId, TblServiceWithChildrenUpdateDto dto)
        {
            // Step 1: Get existing service
            var existingService = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (existingService == null)
                return ServiceResult<TblServicesDto>.Failure(
                    "Service not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Check permissions
            if (!await _entityPermissionService.CanModifyForm(existingService))
                return ServiceResult<TblServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            if (existingService.FormStatus != "Draft" && existingService.FormStatus != "Rejected")
                return ServiceResult<TblServicesDto>.Failure(
                    "Cannot modify submitted or approved services",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 4: Update parent TblService
                _mapper.MapUpdateDtoToEntity(dto, existingService);
                existingService.UpdatedById = _currentUserService.UserId;
                existingService.UpdatedAt = DateTimeOffset.UtcNow;
                await _tableServiceRepository.UpdateAsync(existingService);

                // Step 5: Handle TableHostel children - Hybrid Pattern (CREATE/UPDATE/DELETE)
                var existingHostels = await _tableHostelRepository.GetTableHostelsByServiceIdAsync(serviceId);

                if (dto.TableHostels != null)
                {
                    var incomingHostelIds = dto.TableHostels
                        .Where(h => h.Id.HasValue && h.Id.Value > 0)
                        .Select(h => h.Id!.Value)
                        .ToList();

                    // DELETE: Hostels in DB but NOT in incoming array
                    var hostelsToDelete = existingHostels.Where(h => !incomingHostelIds.Contains(h.Id)).ToList();
                    foreach (var hostel in hostelsToDelete)
                    {
                        await _tableHostelRepository.DeleteTableHostelAsync(hostel.Id);
                    }

                    // CREATE or UPDATE
                    foreach (var hostelDto in dto.TableHostels)
                    {
                        if (hostelDto.Id.HasValue && hostelDto.Id.Value > 0)
                        {
                            // UPDATE existing
                            var existing = existingHostels.FirstOrDefault(h => h.Id == hostelDto.Id.Value);
                            if (existing != null)
                            {
                                _mapper.MapUpdateDtoToEntity(hostelDto, existing);
                                existing.UpdatedById = _currentUserService.UserId;
                                existing.UpdatedAt = DateTimeOffset.UtcNow;
                                await _tableHostelRepository.UpdateTableHostelAsync(existing);
                            }
                        }
                        else
                        {
                            // CREATE new
                            var newHostel = _mapper.MapToEntity(hostelDto);
                            newHostel.ServiceId = serviceId;
                            newHostel.CreatedById = _currentUserService.UserId;
                            newHostel.CreatedAt = DateTimeOffset.UtcNow;
                            await _tableHostelRepository.CreateTableHostelAsync(newHostel);
                        }
                    }
                }
                else
                {
                    // If dto.TableHostels is null, delete all existing
                    foreach (var hostel in existingHostels)
                    {
                        await _tableHostelRepository.DeleteTableHostelAsync(hostel.Id);
                    }
                }

                // Step 6: Handle RevolvingFundStatus children - Hybrid Pattern
                var existingFunds = await _revolvingFundRepository.GetRevolvingFundStatusesByServiceIdAsync(serviceId);

                if (dto.RevolvingFundStatuses != null)
                {
                    var incomingFundIds = dto.RevolvingFundStatuses
                        .Where(f => f.Id.HasValue && f.Id.Value > 0)
                        .Select(f => f.Id!.Value)
                        .ToList();

                    // DELETE
                    var fundsToDelete = existingFunds.Where(f => !incomingFundIds.Contains(f.Id)).ToList();
                    foreach (var fund in fundsToDelete)
                    {
                        await _revolvingFundRepository.DeleteRevolvingFundStatusAsync(fund.Id);
                    }

                    // CREATE or UPDATE
                    foreach (var fundDto in dto.RevolvingFundStatuses)
                    {
                        if (fundDto.Id.HasValue && fundDto.Id.Value > 0)
                        {
                            // UPDATE existing
                            var existing = existingFunds.FirstOrDefault(f => f.Id == fundDto.Id.Value);
                            if (existing != null)
                            {
                                _mapper.MapUpdateDtoToEntity(fundDto, existing);
                                existing.UpdatedById = _currentUserService.UserId;
                                existing.UpdatedAt = DateTimeOffset.UtcNow;
                                await _revolvingFundRepository.UpdateRevolvingFundStatusAsync(existing);
                            }
                        }
                        else
                        {
                            // CREATE new
                            var newFund = _mapper.MapToEntity(fundDto);
                            newFund.ServiceId = serviceId;
                            newFund.CreatedById = _currentUserService.UserId;
                            newFund.CreatedAt = DateTimeOffset.UtcNow;
                            await _revolvingFundRepository.CreateRevolvingFundStatusAsync(newFund);
                        }
                    }
                }
                else
                {
                    foreach (var fund in existingFunds)
                    {
                        await _revolvingFundRepository.DeleteRevolvingFundStatusAsync(fund.Id);
                    }
                }

                // Step 7: Handle VisitorDetail children - Hybrid Pattern
                var existingVisitors = await _visitorDetailsRepository.GetVisitorDetailsByServiceIdAsync(serviceId);

                if (dto.VisitorDetails != null)
                {
                    var incomingVisitorIds = dto.VisitorDetails
                        .Where(v => v.Id.HasValue && v.Id.Value > 0)
                        .Select(v => v.Id!.Value)
                        .ToList();

                    // DELETE
                    var visitorsToDelete = existingVisitors.Where(v => !incomingVisitorIds.Contains(v.Id)).ToList();
                    foreach (var visitor in visitorsToDelete)
                    {
                        await _visitorDetailsRepository.DeleteVisitorDetailAsync(visitor.Id);
                    }

                    // CREATE or UPDATE
                    foreach (var visitorDto in dto.VisitorDetails)
                    {
                        if (visitorDto.Id.HasValue && visitorDto.Id.Value > 0)
                        {
                            // UPDATE existing
                            var existing = existingVisitors.FirstOrDefault(v => v.Id == visitorDto.Id.Value);
                            if (existing != null)
                            {
                                _mapper.MapUpdateDtoToEntity(visitorDto, existing);
                                existing.UpdatedById = _currentUserService.UserId;
                                existing.UpdatedAt = DateTimeOffset.UtcNow;
                                await _visitorDetailsRepository.UpdateVisitorDetailAsync(existing);
                            }
                        }
                        else
                        {
                            // CREATE new
                            var newVisitor = _mapper.MapToEntity(visitorDto);
                            newVisitor.ServiceId = serviceId;
                            newVisitor.CreatedById = _currentUserService.UserId;
                            newVisitor.CreatedAt = DateTimeOffset.UtcNow;
                            await _visitorDetailsRepository.CreateVisitorDetailAsync(newVisitor);
                        }
                    }
                }
                else
                {
                    foreach (var visitor in existingVisitors)
                    {
                        await _visitorDetailsRepository.DeleteVisitorDetailAsync(visitor.Id);
                    }
                }

                // Step 8: Get complete entity with all children and return
                var completeEntity = await _tableServiceRepository.GetWithDetailsAsync(serviceId);
                return ServiceResult<TblServicesDto>.Success(_mapper.MapToDto(completeEntity));
            }
            catch (Exception ex)
            {
                return ServiceResult<TblServicesDto>.Failure(
                    $"Failed to update service with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        // ============================
        // SUBMISSION & APPROVAL
        // ============================

        public async Task<ServiceResult> SubmitForApprovalAsync(int serviceId)
        {
            var entity = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (entity == null)
                return ServiceResult.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(entity))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Draft")
                return ServiceResult.Failure("Only draft services can be submitted", ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Pending";
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _tableServiceRepository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ApproveTblServiceAsync(int serviceId, string? remarks = null)
        {
            var entity = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (entity == null)
                return ServiceResult.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads or Admins can approve", ServiceErrorStatus.FORBIDDEN);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only pending services can be approved", ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Approved";
            entity.FormStatusRemarks = remarks;
            entity.ApprovedById = _currentUserService.UserId;
            entity.ApprovedAt = DateTimeOffset.UtcNow;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _tableServiceRepository.UpdateAsync(entity);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectTblServiceAsync(int serviceId, string remarks)
        {
            var entity = await _tableServiceRepository.GetByIdAsync(serviceId);
            if (entity == null)
                return ServiceResult.Failure("Service not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads or Admins can reject", ServiceErrorStatus.FORBIDDEN);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure("Remarks are required for rejection", ServiceErrorStatus.INVALIDOPERATION);

            if (entity.FormStatus != "Pending")
                return ServiceResult.Failure("Only pending services can be rejected", ServiceErrorStatus.INVALIDOPERATION);

            entity.FormStatus = "Rejected";
            entity.FormStatusRemarks = remarks;
            entity.UpdatedById = _currentUserService.UserId;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _tableServiceRepository.UpdateAsync(entity);
            return ServiceResult.Success();
        }



        // ============================
        // PAGINATION
        // ============================

        public async Task<PaginatedResult<TblServicesDto>> GetPaginatedAsync(
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
                unitLocationIds = new List<int> { unitLocationId.Value };

            var result = await _tableServiceRepository.GetPaginatedAsync(
                unitLocationIds, pageNumber, pageSize, startDate, endDate, categoryId, searchTerm);

            var dtos = result.Items.Select(_mapper.MapToDtoWithDetails).ToList();

            return new PaginatedResult<TblServicesDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<PaginatedResult<TblServicesDto>> GetByStatusAsync(
            string status,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _tableServiceRepository.GetByStatusAsync(unitLocationIds, status, pageNumber, pageSize);
            var dtos = result.Items.Select(_mapper.MapToDtoWithDetails).ToList();

            return new PaginatedResult<TblServicesDto>
            {
                Items = dtos,
                TotalItems = result.TotalItems,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            return await _tableServiceRepository.GetStatusSummaryAsync(unitLocationIds);
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
            var query = _tableServiceRepository.GetQueryable()
                .Include(x => x.Category);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Category?.Name,
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
            var query = _tableServiceRepository.GetQueryable()
                .Include(x => x.Category);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Category?.Name,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize);
        }

        // ============================
        // HELPERS
        // ============================

        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleIds = await GetAccessibleUnitLocationIdsAsync();
            return accessibleIds.Contains(unitLocationId);
        }

        private async Task<List<int>> GetAccessibleUnitLocationIdsAsync()
        {
            if (_currentUserService.Role == Role.TRAINER)
                return await _trainerAssignmentRepository.GetUnitLocationIdsByTrainerIdAsync(_currentUserService.UserId);

            if (_currentUserService.Role == Role.UNITHEAD)
                return await _unitHeadAssignmentRepository.GetUnitLocationIdsByUnitHeadIdAsync(_currentUserService.UserId);

            if (_currentUserService.Role == Role.ADMIN)
                return await _organizationUnitRepository.GetUnitLocationIdsByOrganizationIdAsync(_currentUserService.OrganizationId);

            return new List<int>();
        }
    }
}
