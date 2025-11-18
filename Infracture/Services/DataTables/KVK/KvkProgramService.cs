// Infrastructure/Services/DataTables/KVK/KvkProgramService.cs
using Application.Interface.Repository.DataTables.KVK;
using Application.Interface.Services.DataTables.KVK;
using Application.Mapper.DataTable.KVK;
using Application.Models.DataTables.KVK;
using Application.Services.Common;
using Infrastructure.Repository;

namespace Infrastructure.Services.DataTables.KVK
{
    public class KvkProgramService : IKvkProgramService
    {
        private readonly IKvkProgramDetailsRepository _programRepository;
        private readonly IKvkParticipantDemographicsRepository _demographicsRepository;
        private readonly IKvkProgramContentRepository _contentRepository;
        private readonly IKvkResourcePersonRepository _resourcePersonRepository;
        private readonly IKvkTopicsCoveredRepository _topicsRepository;
        private readonly IKvkTeachingAidsRepository _teachingAidsRepository;
        private readonly IKvkAdvisoryServicesRepository _advisoryRepository;
        private readonly IKvkResultRepository _resultRepository;
        private readonly IKvkFldResultRepository _fldResultRepository;
        private readonly IKvkOftResultRepository _oftResultRepository;
        private readonly IKvkReportRepository _reportRepository;
        private readonly IKvkRecommendationRepository _recommendationRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityPermissionService _entityPermissionService;
        private readonly KvkProgramMapper _mapper;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly ITrainerAssignmentRepository _trainerAssignmentRepository;
        private readonly IUnitHeadAssignmentRepository _unitHeadAssignmentRepository;
        private readonly GenericTrainerHistoryService<KvkProgramDetails> _historyService;

        private const int FLD_CATEGORY_ID = 18;
        private const int OFT_CATEGORY_ID = 24;
        private const int KVK_UNIT_ID = 10;

        public KvkProgramService(
            IKvkProgramDetailsRepository programRepository,
            IKvkParticipantDemographicsRepository demographicsRepository,
            IKvkProgramContentRepository contentRepository,
            IKvkResourcePersonRepository resourcePersonRepository,
            IKvkTopicsCoveredRepository topicsRepository,
            IKvkTeachingAidsRepository teachingAidsRepository,
            IKvkAdvisoryServicesRepository advisoryRepository,
            IKvkResultRepository resultRepository,
            IKvkFldResultRepository fldResultRepository,
            IKvkOftResultRepository oftResultRepository,
            IKvkReportRepository reportRepository,
            IKvkRecommendationRepository recommendationRepository,
            ICurrentUserService currentUserService,
            IEntityPermissionService entityPermissionService,
            ITrainerAssignmentRepository trainerAssignmentRepository,
            IOrganizationUnitRepository organizationUnitRepository,
            IUnitHeadAssignmentRepository unitHeadAssignmentRepository,
            KvkProgramMapper mapper)
        {
            _programRepository = programRepository;
            _demographicsRepository = demographicsRepository;
            _contentRepository = contentRepository;
            _resourcePersonRepository = resourcePersonRepository;
            _topicsRepository = topicsRepository;
            _teachingAidsRepository = teachingAidsRepository;
            _advisoryRepository = advisoryRepository;
            _resultRepository = resultRepository;
            _fldResultRepository = fldResultRepository;
            _oftResultRepository = oftResultRepository;
            _reportRepository = reportRepository;
            _recommendationRepository = recommendationRepository;
            _currentUserService = currentUserService;
            _entityPermissionService = entityPermissionService;
            _trainerAssignmentRepository = trainerAssignmentRepository;
            _organizationUnitRepository = organizationUnitRepository;
            _unitHeadAssignmentRepository = unitHeadAssignmentRepository;
            _mapper = mapper;

            //  generic history service
            _historyService = new GenericTrainerHistoryService<KvkProgramDetails>(currentUserService, trainerAssignmentRepository, organizationUnitRepository);
        }

        // ============================
        // SECTION A: PROGRAM DETAILS
        // ============================

        public async Task<ServiceResult<KvkProgramDetailsDto>> CreateProgramAsync(KvkProgramCreateDto dto)
        {
            var entity = _mapper.MapToEntity(dto);
            entity.OrganizationId = _currentUserService.OrganizationId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            // Auto-approve forms created by Unit Heads
            if (_currentUserService.Role == Role.UNITHEAD)
            {
                entity.FormStatus = "Approved";
                entity.ApprovedById = _currentUserService.UserId;
                entity.ApprovedAt = DateTimeOffset.UtcNow;
                entity.FormStatusRemarks = "Auto-approved (Unit Head)";
            }
            else
            {
                entity.FormStatus = "Draft";
            }

            var created = await _programRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkProgramDetailsDto>> GetProgramByIdAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<KvkProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<KvkProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(program);
            return ServiceResult<KvkProgramDetailsDto>.Success(dto);
        }

