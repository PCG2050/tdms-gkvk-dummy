using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Repository.DataTables;
using Application.Interface.Services;
using Application.Interface.Services.Common;
using Application.Interface.Services.DataTables;
using Application.Mapper;
using Application.Models;
using Application.Models.DataTables;
using Application.Services.Common;
using Domain.Entities.Enum;
using Domain.Entities.GenericTables;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DataTables
{
    public class PublicationService : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IPublisherDetailsRepository _publisherDetailsRepository;
        private readonly IExtensionLiteratureRepository _extensionLiteratureRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly PublicationMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        // Generic history service
        private readonly GenericTrainerHistoryService<Publication> _historyService;

        public PublicationService(
            IPublicationRepository publicationRepository,
            IPublisherDetailsRepository publisherDetailsRepository,
            IExtensionLiteratureRepository extensionLiteratureRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            PublicationMapper mapper,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            ITrainerAssignmentRepository trainerAssignmentRepository
            )
        {
            _publicationRepository = publicationRepository;
            _publisherDetailsRepository = publisherDetailsRepository;
            _extensionLiteratureRepository = extensionLiteratureRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _mapper = mapper;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            //  generic history service
            _historyService = new GenericTrainerHistoryService<Publication>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // PHASE 1: Create Publication
        public async Task<ServiceResult<PublicationDto>> CreatePhase1Async(PublicationCreateDto createDto)
        {
            if (!await CanUserAccessUnitLocationAsync(createDto.UnitLocationId))
                return ServiceResult<PublicationDto>.Failure(
                    "Access denied to unit location",
                    ServiceErrorStatus.FORBIDDEN);

            var publication = _mapper.MapToEntity(createDto);
            publication.CreatedById = _currentUserService.UserId;
            publication.CreatedAt = DateTimeOffset.UtcNow;
            publication.OrganizationId = _currentUserService.OrganizationId;

            // Auto-approve forms created by Unit Heads
            if (_currentUserService.Role == Role.UNITHEAD)
            {
                publication.FormStatus = "Approved";
                publication.ApprovedById = _currentUserService.UserId;
                publication.ApprovedAt = DateTimeOffset.UtcNow;
                publication.FormStatusRemarks = "Auto-approved (Unit Head)";
            }
            else
            {
                publication.FormStatus = "Draft";
            }

            var savedPublication = await _publicationRepository.CreateAsync(publication);

            // Handle many-to-many relationships for newspapers and magazines
            await _publicationRepository.UpdatePublicationNewspapersAndMagazinesAsync(
                savedPublication.Id,
                createDto.KannadaNewsPaperIds,
                createDto.EnglishNewsPaperIds,
                createDto.KannadaMagazineIds,
                createDto.EnglishMagazineIds);

            var publicationWithDetails = await _publicationRepository.GetWithDetailsAsync(savedPublication.Id);
            var dto = _mapper.MapToDtoWithDetails(publicationWithDetails!);

            return ServiceResult<PublicationDto>.Success(dto);
        }

        // Get complete publication for editing
        public async Task<ServiceResult<CompletePublicationDto>> GetCompletePublicationAsync(int id)
        {
            var publication = await _publicationRepository.GetWithDetailsAsync(id);

            if (publication == null)
                return ServiceResult<CompletePublicationDto>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(publication))
                return ServiceResult<CompletePublicationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(publication);
            return ServiceResult<CompletePublicationDto>.Success(dto);
        }

        public async Task<ServiceResult<PublicationDto>> GetByIdAsync(int id)
        {
            var publication = await _publicationRepository.GetWithDetailsAsync(id);

            if (publication == null)
                return ServiceResult<PublicationDto>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(publication))
                return ServiceResult<PublicationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDtoWithDetails(publication);
            return ServiceResult<PublicationDto>.Success(dto);
        }

        public async Task<ServiceResult<PublicationDto>> UpdateAsync(int id, PublicationUpdateDto updateDto)
        {
            var publication = await _publicationRepository.GetWithDetailsAsync(id);

            if (publication == null)
                return ServiceResult<PublicationDto>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult<PublicationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Allow edit for Draft, Rejected, or Approved (if unit head is the creator)
            if (publication.FormStatus == "Draft" || publication.FormStatus == "Rejected")
            {
                // Trainers can edit Draft and Rejected forms
            }
            else if (publication.FormStatus == "Approved" &&
                     _currentUserService.Role == Role.UNITHEAD &&
                     publication.CreatedById == _currentUserService.UserId)
            {
                // Unit heads can edit their own approved forms
            }
            else if (publication.FormStatus == "Pending" &&
                     _currentUserService.Role == Role.UNITHEAD)
            {
                // Unit heads can edit pending forms from trainers in their unit locations
                var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
                if (!unitLocationIds.Contains(publication.UnitLocationId))
                {
                    return ServiceResult<PublicationDto>.Failure(
                        "Access denied to this unit location",
                        ServiceErrorStatus.FORBIDDEN);
                }
                // Allow edit
            }
            else
            {
                return ServiceResult<PublicationDto>.Failure(
                    "Cannot edit in current status",
                    ServiceErrorStatus.INVALIDOPERATION);
            }

            // ✅ Map the update data first
            PublicationMapper.MapUpdateDtoToEntity(updateDto, publication);
            publication.UpdatedById = _currentUserService.UserId;
            publication.UpdatedAt = DateTimeOffset.UtcNow;

            // ✅ AUTO-RESET: If editing Pending or Rejected, reset to Draft
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                string originalStatus = publication.FormStatus;
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = $"Edited by trainer after {originalStatus} status. Reset to Draft.";
                publication.ApprovedById = null; // Clear approval data
                publication.ApprovedAt = null;
            }

            await _publicationRepository.UpdateAsync(publication);

            // Handle many-to-many relationships for newspapers and magazines
            await _publicationRepository.UpdatePublicationNewspapersAndMagazinesAsync(
                id,
                updateDto.KannadaNewsPaperIds,
                updateDto.EnglishNewsPaperIds,
                updateDto.KannadaMagazineIds,
                updateDto.EnglishMagazineIds);

            var updatedPublication = await _publicationRepository.GetWithDetailsAsync(id);
            var dto = _mapper.MapToDtoWithDetails(updatedPublication!);

            return ServiceResult<PublicationDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var publication = await _publicationRepository.GetByIdAsync(id);

            if (publication == null)
                return ServiceResult.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (publication.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only draft publications can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _publicationRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // PHASE 2: Publisher Details
        public async Task<ServiceResult<PublisherDetailsDto>> AddPublisherDetailsAsync(
     int publicationId,
     PublisherDetailsCreateDto dto)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // ✅ ADD: Block editing Approved publications
            if (publication.FormStatus == "Approved")
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Cannot modify approved publications",
                    ServiceErrorStatus.INVALIDOPERATION);

            // ✅ ADD: Auto-reset to Draft if Pending or Rejected
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = "Edited by trainer (Phase 2). Reset to Draft.";
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;
                publication.ApprovedById = null;
                publication.ApprovedAt = null;
                await _publicationRepository.UpdateAsync(publication);
            }

            var publisherDetails = _mapper.MapToEntity(dto);
            publisherDetails.PublicationId = publicationId;
            publisherDetails.CreatedById = _currentUserService.UserId;
            publisherDetails.CreatedAt = DateTimeOffset.UtcNow;

            await _publisherDetailsRepository.CreateAsync(publisherDetails);

            var resultDto = _mapper.MapToDto(publisherDetails);
            return ServiceResult<PublisherDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<PublisherDetailsDto>> UpdatePublisherDetailsAsync(
            int publisherDetailsId,
            PublisherDetailsCreateDto dto)
        {
            var publisherDetails = await _publisherDetailsRepository.GetByIdAsync(publisherDetailsId);

            if (publisherDetails == null)
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Publisher details not found",
                    ServiceErrorStatus.NOTFOUND);

            var publication = await _publicationRepository.GetByIdAsync(publisherDetails.PublicationId);

            if (publication == null || !await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // ✅ ADD: Block editing Approved publications
            if (publication.FormStatus == "Approved")
                return ServiceResult<PublisherDetailsDto>.Failure(
                    "Cannot modify approved publications",
                    ServiceErrorStatus.INVALIDOPERATION);

            // ✅ ADD: Auto-reset to Draft if Pending or Rejected
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = "Edited by trainer (Phase 2 update). Reset to Draft.";
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;
                publication.ApprovedById = null;
                publication.ApprovedAt = null;
                await _publicationRepository.UpdateAsync(publication);
            }

            publisherDetails.PublisherBrochure = dto.PublisherBrochure;
            publisherDetails.PublisherName = dto.PublisherName;
            publisherDetails.PublisherInstitutionName = dto.PublisherInstitutionName;
            publisherDetails.PublisherAddress = dto.PublisherAddress;
            publisherDetails.UpdatedById = _currentUserService.UserId;
            publisherDetails.UpdatedAt = DateTimeOffset.UtcNow;

            await _publisherDetailsRepository.UpdateAsync(publisherDetails);

            var resultDto = _mapper.MapToDto(publisherDetails);
            return ServiceResult<PublisherDetailsDto>.Success(resultDto);
        }

        // PHASE 3: Extension Literature
        public async Task<ServiceResult<ExtensionLiteratureDto>> AddExtensionLiteratureAsync(
            int publicationId,
            ExtensionLiteratureCreateDto dto)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.
                CanModifyForm(publication))
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);
            // ✅ ADD: Block editing Approved publications
            if (publication.FormStatus == "Approved")
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Cannot modify approved publications",
                    ServiceErrorStatus.INVALIDOPERATION);

            // ✅ ADD: Auto-reset to Draft if Pending or Rejected
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = "Edited by trainer (Phase 3). Reset to Draft.";
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;
                publication.ApprovedById = null;
                publication.ApprovedAt = null;
                await _publicationRepository.UpdateAsync(publication);
            }

            var extensionLiterature = _mapper.MapToEntity(dto);
            extensionLiterature.PublicationId = publicationId;
            extensionLiterature.CreatedById = _currentUserService.UserId;
            extensionLiterature.CreatedAt = DateTimeOffset.UtcNow;

            await _extensionLiteratureRepository.CreateAsync(extensionLiterature);

            var resultDto = _mapper.MapToDto(extensionLiterature);
            return ServiceResult<ExtensionLiteratureDto>.Success(resultDto);
        }

        public async Task<ServiceResult<ExtensionLiteratureDto>> UpdateExtensionLiteratureAsync(
            int extensionLiteratureId,
            ExtensionLiteratureCreateDto dto)
        {
            var extensionLiterature = await _extensionLiteratureRepository.GetByIdAsync(extensionLiteratureId);

            if (extensionLiterature == null)
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Extension literature not found",
                    ServiceErrorStatus.NOTFOUND);

            var publication = await _publicationRepository.GetByIdAsync(extensionLiterature.PublicationId);
            if (publication == null || !await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);


            // ✅ ADD: Block editing Approved publications
            if (publication.FormStatus == "Approved")
                return ServiceResult<ExtensionLiteratureDto>.Failure(
                    "Cannot modify approved publications",
                    ServiceErrorStatus.INVALIDOPERATION);

            // ✅ ADD: Auto-reset to Draft if Pending or Rejected
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = "Edited by trainer (Phase 3 update). Reset to Draft.";
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;
                publication.ApprovedById = null;
                publication.ApprovedAt = null;
                await _publicationRepository.UpdateAsync(publication);
            }

            extensionLiterature.Date = dto.Date;
            extensionLiterature.AmountPerCopy = dto.AmountPerCopy;
            extensionLiterature.NumberOfCopies = dto.NumberOfCopies;
            extensionLiterature.TotalAmount = dto.TotalAmount;
            extensionLiterature.UpdatedById = _currentUserService.UserId;
            extensionLiterature.UpdatedAt = DateTimeOffset.UtcNow;

            await _extensionLiteratureRepository.UpdateAsync(extensionLiterature);

            var resultDto = _mapper.MapToDto(extensionLiterature);
            return ServiceResult<ExtensionLiteratureDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteExtensionLiteratureAsync(int extensionLiteratureId)
        {
            var extensionLiterature = await _extensionLiteratureRepository.GetByIdAsync(extensionLiteratureId);

            if (extensionLiterature == null)
                return ServiceResult.Failure(
                    "Extension literature not found",
                    ServiceErrorStatus.NOTFOUND);

            var publication = await _publicationRepository.GetByIdAsync(extensionLiterature.PublicationId);

            if (publication == null || !await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // ✅ ADD: Block editing Approved publications
            if (publication.FormStatus == "Approved")
                return ServiceResult.Failure(
                    "Cannot modify approved publications",
                    ServiceErrorStatus.INVALIDOPERATION);

            // ✅ ADD: Auto-reset to Draft if Pending or Rejected
            if (publication.FormStatus == "Pending" || publication.FormStatus == "Rejected")
            {
                publication.FormStatus = "Draft";
                publication.FormStatusRemarks = "Edited by trainer (Phase 3 delete). Reset to Draft.";
                publication.UpdatedById = _currentUserService.UserId;
                publication.UpdatedAt = DateTimeOffset.UtcNow;
                publication.ApprovedById = null;
                publication.ApprovedAt = null;
                await _publicationRepository.UpdateAsync(publication);
            }

            await _extensionLiteratureRepository.DeleteAsync(extensionLiteratureId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<ExtensionLiteratureDto>>> GetExtensionLiteraturesAsync(
            int publicationId)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult<List<ExtensionLiteratureDto>>.Failure(
                    "Publication not found",
                    ServiceErrorStatus.NOTFOUND);

            var literatures = await _extensionLiteratureRepository.GetByPublicationIdAsync(publicationId);
            var dtos = literatures.Select(l => _mapper.MapToDto(l)).ToList();

            return ServiceResult<List<ExtensionLiteratureDto>>.Success(dtos);
        }

        // SUBMISSION
        public async Task<ServiceResult> SubmitForApprovalAsync(int publicationId)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(publication))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (publication.FormStatus != "Draft")
                return ServiceResult.Failure("Only draft publications can be submitted", ServiceErrorStatus.INVALIDOPERATION);

            publication.FormStatus = "Pending";
            publication.UpdatedById = _currentUserService.UserId;
            publication.UpdatedAt = DateTimeOffset.UtcNow;

            await _publicationRepository.UpdateAsync(publication);
            return ServiceResult.Success();
        }

        // PAGINATION
        public async Task<PaginatedResult<PublicationDto>> GetPaginatedAsync(
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

            int? createdByIdFilter = null;
            if (_currentUserService.Role == Role.TRAINER)
            {
                createdByIdFilter = _currentUserService.UserId;
            }
            var result = await _publicationRepository.GetPaginatedAsync(
                unitLocationIds,
                pageNumber,
                pageSize,
                startDate,
                endDate,
                categoryId,
                searchTerm,
                createdByIdFilter);

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<PublicationDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }


        public async Task<PaginatedResult<PublicationDto>> GetByStatusAsync(
       string status,
       int pageNumber = 1,
       int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _publicationRepository.GetByStatusAsync(
                unitLocationIds,
                status,
                pageNumber,
                pageSize);

            var dtos = result.Items.Select(p => _mapper.MapToDtoWithDetails(p)).ToList();

            return new PaginatedResult<PublicationDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
        }


        public async Task<Dictionary<string, int>> GetStatusSummaryAsync()
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();


            int? createdByIdFilter = null;
            if (_currentUserService.Role == Role.TRAINER)
            {
                createdByIdFilter = _currentUserService.UserId;
            }

            return await _publicationRepository.GetStatusSummaryAsync(
                unitLocationIds,
                createdByIdFilter);
        }

        // APPROVAL/REJECTION (Unit Head)
        public async Task<ServiceResult> ApprovePublicationAsync(int publicationId, string? remarks = null)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads can approve publications", ServiceErrorStatus.FORBIDDEN);

            publication.FormStatus = "Approved";
            publication.FormStatusRemarks = remarks;
            publication.ApprovedAt = DateTimeOffset.UtcNow;
            publication.ApprovedById = _currentUserService.UserId;
            publication.UpdatedById = _currentUserService.UserId;
            publication.UpdatedAt = DateTimeOffset.UtcNow;

            await _publicationRepository.UpdateAsync(publication);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> RejectPublicationAsync(int publicationId, string remarks)
        {
            var publication = await _publicationRepository.GetByIdAsync(publicationId);

            if (publication == null)
                return ServiceResult.Failure("Publication not found", ServiceErrorStatus.NOTFOUND);

            if (_currentUserService.Role != Role.UNITHEAD && _currentUserService.Role != Role.ADMIN)
                return ServiceResult.Failure("Only Unit Heads can reject publications", ServiceErrorStatus.FORBIDDEN);

            if (string.IsNullOrWhiteSpace(remarks))
                return ServiceResult.Failure("Remarks are required for rejection", ServiceErrorStatus.INVALIDOPERATION);

            publication.FormStatus = "Rejected";
            publication.FormStatusRemarks = remarks;
            publication.UpdatedById = _currentUserService.UserId;
            publication.UpdatedAt = DateTimeOffset.UtcNow;

            await _publicationRepository.UpdateAsync(publication);
            return ServiceResult.Success();
        }

        public async Task<object> GetGroupedByUnitAsync(PaginationRequest pagination)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var result = await _publicationRepository.GetPaginatedAsync(
                unitLocationIds,
                pagination.PageNumber,
                pagination.PageSize);

            var groupedByUnit = result.Items
                .GroupBy(p => new
                {
                    UnitId = p.UnitLocation.UnitId,
                    UnitName = p.UnitLocation.Unit.Name
                })
                .Select(g => new
                {
                    UnitId = g.Key.UnitId,
                    UnitName = g.Key.UnitName,
                    PublicationCount = g.Count(),
                    Publications = g.Select(p => _mapper.MapToDtoWithDetails(p)).ToList()
                })
                .ToList();

            var totalPages = (int)Math.Ceiling(result.TotalItems / (double)result.PageSize);

            return new
            {
                units = groupedByUnit,
                pagination = new
                {
                    pageNumber = result.PageNumber,
                    pageSize = result.PageSize,
                    totalItems = result.TotalItems,
                    totalPages,
                    hasPreviousPage = result.PageNumber > 1,
                    hasNextPage = result.PageNumber < totalPages
                }
            };
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
            var query = _publicationRepository.GetQueryable()
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
            var query = _publicationRepository.GetQueryable()
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

        public async Task<PaginatedResult<PublicationDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            var query = _publicationRepository.GetQueryable()
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

            return new PaginatedResult<PublicationDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get unified history - can show own history or specific trainer's history
        /// Unit heads can view their own forms or forms from trainers in their unit locations
        /// </summary>
        public async Task<PaginatedResult<PublicationDto>> GetUnifiedHistoryAsync(
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
            var query = _publicationRepository.GetQueryable()
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
                    return new PaginatedResult<PublicationDto>(
                        new List<PublicationDto>(),
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

            return new PaginatedResult<PublicationDto>(
                dtos,
                totalCount,
                pageNumber,
                pageSize);
        }





        // PRIVATE HELPER METHODS
        //private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        //{
        //    var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();
        //    return accessibleUnitLocationIds.Contains(unitLocationId);
        //}
        private async Task<bool> CanUserAccessUnitLocationAsync(int unitLocationId)
        {
            var accessibleUnitLocationIds = await GetAccessibleUnitLocationIdsAsync();

            Console.WriteLine($"DEBUG: unitLocationId={unitLocationId} ({unitLocationId.GetType()})");
            Console.WriteLine($"DEBUG: accessibleUnitLocationIds={string.Join(", ", accessibleUnitLocationIds)}");
            Console.WriteLine($"DEBUG: first element type={accessibleUnitLocationIds.FirstOrDefault().GetType()}");

            bool match = accessibleUnitLocationIds.Contains(unitLocationId);
            Console.WriteLine($"DEBUG: Contains() returned {match}");
            return match;
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

            return new List<int>(); // Placeholder
        }
    }
}