        public async Task<ServiceResult<KvkProgramCompleteDto>> GetCompleteProgramAsync(int id)
        {
            var program = await _programRepository.GetWithDetailsAsync(id);

            if (program == null)
                return ServiceResult<KvkProgramCompleteDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<KvkProgramCompleteDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToCompleteDto(program);
            return ServiceResult<KvkProgramCompleteDto>.Success(dto);
        }

        public async Task<ServiceResult<KvkProgramDetailsDto>> UpdateProgramAsync(int id, KvkProgramUpdateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult<KvkProgramDetailsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkProgramDetailsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Allow edit for Draft, Rejected, or Approved (if unit head is the creator)
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
            {
                // Allow unit heads to edit their own approved forms
                if (!(program.FormStatus == "Approved" &&
                      _currentUserService.Role == Role.UNITHEAD &&
                      program.CreatedById == _currentUserService.UserId))
                {
                    return ServiceResult<KvkProgramDetailsDto>.Failure(
                        "Cannot modify programs in current status",
                        ServiceErrorStatus.INVALIDOPERATION);
                }
            }

            _mapper.MapUpdateDtoToEntity(dto, program);
            program.UpdatedById = _currentUserService.UserId;
            program.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _programRepository.UpdateAsync(program);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkProgramDetailsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteProgramAsync(int id)
        {
            var program = await _programRepository.GetByIdAsync(id);

            if (program == null)
                return ServiceResult.Failure("Program not found", ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Only draft programs can be deleted",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _programRepository.DeleteAsync(id);
            return ServiceResult.Success();
        }

        // ============================
        // SECTION B: DEMOGRAPHICS
        // ============================

        public async Task<ServiceResult<KvkParticipantDemographicsDto>> AddDemographicsAsync(
            int programId,
            KvkParticipantDemographicsCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Cannot add demographics to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkProgramDetailsId = programId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _demographicsRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkParticipantDemographicsDto>> UpdateDemographicsAsync(
            int demographicsId,
            KvkParticipantDemographicsUpdateDto dto)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Demographics not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkParticipantDemographicsDto>.Failure(
                    "Cannot modify demographics for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, demographics);
            demographics.UpdatedById = _currentUserService.UserId;
            demographics.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _demographicsRepository.UpdateAsync(demographics);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkParticipantDemographicsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteDemographicsAsync(int demographicsId)
        {
            var demographics = await _demographicsRepository.GetByIdAsync(demographicsId);

            if (demographics == null)
                return ServiceResult.Failure("Demographics not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(demographics.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete demographics from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _demographicsRepository.DeleteAsync(demographicsId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkParticipantDemographicsDto>>> GetDemographicsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<KvkParticipantDemographicsDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var demographics = await _demographicsRepository.GetByProgramIdAsync(programId);
            var dtos = demographics.Select(d => _mapper.MapToDto(d)).ToList();

            return ServiceResult<List<KvkParticipantDemographicsDto>>.Success(dtos);
        }

        // ============================
        // SECTION C: PROGRAM CONTENT & RESOURCES
        // ============================

        public async Task<ServiceResult<KvkProgramContentDto>> AddProgramContentAsync(
            int programId,
            KvkProgramContentCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Cannot add content to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkProgramDetailsId = programId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _contentRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkProgramContentDto>.Success(resultDto);
        }

        /// <summary>
        /// Create KvkProgramContentAndResources along with all child entities (ResourcePersons, Topics, TeachingAids) in a single transaction
        /// This solves the problem of needing parent ID before creating children
        /// </summary>
        public async Task<ServiceResult<KvkProgramContentDto>> AddProgramContentWithChildrenAsync(
            int programId,
            KvkProgramContentWithChildrenCreateDto dto)
        {
            // Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Validate form status
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Cannot add content to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity
                var parentEntity = _mapper.MapToEntity(new KvkProgramContentCreateDto
                {
                    Title = dto.Title,
                    Description = dto.Description
                });
                parentEntity.KvkProgramDetailsId = programId;
                parentEntity.UnitLocationId = program.UnitLocationId;
                parentEntity.OrganizationId = program.OrganizationId;
                parentEntity.CreatedById = _currentUserService.UserId;
                parentEntity.CreatedAt = DateTimeOffset.UtcNow;

                // Prepare child entities
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = _mapper.MapToEntity(rp);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var topicsCovered = dto.TopicsCovered?.Select(tc =>
                {
                    var entity = _mapper.MapToEntity(tc);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                var teachingAids = dto.TeachingAids?.Select(ta =>
                {
                    var entity = _mapper.MapToEntity(ta);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Repository handles transaction internally
                var createdContent = await _contentRepository.CreateWithChildrenAsync(
                    parentEntity,
                    resourcePersons,
                    topicsCovered,
                    teachingAids);

                var resultDto = _mapper.MapToDto(createdContent);
                return ServiceResult<KvkProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<KvkProgramContentDto>.Failure(
                    $"Failed to create program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        /// <summary>
        /// Update KvkProgramContentAndResources with all child entities using Hybrid Pattern
        /// - Items WITH Id: UPDATE existing
        /// - Items WITHOUT Id: CREATE new
        /// - Items in DB but NOT in arrays: DELETE
        /// </summary>
        public async Task<ServiceResult<KvkProgramContentDto>> UpdateProgramContentWithChildrenAsync(
            int contentId,
            KvkProgramContentWithChildrenUpdateDto dto)
        {
            // Validate content exists
            var content = await _contentRepository.GetWithDetailsAsync(contentId);
            if (content == null)
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            // Validate program and permissions
            var program = await _programRepository.GetByIdAsync(content.KvkProgramDetailsId ?? 0);
            if (program == null)
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Cannot update content in submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Prepare parent entity for update
                var parentEntity = new KvkProgramContentAndResources
                {
                    Id = contentId,                
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Prepare child entities (hybrid: mix of new and existing)
                var resourcePersons = dto.ResourcePersons?.Select(rp =>
                {
                    var entity = new KvkResourcePerson
                    {
                        Id = rp.Id ?? 0, // 0 means new
                        Name = rp.Name,
                        Designation = rp.Designation,
                        ResourceType = rp.ResourceType,
                        Responsibility = rp.Responsibility,
                        InstitutionOrDepartment = rp.InstitutionOrDepartment,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                var topicsCovered = dto.TopicsCovered?.Select(tc =>
                {
                    var entity = new KvkTopicsCoveredInClass
                    {
                        Id = tc.Id ?? 0,
                        Date = tc.Date,
                        Title = tc.Title,
                        PhotoUpload = tc.PhotoUpload,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                var teachingAids = dto.TeachingAids?.Select(ta =>
                {
                    var entity = new KvkTeachingAidsDeveloped
                    {
                        Id = ta.Id ?? 0,
                        TypeOfAidId = ta.TypeOfAidId,
                        OtherTypeOfAid = ta.OtherTypeOfAid,
                        Purpose = ta.Purpose,
                        Number = ta.Number,
                        UnitLocationId = program.UnitLocationId,
                        OrganizationId = program.OrganizationId
                    };

                    if (entity.Id == 0)
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Repository handles transaction internally
                var updatedContent = await _contentRepository.UpdateWithChildrenAsync(
                    parentEntity,
                    resourcePersons,
                    topicsCovered,
                    teachingAids);

                var resultDto = _mapper.MapToDto(updatedContent);
                return ServiceResult<KvkProgramContentDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<KvkProgramContentDto>.Failure(
                    $"Failed to update program content with children: {ex.Message}",
                    ServiceErrorStatus.INVALIDOPERATION);
            }
        }

        public async Task<ServiceResult<KvkProgramContentDto>> GetProgramContentByIdAsync(int contentId)
        {
            var content = await _contentRepository.GetWithDetailsAsync(contentId);

            if (content == null)
                return ServiceResult<KvkProgramContentDto>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(content);
            return ServiceResult<KvkProgramContentDto>.Success(dto);
        }

        public async Task<ServiceResult> DeleteProgramContentAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult.Failure("Content not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete content from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _contentRepository.DeleteAsync(contentId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkProgramContentDto>>> GetProgramContentsByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<List<KvkProgramContentDto>>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var contents = await _contentRepository.GetByProgramIdAsync(programId);
            var dtos = contents.Select(c => _mapper.MapToDto(c)).ToList();

            return ServiceResult<List<KvkProgramContentDto>>.Success(dtos);
        }

        // ============================
        // C1: RESOURCE PERSONS
        // ============================

        public async Task<ServiceResult<KvkResourcePersonDto>> AddResourcePersonAsync(
            int contentId,
            KvkResourcePersonCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Cannot add resource persons to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkProgramContentAndResourcesId = contentId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _resourcePersonRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkResourcePersonDto>> UpdateResourcePersonAsync(
            int personId,
            KvkResourcePersonUpdateDto dto)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Resource person not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkResourcePersonDto>.Failure(
                    "Cannot modify resource persons for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, person);
            person.UpdatedById = _currentUserService.UserId;
            person.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _resourcePersonRepository.UpdateAsync(person);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkResourcePersonDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteResourcePersonAsync(int personId)
        {
            var person = await _resourcePersonRepository.GetByIdAsync(personId);

            if (person == null)
                return ServiceResult.Failure("Resource person not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(person.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete resource persons from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _resourcePersonRepository.DeleteAsync(personId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkResourcePersonDto>>> GetResourcePersonsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<KvkResourcePersonDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var persons = await _resourcePersonRepository.GetByContentIdAsync(contentId);
            var dtos = persons.Select(p => _mapper.MapToDto(p)).ToList();

            return ServiceResult<List<KvkResourcePersonDto>>.Success(dtos);
        }

        // ============================
        // C2: TOPICS COVERED
        // ============================

        public async Task<ServiceResult<KvkTopicsCoveredDto>> AddTopicAsync(
            int contentId,
            KvkTopicsCoveredCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Cannot add topics to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkProgramContentAndResourcesId = contentId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _topicsRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkTopicsCoveredDto>> UpdateTopicAsync(
            int topicId,
            KvkTopicsCoveredUpdateDto dto)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Topic not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkTopicsCoveredDto>.Failure(
                    "Cannot modify topics for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, topic);
            topic.UpdatedById = _currentUserService.UserId;
            topic.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _topicsRepository.UpdateAsync(topic);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkTopicsCoveredDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTopicAsync(int topicId)
        {
            var topic = await _topicsRepository.GetByIdAsync(topicId);

            if (topic == null)
                return ServiceResult.Failure("Topic not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(topic.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete topics from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _topicsRepository.DeleteAsync(topicId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkTopicsCoveredDto>>> GetTopicsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<KvkTopicsCoveredDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var topics = await _topicsRepository.GetByContentIdAsync(contentId);
            var dtos = topics.Select(t => _mapper.MapToDto(t)).ToList();

            return ServiceResult<List<KvkTopicsCoveredDto>>.Success(dtos);
        }

        // ============================
        // C3: TEACHING AIDS
        // ============================

        public async Task<ServiceResult<KvkTeachingAidsDto>> AddTeachingAidAsync(
            int contentId,
            KvkTeachingAidsCreateDto dto)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Program content not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(content.KvkProgramDetailsId ?? 0);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Cannot add teaching aids to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkProgramContentAndResourcesId = contentId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _teachingAidsRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkTeachingAidsDto>> UpdateTeachingAidAsync(
            int aidId,
            KvkTeachingAidsUpdateDto dto)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Teaching aid not found",
                    ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkTeachingAidsDto>.Failure(
                    "Cannot modify teaching aids for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, aid);
            aid.UpdatedById = _currentUserService.UserId;
            aid.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _teachingAidsRepository.UpdateAsync(aid);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkTeachingAidsDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteTeachingAidAsync(int aidId)
        {
            var aid = await _teachingAidsRepository.GetByIdAsync(aidId);

            if (aid == null)
                return ServiceResult.Failure("Teaching aid not found", ServiceErrorStatus.NOTFOUND);

            var content = await _contentRepository.GetByIdAsync(aid.KvkProgramContentAndResourcesId ?? 0);
            var program = await _programRepository.GetByIdAsync(content?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete teaching aids from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _teachingAidsRepository.DeleteAsync(aidId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkTeachingAidsDto>>> GetTeachingAidsByContentIdAsync(int contentId)
        {
            var content = await _contentRepository.GetByIdAsync(contentId);

            if (content == null)
                return ServiceResult<List<KvkTeachingAidsDto>>.Failure(
                    "Content not found",
                    ServiceErrorStatus.NOTFOUND);

            var aids = await _teachingAidsRepository.GetByContentIdAsync(contentId);
            var dtos = aids.Select(a => _mapper.MapToDto(a)).ToList();

            return ServiceResult<List<KvkTeachingAidsDto>>.Success(dtos);
        }


        // ============================
        // SECTION D: ADVISORY SERVICES
        // ============================

        public async Task<ServiceResult<KvkAdvisoryServicesDto>> AddOrUpdateAdvisoryServicesAsync(
            int programId,
            KvkAdvisoryServicesCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkAdvisoryServicesDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkAdvisoryServicesDto>.Failure(
                    "Cannot modify advisory services for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _advisoryRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.KvkProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _advisoryRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<KvkAdvisoryServicesDto>.Success(resultDto);
            }
            else
            {
                // Update existing
                existing.NoOfFacebookSMS = dto.NoOfFacebookSMS ?? 0;
                existing.NoOfSMSSentToRegisteredFarmers = dto.NoOfSMSSentToRegisteredFarmers ?? 0;
                existing.NoOfWhatsappGroups = dto.NoOfWhatsappGroups ?? 0;
                existing.NoOfWhatsappSMS = dto.NoOfWhatsappSMS ?? 0;
                existing.NoOfAnsweredWhatsappQueries = dto.NoOfAnsweredWhatsappQueries ?? 0;
                existing.NoOfPhoneCalls = dto.NoOfPhoneCalls ?? 0;
                existing.NoOfFaceToFaceDiscussions = dto.NoOfFaceToFaceDiscussions ?? 0;
                existing.NoOfGroupDiscussions = dto.NoOfGroupDiscussions ?? 0;
                existing.NoOfEmailsSent = dto.NoOfEmailsSent ?? 0;
                existing.NoOfNewspaperCoverage = dto.NoOfNewspaperCoverage ?? 0;
                existing.NoOfBeneficiaries = dto.NoOfBeneficiaries ?? 0;
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _advisoryRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<KvkAdvisoryServicesDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<KvkAdvisoryServicesDto>> GetAdvisoryServicesByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkAdvisoryServicesDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var advisory = await _advisoryRepository.GetByProgramIdAsync(programId);

            if (advisory == null)
                return ServiceResult<KvkAdvisoryServicesDto>.Failure(
                    "Advisory services not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(advisory);
            return ServiceResult<KvkAdvisoryServicesDto>.Success(dto);
        }

        // ============================
        // SECTION E: RESULTS (FLD/OFT - CategoryId 18 or 24 ONLY)
        // ============================

        public async Task<ServiceResult<KvkResultDto>> GetOrCreateResultAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanViewForm(program))
                return ServiceResult<KvkResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Check if category allows Results (FLD/OFT only)
            if (program.CategoryId != FLD_CATEGORY_ID && program.CategoryId != OFT_CATEGORY_ID)
                return ServiceResult<KvkResultDto>.Failure(
                    $"Results are only available for FLD (CategoryId {FLD_CATEGORY_ID}) or OFT (CategoryId {OFT_CATEGORY_ID}) categories. This program has CategoryId {program.CategoryId}.",
                    ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _resultRepository.GetByProgramIdAsync(programId);

            if (existing != null)
            {
                var existingDto = _mapper.MapToDto(existing);
                return ServiceResult<KvkResultDto>.Success(existingDto);
            }

            // Create new Result record
            var entity = new KvkResult
            {
                KvkProgramDetailsId = programId,
                UnitLocationId = program.UnitLocationId,
                OrganizationId = program.OrganizationId,
                CreatedById = _currentUserService.UserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var created = await _resultRepository.CreateAsync(entity);
            var dto = _mapper.MapToDto(created);

            return ServiceResult<KvkResultDto>.Success(dto);
        }

        public async Task<ServiceResult<KvkResultDto>> GetResultByIdAsync(int resultId)
        {
            var result = await _resultRepository.GetWithDetailsAsync(resultId);

            if (result == null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.KvkProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanViewForm(program))
                return ServiceResult<KvkResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            var dto = _mapper.MapToDto(result);
            return ServiceResult<KvkResultDto>.Success(dto);
        }

        public async Task<ServiceResult> UpdateResultExcelAsync(int resultId, string excelUrl)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult.Failure("Result not found", ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.KvkProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult.Failure(
                    "Cannot modify results for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            result.UploadExcelUrl = excelUrl;
            result.UpdatedById = _currentUserService.UserId;
            result.UpdatedAt = DateTimeOffset.UtcNow;

            await _resultRepository.UpdateAsync(result);
            return ServiceResult.Success();
        }

        // ============================
        // E1: FLD RESULTS
        // ============================

        public async Task<ServiceResult<KvkFldResultDto>> AddFldResultAsync(
            int resultId,
            KvkFldResultCreateDto dto)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult<KvkFldResultDto>.Failure(
                    "Result record not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.KvkProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkFldResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkFldResultDto>.Failure(
                    "Cannot add FLD results to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkResultId = resultId;
            entity.UnitLocationId = result.UnitLocationId;
            entity.OrganizationId = result.OrganizationId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _fldResultRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkFldResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkFldResultDto>> UpdateFldResultAsync(
            int fldId,
            KvkFldResultUpdateDto dto)
        {
            var fldResult = await _fldResultRepository.GetByIdAsync(fldId);

            if (fldResult == null)
                return ServiceResult<KvkFldResultDto>.Failure(
                    "FLD result not found",
                    ServiceErrorStatus.NOTFOUND);

            var result = await _resultRepository.GetByIdAsync(fldResult.KvkResultId);
            var program = await _programRepository.GetByIdAsync(result?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkFldResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkFldResultDto>.Failure(
                    "Cannot modify FLD results for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, fldResult);
            fldResult.UpdatedById = _currentUserService.UserId;
            fldResult.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _fldResultRepository.UpdateAsync(fldResult);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkFldResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteFldResultAsync(int fldId)
        {
            var fldResult = await _fldResultRepository.GetByIdAsync(fldId);

            if (fldResult == null)
                return ServiceResult.Failure("FLD result not found", ServiceErrorStatus.NOTFOUND);

            var result = await _resultRepository.GetByIdAsync(fldResult.KvkResultId);
            var program = await _programRepository.GetByIdAsync(result?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete FLD results from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _fldResultRepository.DeleteAsync(fldId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkFldResultDto>>> GetFldResultsByResultIdAsync(int resultId)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult<List<KvkFldResultDto>>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            var fldResults = await _fldResultRepository.GetByResultIdAsync(resultId);
            var dtos = fldResults.Select(f => _mapper.MapToDto(f)).ToList();

            return ServiceResult<List<KvkFldResultDto>>.Success(dtos);
        }

        // ============================
        // E2: OFT RESULTS
        // ============================

        public async Task<ServiceResult<KvkOftResultDto>> AddOftResultAsync(
            int resultId,
            KvkOftResultCreateDto dto)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult<KvkOftResultDto>.Failure(
                    "Result record not found",
                    ServiceErrorStatus.NOTFOUND);

            var program = await _programRepository.GetByIdAsync(result.KvkProgramDetailsId);
            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkOftResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkOftResultDto>.Failure(
                    "Cannot add OFT results to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var entity = _mapper.MapToEntity(dto);
            entity.KvkResultId = resultId;
            entity.CreatedById = _currentUserService.UserId;
            entity.CreatedAt = DateTimeOffset.UtcNow;

            var created = await _oftResultRepository.CreateAsync(entity);
            var resultDto = _mapper.MapToDto(created);

            return ServiceResult<KvkOftResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult<KvkOftResultDto>> UpdateOftResultAsync(
            int oftId,
            KvkOftResultUpdateDto dto)
        {
            var oftResult = await _oftResultRepository.GetByIdAsync(oftId);

            if (oftResult == null)
                return ServiceResult<KvkOftResultDto>.Failure(
                    "OFT result not found",
                    ServiceErrorStatus.NOTFOUND);

            var result = await _resultRepository.GetByIdAsync(oftResult.KvkResultId);
            var program = await _programRepository.GetByIdAsync(result?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkOftResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkOftResultDto>.Failure(
                    "Cannot modify OFT results for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            _mapper.MapUpdateDtoToEntity(dto, oftResult);
            oftResult.UpdatedById = _currentUserService.UserId;
            oftResult.UpdatedAt = DateTimeOffset.UtcNow;

            var updated = await _oftResultRepository.UpdateAsync(oftResult);
            var resultDto = _mapper.MapToDto(updated);

            return ServiceResult<KvkOftResultDto>.Success(resultDto);
        }

        public async Task<ServiceResult> DeleteOftResultAsync(int oftId)
        {
            var oftResult = await _oftResultRepository.GetByIdAsync(oftId);

            if (oftResult == null)
                return ServiceResult.Failure("OFT result not found", ServiceErrorStatus.NOTFOUND);

            var result = await _resultRepository.GetByIdAsync(oftResult.KvkResultId);
            var program = await _programRepository.GetByIdAsync(result?.KvkProgramDetailsId ?? 0);

            if (program == null || !await _entityPermissionService.CanModifyForm(program))
                return ServiceResult.Failure("Access denied", ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft")
                return ServiceResult.Failure(
                    "Cannot delete OFT results from submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            await _oftResultRepository.DeleteAsync(oftId);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KvkOftResultDto>>> GetOftResultsByResultIdAsync(int resultId)
        {
            var result = await _resultRepository.GetByIdAsync(resultId);

            if (result == null)
                return ServiceResult<List<KvkOftResultDto>>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            var oftResults = await _oftResultRepository.GetByResultIdAsync(resultId);
            var dtos = oftResults.Select(o => _mapper.MapToDto(o)).ToList();

            return ServiceResult<List<KvkOftResultDto>>.Success(dtos);
        }

        // ============================
        // E3: COMPOSITE CREATE/UPDATE FOR RESULTS WITH CHILDREN
        // ============================

        public async Task<ServiceResult<KvkResultDto>> CreateResultWithChildrenAsync(
            int programId,
            KvkResultWithChildrenCreateDto dto)
        {
            // Step 1: Validate program exists
            var program = await _programRepository.GetByIdAsync(programId);
            if (program == null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Check permissions
            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkResultDto>.Failure(
                    "Cannot add results to submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            // Step 4: Check if result already exists for this program
            var existingResult = await _resultRepository.GetByProgramIdAsync(programId);
            if (existingResult != null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Result record already exists for this program. Use update instead.",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 5: Prepare parent entity
                var parentEntity = new KvkResult
                {
                    KvkProgramDetailsId = programId,
                    UploadExcelUrl = dto.UploadExcelUrl,
                    UnitLocationId = program.UnitLocationId,
                    OrganizationId = program.OrganizationId,
                    CreatedById = _currentUserService.UserId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                // Step 6: Prepare FldResults child entities
                var fldResultEntities = dto.FldResults?.Select(fld =>
                {
                    var entity = _mapper.MapToEntity(fld);
                    entity.UnitLocationId = program.UnitLocationId;
                    entity.OrganizationId = program.OrganizationId;
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Step 7: Prepare OftResults child entities
                var oftResultEntities = dto.OftResults?.Select(oft =>
                {
                    var entity = _mapper.MapToEntity(oft);
                    entity.CreatedById = _currentUserService.UserId;
                    entity.CreatedAt = DateTimeOffset.UtcNow;
                    return entity;
                }).ToList();

                // Step 8: Repository handles transaction - creates parent, gets ID, creates children
                var createdResult = await _resultRepository.CreateWithChildrenAsync(
                    parentEntity,
                    fldResultEntities,
                    oftResultEntities);

                // Step 9: Map to DTO and return
                var resultDto = _mapper.MapToDto(createdResult);
                return ServiceResult<KvkResultDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<KvkResultDto>.Failure(
                    $"Failed to create result with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        public async Task<ServiceResult<KvkResultDto>> UpdateResultWithChildrenAsync(
            int resultId,
            KvkResultWithChildrenUpdateDto dto)
        {
            // Step 1: Get existing result
            var existingResult = await _resultRepository.GetByIdAsync(resultId);
            if (existingResult == null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Result not found",
                    ServiceErrorStatus.NOTFOUND);

            // Step 2: Get program and check permissions
            var program = await _programRepository.GetByIdAsync(existingResult.KvkProgramDetailsId);
            if (program == null)
                return ServiceResult<KvkResultDto>.Failure(
                    "Associated program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkResultDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Step 3: Validate form status
            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkResultDto>.Failure(
                    "Cannot modify results for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            try
            {
                // Step 4: Prepare parent entity for update
                var parentEntity = new KvkResult
                {
                    Id = resultId,
                    KvkProgramDetailsId = existingResult.KvkProgramDetailsId,
                    UploadExcelUrl = dto.UploadExcelUrl,
                    UnitLocationId = existingResult.UnitLocationId,
                    OrganizationId = existingResult.OrganizationId,
                    UpdatedById = _currentUserService.UserId,
                    UpdatedAt = DateTimeOffset.UtcNow
                };

                // Step 5: Prepare FldResults - Hybrid Pattern (items with Id will be updated, without will be created)
                var fldResultEntities = dto.FldResults?.Select(fld =>
                {
                    var entity = new KvkFldResult
                    {
                        Id = fld.Id ?? 0,  // 0 means create new, > 0 means update existing
                        DetailsOfDemoId = fld.DetailsOfDemoId,
                        FldNumber = fld.FldNumber,
                        Parameter1 = fld.Parameter1,
                        Observation1 = fld.Observation1,
                        Parameter2 = fld.Parameter2,
                        Observation2 = fld.Observation2,
                        Parameter3 = fld.Parameter3,
                        Observation3 = fld.Observation3,
                        Parameter4 = fld.Parameter4,
                        Observation4 = fld.Observation4,
                        Parameter5 = fld.Parameter5,
                        Observation5 = fld.Observation5,
                        Yield = fld.Yield,
                        GrossCost = fld.GrossCost,
                        GrossReturns = fld.GrossReturns,
                        NetReturns = fld.NetReturns,
                        BC = fld.BC,
                        UnitLocationId = existingResult.UnitLocationId,
                        OrganizationId = existingResult.OrganizationId
                    };

                    if (entity.Id > 0)
                    {
                        // Update: set update audit fields
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        // Create: set create audit fields
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Step 6: Prepare OftResults - Hybrid Pattern
                var oftResultEntities = dto.OftResults?.Select(oft =>
                {
                    var entity = new KvkOftResult
                    {
                        Id = oft.Id ?? 0,
                        DetailsOfDemoId = oft.DetailsOfDemoId,
                        Parameter1 = oft.Parameter1,
                        Observation1 = oft.Observation1,
                        Parameter2 = oft.Parameter2,
                        Observation2 = oft.Observation2,
                        Parameter3 = oft.Parameter3,
                        Observation3 = oft.Observation3,
                        Parameter4 = oft.Parameter4,
                        Observation4 = oft.Observation4,
                        Parameter5 = oft.Parameter5,
                        Observation5 = oft.Observation5,
                        Yield = oft.Yield,
                        GrossCost = oft.GrossCost,
                        GrossReturns = oft.GrossReturns,
                        NetReturns = oft.NetReturns,
                        BC = oft.BC
                    };

                    if (entity.Id > 0)
                    {
                        entity.UpdatedById = _currentUserService.UserId;
                        entity.UpdatedAt = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entity.CreatedById = _currentUserService.UserId;
                        entity.CreatedAt = DateTimeOffset.UtcNow;
                    }

                    return entity;
                }).ToList();

                // Step 7: Repository handles transaction - updates parent, creates/updates/deletes children
                var updatedResult = await _resultRepository.UpdateWithChildrenAsync(
                    parentEntity,
                    fldResultEntities,
                    oftResultEntities);

                // Step 8: Map to DTO and return
                var resultDto = _mapper.MapToDto(updatedResult);
                return ServiceResult<KvkResultDto>.Success(resultDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<KvkResultDto>.Failure(
                    $"Failed to update result with children: {ex.Message}",
                    ServiceErrorStatus.INTERNALERROR);
            }
        }

        // ============================
        // SECTION F: REPORTS (Non-FLD/OFT categories)
        // ============================

        public async Task<ServiceResult<KvkReportDto>> AddOrUpdateReportAsync(
            int programId,
            KvkReportCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkReportDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            // Check if category allows Reports (not FLD/OFT)
            if (program.CategoryId == FLD_CATEGORY_ID || program.CategoryId == OFT_CATEGORY_ID)
                return ServiceResult<KvkReportDto>.Failure(
                    $"Reports are not available for FLD/OFT categories (CategoryId {FLD_CATEGORY_ID} or {OFT_CATEGORY_ID}). Use Results endpoint instead.",
                    ServiceErrorStatus.INVALIDOPERATION);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkReportDto>.Failure(
                    "Cannot modify reports for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _reportRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.KvkProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _reportRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<KvkReportDto>.Success(resultDto);
            }
            else
            {
                // Update existing
                existing.ProgressReportReportingYear = dto.ProgressReportReportingYear;
                existing.Date = dto.Date;
                existing.UploadPhoto = dto.UploadPhoto;
                existing.PhotosGeotaggedPhotoOrUploadPhoto = dto.PhotosGeotaggedPhotoOrUploadPhoto;
                existing.UploadVideo = dto.UploadVideo;
                existing.SignificantOutcome = dto.SignificantOutcome;
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _reportRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<KvkReportDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<KvkReportDto>> GetReportByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkReportDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var report = await _reportRepository.GetByProgramIdAsync(programId);

            if (report == null)
                return ServiceResult<KvkReportDto>.Failure(
                    "Report not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(report);
            return ServiceResult<KvkReportDto>.Success(dto);
        }

        // ============================
        // SECTION G: RECOMMENDATIONS
        // ============================

        public async Task<ServiceResult<KvkRecommendationDto>> AddOrUpdateRecommendationAsync(
            int programId,
            KvkRecommendationCreateDto dto)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            if (!await _entityPermissionService.CanModifyForm(program))
                return ServiceResult<KvkRecommendationDto>.Failure(
                    "Access denied",
                    ServiceErrorStatus.FORBIDDEN);

            if (program.FormStatus != "Draft" && program.FormStatus != "Rejected")
                return ServiceResult<KvkRecommendationDto>.Failure(
                    "Cannot modify recommendations for submitted or approved programs",
                    ServiceErrorStatus.INVALIDOPERATION);

            var existing = await _recommendationRepository.GetByProgramIdAsync(programId);

            if (existing == null)
            {
                // Create new
                var entity = _mapper.MapToEntity(dto);
                entity.KvkProgramDetailsId = programId;
                entity.CreatedById = _currentUserService.UserId;
                entity.CreatedAt = DateTimeOffset.UtcNow;

                var created = await _recommendationRepository.CreateAsync(entity);
                var resultDto = _mapper.MapToDto(created);

                return ServiceResult<KvkRecommendationDto>.Success(resultDto);
            }
            else
            {
                // Update existing
                existing.ProblemsIdentified = dto.ProblemsIdentified;
                existing.Recommendation = dto.Recommendation;
                existing.ActionTaken = dto.ActionTaken;
                existing.SignificantAchievement = dto.SignificantAchievement;
                existing.SuccessStories = dto.SuccessStories;
                existing.ImpactOutcome = dto.ImpactOutcome;
                existing.UpdatedById = _currentUserService.UserId;
                existing.UpdatedAt = DateTimeOffset.UtcNow;

                var updated = await _recommendationRepository.UpdateAsync(existing);
                var resultDto = _mapper.MapToDto(updated);

                return ServiceResult<KvkRecommendationDto>.Success(resultDto);
            }
        }

        public async Task<ServiceResult<KvkRecommendationDto>> GetRecommendationByProgramIdAsync(int programId)
        {
            var program = await _programRepository.GetByIdAsync(programId);

            if (program == null)
                return ServiceResult<KvkRecommendationDto>.Failure(
                    "Program not found",
                    ServiceErrorStatus.NOTFOUND);

            var recommendation = await _recommendationRepository.GetByProgramIdAsync(programId);

            if (recommendation == null)
                return ServiceResult<KvkRecommendationDto>.Failure(
                    "Recommendation not found",
                    ServiceErrorStatus.NOTFOUND);

            var dto = _mapper.MapToDto(recommendation);
            return ServiceResult<KvkRecommendationDto>.Success(dto);
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
            var query = _programRepository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetTrainerHistoryAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.ProgramType?.Name,
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
            var query = _programRepository.GetQueryable()
                .Include(x => x.Type);

            return await _historyService.GetPendingApprovalsAsync(
                query,
                getUnitLocationId: x => x.UnitLocationId,
                getTitleOrName: x => x.Title ?? x.ProgramType?.Name,
                getFormStatus: x => x.FormStatus,
                getCreatedById: x => x.CreatedById ?? 0,
                _unitHeadAssignmentRepository,
                pageNumber,
                pageSize);
        }

        /// <summary>
        /// Get programs by trainer ID and optionally filter by unit location
        /// </summary>
        public async Task<PaginatedResult<KvkProgramDetailsDto>> GetByTrainerAsync(
            int trainerId,
            int? unitLocationId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var unitLocationIds = await GetAccessibleUnitLocationIdsAsync();
            var result = await _programRepository.GetByTrainerAndUnitLocationAsync(
                trainerId,
                unitLocationId,
                unitLocationIds,
                pageNumber,
                pageSize);

            var dtos = result.Items.Select(p => _mapper.MapToDto(p)).ToList();

            return new PaginatedResult<KvkProgramDetailsDto>(
                dtos,
                result.TotalItems,
                result.PageNumber,
                result.PageSize);
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